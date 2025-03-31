using PruebaTecnicaImaginemos.Application.Abstraction.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Application.Commands.SaleDetail.PaginationSaleDetail;

public sealed record PaginationSaleDetailQuery(int skip, int limit) : IQuery<(List<SaleDetailResponse>, long)>
{
}
