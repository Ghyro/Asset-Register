using RabbitMQ.Client;
using System;
using System.Text;
using System.Text.Json;


namespace Platform.API.AsyncDataServices
{
  using Platform.API.Infrastructure.Dtos;  

  public class MessageBusClient : IMessageBusClient, IDisposable
  {
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public MessageBusClient(string host, string port)
    {
      var rabbitMqFactory = new ConnectionFactory()
      {
        HostName = host,
        Port = int.Parse(port)
      };

      try
      {
        _connection = rabbitMqFactory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
        _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
        Console.WriteLine("Connected to Message Bus");
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Could not connect to the Message Bus. Exception: {ex.Message}");
      }
    }

    public void PublishNewPlatform(PlatformPublishedDto platformPublishedDto)
    {
      var message = JsonSerializer.Serialize(platformPublishedDto);

      if (_connection.IsOpen)
      {
        Console.WriteLine("RabbitMq Connection is opened, sending the message...");
        SendMessage(message);
      }
      else
      {
        Console.WriteLine("RabbitMq Connection is closed, message won't be sended.");
      }
    }

    private void SendMessage(string message)
    {
      var body = Encoding.UTF8.GetBytes(message);

      _channel.BasicPublish(exchange: "trigger", routingKey: "", basicProperties: null, body: body);

      Console.WriteLine($"The message {message} is being sent...");
    }

    public void Dispose()
    {
      Console.WriteLine("Message Bus Disposed");
      if (_channel.IsOpen)
      {
        _channel.Close();
        _connection.Close();
      }
    }

    private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
    {
      Console.WriteLine("RabbitMQ Connection Shutdown.");
    }
  }
}
