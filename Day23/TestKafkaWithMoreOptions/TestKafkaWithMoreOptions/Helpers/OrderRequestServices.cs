using Confluent.Kafka;
using Newtonsoft.Json;

namespace TestKafkaWithMoreOptions.Helpers
{
    public class OrderRequestServices : BackgroundService
    {
        private readonly ProducerConfig _producerConfig;
        private readonly ConsumerConfig _consumerConfig;
        private readonly string _topicNameProcess = "KafkaTestProcess";
        private readonly string _topicNameFinish = "KafkaTestFinish";

        public OrderRequestServices(ProducerConfig producerConfig, ConsumerConfig consumerConfig)
        {
            _producerConfig = producerConfig;
            _consumerConfig = consumerConfig;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Start process for Order");

            if (!stoppingToken.IsCancellationRequested)
            {
                var consumerHelper = new ConsumerProducer(_topicNameProcess, _consumerConfig);
                KeyAndValue orderRequest = consumerHelper.ReadMessage();

                //Deserilaize 
                string keyOrder = orderRequest.KeyKafka;
                Console.WriteLine(keyOrder);
                OrderRequest order = JsonConvert.DeserializeObject<OrderRequest>(orderRequest.ValueKafka);

                //TODO:: Process Order
                Console.WriteLine($"Info: OrderHandler => Processing the order for {order.productname}");
                order.status = OrderStatus.COMPLETED;

                //Write to ReadyToShip Queue
                var producerWrapper = new ProducerManagerFinish(_topicNameFinish, _producerConfig, keyOrder);
                await producerWrapper.WriteMessage(JsonConvert.SerializeObject(order));
            }
        }
    }
}
