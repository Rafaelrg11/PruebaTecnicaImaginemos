using PruebaTecnicaImaginemos.Application.Abstraction.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Application.Commands.User.PaginationUser;

public sealed record PaginationUserQuery(int limit, int skip) : IQuery<(List<UserResponse>, long)> { }
