using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesignPatterns.Strategy.Models;

public class Product
{
    [BsonId] // mongodb id
    [Key]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Name { get; set; }
    [Column(TypeName ="decimal(18,2)")]
    [BsonRepresentation(BsonType.Decimal128)]
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string UserId { get; set; }
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime CreatedDate { get; set; }

}
