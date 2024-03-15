using E_Ticaret.Application.Interfaces.Repositories;
using E_Ticaret.Application.UnitOfWorks;
using E_Ticaret.Persistance.Context;
using E_Ticaret.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Ticaret.Persistance.UnitOfWorks;

public class UnitOfWork : IUnitOfWork
{
    private readonly ETicaretDbContext context;

    public UnitOfWork(ETicaretDbContext context)
    {
        this.context = context;
    }
    public async ValueTask DisposeAsync()=>await context.DisposeAsync();

    public int Save() => context.SaveChanges();
 

    public async Task<int> SaveAsync()=>await context.SaveChangesAsync();

    IReadRepository<T> IUnitOfWork.GetReadRepository<T>() => new ReadRepository<T>(context);

    IWriteRepository<T> IUnitOfWork.GetWriteRepository<T>()=>new WriteRepository<T>(context);
}
