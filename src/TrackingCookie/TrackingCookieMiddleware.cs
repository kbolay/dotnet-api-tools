using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;

namespace TrackingCookie
{
    public class TrackingCookieMiddleware
    {
        protected readonly RequestDelegate _next;
        protected readonly TrackingCookieMiddlewareOptions _options;
        protected readonly ILoggerFactory _loggerFactory;

        public TrackingCookieMiddleware(
            RequestDelegate next, 
            TrackingCookieMiddlewareOptions options,
            ILoggerFactory loggerFactory)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
        } // end method

        public async Task Invoke(HttpContext context)
        {
            var logger = _loggerFactory.CreateLogger<TrackingCookieMiddleware>();

            var testCookieValue = context.Request.Cookies[_options.Name];
            if(string.IsNullOrWhiteSpace(testCookieValue))
            {
                testCookieValue = Guid.NewGuid().ToString();
                var modifiableCollection = new CustomRequestCookieCollection(context.Request.Cookies);
                modifiableCollection[_options.Name] = testCookieValue;
                context.Request.Cookies = modifiableCollection;
            } // end if

            var cookieOptions = new CookieOptions()
                {
                    Expires = DateTime.UtcNow.Add(_options.Expiration),
                    Domain = BuildCookieDomain(context),
                    Path = "/",
                    SameSite = SameSiteMode.Lax, // Default
                    Secure = false,
                    HttpOnly = false,
                };

            context.Response.Cookies.Append(_options.Name, testCookieValue, cookieOptions);
                            
            await _next.Invoke(context);
        } // end method

        private string? BuildCookieDomain(HttpContext context)
        {
            string? result = null;
            
            string? baseDomain = null;
            if(context.Request.Headers.TryGetValue("referer", out StringValues referer)
                && !string.IsNullOrWhiteSpace(referer))
            {
                var refererHost = new Uri(referer).Host;
                if(!refererHost.Equals("localhost", StringComparison.OrdinalIgnoreCase))
                {
                    baseDomain = RemoveSubdomains(refererHost);
                } // end if                
            }
            else
            {
                string requestHost = context.Request.Host.Host;
                if(!requestHost.Equals("localhost", StringComparison.OrdinalIgnoreCase))
                {
                    baseDomain = RemoveSubdomains(requestHost);
                } // end if
            } // end if

            if(!string.IsNullOrWhiteSpace(baseDomain))
            {
                result = $"{_options.SubDomain}.{baseDomain}";
            } // end if

            return result;
        } // end method

        private string? RemoveSubdomains(string host)
        {
            var result = host;
            var hostPieces = host.Split('.', StringSplitOptions.RemoveEmptyEntries);
            if(hostPieces.Length > 2)
            {
                result = string.Join('.', hostPieces.Skip(hostPieces.Count() - 2).Take(2));
            } // end if
            
            return result;
        } // end method
    } // end class
} // end namespace