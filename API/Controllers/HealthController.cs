using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    public class HealthController : BasicApiController
    {
        [HttpGet(Name = "GetHealthState")]
        public ActionResult<HealthState> Get()
        {
            return new HealthState{Status = "OK"};
        }
    }
}