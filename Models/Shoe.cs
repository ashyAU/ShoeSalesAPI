using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace ShoeSalesAPI.Models
{
    public class Shoe
    {
        [JsonIgnore]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _id { get; set; }
        public int SKU { get; set; }
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public bool isAvailable { get; set; }

    }
}