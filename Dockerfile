#########################
# Build Stage
#########################
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

# https://blog.packagecloud.io/eng/2017/02/21/set-environment-variable-save-thousands-of-system-calls/
ENV TZ=:/etc/localtime

WORKDIR /src

COPY ["Domain/GIF.Domain.csproj", "Domain/"]
COPY ["GIF.Service/GIF.Service.csproj", "GIF.Service/"]
COPY ["GIF.Web/GIF.Web.csproj", "GIF.Web/"]

RUN dotnet restore "GIF.Web/GIF.Web.csproj"

COPY . .

WORKDIR "/src/GIF.Web"

RUN dotnet build "GIF.Web.csproj" --configuration Release --output /app/build

#########################
# Publish Stage
#########################
FROM build AS publish

RUN dotnet publish "GIF.Web.csproj" --configuration Release --output /app/publish

#########################
# App Stage
#########################
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS app

# https://blog.packagecloud.io/eng/2017/02/21/set-environment-variable-save-thousands-of-system-calls/
ENV TZ=:/etc/localtime
ENV DEBIAN_FRONTEND=noninteractive

# hadolint ignore=DL3008
RUN apt-get update && \
    apt-get install -y --no-install-recommends \
    ca-certificates \
    dumb-init && \
    rm -rf /var/lib/apt/lists/* /var/cache/apt/*

WORKDIR /usr/src/app

COPY --from=publish /app/publish .

EXPOSE 80

# Set up unprivileged user
# https://docs.docker.com/develop/develop-images/dockerfile_best-practices/#user
ARG APP_USER=900
ARG APP_GROUP=900

RUN set -feu && \
    groupadd --system --gid ${APP_GROUP} gif && \
    useradd --system --gid ${APP_GROUP} --uid ${APP_USER} --home-dir /usr/src/app --shell /bin/true gif && \
    chown -R gif:gif /usr/src/app
USER gif:gif

ENTRYPOINT ["/usr/bin/dumb-init", "--"]
CMD ["dotnet", "GIF.Web.dll"]
