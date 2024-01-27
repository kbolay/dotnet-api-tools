using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FromCookie.ModelBinding
{
    public class CookieValueProviderFactory : IValueProviderFactory
    {
        public Task CreateValueProviderAsync(ValueProviderFactoryContext context)
        {
            var cookies = context.ActionContext.HttpContext.Request.Cookies;

            context.ValueProviders.Add(new CookieValueProvider(CookieBindingSource.CookieSource, cookies));

            return Task.CompletedTask;
        }
    }
} // end namespace