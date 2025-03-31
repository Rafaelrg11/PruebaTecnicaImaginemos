using PruebaTecnicaImaginemos.Domain.Product;
using PruebaTecnicaImaginemos.Domain.sale_detail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Domain.Interfaces;

public interface ISaleDetailRepository
{
    Task<DetailSale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    void Delete(DetailSale entity);
}
