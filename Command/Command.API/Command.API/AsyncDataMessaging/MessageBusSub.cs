using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Command.API.AsyncDataMessaging
{
  using Command.API.Infrastructure.Interfaces;

  public class MessageBusSub : BackgroundService
  {        
    private readonly IConfiguration _configuration;
    private readonly IEventProcessor _eventProcessor;
    private IConnection _connection;
    private string _queueName;
    private IModel _channel;

    public MessageBusSub(IConfiguration configuration, IEventProcessor eventProcessor)
    {
      _configuration = configuration;
      _eventProcessor = eventProcessor;

      InitRabbitMQ();
    }

    private void InitRabbitMQ()
    {
      var factory = new ConnectionFactory()
      {
        HostName = _configuration["RABBITMQ_HOST"],
        Port = int.Parse(_configuration["RABBITMQ_PORT"])
      };

      _connection = factory.CreateConnection();
      _channel = _connection.CreateModel();
      _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
      _queueName = _channel.QueueDeclare().QueueName;
      _channel.QueueBind(queue: _queueName, exchange: "trigger", routingKey: "");

      Console.WriteLine("Listening on the Message Bus...");

      _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
      stoppingToken.ThrowIfCancellationRequested();

      var consumer = new EventingBasicConsumer(_channel);
      consumer.Received += async (ModelHandle, ea) =>
      {
        Console.WriteLine("Event received.");
        var body = ea.Body;
        var message = Encoding.UTF8.GetString(body.ToArray());
        await _eventProcessor.ProcessEvent(message);
      };

      _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);
      return Task.CompletedTask;
    }

    public override void Dispose()
    {
      Console.WriteLine("Message Bus Disposed");

      if (_channel.IsOpen)
      {
        _channel.Close();
        _connection.Close();
      }

      base.Dispose();
    }

    private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
    {
      Console.WriteLine("RabbitMQ Connection Shutdown.");
    }
  }
}
