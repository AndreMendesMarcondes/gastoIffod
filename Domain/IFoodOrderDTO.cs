using System;
using System.Collections.Generic;

namespace Domain
{
    public class Details
    {
        public string mode { get; set; }
        public bool scheduled { get; set; }
        public bool tippable { get; set; }
        public bool indoorTipEnabled { get; set; }
        public bool trackable { get; set; }
        public bool boxable { get; set; }
        public bool placedAtBox { get; set; }
        public bool reviewed { get; set; }
        public bool darkKitchen { get; set; }
    }

    public class Coordinates
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    public class Address
    {
        public string city { get; set; }
        public string country { get; set; }
        public string neighborhood { get; set; }
        public string state { get; set; }
        public string streetName { get; set; }
        public string streetNumber { get; set; }
        public Coordinates coordinates { get; set; }
        public string reference { get; set; }
        public string complement { get; set; }
        public string establishment { get; set; }
    }

    public class Driver
    {
        public string id { get; set; }
        public string name { get; set; }
        public string photoUrl { get; set; }
        public string modal { get; set; }
    }

    public class EstimatedTimeOfArrival
    {
        public DateTime deliversAt { get; set; }
        public DateTime updatedAt { get; set; }
    }

    public class Delivery
    {
        public Address address { get; set; }
        public Driver driver { get; set; }
        public EstimatedTimeOfArrival estimatedTimeOfArrival { get; set; }
        public DateTime expectedDeliveryTime { get; set; }
        public int expectedDuration { get; set; }
    }

    public class Merchant
    {
        public Address address { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string phoneNumber { get; set; }
        public string logo { get; set; }
        public string companyGroup { get; set; }
        public string type { get; set; }
    }

    public class Method2
    {
        public string name { get; set; }
        public string description { get; set; }
    }

    public class Type
    {
        public string name { get; set; }
        public string description { get; set; }
    }

    public class Brand
    {
        public string id { get; set; }
        public string image { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }

    public class Credit
    {
        public string cardNumber { get; set; }
    }

    public class Amount
    {
        public string currency { get; set; }
        public int value { get; set; }
    }

    public class Transaction
    {
        public string id { get; set; }
        public string type { get; set; }
        public string status { get; set; }
        public DateTime createdAt { get; set; }
        public int value { get; set; }
        public List<object> refunds { get; set; }
    }

    public class Method
    {
        public string id { get; set; }
        public Method method { get; set; }
        public Type type { get; set; }
        public Brand brand { get; set; }
        public Credit credit { get; set; }
        public Amount amount { get; set; }
        public List<Transaction> transactions { get; set; }
    }

    public class Total
    {
        public string currency { get; set; }
        public int value { get; set; }
        public int valueWithDiscount { get; set; }
    }

    public class Payments
    {
        public List<Method> methods { get; set; }
        public Total total { get; set; }
    }

    public class DeliveryFee
    {
        public int value { get; set; }
        public int valueWithDiscount { get; set; }
    }

    public class SubItem
    {
        public string externalId { get; set; }
        public string name { get; set; }
        public int quantity { get; set; }
        public List<object> tags { get; set; }
        public int totalPrice { get; set; }
        public int totalPriceWithDiscount { get; set; }
        public int unitPrice { get; set; }
        public int unitPriceWithDiscount { get; set; }
    }

    public class Item
    {
        public string externalId { get; set; }
        public string name { get; set; }
        public int quantity { get; set; }
        public List<SubItem> subItems { get; set; }
        public List<string> tags { get; set; }
        public int totalPrice { get; set; }
        public int totalPriceWithDiscount { get; set; }
        public int unitPrice { get; set; }
        public int unitPriceWithDiscount { get; set; }
        public string notes { get; set; }
    }

    public class SubTotal
    {
        public int value { get; set; }
        public int valueWithDiscount { get; set; }
    }

    public class Bag
    {
        public List<object> benefits { get; set; }
        public DeliveryFee deliveryFee { get; set; }
        public List<Item> items { get; set; }
        public SubTotal subTotal { get; set; }
        public Total total { get; set; }
        public bool updated { get; set; }
    }

    public class Origin
    {
        public string platform { get; set; }
        public string appName { get; set; }
        public string appVersion { get; set; }
    }

    public class DeliveryMethod
    {
        public string id { get; set; }
        public string mode { get; set; }
    }

    public class IFoodOrderDTO
    {
        public string id { get; set; }
        public string shortId { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public DateTime closedAt { get; set; }
        public string lastStatus { get; set; }
        public Details details { get; set; }
        public Delivery delivery { get; set; }
        public Merchant merchant { get; set; }
        public Payments payments { get; set; }
        public Bag bag { get; set; }
        public Origin origin { get; set; }
        public DeliveryMethod deliveryMethod { get; set; }
        public List<object> fees { get; set; }
    }


}
