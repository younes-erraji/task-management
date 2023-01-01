namespace TaskManagement.Services.Mongo.Settings
{
    public interface ITaskManagementDatabaseSettings
    {
        string CollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
