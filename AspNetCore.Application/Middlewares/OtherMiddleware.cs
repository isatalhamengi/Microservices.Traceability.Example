namespace AspNetCore.Application.Middlewares
{
    public class OtherMiddleware
    {
        RequestDelegate _requestDelegate;

        public OtherMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task Invoke(HttpContext context, ILogger<OtherMiddleware> logger)
        {
            var correlationId = context.Request.Headers["X-Correlation-ID"].FirstOrDefault();
            //correlationId = context.Items["CorrelationId"].ToString();

            NLog.MappedDiagnosticsContext.Set("CorrelationId",correlationId);
            logger.LogDebug("OtherMiddleware Log");
            await _requestDelegate(context);
        }
    }
}
