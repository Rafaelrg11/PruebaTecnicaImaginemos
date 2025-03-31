using PruebaTecnicaImaginemos.Application.Abstraction.Messaging;
using PruebaTecnicaImaginemos.Domain.DTOs.Sale;
using PruebaTecnicaImaginemos.Domain.DTOs.SaleDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Application.Commands.Sale.CreateSale;

public sealed record CreateSaleCommand(Guid idUser) : ICommand<Guid> {}
