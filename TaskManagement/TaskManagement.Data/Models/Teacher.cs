using System.ComponentModel.DataAnnotations;

using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TaskManagement.Data.Models
{
    [BsonIgnoreExtraElements]
    public class Teacher
    {
        [Key]
        [BsonElement("teacherId")]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required, StringLength(50)]
        [BsonElement("name")]
        public string Name { get; set; }
        [Required]
        [BsonElement("birthDate")]
        public DateTime BirthDate { get; set; }
        [Required, StringLength(50)]
        [BsonElement("mainSubjectTeaching")]
        public string MainSubjectTeaching { get; set; }
        [BsonElement("CreationDate")]
        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}
