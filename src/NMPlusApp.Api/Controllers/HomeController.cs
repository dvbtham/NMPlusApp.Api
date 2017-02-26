using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace NMPlusApp.Api.Controllers
{
    public class HomeController
    {
        [ActionContext]
        public ActionContext ActionContext { get; set; }
        [Route("")]
        public IActionResult Get()
        {
            const string logo = @"
                            __________________________
                           |                          |
                           |  NINETY MINUTES PLUS APP |
                           |  developed by David Thâm |
                           |__________________________|
                                ";
            var result = $"App name: {logo}\n App Id: {ActionContext.HttpContext.User.Claims.SingleOrDefault(x => x.Type == "appid").Value}";
            return new OkObjectResult(result);
        }
    }
}
