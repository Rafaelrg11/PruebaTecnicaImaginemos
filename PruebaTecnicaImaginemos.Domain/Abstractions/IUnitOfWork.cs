using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Domain.Abstractions;

public interface IUnitOfWork
{
    void Add(Entity entity);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
