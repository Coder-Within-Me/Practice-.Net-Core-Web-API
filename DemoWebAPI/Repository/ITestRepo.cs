namespace DemoWebAPI.Repository
{
    public interface ITestRepo
    {
        string GetAllData();
        string GetAllDataByName(string name);
    }
}
