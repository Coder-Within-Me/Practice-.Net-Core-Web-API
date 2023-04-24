using DemoWebAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoWebAPI.Repository
{
    public interface ITestRepo
    {
        Task<(List<TestModel>, int)> GetAllData(int pagenumber, int pageSize);
        string GetAllDataByName(string name);
        Task<TestModel> GetAllDataById(int id);
        Task<TestModel> InsertAllData(TestModel test);
        Task<TestModel> DeleteDataById(int id);
        Task<(TestModel, bool)> UpdateData(TestModel testmodel);
        Task<TestModel> UpdateDataPatch(int id, JsonPatchDocument testmodel);
    }
}
