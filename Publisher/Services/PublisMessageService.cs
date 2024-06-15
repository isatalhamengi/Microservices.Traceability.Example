using MassTransit;
using Shared;
using System.Diagnostics;
using System.Text.Json;

namespace Publisher.Services
{
    public class PublisMessageService : BackgroundService
    {
        IPublishEndpoint _publishEndpoint;
        ILogger<PublisMessageService> _logger;

        public PublisMessageService(IPublishEndpoint publishEndpoint,ILogger<PublisMessageService> logger)
        {
            _logger = logger;
            _publishEndpoint = publishEndpoint;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var correlationId = Guid.NewGuid();

            int i = 0;
            while (true)
            {
                ExampleMessage message = new()
                {
                    Text = $"{++i}. mesaj"
                };

                Trace.CorrelationManager.ActivityId = correlationId;
                _logger.LogDebug("Publisher Log");

                await Console.Out.WriteLineAsync($"{JsonSerializer.Serialize(message)} - CorrelationId : {correlationId}");

                await _publishEndpoint.Publish(message,async context =>
                {
                    context.Headers.Set("CorrelationId",correlationId);
                });
                await Task.Delay(750);

            }

        }
    }
}

