using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SchoolApiGW.Services.Timetable_Arrangement.TTPeroidsNo
{
    [Route("api/[controller]")]
    [ApiController]
    public class TPeroidsNoController : ControllerBase
    {
        private readonly ITPeroidsNoClient _ttPeroidsNoClient;

        public TPeroidsNoController(ITPeroidsNoClient ttPeroidsNoClient)
        {
            _ttPeroidsNoClient = ttPeroidsNoClient;
        }
    }
}
