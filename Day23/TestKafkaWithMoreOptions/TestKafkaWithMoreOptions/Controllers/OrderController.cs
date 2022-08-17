using Confluent.Kafka;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TestKafkaWithMoreOptions.Helpers;

namespace TestKafkaWithMoreOptions.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ProducerConfig _config;
        private readonly string _topicName = "KafkaTestProcess";

        public OrderController(ProducerConfig config)
        {
            _config = config;
        }

        [HttpPost]
        [Route("PostAsyncForKafka")]
        public async Task<IActionResult> PostMessageForKafka([FromBody] OrderRequest request)
        {
            string serializeOrder = JsonConvert.SerializeObject(request);

            var producerManager = new ProducerManager(_topicName, _config);

            await producerManager.WriteMessage(serializeOrder);


            return Created("Create Success", producerManager);
        }
    }
}
