using Dapper;
using PruebaTecnicaImaginemos.Application.Abstraction.Data;
using PruebaTecnicaImaginemos.Application.Abstraction.Messaging;
using PruebaTecnicaImaginemos.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Application.Commands.Product.Pagination;

internal class ProductPaginationQueryHandler : IQueryHandler<ProductPaginationQuery, (List<ProductsResponse>, long)>
{
    private readonly ISqlConnectionFactory _connectionFactory;

    public ProductPaginationQueryHandler(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<Result<(List<ProductsResponse>, long)>> Handle(ProductPaginationQuery request, CancellationToken cancellationToken)
    {
        using var _connection = _connectionFactory.CreateConnection();

        string sql = $@"SELECT ""Id"" AS ""IdProduct"",
                ""NameProduct"" AS ""NameProduct"",
                ""Price"" AS ""Price"",
                ""Description"" AS ""Description""
                FROM public. ""products""
                LIMIT {request.limit}
                OFFSET {request.skip};
                ;";

        var result = await _connection.QueryAsync<ProductsResponse>(sql);

        var module = result.ToList();

        string countSql = @"
                SELECT COUNT(*)
                FROM public. ""products""";

        var totalCount = await _connection.ExecuteScalarAsync<long>(countSql);

        return (module, totalCount);
    }
}
