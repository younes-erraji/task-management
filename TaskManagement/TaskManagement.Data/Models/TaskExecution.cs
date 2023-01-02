using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TaskManagement.Data.Models
{
    [BsonIgnoreExtraElements]
    public class TaskExecution
    {
        [Key]
        [BsonElement("taskExecutionId")]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        [BsonElement("taskId")]
        public Guid TaskId { get; set; }
        [Required]
        [BsonElement("taskStartDate")]
        public DateTime TaskStartDate { get; set; }
        [BsonElement("taskEndDate")]
        public DateTime? TaskEndDate { get; set; }

        [ForeignKey(nameof(TaskId))]
        [BsonElement("task")]
        public virtual Task Task { get; set; }
    }
}
