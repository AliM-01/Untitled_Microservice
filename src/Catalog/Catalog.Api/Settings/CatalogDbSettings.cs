namespace Catalog.Api.Settings;
public class CatalogDbSettings
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