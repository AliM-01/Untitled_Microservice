namespace Catalog.Api.Settings;

public interface ICatalogDbSettings
{
    string ConnectionString { get; set; }

    string DbName { get; set; }

    string CollectionName { get; set; }
}

public class CatalogDbSettings : ICatalogDbSettings
{
    public string ConnectionString { get; set; }

    public string DbName { get; set; }

    public string CollectionName { get; set; }
}