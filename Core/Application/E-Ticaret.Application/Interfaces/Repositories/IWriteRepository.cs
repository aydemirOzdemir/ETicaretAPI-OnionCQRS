using E_Ticaret.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Ticaret.Application.Interfaces.Repositories;

public interface IWriteRepository<T> where T : class,IEntityBase,new()
{
}
