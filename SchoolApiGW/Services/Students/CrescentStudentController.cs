using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolApiGW.Helper;

namespace SchoolApiGW.Services.Students
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrescentStudentController : ControllerBase
    {

        private readonly ICrescentStudentClient _cresentstudentClient;

        public CrescentStudentController(ICrescentStudentClient cresentstudentClient)
        {
            _cresentstudentClient = cresentstudentClient;
        }

        [HttpPost("addcresentstudent")]
        public async Task<ActionResult<ResponseModel>> AddCresentStudent([FromQuery] int actionType, [FromBody] AddStudentRequestDTO request)
        {
            var response = new ResponseModel { IsSuccess = true, Status = 0, Message = "Issue at Controller Level !" };

                try
                {
                    var clientId = User.Claims.FirstOrDefault(c => c.Type == "ClientId")?.Value;

                    if (string.IsNullOrEmpty(clientId))
                        return Unauthorized("ClientId claim missing");


                    switch (actionType)
                {
                    case 0:
                        response = await _cresentstudentClient.AddNewStudentWithUID(request, clientId);
                        break;

                    default:
                        response.Message = "Invalid actionType.";
                        return Ok(response);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "DUPLICATE_STUDENT")
                {
                    response.IsSuccess = false;
                    response.Status = 0;
                    response.Message = "Duplicate student entry found.";
                    return Conflict(response); // HTTP 409
                }

                Helper.Error.ErrorBLL.CreateErrorLog("StudentController", "AddStudentByActionType", ex.ToString());

                response.IsSuccess = false;
                response.Status = -1;
                response.Message = "Error: " + ex.ToString();
                return Ok(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionType"></param>
        /// <param name="param1"></param>
        /// <param name="param2"></param>
        /// <returns></returns>
        [HttpGet("Get")]
        public async Task<ActionResult<ResponseModel>> Get(int actionType, string param1, string? param2 = null)
        {
            var response = new ResponseModel
            {
                IsSuccess = true,
                Status = 0,
                Message = "Issue at Controller Level!"
            };

            try
            {
                var clientId = User.Claims.FirstOrDefault(c => c.Type == "ClientId")?.Value;

                if (string.IsNullOrEmpty(clientId))
                    return Unauthorized("ClientId claim missing");

                switch (actionType)
                {
                    case 0: // Active students
                        return Ok(await _cresentstudentClient.GetAllActiveStudentsOnSectionIDCrescentSchool(param1, clientId));

                    case 1: // Discharged students
                        return Ok(await _cresentstudentClient.GetAllDischargedStudentsOnSectionIDCrescentSchool(param1, clientId));

                    case 2: // All students on section ID
                        return Ok(await _cresentstudentClient.GetAllStudentsOnSectionIDCrescent(param1, clientId));
                    case 3: // Students by class ID
                        return Ok(await _cresentstudentClient.GetAllStudentsOnClassIDCrescent(param1, clientId));
                    case 4: // Students by class ID
                        return Ok(await _cresentstudentClient.GetInvalidDischargeListOnClassIDCrescent(param1, clientId));


                    case 5: // Get Max UID
                        return Ok(await _cresentstudentClient.GetMaxUID(param1, clientId));

                    case 6: // Get Max Roll No by ClassID and SectionID
                        if (param2 == null)
                            return BadRequest(new ResponseModel { IsSuccess = false, Status = 0 });

                        return Ok(await _cresentstudentClient.GetMaxRollNo(param1, param2, clientId));

                    case 7: // Get Crescent Student On UID 
                        return Ok(await _cresentstudentClient.GetActiveStudentsOnUID(param1, clientId));

                    case 8: // Get students by phone number
                        return Ok(await _cresentstudentClient.GetStudentsByPhoneNumber(param1, clientId));

                    case 9: // Get students by address
                        return Ok(await _cresentstudentClient.GetStudentsByAddress(param1, clientId));
                    case 10: // Get students by address
                        return Ok(await _cresentstudentClient.GetStudentsByName(param1, clientId));
                    case 11: // Get students by address
                        return Ok(await _cresentstudentClient.GetStudentsByAcademicNo(param1, clientId));

                    default:
                        return BadRequest(new ResponseModel { IsSuccess = false, Status = 0 });
                }
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("StudentsController", "Get", ex.ToString());

                response.Status = -1;
                response.Error = ex.Message;
                return StatusCode(500, response);
            }
        }

    }
}
