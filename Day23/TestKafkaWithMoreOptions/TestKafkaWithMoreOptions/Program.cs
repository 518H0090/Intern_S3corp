using Confluent.Kafka;
using TestKafkaWithMoreOptions.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IHostedService, OrderRequestServices>();

var producerConfig = new ProducerConfig();
var consumerConfig = new ConsumerConfig();

builder.Configuration.Bind("producer", producerConfig);
//builder.Configuration.Bind("consumer", consumerConfig);

consumerConfig.GroupId = "csharp-consumer";
consumerConfig.BootstrapServers = "localhost:9092";
consumerConfig.EnableAutoCommit = true;
consumerConfig.AutoOffsetReset = 0;
consumerConfig.EnablePartitionEof = true;
consumerConfig.EnableAutoOffsetStore = true;
consumerConfig.AutoOffsetReset = AutoOffsetReset.Earliest;

builder.Services.AddSingleton<ProducerConfig>(producerConfig);
builder.Services.AddSingleton<ConsumerConfig>(consumerConfig);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
