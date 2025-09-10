using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolApiGW.Helper;

namespace SchoolApiGW.Services.Result.Gazet
{
    [Route("api/[controller]")]
    [ApiController]
    public class GazetController : ControllerBase
    {
        private readonly IGazetClient _gazetClient;
        public GazetController(IGazetClient gazetClient)
        {
            _gazetClient= gazetClient;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionType"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet("fetch")]
        public async Task<ActionResult<ResponseModel>> Get([FromQuery] int actionType, [FromQuery] string? param = null)
        {
            var response = new ResponseModel { IsSuccess = true, Status = 0, Message = "Issue at Controller Level!" };

            try
            {
                var clientId = User.Claims.FirstOrDefault(c=>c.Type=="x_clientid")?.Value;

                if (string.IsNullOrEmpty(clientId))
                    return Unauthorized("ClientId claim missing");

                switch (actionType)
                {
                    case 1: // Get Gazet results by Class, Section, Session, Unit
                        response = await _gazetClient.GetGazetResults(param, clientId);
                        break;

                    default:
                        response.IsSuccess = false;
                        response.Message = "Invalid actionType provided.";
                        break;
                }
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("GazetController", "Get", ex.ToString());
                response.IsSuccess = false;
                response.Status = -1;
                response.Message = "An unexpected error occurred.";
                response.Error = ex.Message;
            }

            return Ok(response);
        }
    }
}

