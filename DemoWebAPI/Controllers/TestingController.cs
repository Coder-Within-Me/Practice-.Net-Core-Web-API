using DemoWebAPI.Models;
using DemoWebAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestingController : ControllerBase
    {
        private readonly ITestRepo TestRepoObj;
        public TestingController(){

            TestRepoObj = new TestRepo();
        }
        [Route("GetData")]
        [HttpGet]
        public string GetData()
        {
            return TestRepoObj.GetAllData();
        }
        [HttpPost]
        public string GetDataByName(TestModel test)
        {
            return TestRepoObj.GetAllDataByName(test.Name);
        }
    }
}
