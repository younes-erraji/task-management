using System.ComponentModel.DataAnnotations;

using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TaskManagement.Data.Models
{
    [BsonIgnoreExtraElements]
    public class Student
    {
        [Key]
        // [BsonId]
        // [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("studentId")]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required, StringLength(50)]
        [BsonElement("name")]
        public string Name { get; set; }
        [Required]
        [BsonElement("birthDate")]
        public DateTime BirthDate { get; set; }
        [Required, Range(0, 2024)]
        [BsonElement("yearOfStudy")]
        public int YearOfStudy { get; set; }

        [BsonElement("creationDate")]
        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}
