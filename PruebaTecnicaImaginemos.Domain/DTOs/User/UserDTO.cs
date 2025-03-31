using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Domain.DTOs.User;

public sealed class UserDTO
{
    public Guid IdUser { get; set; }
    public string Name { get; set; }
    public string DNI { get; set; }
}
