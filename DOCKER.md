# Docker

## Como construir a imagem

Execute o comando abaixo para construir a imagem base:

```bash
docker build --tag gif .
```

## Como executar o container

Execute o comando abaixo para executar o container a partir da imagem do passo anterior:

```bash
docker run --rm --publish 80:80 gif
```

## Como acessar a página

Após o último passo, vá ao seu navegador e acesse [http://localhost](http://localhost).
