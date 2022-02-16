namespace Catalog.Api.Settings;
public class CatalogDbSettings
{
    public string ConnectionString
    {
        get
        {
            return $@"mongodb://{Host}:{Port}";
        }

    }
    
    public string DbName { get; set; }

    public string Host { get; set; }

    public int Port { get; set; }

    public string CollectionName { get; set; }

}