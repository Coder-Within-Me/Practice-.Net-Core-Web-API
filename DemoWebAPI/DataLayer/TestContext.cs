using DemoWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoWebAPI.DataLayer
{
    public class TestContext : DbContext
    {
        public TestContext(DbContextOptions<TestContext> options) : base(options) { 
        }
        public DbSet<TestModel> testdata { get; set; }
        public DbSet<Authentication> Users { get; set; }
    }
}
