using System.ComponentModel.DataAnnotations;

using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TaskManagement.Data.Models
{
    [BsonIgnoreExtraElements]
    public class Task
    {
        [Key]
        [BsonElement("taskId")]
        public Guid Id { get; set; } = Guid.NewGuid();
        [BsonElement("name")]
        public string? Name { get; set; }
        [Required]
        [BsonElement("tableName")]
        public string TableName { get; set; }
        [Required]
        [BsonElement("actionType")]
        public string ActionType { get; set; }
    }
}
