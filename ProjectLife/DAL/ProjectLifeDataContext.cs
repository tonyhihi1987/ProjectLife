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
            builder.Entity<Project>()
            .HasOne(p => p.Image)
            .WithOne();

            builder.Entity<Image>()
           .HasOne(p => p.Project)
           .WithOne();




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