using Dapper;
using PruebaTecnicaImaginemos.Application.Abstraction.Data;
using PruebaTecnicaImaginemos.Application.Abstraction.Messaging;
using PruebaTecnicaImaginemos.Domain.Abstractions;
using PruebaTecnicaImaginemos.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Application.Commands.User.GetUsers;

internal class GetAllUsersQueryHandler : IQueryHandler<GetAllUsersQuery, (List<UserResponse>, long)>
{
    private readonly ISqlConnectionFactory _connectionFactory;
    private readonly IUserRepository _userRepository;

    public GetAllUsersQueryHandler(ISqlConnectionFactory connectionFactory, IUserRepository userRepository)
    {
        _connectionFactory = connectionFactory;
        _userRepository = userRepository;
    }

    public async Task<Result<(List<UserResponse>, long)>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();

        const string sql = @"SELECT
        ""Id"" AS ""IdUser"",
        ""Name"" AS ""Name"",
        ""DNI"" AS ""DNI""
        FROM public. ""users"";";
        
        var result = await connection.QueryAsync<UserResponse>(sql);

        var module = result.ToList();

        string countSql = @"
        SELECT COUNT(*)
        FROM public. ""users"""; 

        var totalCount = await connection.ExecuteScalarAsync<long>(countSql);

        return (module, totalCount);
    }
}
