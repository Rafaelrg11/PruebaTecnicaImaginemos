using PruebaTecnicaImaginemos.Domain.Interfaces;
using PruebaTecnicaImaginemos.Domain.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Infraestructure.Repository.EntityRepository;

internal class ProductRepository : Repository<Products>, IProductRepository
{
    public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
