using SchoolApiGW.Helper;
using SchoolApiGW.Services.Employee;

namespace SchoolApiGW.Services.Salary
{
    public interface ISalaryClient
    {
        Task<ResponseModel> SalaryReleaseOnDepartments(Salary salary, string clientId);
        Task<ResponseModel> SalaryReleaseOnEmployeeCode(Salary salary, string clientId);
        Task<ResponseModel> UpdateSalaryDetails(EmployeeDetail employeeDetail, string clientId);
        Task<ResponseModel> UpdateSalaryDetailsOnField(List<EmployeeDetail> employeeDetails, string clientId);
        Task<ResponseModel> DeleteSalaryOnEmployeeCode(string sal, string clientId);
        Task<ResponseModel> DeleteSalaryOnDepartments(List<Salary> salary, string clientId);
        Task<ResponseModel> GetDemoSalaryOnDepartments(Salary salary, string clientId);
        Task<ResponseModel> AddNewLoan(string salary, string clientId);
        
        // Fetch methods
        Task<ResponseModel> GetEmployeeSalaryToEdit(string eCode, string clientId);
        Task<ResponseModel> GetEmployeeSalaryToEditOnEDID(string param, string clientId);
        Task<ResponseModel> GetEmployeeSalaryToEditOnECode(string param, string clientId);
        Task<ResponseModel> GetEmployeeSalaryToEditOnFieldName(string param, string clientId);
        Task<ResponseModel> GetSalaryDataOnMonthFromSalaryOnDeparts(string param, string clientId);
        Task<ResponseModel> GetCalculatedGrossNetEtc(string param, string clientId);
        Task<ResponseModel> GetCalculatedGrossNetEtcOnEDID(string param, string clientId);
        Task<ResponseModel> GetSalaryDataOnYearFromSalaryOnECode(string param, string clientId);
        Task<ResponseModel> GetLoanDefaultList(string clientId);
        Task<ResponseModel> SalaryPaymentAccountStatementOnEcodeAndDates(string param, string clientId);
        Task<ResponseModel> GetAvailableNetSalaryOnMonthFromSalaryAndSalaryPaymentOnDeparts(string param, string clientId);
        Task<ResponseModel> GetBankSalarySlipOnMonthFromSalaryAndSalaryPaymentOnDeparts(string param, string clientId);
    }
}
