namespace TaskManagement.Services.Mongo.Settings
{
    public class TaskManagementDatabaseSettings : ITaskManagementDatabaseSettings
    {
        public string URI { get; set; }
        public string DatabaseName { get; set; }
    }
}
