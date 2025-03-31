using Dapper;
using PruebaTecnicaImaginemos.Application.Abstraction.Data;
using PruebaTecnicaImaginemos.Application.Abstraction.Messaging;
using PruebaTecnicaImaginemos.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Application.Commands.Sale.PaginationSale;

internal class PaginationSaleQueryHandler : IQueryHandler<PaginationSaleQuery, (List<SaleResponse>, long)>
{
    private readonly ISqlConnectionFactory _connectionFactory;

    public PaginationSaleQueryHandler(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<Result<(List<SaleResponse>, long)>> Handle(PaginationSaleQuery request, CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();

        string sql = $@"SELECT ""Id"" AS ""IdSale"",
            ""TimeSale"" AS ""TimeSale"",
            ""Total"" AS ""Total"",
            ""UserId AS ""UserId""
            FROM public ""sale""
            LIMIT {request.limit}
            OFFSET{request.skip};
            ";

        var result = await connection.QueryAsync<SaleResponse>(sql, cancellationToken);

        var module = result.ToList();

        string countSql = @"
                SELECT COUNT(*)
                FROM public. ""sale""";

        var totalCount = await connection.ExecuteScalarAsync<long>(countSql, cancellationToken);

        return (module, totalCount);
    }
}
