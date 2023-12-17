using Microsoft.AspNetCore.Mvc;

namespace DiyorMarket.Controllers
{
    [Route("api/test")]
    public class TestController5
    {
        [HttpGet("id")]
        public string Get(int id)
        {
            return "empty";
        }
    }
}
