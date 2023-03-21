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

        public async Task<List<TestModel>> GetAllData()
        {
            return await _testContext.testdata.Select(x => new TestModel { Id = x.Id, Name = x.Name}).ToListAsync();
        }
        public string GetAllDataByName(string name)
        {
            return "Testing API " + name;
        }        
        public async Task<TestModel> GetAllDataById(int id)
        {
            return await _testContext.testdata.Where(x => x.Id == id).Select(x => new TestModel { Id = x.Id, Name = x.Name }).FirstOrDefaultAsync();
        }
        public async Task<string> InsertAllData(TestModel test)
        {
            var testData = new TestModel()
            {
                Name = test.Name,
            };
            try
            {
                _testContext.testdata.Add(testData);
                await _testContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return "Data not Inserted. Error : " + ex;
            }
            return "Data inserted Successfully...!";                
        }
        
        public async Task<string> DeleteDataById(int id)
        {
            try
            {
                var modeldata = await _testContext.testdata.FindAsync(id);
                if (modeldata != null)
                {
                    var data = await _testContext.testdata.Where(x => x.Id == id).Select(x => new TestModel { Id = x.Id, Name = x.Name }).FirstOrDefaultAsync();
                    _testContext.testdata.Remove(data);
                    await _testContext.SaveChangesAsync();
                }
                else
                {
                    return "Please enter proper ID. Data is not available for this ID.";
                }
                
            }
            catch (Exception ex)
            {
                return "Data not Deleted. Error : " + ex;
            }
            return "Data deleted Successfully...!";
        }
        public async Task<string> UpdateData(TestModel testmodel)
        {
            try
            {
                var modeldata = await _testContext.testdata.FindAsync(testmodel.Id); //To check whether data is available or not
                if (modeldata != null)
                {
                    modeldata.Name = testmodel.Name;
                    modeldata.Id = testmodel.Id;
                    await _testContext.SaveChangesAsync();
                }
                else
                {
                    return "Please enter proper ID. Data is not available for this ID.";
                }

                // Alternate way
                //_testContext.testdata.Update(testmodel);
                //await _testContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                return "Data not Updated. Error : " + ex;
            }
            return "Data updated Successfully...!";
        }
        
        public async Task<string> UpdateDataPatch(int id,JsonPatchDocument testmodel)
        {
            try
            {
                var modeldata = await _testContext.testdata.FindAsync(id); //To check whether data is available or not
                if (modeldata != null)
                {
                    testmodel.ApplyTo(modeldata);
                    await _testContext.SaveChangesAsync();
                }
                else
                {
                    return "Please enter proper ID. Data is not available for this ID.";
                }
            }
            catch (Exception ex)
            {
                return "Data not Updated. Error : " + ex;
            }
            return "Data updated Successfully...!";
        }
    }
}
