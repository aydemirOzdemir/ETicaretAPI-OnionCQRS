using E_Ticaret.Application.Interfaces.Repositories;
using E_Ticaret.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Ticaret.Application.UnitOfWorks;

public interface IUnitOfWork:IAsyncDisposable
{
    IReadRepository<T> GetReadRepository<T>() where T : class, IEntityBase,new();
    IWriteRepository<T> GetWriteRepository<T>() where T : class, IEntityBase,new();
    Task<int> SaveAsync();
    int Save();
}
