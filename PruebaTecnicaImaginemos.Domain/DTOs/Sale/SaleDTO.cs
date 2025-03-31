using PruebaTecnicaImaginemos.Domain.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Domain.DTOs.Sale;

public sealed class SaleDTO
{
    public Guid IdSale { get; set; }
    public DateTime DateTime { get;  set; }
    public int Total { get;  set; }
    public Guid UserId { get;  set; }
    public UserDTO2 User { get;  set; }
}
