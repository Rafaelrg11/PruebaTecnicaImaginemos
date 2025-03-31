using PruebaTecnicaImaginemos.Domain.Product;
using PruebaTecnicaImaginemos.Domain.Sale;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Domain.Interfaces;

public interface ISaleRepository
{
    Task<Sales?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    void Delete(Sales entity);
}
