using PruebaTecnicaImaginemos.Application.Abstraction.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Application.Commands.SaleDetail.GetAllSaleDetail;

public sealed record GetAllSaleDetailQuery() : IQuery<(List<SaleDetailResponse>, long)> { }
