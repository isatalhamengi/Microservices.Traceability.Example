using AspNetCore.Application.Middlewares;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);

#region NLog Setup
builder.Logging.ClearProviders();
builder.Host.UseNLog();
#endregion

var app = builder.Build();

app.UseMiddleware<CorrelationIdMiddleware>();
app.UseMiddleware<OtherMiddleware>();

app.MapGet("/",async (HttpContext context, ILogger<Program> logger) =>
{
    var correlationId = context.Request.Headers["X-Correlation-ID"].FirstOrDefault();
    NLog.MappedDiagnosticsContext.Set("CorrelationId",correlationId);
    logger.LogDebug("Minimal API Log");
});

app.Run();
