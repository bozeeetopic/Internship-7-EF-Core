using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Data.Entities.Models;
using Data.Seeds;
using System.IO;
using System.Linq;

namespace PaymentManager.Data.Entities
{
    public class InternshipAppDbContext : DbContext
    {
        public InternshipAppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Member> Members { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder
                .Entity<Post>()
                .HasOne(a => a.Author)
                .WithMany(p => p.Posts)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder
                .Entity<Comment>()
                .HasOne(a => a.Author)
                .WithMany(p => p.Comments)
                .OnDelete(DeleteBehavior.NoAction);

            DBSeed.Execute(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
    }

    public class PaymentManagerContextFactory : IDesignTimeDbContextFactory<InternshipAppDbContext>
    {
        public InternshipAppDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddXmlFile("App.config")
                .Build();

            configuration
                .Providers
                .First()
                .TryGet("connectionStrings:add:InternshipApp:connectionString", out var connectionString);

            var options = new DbContextOptionsBuilder<InternshipAppDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            return new InternshipAppDbContext(options);
        }
    }
}
