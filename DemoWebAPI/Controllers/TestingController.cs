using DemoWebAPI.Models;
using DemoWebAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestingController : ControllerBase
    {
        private readonly ITestRepo TestRepoObj;
        public TestingController(ITestRepo TestRepo)
        {
            TestRepoObj = TestRepo;
        }

        [HttpGet("GetData")]
        public async Task<IActionResult> GetData()
        {
            var data = await TestRepoObj.GetAllData();
            return Ok(data);
        }
        [HttpGet]
        public async Task<List<TestModel>> GetAllData()
        {
            return await TestRepoObj.GetAllData();
        }

        [HttpGet("GetAllDataById/{id}")]
        public async Task<IActionResult> GetAllDataById([FromRoute] int id)
        {
            var data = await TestRepoObj.GetAllDataById(id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }
        [HttpPost("InsertAllData")]
        public async Task<string> InsertAllData([FromBody]TestModel test)
        {
             return await TestRepoObj.InsertAllData(test);
        }
        [HttpDelete("DeleteDataById/{id}")]
        public async Task<string> DeleteDataById([FromRoute]int id)
        {
            return await TestRepoObj.DeleteDataById(id);
        }
        [HttpPut("updatedata")]
        public async Task<string> UpdateData([FromBody] TestModel testmodel)
        {
            return await TestRepoObj.UpdateData(testmodel);
        }
    }
}
