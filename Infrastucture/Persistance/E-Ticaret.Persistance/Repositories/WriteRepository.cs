using E_Ticaret.Application.Interfaces.Repositories;
using E_Ticaret.Domain.Common;
using E_Ticaret.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Ticaret.Persistance.Repositories;

public class WriteRepository<T> : IWriteRepository<T> where T : class, IEntityBase, new()
{
    private readonly ETicaretDbContext context;
    private DbSet<T> Table { get => context.Set<T>(); }

    public WriteRepository(ETicaretDbContext context)
    {
        this.context = context;

    }
    public async Task AddAsync(T entity)
    {
        await Table.AddAsync(entity);
    }

    public async Task AddRangeAsync(IList<T> entities)
    {
        await Table.AddRangeAsync(entities);
    }

    public async Task<T> DeleteAsync(T entity)
    {
        await Task.Run(() => Table.Remove(entity));
        return entity;
    }



    public async Task<T> UpdateAsync(T entity)
    {
        await Task.Run(()=>Table.Update(entity));
        return entity;
    }
}
