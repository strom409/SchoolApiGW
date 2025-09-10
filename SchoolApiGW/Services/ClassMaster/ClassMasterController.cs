using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SchoolApiGW.Helper;

namespace SchoolApiGW.Services.ClassMaster
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassMasterController : ControllerBase
    {

        private readonly IClassMasterClient _classMasterClient;

        public ClassMasterController(IClassMasterClient classMasterClient)
        {
            _classMasterClient = classMasterClient;
        }


        [HttpPost("add-info")]
        public async Task<ActionResult<ResponseModel>> Add([FromQuery] int actionType, [FromBody] object request)
        {
            var response = new ResponseModel { IsSuccess = true, Status = 0, Message = "Issue at Controller Level!" };

            try
            {
                var clientId = User.Claims.FirstOrDefault(c => c.Type == "ClientId")?.Value;

                if (string.IsNullOrEmpty(clientId))
                    return Unauthorized("ClientId claim missing");


                switch (actionType)
                {
                    case 0: // Add Class
                        var classDto = JsonConvert.DeserializeObject<ClassDto>(request.ToString());
                        response = await _classMasterClient.AddClass(classDto, clientId);
                        break;

                    case 1: // Add Section
                        var sectionDto = JsonConvert.DeserializeObject<SectionDto>(request.ToString());
                        response = await _classMasterClient.AddSection(sectionDto, clientId);
                        break;
                    case 2: // Upgrade Class + Subjects + Sections with Duplicate Check
                        var upgradeDto = JsonConvert.DeserializeObject<UpgradeClassDto>(request.ToString());
                        response = await _classMasterClient.UpgradeClassSubjectsSectionsAsync(upgradeDto, clientId);
                        break;

                    default:
                        response.IsSuccess = false;
                        response.Message = "Invalid actionType.";
                        return Ok(response);
                }
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("ClassMasterController", "AddInfo", ex.ToString());
                response.IsSuccess = false;
                response.Status = -1;
                response.Message = "An unexpected error occurred.";
                response.Error = ex.Message;
            }

            return Ok(response);
        }


        [HttpGet("class-master")]
        public async Task<ActionResult<ResponseModel>> FetchClassMaster(int actionType, string? param)
        {
            var response = new ResponseModel
            {
                IsSuccess = true,
                Status = 0
            };

            // var clientId = Request.Headers["X-Client-Id"].FirstOrDefault();
            var clientId = User.Claims.FirstOrDefault(c => c.Type == "ClientId")?.Value;

            if (string.IsNullOrEmpty(clientId))
                return Unauthorized("ClientId claim missing");


            try
            {
                switch (actionType)
                {
                    case 0:
                        return Ok(await _classMasterClient.GetEducationalDepartments(clientId));

                    case 1:
                        if (!int.TryParse(param, out int classId))
                            return BadRequest(response);

                        return Ok(await _classMasterClient.GetSectionsByClassId(classId, clientId));

                    case 2:
                        if (string.IsNullOrWhiteSpace(param))
                            return BadRequest(response);

                        return Ok(await _classMasterClient.GetClassesBySessionWithDepartment(param, clientId));

                    default:
                        return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("ClassMasterController", "FetchClassMasterInfo", ex.ToString());

                response.IsSuccess = false;
                response.Status = -1;
                response.Error = ex.Message;

                return StatusCode(500, response);
            }
        }


        [HttpPut("update-by-action")]
        public async Task<ActionResult<ResponseModel>> Update([FromQuery] int actionType, [FromBody] object dto)
        {
            var response = new ResponseModel { IsSuccess = false, Status = 0, Message = "Invalid request." };

            try
            {
                var clientId = User.Claims.FirstOrDefault(c => c.Type == "ClientId")?.Value;

                if (string.IsNullOrEmpty(clientId))
                    return Unauthorized("ClientId claim missing");


                switch (actionType)
                {
                    case 0: // Update Class
                        var classDto = JsonConvert.DeserializeObject<ClassDto>(dto.ToString());
                        if (classDto.ClassId <= 0)
                            return BadRequest(new ResponseModel { IsSuccess = false, Status = 0, Message = "Invalid ClassId for update." });

                        response = await _classMasterClient.UpdateClass(classDto, clientId);
                        break;

                    case 1: // Update Section
                        var sectionDto = JsonConvert.DeserializeObject<SectionDto>(dto.ToString());
                        if (sectionDto.SectionID <= 0)
                            return BadRequest(new ResponseModel { IsSuccess = false, Status = 0, Message = "Invalid SectionID for update." });

                        response = await _classMasterClient.UpdateSection(sectionDto, clientId);
                        break;

                    default:
                        response.Message = "Invalid actionType.";
                        break;
                }
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("ClassMasterController", "UpdateByActionType", ex.ToString());
                response.IsSuccess = false;
                response.Status = -1;
                response.Message = "An error occurred during update.";
                response.Error = ex.Message;
            }

            return StatusCode(200, response); // Always return 200 with proper response structure
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromQuery] int actionType, [FromQuery] int id)
        {
            var response = new ResponseModel { IsSuccess = true, Status = 0 };

            try
            {
                var clientId = User.Claims.FirstOrDefault(c => c.Type == "ClientId")?.Value;

                if (string.IsNullOrEmpty(clientId))
                    return Unauthorized("ClientId claim missing");


                switch (actionType)
                {
                    case 0: // Delete Class
                        response = await _classMasterClient.DeleteClass(id, clientId);
                        break;

                    case 1: // Delete Section
                        response = await _classMasterClient.DeleteSection(id, clientId);
                        break;

                    default:
                        response.IsSuccess = false;
                        response.Status = 0;
                        return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Status = -1;
                response.Message = "Error occurred during deletion.";
                response.Error = ex.Message;
                Helper.Error.ErrorBLL.CreateErrorLog("ClassMasterController", "DeleteByAction", ex.ToString());
            }

            return StatusCode(response.Status == 0 ? 400 : 200, response);
        }



    }

}

