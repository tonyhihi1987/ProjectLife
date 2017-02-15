using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using ProjectLife.Model;
using System.IO;

namespace ProjectLife.DAL
{    

    public class ProjectLifeDataContext:DbContext
    {

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
            .Entity<Project>()
             .HasOne(s => s.Image)
            .WithMany()
            .HasForeignKey(e => e.ImageId);
            base.OnModelCreating(builder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();
            optionsBuilder.UseSqlServer(config["Data:DefaultConnection:ConnectionString"]);
        }
    }
}