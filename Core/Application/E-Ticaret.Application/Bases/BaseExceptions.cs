using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Ticaret.Application.Bases;

public class BaseExceptions:ApplicationException
{
    public BaseExceptions()
    {
        
    }
    public BaseExceptions(string message):base(message) { }
   
}
