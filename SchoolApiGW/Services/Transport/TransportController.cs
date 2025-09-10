using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SchoolApiGW.Helper;

namespace SchoolApiGW.Services.Transport
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransportController : ControllerBase
    {
        public readonly ITransportClient _transportClient;
        public TransportController(ITransportClient transportClient)
        {
            _transportClient = transportClient;
        }


        [HttpPost("AddTransport")]
        public async Task<ActionResult<ResponseModel>> AddTransportAsync(
    [FromQuery] int actionType,
    [FromBody] TransportDTO request)
        {
            var response = new ResponseModel { IsSuccess = true, Status = 0, Message = "Invalid Request!" };

            try
            {
                // Extract clientId from header (or from JWT claims if applicable)
                var clientId = User.Claims.FirstOrDefault(c => c.Type == "ClientId")?.Value;

                if (string.IsNullOrEmpty(clientId))
                    return Unauthorized("ClientId claim missing");

                // Validate input
                if (request == null)
                {
                    response.Message = "Invalid transport data.";
                    return BadRequest(response);
                }

                // Switch based on actionType
                switch (actionType)
                {
                    case 0: // Add Transport
                        return Ok(await _transportClient.AddTransport(request, clientId));

                    case 1: // Add Bus Stops
                        return Ok(await _transportClient.AddBusStops(request, clientId));

                    default:
                        response.Message = "Unsupported actionType.";
                        return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Status = -1;
                response.Message = "Exception occurred.";
                response.Error = ex.Message;

                return StatusCode(500, response);
            }
        }


        [HttpGet("FetchTransport")]
        public async Task<ActionResult<ResponseModel>> FetchTransport([FromQuery] int actionType, [FromQuery] string? param)
        {
            var response = new ResponseModel { IsSuccess = true, Status = 0 };

            var clientId = User.Claims.FirstOrDefault(c => c.Type == "ClientId")?.Value;

            if (string.IsNullOrEmpty(clientId))
                return Unauthorized("ClientId claim missing");

            try
            {
                switch (actionType)
                {
                    case 0:
                        return Ok(await _transportClient.GetTransportListOnSession(param, clientId));

                    case 1:
                        return Ok(await _transportClient.GetTransportList(param, clientId));

                    case 2:
                        return Ok(await _transportClient.GetTransportListRateFromInfo(param, clientId));

                    case 3:
                        return Ok(await _transportClient.GetTransportListWithBusRate(param, clientId));

                    case 4:
                        return Ok(await _transportClient.GetTransportByRouteId(param, clientId));

                    case 5:
                        return Ok(await _transportClient.GetStudentRouteDetails(param, clientId));

                    case 6:
                        return Ok(await _transportClient.GetStopListByName(param, clientId));

                    case 7:
                        return Ok(await _transportClient.GetAllStops(clientId));

                    case 8:
                        return Ok(await _transportClient.GetClassIdsAssigned(param, clientId));

                    case 9:
                        return Ok(await _transportClient.GetAssignedSections(param, clientId));

                    case 10:
                        return Ok(await _transportClient.GetStudentBusReportListOnSectionID(param, clientId));

                    case 11:
                        return Ok(await _transportClient.GetStudentListOnRouteID(param, clientId));

                    case 12:
                        return Ok(await _transportClient.GetStudentBusRateClasswise(param, clientId));

                    case 13:
                        return Ok(await _transportClient.GetStudentBusRate(param, clientId));

                    case 14:

                        return Ok(await _transportClient.GetTransportNameById(param, clientId));

                    case 15:
                        return Ok(await _transportClient.getTransportList(clientId));

                    case 16:
                        return Ok(await _transportClient.GetStopListWithLatLong(param, clientId));

                    default:
                        return BadRequest(new ResponseModel
                        {
                            IsSuccess = false,
                            Message = "Invalid actionType"
                        });
                }
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("TransportController", "FetchTransport", ex.ToString());

                response.IsSuccess = false;
                response.Status = -1;
                response.Message = "Exception occurred.";
                response.Error = ex.Message;

                return StatusCode(500, response);
            }
        }


        [HttpPut("UpdateTransport")]
        public async Task<ActionResult<ResponseModel>> PutTransport(
     [FromQuery] int actionType,
     [FromBody] object payload) // we'll cast manually based on actionType
        {
            var response = new ResponseModel { IsSuccess = true, Status = 0, Message = "Invalid Request" };

            // Simulate clientId fetch
            var clientId = User.Claims.FirstOrDefault(c => c.Type == "ClientId")?.Value;

            if (string.IsNullOrEmpty(clientId))
                return Unauthorized("ClientId claim missing");

            try
            {
                switch (actionType)
                {
                    case 0:
                        // Update student's route and stop (TransportDTO)
                        var studentRouteDto = JsonConvert.DeserializeObject<TransportDTO>(payload.ToString());
                        return Ok(await _transportClient.updateroute(studentRouteDto, clientId));

                    case 1:
                        // Update transport info (TransportDTO)
                        var transportDto = JsonConvert.DeserializeObject<TransportDTO>(payload.ToString());
                        return Ok(await _transportClient.UpdateTransport(transportDto, clientId));

                    case 2:
                        // Update bus stop info (TransportDTO)
                        var stopDto = JsonConvert.DeserializeObject<TransportDTO>(payload.ToString());
                        return Ok(await _transportClient.UpdateBusStops(stopDto, clientId));

                    case 3:
                        // Update bus stop lat/long (TransportDTO)
                        var latLongDto = JsonConvert.DeserializeObject<TransportDTO>(payload.ToString());
                        return Ok(await _transportClient.UpdateBusStopsLatLong(latLongDto, clientId));

                    case 4:
                        // Update bus stop rates (StudentBusReportDTO)
                        var rateDto = JsonConvert.DeserializeObject<StudentBusReportDTO>(payload.ToString());
                        return Ok(await _transportClient.UpdateBusStopRates(rateDto, clientId));

                    case 5:
                        // Update student route and bus stop (StudentBusReportDTO)
                        var fullDto = JsonConvert.DeserializeObject<StudentBusReportDTO>(payload.ToString());
                        return Ok(await _transportClient.UpdateStudentRouteAndBusStop(fullDto, clientId));
                    case 6:
                        // Update aupdateBusStop 
                        var busDto = JsonConvert.DeserializeObject<BusStop>(payload.ToString());
                        return Ok(await _transportClient.aupdateBusStop(busDto, clientId));
                    default:
                        response.Message = "Invalid actionType";
                        return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("TransportController", "PutTransport", ex.ToString());

                response.IsSuccess = false;
                response.Status = -1;
                response.Message = "Exception occurred";
                response.Error = ex.Message;

                return StatusCode(500, response);
            }
        }

        [HttpDelete("DeleteTransport")]
        public async Task<IActionResult> DeleteTransport(
    [FromQuery] int actionType,
    [FromQuery] string? param)
        {
            var response = new ResponseModel { IsSuccess = true, Status = 0, Message = "Invalid Request" };

            // var clientId = User.Claims.FirstOrDefault(c => c.Type == "ClientID")?.Value;
            var clientId = User.Claims.FirstOrDefault(c => c.Type == "ClientId")?.Value;

            if (string.IsNullOrEmpty(clientId))
                return Unauthorized("ClientId claim missing");

            try
            {
                switch (actionType)
                {
                    case 0: // Delete Transport by RouteID

                        return Ok(await _transportClient.deleteTransport(param, clientId));
                    case 1: // Soft delete BusStop by BusStopID
                        return Ok(await _transportClient.DeleteBusStop(param, clientId));

                    default:
                        response.Message = "Invalid actionType";
                        return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Status = -1;
                response.Message = "Exception occurred";
                response.Error = ex.Message;
                return StatusCode(500, response);
            }
        }

    }
}
