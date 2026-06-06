using WebApp.Models;

namespace WebApp.Context;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<Example> Examples { get; set; }
    public DbSet<ExampleModel> ExampleModels { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Example>().HasData(
            new ExampleModel { ExampleId = 1, Name = "Example 1"},
            new ExampleModel { ExampleId = 2, Name = "Example 2" }
            );

        modelBuilder.Entity<ExampleModel>().HasData(
            new  ExampleModel { Id = 3, Name = "Example 3", ExampleId = 1},
            new ExampleModel { Id = 4, Name = "Example 4",   ExampleId = 1 }
            );
    }
}