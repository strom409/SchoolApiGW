using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SchoolApiGW.Services.Timetable_Arrangement.TTPeroids
{
    [Route("api/[controller]")]
    [ApiController]
    public class TTPeroidController : ControllerBase
    {
        private readonly ITTPeroidClient _ttPeroidClient;

        public TTPeroidController(ITTPeroidClient ttPeroidClient)
        {
            _ttPeroidClient = ttPeroidClient;
        }
    }
}
