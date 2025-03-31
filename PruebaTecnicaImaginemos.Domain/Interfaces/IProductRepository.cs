using PruebaTecnicaImaginemos.Domain.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Domain.Interfaces;

public interface IProductRepository
{
    Task<Products?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    void Delete(Products entity);
}
