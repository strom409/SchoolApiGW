using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SchoolApiGW.Services.Timetable_Arrangement.TTAssignPeroids
{
    [Route("api/[controller]")]
    [ApiController]
    public class TTAssignPeroidsController : ControllerBase
    {
        private readonly ITTAssignPeroidsClient _ttAssignPeroidsClient;

        public TTAssignPeroidsController(ITTAssignPeroidsClient ttAssignPeroidsClient)
        {
            _ttAssignPeroidsClient = ttAssignPeroidsClient;
        }
    }
}
