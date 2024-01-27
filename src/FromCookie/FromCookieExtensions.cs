using Microsoft.AspNetCore.Mvc;

namespace FromCookie.ModelBinding
{
    public static class FromCookieExtensions
    {
        public static MvcOptions AddFromCookieBinder(this MvcOptions options)
        {
            options.ValueProviderFactories.Add(new CookieValueProviderFactory());
            return options;
        } // end method
    } // end class
} // end namespace