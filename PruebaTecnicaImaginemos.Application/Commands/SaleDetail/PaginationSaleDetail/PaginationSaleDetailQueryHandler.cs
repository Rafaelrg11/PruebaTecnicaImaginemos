using Dapper;
using PruebaTecnicaImaginemos.Application.Abstraction.Data;
using PruebaTecnicaImaginemos.Application.Abstraction.Messaging;
using PruebaTecnicaImaginemos.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Application.Commands.SaleDetail.PaginationSaleDetail;

internal class PaginationSaleDetailQueryHandler : IQueryHandler<PaginationSaleDetailQuery, (List<SaleDetailResponse>, long)>
{
    private readonly ISqlConnectionFactory _connectionFactory;

    public PaginationSaleDetailQueryHandler(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<Result<(List<SaleDetailResponse>, long)>> Handle(PaginationSaleDetailQuery request, CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();

        string sql = $@"SELECT ""Id"" AS ""IdSaleDetail"",
            ""IdProduct"" AS ""IdProduct"",
            ""IdSale"" AS ""IdSale"",
            ""Amount"" AS ""Amount"",
            ""UnitPrice"" AS ""UnitPrice"",
            ""Total"" AS ""Total""
            FROM public. ""detail_sale""
            LIMIT {request.limit}
            OFFSET {request.skip};
            ";

        var result = await connection.QueryAsync<SaleDetailResponse>(sql, cancellationToken);

        var module = result.ToList();

        string countSql = @"
                SELECT COUNT(*)
                FROM public ""detail_sale""";

        var totalCount = await connection.ExecuteScalarAsync<long>(countSql, cancellationToken);

        return (module, totalCount);
    }
}
