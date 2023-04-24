using DemoWebAPI.DataLayer;
using DemoWebAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Threading.Tasks;

namespace DemoWebAPI.Repository
{
    public class TestRepo : ITestRepo
    {
        private readonly TestContext _testContext;
        public TestRepo(TestContext context)
        {
            _testContext = context;
        }

        public async Task<(List<TestModel>,int)> GetAllData(int pagenumber = 1, int pageSize = 5)
        {
            //return await _testContext.testdata.Select(x => new TestModel { Id = x.Id, Name = x.Name, Age = x.Age, GenderID = x.GenderID }).ToListAsync();
            var totalrecords = await _testContext.testdata.CountAsync();
            var skipRecords = (pagenumber - 1) * pageSize;
            var records = await _testContext.testdata.Skip(skipRecords).Take(pageSize).ToListAsync();
            return (records, totalrecords);
        }
        public string GetAllDataByName(string name)
        {
            return "Testing API " + name;
        }        
        public async Task<TestModel> GetAllDataById(int id)
        {
            return await _testContext.testdata.Where(x => x.Id == id).Select(x => new TestModel { Id = x.Id, Name = x.Name, Age = x.Age, GenderID = x.GenderID }).FirstOrDefaultAsync();
        }
        public async Task<TestModel> InsertAllData(TestModel test)
        {
            var testData = new TestModel()
            {
                Name = test.Name,
                Age = test.Age,
                GenderID = test.GenderID,
            };
            _testContext.testdata.Add(testData);
            await _testContext.SaveChangesAsync();
            return test;
        }
        
        public async Task<TestModel> DeleteDataById(int id)
        {
            var modeldata = await _testContext.testdata.FindAsync(id);
            if (modeldata != null)
            {
                //var data = await _testContext.testdata.Where(x => x.Id == id).Select(x => new TestModel { Id = x.Id, Name = x.Name, Age = x.Age, Gender = x.Gender}).FirstOrDefaultAsync();
                _testContext.testdata.Remove(modeldata);
                await _testContext.SaveChangesAsync();
                return modeldata;
            }
            else
            {
                return modeldata;
            }
        }
        public async Task<(TestModel,bool)> UpdateData(TestModel testmodel)
        {
            var modeldata = await _testContext.testdata.FindAsync(testmodel.Id); //To check whether data is available or not
            if (modeldata != null)
            {
                modeldata.Id = testmodel.Id;
                modeldata.Name = testmodel.Name;
                modeldata.Age = testmodel.Age;
                modeldata.GenderID = testmodel.GenderID;
                await _testContext.SaveChangesAsync();
                return (modeldata,true);
            }
            else
            {
                return (modeldata,false);
            }
        }

        public async Task<TestModel> UpdateDataPatch(int id, JsonPatchDocument testmodel)
        {
            var modeldata = await _testContext.testdata.FindAsync(id); //To check whether data is available or not
            if (modeldata != null)
            {
                testmodel.ApplyTo(modeldata);
                await _testContext.SaveChangesAsync();
                return modeldata;
            }
            else
            {
                return modeldata;
            }
        }
    }
}
