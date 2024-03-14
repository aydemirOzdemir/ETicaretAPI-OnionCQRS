using E_Ticaret.Application.Interfaces.Repositories;
using E_Ticaret.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Ticaret.Persistance.Repositories;

public class WriteRepository<T>:IWriteRepository<T> where T : class,IEntityBase,new()
{

}
