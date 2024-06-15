using MassTransit;
using NLog.Extensions.Logging;
using Publisher.Services;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddMassTransit(configurator =>
{
    configurator.UsingRabbitMq((context,_configurator) =>
    {
        _configurator.Host("your_url");
    });
});

builder.Logging.ClearProviders();
builder.Logging.AddNLog();

builder.Services.AddHostedService<PublisMessageService>(provider =>
{
    using IServiceScope scope = provider.CreateScope();
    IPublishEndpoint publishEndpoint = scope.ServiceProvider.GetService<IPublishEndpoint>();
    var logger = scope.ServiceProvider.GetService<ILogger<PublisMessageService>>();
    return new(publishEndpoint, logger);
});

var app = builder.Build();
app.Run();