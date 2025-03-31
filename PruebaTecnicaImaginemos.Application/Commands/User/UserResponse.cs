using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Application.Commands.User;

public sealed class UserResponse
{
    public Guid IdUser { get; set; }
    public string Name { get; set; }
    public int DNI { get; set; }
}
