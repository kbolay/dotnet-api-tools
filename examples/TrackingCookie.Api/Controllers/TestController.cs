using FromCookie;
using Microsoft.AspNetCore.Mvc;

namespace TrackingCookie.Api.Controllers
{
    [ApiController]
    [Route("v1/test")]
    public class TestController : ControllerBase
    {
        protected readonly ILogger _logger;
        
        public TestController(ILogger<TestController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        } // end method

        [HttpGet]
        public async Task<IActionResult> TestAsync(
            [FromCookie(Name = ApiConstants.COOKIE_NAME)] string trackingCookieValue,
            CancellationToken token = default)
        {
            return Ok(trackingCookieValue);
        } // end method
    } // end class
} // end namespace