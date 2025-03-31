using PruebaTecnicaImaginemos.Domain.Interfaces;
using PruebaTecnicaImaginemos.Domain.Product;
using PruebaTecnicaImaginemos.Domain.Sale;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Infraestructure.Repository.EntityRepository;

internal class SaleRepository : Repository<Sales>, ISaleRepository
{
    public SaleRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
