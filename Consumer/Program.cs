using Consumer.Consumers;
using MassTransit;
using NLog.Extensions.Logging;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddMassTransit(configurator =>
{
    configurator.AddConsumer<ExampleMessageConsumer>();
    configurator.UsingRabbitMq((context,_configurator) =>
    {
        _configurator.Host("your_url");
        _configurator.ReceiveEndpoint("example-message-queue", e=> e.ConfigureConsumer<ExampleMessageConsumer>(context));
    });
});

builder.Logging.ClearProviders();
builder.Logging.AddNLog();

var app = builder.Build();
app.Run();
