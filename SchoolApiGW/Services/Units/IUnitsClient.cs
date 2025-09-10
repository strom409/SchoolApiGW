using SchoolApiGW.Helper;

namespace SchoolApiGW.Services.Units
{
    public interface IUnitsClient
    {
        Task<ResponseModel> AddUnit(UnitDto unit, string clientId);
        Task<ResponseModel> UpdateUnit(UnitDto unit, string clientId);
        Task<ResponseModel> GetAllUnits(string clientId);
        Task<ResponseModel> GetUnitById(string? param, string clientId);
    }
}
