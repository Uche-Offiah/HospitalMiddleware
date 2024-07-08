using AspNetCoreRateLimit;
using System.Security.Claims;

namespace HospitalMiddleware.Middleware
{
    public class RateLimitingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IpRateLimitOptions _options;
        private readonly IRateLimitProcessor _processor;

        public RateLimitingMiddleware(RequestDelegate next, IpRateLimitOptions options, IRateLimitProcessor processor)
        {
            _next = next;
            _options = options;
            _processor = processor;
        }

        //public async Task Invoke(HttpContext context)
        //{
        //    var identity = context.User.Identity as ClaimsIdentity;
        //    if (identity != null && identity.IsAuthenticated)
        //    {
        //        var clientId = identity.FindFirst("client_id")?.Value;
        //        var rateLimitCounter = await _processor.ProcessRequestAsync(context, _options.GeneralRules.FirstOrDefault());

        //        if (!rateLimitCounter.IsWhitelisted && rateLimitCounter.Counter >= rateLimitCounter.Limit)
        //        {
        //            context.Response.StatusCode = 429; // Too Many Requests
        //            await context.Response.WriteAsync("Rate limit exceeded. Try again later.");
        //            return;
        //        }
        //    }

        //    await _next(context);
        //}
    }
}
