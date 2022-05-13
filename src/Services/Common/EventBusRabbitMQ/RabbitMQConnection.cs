using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

namespace EventBusRabbitMQ;

public interface IRabbitMQConnection : IDisposable
{
    bool IsConnected { get; }

    bool TryConnect();

    IModel CreateModel();
}

public class RabbitMQConnection : IRabbitMQConnection
{
    #region ctor

    private readonly IConnectionFactory _connectionFactory;
    private IConnection _connection;
    private bool _disposed;

    public RabbitMQConnection(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;

        if (!IsConnected)
        {
            TryConnect();
        }
    }

    #endregion

    public bool IsConnected
    {
        get
        {
            return _connection is not null && _connection.IsOpen && !_disposed;
        }
    }

    public bool TryConnect()
    {
        try
        {
            _connection = _connectionFactory.CreateConnection();
        }
        catch (BrokerUnreachableException)
        {
            Thread.Sleep(TimeSpan.FromMinutes(2));
            _connection = _connectionFactory.CreateConnection();
        }

        if (IsConnected)
        {
            return true;
        }

        return false;
    }

    public IModel CreateModel()
    {
        if (!IsConnected)
        {
            throw new InvalidOperationException("Failed to connect");
        }

        return _connection.CreateModel();
    }

    public void Dispose()
    {
        if (_disposed)
            return;

        try
        {
            _connection.Dispose();
        }
        catch (Exception)
        {
            throw;
        }
    }

}
