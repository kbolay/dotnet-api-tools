using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FromCookie.ModelBinding
{
    public class CookieValueProvider : BindingSourceValueProvider
    {
        public CookieValueProvider(BindingSource bindingSource, IRequestCookieCollection cookies) : base(bindingSource)
        {
            Cookies = cookies;
        } // end method

        private IRequestCookieCollection Cookies { get; }

        public override bool ContainsPrefix(string prefix)
        {
            return Cookies.ContainsKey(prefix);
        } // end method

        public override ValueProviderResult GetValue(string key)
        {
            return Cookies.TryGetValue(key, out var value) ? new ValueProviderResult(value) : ValueProviderResult.None;
        } // end method
    } // end class
} // end namespace