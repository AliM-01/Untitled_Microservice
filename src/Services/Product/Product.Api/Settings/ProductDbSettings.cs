namespace Product.Api.Settings;
public class ProductDbSettings
{
    public string ConnectionString => $@"mongodb://{Host}:{Port}";

    public string DbName { get; set; }

    public string Host { get; set; }

    public int Port { get; set; }

    public string CollectionName { get; set; }

}