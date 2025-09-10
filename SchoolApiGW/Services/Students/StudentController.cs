using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SchoolApiGW.Helper;
using SchoolApiGW.Helper.Error;
using SchoolApiGW.Middleware;
using System.Text.Json;

namespace SchoolApiGW.Services.Students
{

    [Route("api/[controller]")]
    [ApiController]
    [JWTMiddleware]
    public class StudentController : ControllerBase
    {
        private readonly IStudentClient _studentClient;

        public StudentController(IStudentClient studentClient)
        {
            _studentClient = studentClient;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionType"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("AddStudent")]
        public async Task<ActionResult<ResponseModel>> AddStudent([FromQuery] int actionType, [FromForm] AddStudentRequestDTO request)
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
                        response = await _studentClient.AddStudentAsync(request, clientId);
                        break;

                    default:
                        response.Message = "Invalid actionType.";
                        return Ok(response);
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Status = -1;
                response.Message = "Error: " + ex.ToString();
                Helper.Error.ErrorBLL.CreateErrorLog("StudentController", "AddStudentByActionType", ex.ToString());
                return Ok(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionType"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        /// 
        [HttpGet("student")]
        public async Task<ActionResult<ResponseModel>> FetchStudent(int actionType, string? param)
        {
            var response = new ResponseModel
            {
                IsSuccess = true,
                Status = 0,
                Message = "Issue at Controller Level !",

            };
            var clientId = User.Claims.FirstOrDefault(c => c.Type == "ClientId")?.Value;

            if (string.IsNullOrEmpty(clientId))
                return Unauthorized("ClientId claim missing");
            try
            {
                switch (actionType)
                {
                    case 0:
                        if (!long.TryParse(param, out long classId))
                            return BadRequest(new ResponseModel { IsSuccess = true, Status = 0 });

                        response = await _studentClient.GetStudentsByClassAsync(classId, clientId);
                        return Ok(response);
                    case 1:
                        if (string.IsNullOrEmpty(param))
                            return BadRequest(new ResponseModel { IsSuccess = true, Status = 0 });

                        response = await _studentClient.GetStudentByAdmissionNoAsync(param, clientId);
                        return Ok(response);
                    case 2:
                        if (string.IsNullOrEmpty(param))
                            return BadRequest(new ResponseModel { IsSuccess = true, Status = 0 });

                        response = await _studentClient.GetStudentsByNameAsync(param, clientId);
                        return Ok(response);
                    case 3:
                        if (!long.TryParse(param, out long studentInfoId))
                            return BadRequest(new ResponseModel { IsSuccess = true, Status = 0 });

                        response = await _studentClient.GetStudentByStudentInfoIdAsync(studentInfoId, clientId);
                        return Ok(response);
                    case 4:
                        if (string.IsNullOrEmpty(param))
                            return BadRequest(new ResponseModel { IsSuccess = true, Status = 0 });

                        response = await _studentClient.GetStudentByPhoneAsync(param, clientId);
                        return Ok(response);
                    case 5:
                        if (string.IsNullOrEmpty(param))
                            return BadRequest(new ResponseModel { IsSuccess = true, Status = 0 });

                        response = await _studentClient.GetStudentsByCurrentSessionAsync(param, clientId);
                        return Ok(response);
                    case 6:
                        response = await _studentClient.GetNextAdmissionNoAsync(clientId);
                        return Ok(response);
                    case 7:
                        if (string.IsNullOrEmpty(param))
                            return BadRequest(new ResponseModel { IsSuccess = true, Status = 0 });

                        response = await _studentClient.GetAllStudentsOnSectionIDAsync(param, clientId);
                        return Ok(response);
                    case 8:
                        if (!long.TryParse(param, out long activeClassId))
                            return BadRequest(new ResponseModel { IsSuccess = true, Status = 0 });
                        // Get Only Active Students on Class ID in Current Session
                        response = await _studentClient.GetOnlyActiveStudentsOnClassIDAsync(activeClassId, clientId);
                        return Ok(response);
                    case 9:
                        if (!long.TryParse(param, out long activeSectionId))
                            return BadRequest(new ResponseModel { IsSuccess = true, Status = 0 });
                        // Get Only Active Students on Section ID in Current Session
                        response = await _studentClient.GetOnlyActiveStudentsOnSectionIDAsync(activeSectionId, clientId);
                        return Ok(response);
                    case 10: // 
                        if (string.IsNullOrEmpty(param))
                            return BadRequest(new ResponseModel { IsSuccess = true, Status = 0 });

                        response = await _studentClient.GetMaxRollnoAsync(param, clientId);
                        return Ok(response);
                    case 11: // Get All List of Students on SectionID(Both Active as well as Discharged)
                        if (string.IsNullOrEmpty(param))
                            return BadRequest(new ResponseModel { IsSuccess = true, Status = 0 });

                        response = await _studentClient.GetAllStudentsOnClassIDAsync(param, clientId);
                        return Ok(response);
                    case 12: // Get All List of Discharged Students on SectionID for General Schools
                        if (string.IsNullOrEmpty(param))
                            return BadRequest(new ResponseModel { IsSuccess = true, Status = 0 });

                        response = await _studentClient.GetAllDischargedStudentsOnSectionIDAsync(param, clientId);
                        return Ok(response);
                    case 13: // Get Total Student Roll in Current Session for dash Board
                        if (string.IsNullOrEmpty(param))
                            return BadRequest(new ResponseModel { IsSuccess = true, Status = 0 });

                        response = await _studentClient.TotalStudentsRollForDashBoardAsync(param, clientId);
                        return Ok(response);
                    case 14: // Get Class Wise Student Roll in Current Session for dash Board
                        if (string.IsNullOrEmpty(param))
                            return BadRequest(new ResponseModel { IsSuccess = true, Status = 0 });

                        response = await _studentClient.ClassWisStudentsRollForDashBoardAsync(param, clientId);
                        return Ok(response);
                    case 15: // Get Total Student Roll in Current Session for dash Board
                        if (string.IsNullOrEmpty(param))
                            return BadRequest(new ResponseModel { IsSuccess = true, Status = 0 });

                        response = await _studentClient.TotalStudentsRollForDashBoardOnDateAsync(param, clientId);
                        return Ok(response);
                    case 16: // Get Section Wise Student Roll With Attendance  on ClassID for dash Board
                        if (string.IsNullOrEmpty(param))
                            return BadRequest(new ResponseModel { IsSuccess = true, Status = 0 });

                        response = await _studentClient.SectionWisStudentsRollWithAttendanceForDashBoardAsync(param, clientId);
                        return Ok(response);
                    case 17: // Get Student List in Current Session on Student Name Like Search
                        if (string.IsNullOrEmpty(param))
                            return BadRequest(new ResponseModel { IsSuccess = true, Status = 0 });

                        response = await _studentClient.GetAllStudentsOnStudentNameAndCSessionAsync(param, clientId);
                        return Ok(response);
                    case 18: // Get Student List in with Board Numbers on SectionID
                        if (string.IsNullOrEmpty(param))
                            return BadRequest(new ResponseModel { IsSuccess = true, Status = 0 });

                        response = await _studentClient.GetBoardNoWithDateAsync(param, clientId);
                        return Ok(response);
                    //case 19: // Get Whole Student Data on Session for all classes
                    //    if (string.IsNullOrEmpty(param))
                    //        return BadRequest(new ResponseModel { IsSuccess = true, Status = 0 });

                    //    response = await _studentClient.GetAllStudentsOnSessionAsync(param, clientId);
                    //    return Ok(response);
                    case 19: // Get GetAllSessions 
                        return Ok(await _studentClient.GetAllSessions(clientId));

                    default:
                        return BadRequest(new ResponseModel { IsSuccess = true, Status = 0 });
                }
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("StudentController", "FetchStudentInfo", ex.ToString());
                response.Status = -1;
                response.Error = ex.Message;
                return StatusCode(500, response);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionType"></param>
        /// <param name="request"></param>
        /// <returns></returns>

        [HttpPut("update-student")]
        public async Task<IActionResult> UpdateStudent(int actionType, [FromForm] UpdateStudentRequestDTO request)
        {
            try
            {
                // var clientId = Request.Headers["X-Client-Id"].FirstOrDefault();
                var clientId = User.Claims.FirstOrDefault(c => c.Type == "ClientId")?.Value;

                if (string.IsNullOrEmpty(clientId))
                    return Unauthorized("ClientId claim missing");

                var response = new ResponseModel { IsSuccess = true, Status = 0, Message = "Issue at Controller Level !" };

                switch (actionType)
                {
                    case 0:
                        response = await _studentClient.UpdateStudentAsync(request, clientId);
                        break;

                    case 1:
                        response = await _studentClient.UpdateParentDetailAsync(request, clientId);
                        break;

                    case 2:
                        response = await _studentClient.UpdateAddressDetailAsync(request, clientId);
                        break;

                    case 3:

                        response = await _studentClient.UpdatePersonalDetailAsync(request, clientId);
                        break;

                    case 4:
                        response = await _studentClient.UpdateStudentRollNoAsync(request, clientId);
                        break;

                    case 5:
                        response = await _studentClient.UpdateBoardNoAsync(request, clientId);
                        break;
                    case 6:
                        response = await _studentClient.UpdateDOBAsync(request, clientId);
                        break;

                    case 7:
                        response = await _studentClient.UpdateSectionAsync(request, clientId);
                        break;

                    case 8:
                        response = await _studentClient.UpdateClassAsync(request, clientId);
                        break;

                    case 9: // Discharge Student AND SET IsDischarged='True' of StudentInfo Table as itz Bit
                        response = await _studentClient.DischargeStudentAsync(request, clientId);
                        break;
                    case 10: // Discharge Student AND SET IsDischarged=1 of StudentInfo Table as itz Int

                        response = await _studentClient.DischargeStudentForIntValueAsync(request, clientId);
                        break;

                    case 11: // Discharge Student AND SET IsDischarged=1 of StudentInfo Table as itz Int

                        response = await _studentClient.RejoinStudentAsync(request, clientId);
                        break;

                    case 12: // Discharge Student AND SET IsDischarged=1 of StudentInfo Table as itz Int

                        response = await _studentClient.RejoinStudentForIntValueAsync(request, clientId);
                        break;

                    case 13: // Discharge Student AND SET IsDischarged=1 of StudentInfo Table as itz Int

                        response = await _studentClient.UpdateStudentEducationAdmissionPrePrimaryEtcAsync(request, clientId);
                        break;

                    case 14: // Discharge Student AND SET IsDischarged=1 of StudentInfo Table as itz Int

                        response = await _studentClient.UpdateStudentHeightWeightAdharNamePENEtcUDISEAsync(request, clientId);
                        break;
                 
                    //case 16:
                    //    try
                    //    {
                    //        // Initialize empty updates list
                    //        var updates = new List<StudentRollNoUpdate>();

                    //        // Handle bulk updates
                    //        if (request.BulkUpdates != null)
                    //        {
                    //            updates = request.BulkUpdates
                    //                .Where(u => u != null && u.StudentInfoID > 0 && !string.IsNullOrWhiteSpace(u.RollNo))
                    //                .ToList();
                    //        }
                    //        // Handle single update
                    //        else if (!string.IsNullOrWhiteSpace(request.StudentInfoID) &&
                    //                int.TryParse(request.StudentInfoID, out var studentId) &&
                    //                studentId > 0 &&
                    //                !string.IsNullOrWhiteSpace(request.RollNo))
                    //        {
                    //            updates.Add(new StudentRollNoUpdate
                    //            {
                    //                StudentInfoID = studentId,
                    //                RollNo = request.RollNo,
                    //                UpdatedBy = request.UpdatedBy ?? "System"
                    //            });
                    //        }

                    //        if (updates.Count == 0)
                    //        {
                    //            response = new ResponseModel
                    //            {
                    //                IsSuccess = false,
                    //                Message = "No valid roll number updates provided"
                    //            };
                    //            break;
                    //        }

                    //        response = await _studentClient.UpdateClassStudentRollNumbers(updates, clientId);
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        response = new ResponseModel
                    //        {
                    //            IsSuccess = false,
                    //            Message = $"Roll number update failed: {ex.Message}"
                    //        };
                    //        // Log error if needed
                    //        Helper.Error.ErrorBLL.CreateErrorLog("StudentController", "UpdateRollNumbers", ex.ToString());
                    //    }
                    //    break;

                    default:
                        response = new ResponseModel
                        {
                            IsSuccess = false,
                            Status = 0,
                            Message = "Invalid action type."
                        };
                        break;
                }

                if (response.IsSuccess)
                    return Ok(response);
                else
                    return BadRequest(response);
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("StudentController", "UpdateStudentByActionType", ex.ToString());

                return StatusCode(500, new ResponseModel
                {
                    IsSuccess = false,
                    Status = -1,
                    Error = ex.Message
                });
            }
        }


        [HttpPut("bulk-update-students")]
        public async Task<IActionResult> BulkUpdateStudents([FromQuery] int actionType, [FromBody] JsonElement request)
        {
            var response = new ResponseModel { IsSuccess = true, Status = 0, Message = "Issue at Controller Level !" };

            try
            {
                // Get ClientId from claims
                var clientId = User.Claims.FirstOrDefault(c => c.Type == "ClientId")?.Value;
                if (string.IsNullOrEmpty(clientId))
                    return Unauthorized("ClientId missing");

                switch (actionType)
                {
                    case 0: // Bulk RollNo Update
                        {
                            var rollUpdateRequest = JsonConvert.DeserializeObject<BulkRollNoUpdateRequest>(request.GetRawText());

                            if (rollUpdateRequest?.BulkUpdates == null || !rollUpdateRequest.BulkUpdates.Any())
                            {
                                return BadRequest(new ResponseModel
                                {
                                    IsSuccess = false,
                                    Message = "No roll number updates provided"
                                });
                            }

                            var updates = rollUpdateRequest.BulkUpdates
                                .Where(u => u.StudentInfoID.HasValue &&
                                            u.StudentInfoID.Value > 0 &&
                                            !string.IsNullOrWhiteSpace(u.RollNo))
                                .ToList();

                            if (!updates.Any())
                            {
                                return BadRequest(new ResponseModel
                                {
                                    IsSuccess = false,
                                    Message = "No valid roll number updates found"
                                });
                            }

                            response = await _studentClient.UpdateClassStudentRollNumbers(updates, clientId);
                            break;
                        }

                    case 15: // Bulk Session Update
                        {
                            var sessionRequest = JsonConvert.DeserializeObject<StudentSessionUpdateRequest>(request.GetRawText());

                            if (sessionRequest?.Students == null || !sessionRequest.Students.Any())
                            {
                                return BadRequest(new ResponseModel
                                {
                                    IsSuccess = false,
                                    Message = "No session update data provided"
                                });
                            }

                            response = await _studentClient.UpdateStudentSessionAsync(sessionRequest, clientId);
                            break;
                        }

                    default:
                        response = new ResponseModel
                        {
                            IsSuccess = false,
                            Message = "Invalid ActionType"
                        };
                        break;
                }

                return response.IsSuccess ? Ok(response) : BadRequest(response);
            }
            catch (Exception ex)
            {
                Helper.Error.ErrorBLL.CreateErrorLog("StudentRollNoController", "BulkUpdateStudents", ex.ToString());

                return StatusCode(500, new ResponseModel
                {
                    IsSuccess = false,
                    Status = -1,
                    Error = ex.Message
                });
            }
        }


    }
}
