namespace Catalog.Api.Settings;

public interface ICatalogDbSettings
{
    string ConnectionString { get; }

    string DbName { get; set; }

    string Host { get; set; }

    int Port { get; set; }

    string User { get; set; }

    string Password { get; set; }

    string CollectionName { get; set; }
}

public class CatalogDbSettings : ICatalogDbSettings
{
    public string ConnectionString
    {
        get
        {
            if (string.IsNullOrEmpty(User) || string.IsNullOrEmpty(Password))
                return $@"mongodb://{Host}:{Port}";

            return $@"mongodb://{User}:{Password}@{Host}:{Port}";
        }

    }

    public string DbName { get; set; }

    public string Host { get; set; }

    public int Port { get; set; }

    public string User { get; set; }

    public string Password { get; set; }

    public string CollectionName { get; set; }

}