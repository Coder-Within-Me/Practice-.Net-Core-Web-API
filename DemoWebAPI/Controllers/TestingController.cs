using DemoWebAPI.Models;
using DemoWebAPI.Repository;
using DemoWebAPI.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class TestingController : ControllerBase
    {
        private readonly ITestRepo TestRepoObj;
        public TestingController(ITestRepo TestRepo)
        {
            TestRepoObj = TestRepo;
        }

        //[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("GetData/{pagenumber}/{pagesize}")]
        public async Task<IActionResult> GetData([FromRoute] int pagenumber,[FromRoute] int pagesize)
        {
            var data = await TestRepoObj.GetAllData(pagenumber, pagesize);
            return Ok(data);
        }
        //[HttpGet]
        //public async Task<List<TestModel>> GetAllData()
        //{
        //    return await TestRepoObj.GetAllData();
        //}

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
        public async Task<IActionResult> InsertAllData([FromBody]TestModel test)
        {
            return Ok(await TestRepoObj.InsertAllData(test));
        }
        [HttpDelete("DeleteDataById/{id}")]
        public async Task<IActionResult> DeleteDataById([FromRoute]int id)
        {
            return Ok(await TestRepoObj.DeleteDataById(id));
        }
        [HttpPut("updatedata")]
        public async Task<IActionResult> UpdateData([FromBody] TestModel testmodel)
        {
            var data = await TestRepoObj.UpdateData(testmodel);
            return Ok(data);
        }
        [HttpPatch("updatedataPatch/{id}")]
        public async Task<IActionResult> UpdateDataPatch(int id,[FromBody] JsonPatchDocument testmodel)
        {
            return Ok(await TestRepoObj.UpdateDataPatch(id,testmodel));
        }
    }
}
