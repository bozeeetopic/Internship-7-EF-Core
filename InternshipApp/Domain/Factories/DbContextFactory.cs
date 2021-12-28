using Microsoft.EntityFrameworkCore;
using Data.Entities;
using System.Configuration;

namespace PaymentManager.Domain.Factories
{
    public static class DbContextFactory
    {
        public static InternshipAppDbContext GetPaymentManagerDbContext()
        {
            var options = new DbContextOptionsBuilder()
                .UseSqlServer(ConfigurationManager.ConnectionStrings["InternshipApp"].ConnectionString)
                .Options;

            return new InternshipAppDbContext(options);
        }
    }
}