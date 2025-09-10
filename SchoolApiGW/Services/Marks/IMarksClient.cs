﻿using SchoolApiGW.Helper;

namespace SchoolApiGW.Services.Marks
{
    public interface IMarksClient
    {
        Task<ResponseModel> AddMarks(MarksDto marksDto, string clientId);
        Task<ResponseModel> UpdateMarks(MarksDto dto, string clientId);
        Task<ResponseModel> GetMarksWithNames(string param, string clientId);
        Task<ResponseModel> DeleteMarks(string marksId, string clientId);
    }
}
