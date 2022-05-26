using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Product.Api.Entities;

public class Product
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("Title")]
    public string Title { get; set; }

    [BsonElement("Category")]
    public string Category { get; set; }

    [BsonElement("Summary")]
    public string Summary { get; set; }

    [BsonElement("Description")]
    public string Description { get; set; }

    [BsonElement("ImagePath")]
    public string ImagePath { get; set; }

    [BsonElement("Price")]
    public double Price { get; set; }
}

