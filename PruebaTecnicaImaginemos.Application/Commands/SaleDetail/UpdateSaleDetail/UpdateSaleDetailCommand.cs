using PruebaTecnicaImaginemos.Application.Abstraction.Messaging;
using PruebaTecnicaImaginemos.Application.Responses;
using PruebaTecnicaImaginemos.Domain.DTOs.SaleDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PruebaTecnicaImaginemos.Application.Commands.SaleDetail.UpdateSaleDetail;

public sealed record UpdateSaleDetailCommand(SaleDetailDTO2 Sale) : ICommand<SailDetailResponse2>  { }
