﻿using E_Ticaret.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Ticaret.Domain.Entities;

public class Product:EntityBase
{
    public required string  Title { get; set; }
    public required string  Description { get; set; }
    public required int  BrandId { get; set; }
    public required decimal  Price { get; set; }
    public required decimal  Discount { get; set; }
    public virtual Brand  Brand { get; set; }
    public virtual ICollection<Category>  Categories { get; set; }
}