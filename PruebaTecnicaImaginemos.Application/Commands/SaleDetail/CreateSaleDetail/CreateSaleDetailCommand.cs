using MediatR;
using PruebaTecnicaImaginemos.Application.Abstraction.Messaging;
using PruebaTecnicaImaginemos.Domain.DTOs.SaleDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Application.Commands.SaleDetail.CreateSaleDetail;

public sealed record CreateDetailSaleCommand(Guid product, Guid idSale, int amount, int unitPrice) : ICommand<Guid> { }