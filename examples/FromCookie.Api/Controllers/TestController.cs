using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FromCookie.Api.Controllers
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
        public async Task<IActionResult> ShowCookieAsync(
            [FromCookie(Name = "my-cookie")] string cookieValue,
            CancellationToken token = default)
        {
            return Ok(cookieValue);
        } // end method
    } // end class
} // end namespace