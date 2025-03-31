using Dapper;
using PruebaTecnicaImaginemos.Application.Abstraction.Data;
using PruebaTecnicaImaginemos.Application.Abstraction.Messaging;
using PruebaTecnicaImaginemos.Domain.Abstractions;
using PruebaTecnicaImaginemos.Domain.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Application.Commands.User.GetUser;

internal class GetUserQueryHandler : IQueryHandler<GetUserQuery, UserDTO>
{
    private readonly ISqlConnectionFactory _connectionFactory;

    public GetUserQueryHandler(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<Result<UserDTO>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();

        string sql = $@"SELECT ""Id"" AS ""IdUser"", ""Name"", ""DNI""
                     FROM public. ""users"" WHERE ""Id"" = '{request.id.ToString()}'";

        var result = await connection.QueryAsync<UserDTO>(sql);

        var resultG = result.FirstOrDefault();

        return resultG;
    }
}
