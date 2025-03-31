using PruebaTecnicaImaginemos.Domain.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Application.Commands.Sale;

public sealed class SaleResponse
{
    public Guid IdSale { get;  set; }
    public DateTime TimeSale { get;  set; }
    public int Total { get;  set; }
    public Guid UserId { get;  set; }
}
