using PruebaTecnicaImaginemos.Application.Abstraction.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Application.Commands.Product.Pagination;

public sealed record ProductPaginationQuery(int skip, int limit) : IQuery<(List<ProductsResponse>, long)>
{
}
