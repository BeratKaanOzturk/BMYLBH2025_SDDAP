using System;
using System.Web.Http;

namespace BMYLBH2025_SDDAP.Controllers
{
    [RoutePrefix("api/test")]
    public class TestController : ApiController
    {
        // GET api/test/ping
        [HttpGet]
        [Route("ping")]
        public IHttpActionResult Ping()
        {
            return Ok(new { 
                message = "API is working", 
                timestamp = DateTime.Now,
                status = "healthy"
            });
        }
    }
} 