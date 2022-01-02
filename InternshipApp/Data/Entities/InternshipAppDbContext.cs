using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Data.Entities.Models;
using Data.Seeds;
using System.IO;
using System.Linq;

namespace Data.Entities
{
    public class InternshipAppDbContext : DbContext
    {
        public InternshipAppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Reaction> Reactions { get; set; }
        public DbSet<Perceive> Perceptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder
                .Entity<Post>()
                .HasOne(a => a.Author)
                .WithMany(p => p.Posts)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder
                .Entity<Reaction>()
                .HasOne(a => a.Reactor)
                .WithMany(p => p.Reactions)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder
                .Entity<Reaction>()
                .HasOne(a => a.Post)
                .WithMany(p => p.Reactions)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder
                .Entity<Comment>()
                .HasOne(a => a.Resource)
                .WithMany(p => p.Comments)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder
               .Entity<Comment>()
               .HasOne(a => a.ParentComment)
               .WithMany(p => p.Comments)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder
                .Entity<Perceive>()
                .HasOne(a => a.Perceiver)
                .WithMany(p => p.Perceptions)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder
                .Entity<Perceive>()
                .HasOne(a => a.Resource)
                .WithMany(p => p.Perceptions)
                .OnDelete(DeleteBehavior.NoAction);

            /*modelBuilder.Entity<Comment>().ToTable("Comments");
            modelBuilder.Entity<Resource>().ToTable("Resources");*/

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
