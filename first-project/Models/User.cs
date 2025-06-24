using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace first_project.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Name")]
        public required string Name { get; set; }

        [BsonElement("Email")]
        public required string Email { get; set; }

        [BsonElement("Password")]
        public required string Password { get; set; }
    }
}
