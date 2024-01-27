using Microsoft.AspNetCore.Builder;

namespace TrackingCookie
{
    public static class CookieMiddlewareExtensions
    {
        public static IApplicationBuilder UseTrackingCookieMiddleware(
            this IApplicationBuilder app, 
            Action<TrackingCookieMiddlewareOptions>? setupAction = null)
        {
            var options = new TrackingCookieMiddlewareOptions();
            if(setupAction != null)
            {
                setupAction(options); 
            } // end  if
            return app.UseMiddleware<TrackingCookieMiddleware>(options);
        } // end method
    } // end class
} // end namespace