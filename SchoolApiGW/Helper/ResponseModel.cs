namespace SchoolApiGW.Helper
{
    public class ResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public int Status { get; set; }
        public object ResponseData { get; set; } = null;
        public string Error { get; set; }
    }
}
