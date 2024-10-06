using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ForumApp.API.Models
{
    public class Article
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? PostId { get; set; }

        [Required]
        public string Heading { get; set; } = string.Empty;

        [Required]
        public string Body { get; set; } = string.Empty;

        public string? Thumbnail { get; set; }

        public List<string> Tags { get; set; } = new List<string>();
    }
}
