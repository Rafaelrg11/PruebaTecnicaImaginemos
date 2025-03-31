using PruebaTecnicaImaginemos.Application.Abstraction.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Application.Commands.SaleDetail.DeleteSaleDetail;

public sealed record DeleteSaleDetailCommand(Guid id) : ICommand<bool> { }
