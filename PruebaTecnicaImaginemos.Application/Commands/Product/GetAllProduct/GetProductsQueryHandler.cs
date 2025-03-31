using Dapper;
using PruebaTecnicaImaginemos.Application.Abstraction.Data;
using PruebaTecnicaImaginemos.Application.Abstraction.Messaging;
using PruebaTecnicaImaginemos.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Application.Commands.Product.GetAllProduct;

internal class GetProductsQueryHandler : IQueryHandler<GetProductsQuery, (List<ProductsResponse>, long)>
{
    private readonly ISqlConnectionFactory _connectionFactory;

    public GetProductsQueryHandler(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<Result<(List<ProductsResponse>, long)>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();

        const string sql = @"SELECT
            ""Id"" AS ""IdProduct"",
            ""NameProduct"" AS ""NameProduct"",
            ""Price"" AS ""Price"",
            ""Description"" AS ""Description""
            FROM public. ""products"";";

        var result = await connection.QueryAsync<ProductsResponse>(sql);

        var module = result.ToList();

        string countSql = @"
                SELECT COUNT(*)
                FROM public. ""products""
                ";

        var totalCount = await connection.ExecuteScalarAsync<long>(countSql);

        return (module, totalCount);
    }
}
