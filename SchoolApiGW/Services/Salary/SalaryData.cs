using SchoolApiGW.Services.Employee;

namespace SchoolApiGW.Services.Salary
{
    public class SalaryData
    {
        //   public int ActionType { get; set; }
        public List<EmployeeDetail> EMD { get; set; }
        public List<Salary> Salary { get; set; }
        public Salary Sal { get; set; }

        public string EDID { get; set; }
        public string ECode { get; set; }

    }
}
