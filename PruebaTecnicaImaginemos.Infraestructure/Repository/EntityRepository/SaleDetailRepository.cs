using PruebaTecnicaImaginemos.Domain.Interfaces;
using PruebaTecnicaImaginemos.Domain.Product;
using PruebaTecnicaImaginemos.Domain.sale_detail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Infraestructure.Repository.EntityRepository;

internal class SaleDetailRepository : Repository<DetailSale>, ISaleDetailRepository
{
    public SaleDetailRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
