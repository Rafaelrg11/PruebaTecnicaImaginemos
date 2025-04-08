using PruebaTecnicaImaginemos.Application.Abstraction.Messaging;
using PruebaTecnicaImaginemos.Application.Responses;
using PruebaTecnicaImaginemos.Domain.DTOs.SaleDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Application.Commands.SaleDetail.GetSaleDetail;

public sealed record GetSaleDetailQuery(Guid id) : IQuery<SailDetailResponse2> { }
