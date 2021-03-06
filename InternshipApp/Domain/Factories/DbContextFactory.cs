using Microsoft.EntityFrameworkCore;
using Data.Entities;
using System.Configuration;

namespace Domain.Factories
{
    public static class DbContextFactory
    {
        public static InternshipAppDbContext GetInternshipAppDbContext()
        {
            var options = new DbContextOptionsBuilder()
                .UseSqlServer(ConfigurationManager.ConnectionStrings["InternshipApp"].ConnectionString)
                .Options;

            return new InternshipAppDbContext(options);
        }
    }
}