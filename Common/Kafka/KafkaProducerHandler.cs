using Confluent.Kafka;

namespace user_service.Common.Kafka
{
    public class KafkaProducerHandler
    {
        private readonly ILogger<KafkaProducerHandler> _logger;
        private IProducer<Null, string> _producer;

        public KafkaProducerHandler(ILogger<KafkaProducerHandler> logger)
        {
            _logger = logger;
            var config = new ProducerConfig()
            {
                BootstrapServers = "localhost:9092"
            };
            _producer = new ProducerBuilder<Null, string>(config).Build();
        }

        public async Task<bool> sendMessage(string topic, string message)
        {
            try
            {
                await _producer.ProduceAsync(topic, new Message<Null, string> { Value = message });
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured: {ex.Message}");
            }
            return await Task.FromResult(false);
        }

        

        //public async Task StartAsync(CancellationToken cancellationToken)
        //{
        //    for (int i = 0; i < 100; i++)
        //    {
        //        var value = $"Hello world {i}";
        //        _logger.LogInformation(value);
        //        await _producer.ProduceAsync("demo", new Message<Null, string>()
        //        {
        //            Value = value
        //        }, cancellationToken);
        //    }

        //    _producer.Flush(TimeSpan.FromSeconds(10));
        //}

        //public Task StopAsync(CancellationToken cancellationToken)
        //{
        //    _producer.Dispose();
        //    return Task.CompletedTask;
        //}
    }
}
