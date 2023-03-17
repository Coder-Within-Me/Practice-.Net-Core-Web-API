using Microsoft.AspNetCore.Mvc;

namespace DemoWebAPI.Repository
{
    public class TestRepo : ITestRepo
    {
        public string GetAllData()
        {
            return "Testing API";
        }
        public string GetAllDataByName(string name)
        {
            return "Testing API " + name;
        }
    }
}
