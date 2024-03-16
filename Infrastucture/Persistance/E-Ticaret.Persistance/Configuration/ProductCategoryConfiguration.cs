using E_Ticaret.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Ticaret.Persistance.Configuration;

public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
{
    public void Configure(EntityTypeBuilder<ProductCategory> builder)
    {
        builder.HasKey(x=>new{x.ProductId,x.CategoryId});
        builder.HasOne(p => p.Product).WithMany(p => p.Categories).HasForeignKey(p=>p.ProductId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(p=>p.Category).WithMany(p => p.Products).HasForeignKey(c=>c.CategoryId).OnDelete(DeleteBehavior.Cascade);
    }
}
