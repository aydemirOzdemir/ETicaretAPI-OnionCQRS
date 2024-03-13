﻿using E_Ticaret.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Ticaret.Domain.Entities;

public class Category:EntityBase
{
    public Category()
    {
        
    }
    public Category(int parentId, string name, int priorty)
    {
        ParentId = parentId;
        Name = name;
        Priorty = priorty;
    }
    public required int ParentId { get; set; }
    public required int Priorty { get; set; }
    public required string Name { get; set; }
    public virtual ICollection<Detail> Details { get; set;}
    public virtual ICollection<Product> Products { get; set;}
}