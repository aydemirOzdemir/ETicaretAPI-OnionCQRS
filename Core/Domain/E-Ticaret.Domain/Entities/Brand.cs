﻿using E_Ticaret.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Ticaret.Domain.Entities;

public class Brand:EntityBase
{
    public Brand()
    {
        
    }
    public Brand(string name)
    {
        Name = name;
    }
    public string Name { get; set; }
}
