using E_Ticaret.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace E_Ticaret.Persistance.Context;

public class ETicaretDbContext:DbContext
{
    public ETicaretDbContext()
    {
        
    }
    public ETicaretDbContext(DbContextOptions<ETicaretDbContext> options):base(options)
    {
        
    }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Detail> Details { get; set; }
    public DbSet<Product> Products { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder); 
    }
}
