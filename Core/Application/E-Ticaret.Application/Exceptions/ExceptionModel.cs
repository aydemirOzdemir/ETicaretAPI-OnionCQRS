﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Ticaret.Application.Exceptions;

public class ExceptionModel:ErrorStatusCode
{
    public IEnumerable<string> Errors { get; set; }
    public override string ToString()=> JsonConvert.SerializeObject(Errors);
    
}
