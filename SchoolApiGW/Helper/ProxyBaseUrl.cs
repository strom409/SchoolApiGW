namespace SchoolApiGW.Helper
{
    public class ProxyBaseUrl
    {
        
        private readonly IConfiguration _configuration;
        private protected readonly string _key;
        private readonly string _student_svc_key = "StudentAPI";
        private readonly string _user_svc_key = "UserAPI";
        private readonly string _transport_svc_key = "TransportAPI";
        private readonly string _Attendence_svc_key = "AttendenceAPI";
        private readonly string _classTest_svc_key = "classTestAPI";
        private readonly string _subject_svc_key = "subjectAPI";
        private readonly string _employee_svc_key = "EmployeeAPI";
        private readonly string _Examination_svc_key = "ExaminationAPI";
        private readonly string _teacherlog_svc_key = "TeacherLogAPI";
        private readonly string _feemanagement_svc_key = "FeeManagementAPI";


        public string student_Universal_API_Host
        {
            get { return _configuration.GetValue<string>(_student_svc_key); }
        }

        public string user_Universal_API_Host
        {
            get { return _configuration.GetValue<string>(_user_svc_key); }
        }

        public string transport_Universal_API_Host
        {
            get { return _configuration.GetValue<string>(_transport_svc_key); }
        }

        public string Attendence_Universal_API_Host
        {
            get { return _configuration.GetValue<string>(_Attendence_svc_key); }
        }
        public string classTest_Universal_API_Host
        {
            get { return _configuration.GetValue<string>(_classTest_svc_key); }
        }

        public string subject_Universal_API_Host
        {
            get { return _configuration.GetValue<string>(_subject_svc_key); }
        }

        public string employee_Universal_API_Host
        {
            get { return _configuration.GetValue<string>(_employee_svc_key); }
        }

        public string Examination_Universal_API_Host
        {
            get { return _configuration.GetValue<string>(_Examination_svc_key); }
        }

        public string teacherlog_Universal_API_Host
        {
            get { return _configuration.GetValue<string>(_teacherlog_svc_key); }
        }
        public string FeeManagement_Universal_API_Host  
        {
            get { return _configuration.GetValue<string>(_feemanagement_svc_key); }
        }

        public ProxyBaseUrl(IConfiguration configuration)
        {
            _configuration = configuration;
        }
    }
}
