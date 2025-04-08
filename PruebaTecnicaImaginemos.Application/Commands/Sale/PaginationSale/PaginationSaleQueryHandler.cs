using Dapper;
using PruebaTecnicaImaginemos.Application.Abstraction.Data;
using PruebaTecnicaImaginemos.Application.Abstraction.Messaging;
using PruebaTecnicaImaginemos.Application.Commands.User;
using PruebaTecnicaImaginemos.Application.Responses;
using PruebaTecnicaImaginemos.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Application.Commands.Sale.PaginationSale;

internal class PaginationSaleQueryHandler : IQueryHandler<PaginationSaleQuery, (List<SaleResponse2>, long)>
{
    private readonly ISqlConnectionFactory _connectionFactory;

    public PaginationSaleQueryHandler(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<Result<(List<SaleResponse2>, long)>> Handle(PaginationSaleQuery request, CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();

        string sql = $@"
        SELECT 
            s.""Id"" AS IdSale, 
            s.""TimeSale"", 
            s.""UserId"",
            u.""Id"" AS ""UserId"", 
            u.""Name"" AS ""Name"", 
            u.""DNI"" AS ""DNI""
        FROM public.""sale"" s
        LEFT JOIN public.""users"" u ON s.""UserId"" = u.""Id""
            LIMIT {request.limit}
            OFFSET {request.skip};";

        var sales = await connection.QueryAsync<SaleResponse2, UserResponse2, SaleResponse2>(
                    sql,
                    (sale, user) =>
                    {
                        sale.user = user;
                        return sale;
                    },
                    splitOn: "UserId");

        var module = sales.ToList();

        string countSql = @"
                SELECT COUNT(*)
                FROM public. ""sale""";

        var totalCount = await connection.ExecuteScalarAsync<long>(countSql, cancellationToken);

        return (module, totalCount);
    }
}
