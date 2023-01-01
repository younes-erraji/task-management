using Microsoft.EntityFrameworkCore;

using TaskManagement.Data.Models;

namespace TaskManagement.Services.EF
{
    public class TaskManagementDBContext : DbContext
    {
        public TaskManagementDBContext(DbContextOptions<TaskManagementDBContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TaskManagement.Data.Models.Task> Tasks { get; set; }
        public DbSet<TaskExecution> TasksExecution { get; set; }
    }
}
