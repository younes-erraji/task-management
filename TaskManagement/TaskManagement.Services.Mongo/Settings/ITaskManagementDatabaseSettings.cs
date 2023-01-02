namespace TaskManagement.Services.Mongo.Settings
{
    public interface ITaskManagementDatabaseSettings
    {
        string URI { get; set; }
        string DatabaseName { get; set; }
    }
}
