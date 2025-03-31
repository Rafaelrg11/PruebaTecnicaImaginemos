using Microsoft.EntityFrameworkCore;
using PruebaTecnicaImaginemos.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Infraestructure.Repository;

internal abstract class Repository<T>
where T : Entity
{
    protected readonly ApplicationDbContext DbContext;

    protected Repository(ApplicationDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public async Task<T?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await DbContext
            .Set<T>()
            .FirstOrDefaultAsync(user => user.Id == id, cancellationToken);
    }
    public void Update(T entity)
    {
        DbContext.Set<T>().Update(entity);
    }

    public void Add(T entity)
    {
        DbContext.Add(entity);
    }
    public void Delete(T entity)
    {
        DbContext.Set<T>().Remove(entity);
    }
}
