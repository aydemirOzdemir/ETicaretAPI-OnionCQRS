using E_Ticaret.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Ticaret.Persistance.Configuration;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        Category category1 = new()
        {
            Id = 1,
            Name = "Elektrik",
            ParentId = 0,
            Priorty = 1,
            IsDeleted=false,
            CreatedDate=DateTime.Now
        };
        Category category2 = new()
        {
            Id = 2,
            Name = "Moda",
            ParentId = 0,
            Priorty = 2,
            IsDeleted = false,
            CreatedDate = DateTime.Now
        };
        Category child1 = new()
        {
            Id = 3,
            Name = "Bilgisayar",
            ParentId = 1,
            Priorty = 1,
            IsDeleted = false,
            CreatedDate = DateTime.Now
        };
        Category child2 = new()
        {
            Id = 4,
            Name = "Kadın",
            ParentId = 2,
            Priorty = 1,
            IsDeleted = false,
            CreatedDate = DateTime.Now
        };
        builder.HasData(category1, category2,child1,child2);
    }
}
