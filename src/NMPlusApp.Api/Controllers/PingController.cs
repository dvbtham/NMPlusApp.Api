using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NMPlusApp.Api.Controllers
{
    [AllowAnonymous]
    public class PingController
    {
        [Route("/ping")]
        public ActionResult Get()
        {
            return new OkObjectResult("Pong!");
        }
    }
}
