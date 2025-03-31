using Dapper;
using Microsoft.Extensions.Logging;
using PruebaTecnicaImaginemos.Application.Abstraction.Data;
using PruebaTecnicaImaginemos.Application.Abstraction.Messaging;
using PruebaTecnicaImaginemos.Domain.Abstractions;
using PruebaTecnicaImaginemos.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Application.Commands.User.PaginationUser;

internal class PaginationUserQueryHandler : IQueryHandler<PaginationUserQuery, (List<UserResponse>, long)>
{
    private readonly ISqlConnectionFactory _connectionFactory;
    private readonly ILogger<PaginationUserQuery> _logger;

    public PaginationUserQueryHandler(ISqlConnectionFactory connectionFactory, ILogger<PaginationUserQuery> logger)
    {
        _connectionFactory = connectionFactory;
        _logger = logger;
    }

    public async Task<Result<(List<UserResponse>, long)>> Handle(PaginationUserQuery request, CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();

        string sql = @$"
        SELECT ""Id"" AS ""IdUser"", ""Name"" AS ""Name"", ""DNI"" AS ""DNI""
        FROM public.""users""
        ORDER BY ""Id""
        LIMIT @Limit OFFSET @Skip;";


        var result = await connection.QueryAsync<UserResponse>(sql, new { Limit = request.limit, Skip = request.skip });

        _logger.LogInformation("Limit: {Limit}, Skip: {Skip}", request.limit, request.skip);

        var rawUsers = await connection.QueryAsync(sql, new { Limit = request.limit, Skip = request.skip });
        _logger.LogInformation("Usuarios crudos: {RawUsers}", rawUsers);

        var module = result.ToList();

        string countSql = @"SELECT COUNT(*) FROM public.""users"";";

        var totalCount = await connection.ExecuteScalarAsync<long>(countSql);

        _logger.LogInformation("Usuarios encontrados: {Count}", module.Count);
        foreach (var user in module)
        {
            _logger.LogInformation("ID: {IdUser}, Name: {Name}, DNI: {DNI}", user.IdUser, user.Name, user.DNI);
        }

        return (module, totalCount);

    }

}
