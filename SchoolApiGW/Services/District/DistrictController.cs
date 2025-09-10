using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolApiGW.Helper;

namespace SchoolApiGW.Services.District
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistrictController : ControllerBase
    {
        private readonly IDistrictClient _districtClient;
        public DistrictController(IDistrictClient districtClient)
        {
            _districtClient= districtClient;

        }

        [HttpGet("district-master")]
        public async Task<ActionResult<ResponseModel>> FetchDistrictInfo(int actionType, string? param)
        {
            var response = new ResponseModel
            {
                IsSuccess = true,
                Status = 0
            };

            // You can use this line if passing clientId from header:
            // var clientId = Request.Headers["clientid"].FirstOrDefault();

            var clientId = User.Claims.FirstOrDefault(c => c.Type == "ClientId")?.Value;

            if (string.IsNullOrEmpty(clientId))
                return BadRequest("ClientId header missing");

            try
            {
                switch (actionType)
                {
                    case 0:
                        // Get all districts
                        return Ok(await _districtClient.GetAllDistricts(clientId));

                    case 1:
                        // Get districts by state ID
                        if (!int.TryParse(param, out int stateId))
                            return BadRequest("Invalid StateID");

                        return Ok(await _districtClient.GetDistrictsByStateId(stateId, clientId));
                    case 2:
                        return Ok(await _districtClient.GetAllStates(clientId));

                    default:
                        response.IsSuccess = false;
                        response.Status = 0;
                        response.Message = "Invalid action type";
                        return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("DistrictController", "FetchDistrictInfo", ex.ToString());

                response.IsSuccess = false;
                response.Status = -1;
                response.Message = "Exception occurred";
                response.Error = ex.Message;

                return StatusCode(500, response);
            }
        }
    }
}
