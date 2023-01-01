namespace TaskManagement.Services.Mongo.Settings
{
    public class TaskManagementDatabaseSettings: ITaskManagementDatabaseSettings
    {
        public string CollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
