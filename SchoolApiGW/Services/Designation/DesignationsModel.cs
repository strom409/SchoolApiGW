namespace SchoolApiGW.Services.Designation
{
    public class DesignationsModel
    {
        public long? DesignationID { get; set; }
        public string? Designation { get; set; }
        public string? Current_Session { get; set; }
        public Nullable<long> SessionID { get; set; }
    }
}
