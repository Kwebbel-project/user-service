using Confluent.Kafka;
using Kafka.Public;
using Kafka.Public.Loggers;
using System.Text;

namespace user_service.Common.Kafka
{
    public class KafkaConsumerHandler : IHostedService
    {
        private readonly ILogger<KafkaConsumerHandler> _logger;
        private ClusterClient _cluster;

        public KafkaConsumerHandler(ILogger<KafkaConsumerHandler> logger)
        {
            _logger = logger;
            _cluster = new ClusterClient(new Configuration
            {
                Seeds = "localhost:9092"
            }, new ConsoleLogger());
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _cluster.ConsumeFromLatest("USER_REGISTERED");
            _cluster.MessageReceived += record =>
            {
                _logger.LogInformation($"Received: {Encoding.UTF8.GetString(record.Value as byte[])}");
            };
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _cluster.Dispose();
            return Task.CompletedTask;
        }
    }
}
