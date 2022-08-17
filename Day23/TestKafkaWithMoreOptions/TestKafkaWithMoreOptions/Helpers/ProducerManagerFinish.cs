using Confluent.Kafka;

namespace TestKafkaWithMoreOptions.Helpers
{
    public class ProducerManagerFinish
    {
        private readonly string _topicName;
        private readonly ProducerConfig _config;
        private readonly string _keyProducer;
        private static ProducerBuilder<string, string> _producer;

        public ProducerManagerFinish(string topicName, ProducerConfig config,string keyProducer)
        {
            _topicName = topicName;
            _keyProducer = keyProducer;
            _config = config;
            _producer = new ProducerBuilder<string, string>(_config);
        }

        public async Task WriteMessage(string messageReceive)
        {

            var produceValue = await _producer.Build().ProduceAsync(_topicName, new Message<string, string>
            {
                Key = _keyProducer,
                Value = messageReceive
            });

            Console.WriteLine($"{produceValue.Offset} - {produceValue.TopicPartitionOffset} " +
                $"- {produceValue.Message.Value} - {produceValue.Message.Key}");

            return;
        }
    }
}
