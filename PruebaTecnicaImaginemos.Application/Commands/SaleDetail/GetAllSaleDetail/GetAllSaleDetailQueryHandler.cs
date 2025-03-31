using Dapper;
using PruebaTecnicaImaginemos.Application.Abstraction.Data;
using PruebaTecnicaImaginemos.Application.Abstraction.Messaging;
using PruebaTecnicaImaginemos.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Application.Commands.SaleDetail.GetAllSaleDetail;

internal class GetAllSaleDetailQueryHandler : IQueryHandler<GetAllSaleDetailQuery, (List<SaleDetailResponse>, long)>
{
    private readonly ISqlConnectionFactory _connectionFactory;

    public GetAllSaleDetailQueryHandler(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<Result<(List<SaleDetailResponse>, long)>> Handle(GetAllSaleDetailQuery request, CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();

        const string sql = @"SELECT
            sd. ""Id"" AS ""IdSaleDetail"",
            sd. ""IdProduct"" AS ""IdProduct"",
            sd. ""IdSale"" AS ""IdSale"",
            sd. ""Amount"" AS ""Amount"",
            sd. ""UnitPrice"" AS ""UnitPrice"",
            sd. ""Total"" AS ""Total"",
            
            FROM public. ""detail_sale"";";

        var result = await connection.QueryAsync<SaleDetailResponse>(sql, cancellationToken);

        var module = result.ToList();

        string countSql = @"
                SELECT COUNT(*)
                FROM public. ""detail_sale""";

        var totalCount = await connection.ExecuteScalarAsync<long>(countSql, cancellationToken);

        return (module, totalCount);
    }
}
