using PruebaTecnicaImaginemos.Application.Abstraction.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Application.Commands.User.CreateUser;

public sealed record CreateUserCommand(string name, string dni) : ICommand<Guid>
{
}
