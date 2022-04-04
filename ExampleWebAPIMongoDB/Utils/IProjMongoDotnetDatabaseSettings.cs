namespace ExampleWebAPIMongoDB.Utils
{
    public interface IProjMongoDotnetDatabaseSettings
    {
        string PassengerCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }

    }
}
