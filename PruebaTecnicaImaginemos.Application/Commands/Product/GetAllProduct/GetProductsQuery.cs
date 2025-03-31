using PruebaTecnicaImaginemos.Application.Abstraction.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Application.Commands.Product.GetAllProduct;

public sealed record GetProductsQuery() : IQuery<(List<ProductsResponse>, long)> { }

