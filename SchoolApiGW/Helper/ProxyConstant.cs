namespace SchoolApiGW.Helper
{
    public static class ProxyConstant
    {
        // login for testing 
        public static readonly string ClientLoginUser_LoginUser = "/Login";
        public const string HT_GetHT = "/api/HT?actionType=0&clientId={0}";
        public const string HT_UpdateHT = "/api/HT?actionType=0&clientId={0}";
        //  for user endpoint 
        public const string Clientuserpost_PostAddUser = "api/User/User?actionType=0";
        public const string Clientuserput_PutUpdateUser = "api/User/UpdateUser?actionType=0";
        public const string Clientuserdelete_DeleteUser = "/api/User/Users/{0}?actionType=0";
        // Add Student
        public static readonly string ClientCresentstudentadd_addstudent = "/api/student";

        // Get all active students by section ID
        public static readonly string ClientCresentstudentgetactivebysection_getactive = "/api/student/active/section/{0}";

        // Get all discharged students by section ID
        public static readonly string ClientCresentstudentgetdischargedbysection_getdischarged = "/api/student/discharged/section/{0}";

        // Get all students by class ID
        public static readonly string ClientCresentstudentgetbyclass_getstudentsbyclass = "/api/student/class/{0}";

        // Get all students by section ID
        public static readonly string ClientCresentstudentgetbysection_getstudentsbysection = "/api/Student/fetch-student-info/{0}/{1}";

        // Get max roll number by class ID and section ID
        public static readonly string ClientCresentstudentgetmaxroll_getmaxroll = "/api/student/max-roll/class/{0}/section/{1}";

        // Get max UID by session
        public static readonly string ClientCresentstudentgetmaxuid_getmaxuid = "/api/student/max-uid/session/{0}";



        // Add Student
        public const string Clientstudentpost_PostAddStudent = "AddStudent?actionType=0";

        // Get Next Admission Number
        public static readonly string Clientstudentgetby_GetNextAdmissionNo = "/student?actionType={0}";
        // Get Student By Admission Number
        public static readonly string Clientstudentgetbyadmissionno_getstudentbyadmissionno = "/student?actionType={0}&param={1}";

        // Get Student By Phone Number
        public static readonly string Clientstudentgetbyphone_getstudentbyphone = "/student?actionType={0}&param={1}";

        // Get Student By StudentInfoId
        public static readonly string Clientstudentgetbystudentinfoid_getstudentbystudentinfoid = "/student?actionType={0}&param={1}";

        // Get Students By Class ID
        public static readonly string Clientstudentgetbyclass_getstudentsbyclass = "/student?actionType={0}&param={1}";


        // Get Students By Current Session
        public static readonly string Clientstudentgetbycurrentsession_getstudentsbycurrentsession = "/student?actionType={0}&param={1}";

        // Get Students By Name
        public static readonly string Clientstudentgetbyname_getstudentsbyname = "/student?actionType={0}&param={1}";
        public static readonly string Clientstudentgetby_OnlyActiveStudentsOnClassID = "/student?actionType={0}&param={1}";

        public static readonly string Clientstudentgetby_OnlyActiveStudentsOnSectionID = "/student?actionType={0}&param={1}";

        public static readonly string Clientstudentgetby_MaxRollno = "/student?actionType={0}&param={1}";

        public static readonly string Clientstudentgetby_GetAllStudentsOnClassID = "/student?actionType={0}&param={1}";

        public static readonly string Clientstudentgetby_GetAllDischargedStudentsOnSectionID = "/student?actionType={0}&param={1}";

        public static readonly string Clientstudentgetby_TotalStudentsRollForDashBoard = "/student?actionType={0}&param={1}";
         public static readonly string Clientstudentgetby_AttendanceDashboard = "/student?actionType={0}&param={1}";

        public static readonly string Clientstudentgetby_ClassWisStudentsRollForDashBoard = "/student?actionType={0}&param={1}";

        public static readonly string Clientstudentgetby_TotalStudentsRollForDashBoardOnDate = "/student?actionType={0}&param={1}";

        public static readonly string Clientstudentgetby_SectionWisStudentsRollWithAttendanceForDashBoard = "/student?actionType={0}&param={1}";

        public static readonly string Clientstudentgetby_GetAllStudentsOnStudentNameAndCSession = "/student?actionType={0}&param={1}";

        public static readonly string Clientstudentgetby_GetBoardNoWithDate = "/student?actionType={0}&param={1}";

        public static readonly string Clientstudentgetby_GetAllStudentsOnSession = "/student?actionType={0}&param={1}";
        public const string Clientstudentgetby_GetAllSessions = "/student?actionType={0}";
        // for district n state 
        public const string District_GetAll = "api/district/district-master?actionType=0";
        public const string District_GetByStateId = "api/district/district-master?actionType=1&param={0}";
        public const string State_GetAll = "api/District/district-master?actionType=2";

        // Update Student
        public const string Clientstudentpost_UpdateStudent = "/update-student?actionType={0}";

        public const string Clientstudentput_UpdateParentDetail = "/update-student?actionType={0}";

        public const string Clientstudentput_UpdateAddressDetail = "/update-student?actionType={0}";

                           
        public const string Clientstudentput_UpdatePersonalDetail = "/update-student?actionType={0}";

        public const string Clientstudentget_GetStudentId = "/student?actionType={0}&param={1}";

        public const string Clientstudentpost_PostUpdateStudentRollNo = "/update-student?actionType={0}";
        public const string ClientstudentUpdate_UpdateClassStudentRollNumbers = "/bulk-update-students?actionType=0";

        public const string Clientstudentput_UpdateStudentBoardNo = "/update-student?actionType={0}";

        public const string Clientstudentput_UpdateStudentDOB = "/update-student?actionType={0}";

        public const string Clientstudentpost_PostUpdateStudentSection = "/update-student?actionType={0}";

        public const string Clientstudentput_PutUpdateStudentClass = "/update-student?actionType={0}";

        public const string Clientstudentpost_PostDischargeStudent = "update-student?actionType={0}";

        public const string Clientstudentpost_PostDischargeStudentForIntValue = "update-student-by-action/{0}";

                 
        public const string ClientstudentPut_PutRejoinStudent = "update-student?actionType={0}";

        public const string Clientstudentpost_PostRejoinStudentForIntValue = "update-student-by-action/{0}";

        public const string Clientstudentpost_PostUpdateStudentEducationAdmissionPrePrimaryEtc = "update-student-by-action/{0}";

        public const string Clientstudentpost_PostUpdateStudentHeightWeightAdharNamePENEtcUDISE = "update-student-by-action/{0}";

        public const string Clientstudentput_UpdateStudentSession = "bulk-update-students?actionType={0}";
        // Add User
        public static readonly string Clientuseradd_adduser = "/api/User/add";


        // for Cresent School

        public const string Clientstudentpost_AddNewStudentWithUID = "api/CrescentStudent/addcresentstudent?actionType=0";
        //get url
        public const string Clientstudent_getactivestudentsonuid = "api/CrescentStudent/Get?actionType={0}&param1={1}";

        public const string Clientstudent_getallactivestudentsonsectionidcrescent = "api/CrescentStudent/Get?actionType={0}&param1={1}";


        public const string Clientstudent_getalldischargedstudentsonsectionidcrescent = "api/CrescentStudent/Get?actionType={0}&param1={1}";


        public const string Clientstudent_getallstudentsonclassidcrescent = "api/CrescentStudent/Get?actionType={0}&param1={1}";

        public const string Clientstudent_getallstudentsonsectionidcrescent = "api/CrescentStudent/Get?actionType={0}&param1={1}";

        public const string Clientstudent_getinvaliddischargelistonclassidcrescent = "api/CrescentStudent/Get?actionType={0}&param1={1}";


        public const string Clientstudent_getmaxrollno = "api/CrescentStudent/Get?actionType={0}&param1={1}&param2={2}";


        public const string Clientstudent_getmaxuid = "api/CrescentStudent/Get?actionType={0}&param1={1}";

        public const string Clientstudent_getstudentsbyphoneno = "api/CrescentStudent/Get?actionType={0}&param1={1}";

        public const string Clientstudent_getstudentsbyaddress = "api/CrescentStudent/Get?actionType={0}&param1={1}";

        public const string Clientstudent_getstudentsbyname = "api/CrescentStudent/Get?actionType={0}&param1={1}";

        public const string Clientstudent_getstudentsbyacademicno = "api/CrescentStudent/Get?actionType={0}&param1={1}";




        //// transport url
        public const string TransportAdd_PostAddTransport = "api/Transport/AddTransport?actionType=0";

        public const string TransportAddBusStops_PostAddBusStops = "api/Transport/AddTransport?actionType=1";
      
        //update 
        public const string TransportUpdateRoute_PostUpdateRoute = "api/transport/UpdateTransport?actionType=0";

        public const string TransportUpdate_PostUpdateTransport = "api/Transport/UpdateTransport?actionType=1";

        public const string TransportUpdateBusStops_PostUpdateBusStops = "api/transport/UpdateTransport?actionType=2";

        public const string TransportUpdateBusStopsLatLong_PostUpdateBusStopsLatLong = "api/Transport/UpdateTransport?actionType=3";

        public const string TransportUpdateBusStopRates_PostUpdateBusStopRates = "api/transport/UpdateTransport?actionType=4";

        public const string TransportUpdateStudentRouteAndBusStop_PostUpdateStudentRouteAndBusStop = "api/transport/UpdateTransport?actionType=5";


        public const string TransportAUpdateBusStop_PostAUpdateBusStop = "api/transport/UpdateTransport?actionType=6";


        // get url
        public const string TransportGetListOnSession_GetTransportListOnSession = "api/transport/FetchTransport?actionType={0}&param={1}";

        public const string TransportGetList_GetTransportList = "api/Transport/FetchTransport?actionType={0}&param={1}";


        public const string TransportGetListRateFromInfo_GetTransportListRateFromInfo = "api/Transport/FetchTransport?actionType={0}&param={1}";


        public const string TransportGetListWithBusRate_GetTransportListWithBusRate = "api/Transport/FetchTransport?actionType={0}&param={1}";


        public const string TransportGetByRouteId_GetTransportByRouteId = "api/Transport/FetchTransport?actionType={0}&param={1}";


        public const string TransportGetStudentRouteDetails_GetStudentRouteDetails = "api/Transport/FetchTransport?actionType={0}&param={1}";

        public const string TransportGetStopListByName_GetStopListByName = "api/Transport/FetchTransport?actionType={0}&param={1}";

        public const string TransportGetAllStops_GetAllStops = "api/Transport/FetchTransport?actionType={0}";

        public const string TransportGetClassIdsAssigned_GetClassIdsAssigned = "api/Transport/FetchTransport?actionType={0}&param={1}";

        public const string TransportGetAssignedSections_GetAssignedSections = "api/Transport/FetchTransport?actionType={0}&param={1}";

        public const string TransportGetStudentBusReportListOnSectionID_GetStudentBusReportListOnSectionID = "api/Transport/FetchTransport?actionType={0}&param={1}";

        public const string TransportGetStudentListOnRouteID_GetStudentListOnRouteID = "api/Transport/FetchTransport?actionType={0}&param={1}";

        public const string TransportGetStudentBusRateClasswise_GetStudentBusRateClasswise = "api/Transport/FetchTransport?actionType={0}&param={1}";

        public const string TransportGetStudentBusRate_GetStudentBusRate = "api/Transport/FetchTransport?actionType={0}&param={1}";

        public const string TransportGetTransportNameById_GetTransportNameById = "api/Transport/FetchTransport?actionType={0}&param={1}";

        public const string TransportGetTransportList_GetTransportList = "api/Transport/FetchTransport?actionType={0}";


        public const string TransportGetStopListWithLatLong_GetStopListWithLatLong = "api/Transport/FetchTransport?actionType={0}&param={1}";

        public const string TransportDelete_DeleteTransport = "api/transport/{0}";
        public const string TransportDeleteBusStop_DeleteBusStop = "api/transport/busstop/{0}";

        // ClassMaster endpoints
        public const string ClassMaster_GetEducationalDepartments = "api/ClassMaster/class-master?actionType=0";

        public const string ClassMaster_GetSectionsByClassId = "api/ClassMaster/class-master?actionType=1&param={0}";

        public const string ClassMaster_GetClassesBySessionWithDepartment = "api/ClassMaster/class-master?actionType=2&param={0}";

        public const string ClassMaster_AddClass = "api/ClassMaster/add-info?actionType=0";

        public const string ClassMaster_AddSection = "api/ClassMaster/add-info?actionType=1";

        public const string ClassMaster_UpgradeClassesSubjectsSections = "api/ClassMaster/add-info?actionType=2";

        public const string ClassMaster_UpdateClass = "api/ClassMaster/update-by-action?actionType=0";

        public const string ClassMaster_UpdateSection = "api/ClassMaster/update-by-action?actionType=1";

        public const string ClassMaster_DeleteClass = "api/ClassMaster/delete?actionType=0&id={0}";

        public const string ClassMaster_DeleteSection = "api/ClassMaster/delete?actionType=1&id={0}";

        // Attendance endpoints
        public const string Attendance_AddAttendance = "api/Attendence/add?actionType=0";

        public const string Attendance_AddAttendanceList = "api/Attendance/add?actionType=1";


        public const string Attendance_UpdateTodaysAttendance = "api/Attendence/update?actionType=0";


        public const string Attendance_GetTodaysAttendance = "api/Attendence/attendance?actionType=0&param={0},{1}";


        public const string Attendance_GetEditAttendance = "api/Attendence/attendance?actionType=1&param=\"{0}\",\"{1}\"";

        public const string Attendance_GetAbsentList = "api/Attendence/attendance?actionType=2&&param={0},{1}";

        public const string Attendance_GetAttendanceListOnDates = "api/Attendence/attendance?actionType=3&param={0},{1},{2}";


        public const string Attendance_CheckAttendanceAddedorNot =
     "api/Attendence/attendance?actionType=4&param={0},{1},\"{2}\"";


        public const string Attendance_GetMonthlyAttendance =
     "api/Attendence/attendance?actionType=5&param=\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\"";



        public const string Attendance_GetAttendanceListOnDatesWithClassId = "api/Attendence/attendance?actionType=6&param={0}";

        public const string Attendance_GetPendingAttendanceStudents = "api/Attendence/attendance?actionType=7&param={0}";
        public const string Attendance_GetAttendanceReport = "api/Attendence/attendance?actionType=9&param={0},{1},{2},{3}";

        
        // ClassTest endpoints
        public const string ClassTest_AddClassTestMaxMarks = "api/ClassTest/class-test?actionType=0";
        public const string ClassTest_AddClassTestMarks = "api/ClassTest/class-test?actionType=1";
        public const string ClassTest_UpdateClassTestMaxMarks = "api/classtest/updatemaxmarks";
        public const string ClassTest_EditUpdateClassTestMarks = "api/classtest/editupdatemarks";
        public const string ClassTest_GetSubjectForMaxMarks = "api/classtest/getsubjectformaxmarks/{0}";
        public const string ClassTest_GetMaxMarks = "api/ClassTest/class-test?actionType=1&param={0}";
        public const string ClassTest_GetStudents = "api/classtest/getstudents/{0}";
        public const string ClassTest_GetStudentsWithMarks = "api/classtest/getstudentswithmarks/{0}";
        public const string ClassTest_ViewDateWiseResult = "api/classtest/viewdatewiseresult/{0}";
        public const string ClassTest_ClassTestReport = "api/classtest/report/{0}";
        public const string ClassTest_ViewDateWiseResultForAllSubjects = "api/classtest/viewdatewiseresultforallsubjects/{0}";
        public const string ClassTest_ViewDateWiseResultForTotalMarks = "api/classtest/viewdatewiseresultfortotalmarks/{0}";
        public const string ClassTest_ViewDateWiseTotalMMandObtMarks = "api/classtest/viewdatewisetotalmmandobtmarks/{0}";
        public const string ClassTest_GetMissingClassTestMarks = "api/ClassTest/class-test?actionType=9&param={0}";

        // Subject endpoints
        public const string Subject_InsertNewSubject = "api/Subject/subject-info?actionType=0";
        public const string Subject_InsertNewOptionalSubject = "api/Subject/subject-info?actionType=1";
        public const string Subject_InsertNewSubSubject = "api/Subject/subject-info?actionType=2";
        public const string Subject_UpdateSubject = "api/Subject/subject-info?actionType=0";
        public const string Subject_UpdateOptionalSubject = "api/Subject/subject-info?actionType=1";
        public const string Subject_UpdateSubSubject = "api/Subject/subject-info?actionType=2";
        public const string Subject_GetSubjectsByClassId = "api/Subject/subject-info?actionType=0&param={0}";
        public const string Subject_GetOptionalSubjectsByClassId = "api/Subject/subject-info?actionType=1&param={0}";
        public const string Subject_GetSubSubjectsBySubjectId = "api/Subject/subject-info?actionType=2&param={0}";
        public const string Subject_DeleteSubject = "api/Subject/subject-info?actionType=0&param={0}";
        public const string Subject_DeleteOptionalSubject = "api/Subject/subject-info?actionType=1&param={0}";
        public const string Subject_DeleteSubSubject = "api/Subject/subject-info?actionType=2&param={0}";

        // Employee API endpointsapi/Employee/manage?actionType=0
        public const string Employee_AddNewEmployee = "api/Employee/manage?actionType=0";
        public const string Employee_UpdateEmployee = "api/Employee/manage?actionType=0";
        public const string Employee_UpdateMultipleEmployee = "api/Employee/manage?actionType=1";
        public const string Employee_UpdateEmployeeMonthlyAttendance = "api/Employee/manage?actionType=2";
        public const string Employee_WithdrawEmployee = "api/Employee/manage?actionType=3";
        public const string Employee_RejoinEmployee = "api/Employee/manage?actionType=4";
        public const string Employee_UpdateEmployeeDetailField = "api/Employee/manage?actionType=5";
        
        public const string Employee_GetEmployeeByCode = "api/Employee/fetch?actionType=0&param={0}";
        public const string Employee_GetAllEmployeesByYear = "api/Employee/fetch?actionType=1&param={0}";
        public const string Employee_GetEmployeesBySubDept = "api/Employee/fetch?actionType=2&param={0}";
        public const string Employee_GetEmployeesByDesignation = "api/Employee/fetch?actionType=3&param={0}";
        public const string Employee_GetEmployeesByStatus = "api/Employee/fetch?actionType=4&param={0}";
        public const string Employee_GetEmployeesByName = "api/Employee/fetch?actionType=5&param={0}";
        public const string Employee_GetEmployeesByField = "api/Employee/fetch?actionType=6&param={0}";
        public const string Employee_GetEmployeesByMobile = "api/Employee/fetch?actionType=7&param={0}";
        public const string Employee_GetEmployeesByParentage = "api/Employee/fetch?actionType=8&param={0}";
        public const string Employee_GetEmployeesByAddress = "api/Employee/fetch?actionType=9&param={0}";
        public const string Employee_GetEmployeesForAttendanceUpdate = "api/Employee/fetch?actionType=10&param={0}";
        public const string Employee_GetEmployeeTableFields = "api/Employee/fetch?actionType=11";
        public const string Employee_GetNextEmployeeCode = "api/Employee/fetch?actionType=12";

        // Salary API endpoints
        public const string Salary_SalaryReleaseOnDepartments = "api/Salary/salary?actionType=0";
        public const string Salary_SalaryReleaseOnEmployeeCode = "api/Salary/salary?actionType=1";
        public const string Salary_UpdateSalaryDetails = "api/Salary/salary?actionType=2";
        public const string Salary_GetEmployeeSalaryToEdit = "api/Salary/salary?actionType=3";
        public const string Salary_UpdateSalaryDetailsOnField = "api/Salary/salary?actionType=4";
        public const string Salary_DeleteSalaryOnEmployeeCode = "api/Salary/salary?actionType=5";
        public const string Salary_DeleteSalaryOnDepartments = "api/Salary/salary?actionType=6";
        public const string Salary_GetDemoSalaryOnDepartments = "api/Salary/salary?actionType=7";
        public const string Salary_AddNewLoan = "api/Salary/salary?actionType=8";
        
        public const string Salary_GetEmployeeSalaryToEditOnEDID = "api/Salary/fetch-salary-data?actionType=0&param={0}";
        public const string Salary_GetEmployeeSalaryToEditOnECode = "api/Salary/fetch-salary-data?actionType=1&param={0}";
        public const string Salary_GetEmployeeSalaryToEditOnFieldName = "api/Salary/fetch-salary-data?actionType=2&param={0}";
        public const string Salary_GetSalaryDataOnMonthFromSalaryOnDeparts = "api/Salary/fetch-salary-data?actionType=3&param={0}";
        public const string Salary_GetCalculatedGrossNetEtc = "api/Salary/fetch-salary-data?actionType=4&param={0}";
        public const string Salary_GetCalculatedGrossNetEtcOnEDID = "api/Salary/fetch-salary-data?actionType=5&param={0}";
        public const string Salary_GetSalaryDataOnYearFromSalaryOnECode = "api/Salary/fetch-salary-data?actionType=6&param={0}";
        public const string Salary_GetLoanDefaultList = "api/Salary/fetch-salary-data?actionType=7";
        public const string Salary_SalaryPaymentAccountStatementOnEcodeAndDates = "api/Salary/fetch-salary-data?actionType=8&param={0}";
        public const string Salary_GetAvailableNetSalaryOnMonthFromSalaryAndSalaryPaymentOnDeparts = "api/Salary/fetch-salary-data?actionType=9&param={0}";
        public const string Salary_GetBankSalarySlipOnMonthFromSalaryAndSalaryPaymentOnDeparts = "api/Salary/fetch-salary-data?actionType=10&param={0}";

        // Designation endpoints
        public const string Designation_GetDesignations = "api/Designation/fetch-designation-data?actionType=0";
        public const string Designation_GetDesignationById = "api/Designation/fetch-designation-data?actionType=1&param={0}";
        public const string Designation_AddDesignation = "api/Designation/add-designation?actionType=0";
        public const string Designation_UpdateDesignation = "api/Designation/update-designation?actionType=1";
        public const string Designation_DeleteDesignation = "api/Designation/delete-designation?actionType=0&Id={0}";

        // Department endpoints     
        public const string Department_AddDepartment = "api/Departments/department-add?actionType=0";
        public const string Department_UpdateDepartment = "api/Departments/update-department?actionType=0";
        public const string Department_GetDepartments = "api/Departments/get-department-data?actionType=0";
        public const string Department_GetDepartmentById = "api/Departments/get-department-data?actionType=1&id={0}";
        public const string Department_DeleteDepartment = "api/Departments/delete-department?actionType=0&id={0}";

        // empstatus endpoints 
        public const string Employee_GetEmployeeStatus = "/api/EmpStatus/get-employee-status?actionType=0";

        // qualification endpoints
        public const string Employee_GetQualifications = "/api/Qualifications/get-qualifications?actionType=0";

        public const string Employee_GetQualificationById = "/api/Qualifications/get-qualifications?actionType=1&param={0}";
       
        public const string Employee_AddQualification = "/api/Qualifications/add-qualification?actionType=0";

        public const string Employee_UpdateQualification = "/api/Qualifications/update-qualification?actionType=0";

        public const string Employee_DeleteQualification = "/api/Qualifications/delete-qualification?actionType=0&id={0}";

        //subjects endpoints
        public const string Employee_GetEmployeeSubjects = "/api/EmployeeSubjects/get?actionType=1";

        public const string Employee_GetEmployeeSubjectById = "/api/EmployeeSubjects/get?actionType=0&param={0}";

        // TeacherLog endpoints
        public const string TeacherLog_AddTeacherLogDataOnSectionIDandDate = "api/TeacherLog/post?actionType=1";
        public const string TeacherLog_AddTeacherLogForNewTiny = "api/TeacherLog/post?actionType=2";
        public const string TeacherLog_AddTeacherPerformance = "api/TeacherLog/post?actionType=3";
        public const string TeacherLog_UpdateTeacherLog = "api/TeacherLog/update?actionType=0";
        public const string TeacherLog_GetTeacherLogDataOnSectionIDandDate = "api/TeacherLog/fetch?actionType=0&param={0}";
        public const string TeacherLog_GetTeacherLogDataOnECodeandDate = "api/TeacherLog/fetch?actionType=1&param={0}";
        public const string TeacherLog_GetSubjectList = "api/TeacherLog/fetch?actionType=2&param={0}";
        public const string TeacherLog_GetSubSubjectList = "api/TeacherLog/fetch?actionType=3&param={0}";
        public const string TeacherLog_GetOptSubjectList = "api/TeacherLog/fetch?actionType=4&param={0}";
        public const string TeacherLog_GetTeacherLogOnDateList = "api/TeacherLog/fetch?actionType=5&param={0}";
        public const string TeacherLog_GetTeacherLogOnDateAndCodeList = "api/TeacherLog/fetch?actionType=6&param={0}";
        public const string TeacherLog_GetTeachersLog = "api/TeacherLog/fetch?actionType=7&param={0}";
        public const string TeacherLog_GetTeachersLogfromTT = "api/TeacherLog/fetch?actionType=8&param={0}";
        public const string TeacherLog_GetTeachersLogFromTTEmpty = "api/TeacherLog/fetch?actionType=9&param={0}";
        public const string TeacherLog_GetTeacherPerformance = "api/TeacherLog/fetch?actionType=10";
        public const string TeacherLog_GetTeachersLogRangeWise = "api/TeacherLog/fetch?actionType=11&param={0}";
        public const string TeacherLog_DeleteTeacherLogOnLogID = "api/TeacherLog/delete?actionType=0";
        //  marks endpoint
        public const string Marks_AddMarks = "api/Marks/save?actionType=1";
        public const string Marks_UpdateMarks = "api/Marks/update?actionType=2";
        public const string Marks_DeleteMarks = "api/Marks/delete?actionType=1&marksId={0}";
        public const string Marks_GetMarksWithNames = "api/Marks/fetch?actionType=1&param={0}";

        // maxmarks endpoint
        public const string MaxMarks_Add = "api/MaxMarks/post?actionType=1";
        public const string MaxMarks_Update = "api/MaxMarks/update?actionType=1";
        public const string MaxMarks_Delete = "api/MaxMarks/delete?actionType=1&param={0}";
        public const string MaxMarks_GetAllBySession = "api/MaxMarks/fetch?actionType=2&param={0}";
        public const string MaxMarks_GetByClassAndSubject = "api/MaxMarks/fetch?actionType=1&param={0}";

        //OptionalMarks endpoint
        public const string OptionalMarks_Add = "api/OptionalMarks/post?actionType=1";
        public const string OptionalMarks_Update = "api/OptionalMarks/update?actionType=1";
        public const string OptionalMarks_Delete = "api/OptionalMarks/delete?actionType=1&param={0}";
        public const string OptionalMarks_GetByClassSectionSubjectUnit = "api/OptionalMarks/fetch?actionType=1&param={0}";

        // OptionalMaxMarks endpoints
        public const string OptionalMaxMarks_Add = "api/OptionalMaxMarks/post?actionType=1";
        public const string OptionalMaxMarks_Update = "api/OptionalMaxMarks/update?actionType=1";
        public const string OptionalMaxMarks_GetByFilter = "api/OptionalMaxMarks/fetch?actionType=1&param={0}";

        // units endpoints 

        public const string Unit_Add = "api/Units/post?actionType=1";
        public const string Unit_GetAll = "api/Units/fetch?actionType=1";
        public const string Unit_GetById = "api/Units/fetch?actionType=2";
        public const string Unit_Update = "api/Units/update?actionType=1";

        // marks sheet endpoint

        public const string MarksSheetSetting_SaveMarksSheetSetting = "api/markssheetsetting/updatemarkssheetsetting";

        //Exam Grades  endpoint

        public const string ExamGrades_Add = "api/ExamGrades/add";
        public const string ExamGrades_Update = "api/ExamGrades/update";
        public const string ExamGrades_Delete = "api/ExamGrades/delete/{0}"; 
        public const string ExamGrades_GetById = "api/ExamGrades/get/{0}";   
        public const string ExamGrades_GetAll = "api/ExamGrades/getall";

        // result endpoint

        public const string ExamGrades_GetOptionalResults = "api/ExamGrades/getoptionalresults";
        public const string ExamGrades_GetStudentResults = "api/ExamGrades/getstudentresults";

        // gazet endpoint

        public const string ExamGrades_GetGazetResults = "api/Gazet/fetch?actionType=1&param={0}";
    }


}
