using DemoWebAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoWebAPI.Repository
{
    public interface ITestRepo
    {
        Task<List<TestModel>> GetAllData();
        string GetAllDataByName(string name);
        Task<TestModel> GetAllDataById(int id);
        Task<string> InsertAllData(TestModel test);
        Task<string> DeleteDataById(int id);
        Task<string> UpdateData(TestModel testmodel);
        Task<string> UpdateDataPatch(int id, JsonPatchDocument testmodel);
    }
}
