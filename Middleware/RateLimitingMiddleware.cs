using AspNetCoreRateLimit;
using System.Security.Claims;

namespace HospitalMiddleware.Middleware
{
    public class RateLimitingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IpRateLimitOptions _options;
        private readonly IRateLimitProcessor _processor;
        private readonly IRateLimitConfiguration _config;

        public RateLimitingMiddleware(RequestDelegate next, IpRateLimitOptions options, IRateLimitProcessor processor, IRateLimitConfiguration config)
        {
            _next = next;
            _options = options;
            _processor = processor;
            _config = config;
            _config.RegisterResolvers();
        }

        public async Task Invoke(HttpContext context)
        {
            var clientRequestIdentity = ResolveIdentityAsync(context);

            var identity = context.User.Identity as ClaimsIdentity;
            if (identity != null && identity.IsAuthenticated)
            {
                var clientId = identity.FindFirst("client_id")?.Value;
                var rateLimitCounter = await _processor.ProcessRequestAsync(clientRequestIdentity, _options.GeneralRules.FirstOrDefault());

                if (!rateLimitCounter.IsWhitelisted && rateLimitCounter.Counter >= rateLimitCounter.Limit)
                {
                    context.Response.StatusCode = 429; // Too Many Requests
                    await context.Response.WriteAsync("Rate limit exceeded. Try again later.");
                    return;
                }
            }

            await _next(context);
        }

        // Implementation of ResolveIdentityAsync that Extracts httpRequest information for clientRequestIdentity

        public virtual async Task<ClientRequestIdentity> ResolveIdentityAsync(HttpContext httpContext)
        {
            string clientIp = null;
            string clientId = null;

            if (_config.ClientResolvers?.Any() == true)
            {
                foreach (var resolver in _config.ClientResolvers)
                {
                    clientId = await resolver.ResolveClientAsync(httpContext);

                    if (!string.IsNullOrEmpty(clientId))
                    {
                        break;
                    }
                }
            }

            if (_config.IpResolvers?.Any() == true)
            {
                foreach (var resolver in _config.IpResolvers)
                {
                    clientIp = resolver.ResolveIp(httpContext);

                    if (!string.IsNullOrEmpty(clientIp))
                    {
                        break;
                    }
                }
            }
            var path = httpContext.Request.Path.ToString().ToLowerInvariant();
            return new ClientRequestIdentity
            {
                ClientIp = clientIp,
                Path = path == "/"
                    ? path
                    : path.TrimEnd('/'),
                HttpVerb = httpContext.Request.Method.ToLowerInvariant(),
                ClientId = clientId ?? "anon"
            };
        }
    }
}
