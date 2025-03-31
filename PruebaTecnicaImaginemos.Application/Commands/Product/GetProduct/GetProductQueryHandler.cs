using Dapper;
using PruebaTecnicaImaginemos.Application.Abstraction.Data;
using PruebaTecnicaImaginemos.Application.Abstraction.Messaging;
using PruebaTecnicaImaginemos.Domain.Abstractions;
using PruebaTecnicaImaginemos.Domain.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Application.Commands.Product.GetProduct;

internal class GetProductQueryHandler : IQueryHandler<GetProductQuery, ProductDTO2>
{
    private readonly ISqlConnectionFactory _connectionFactory;

    public GetProductQueryHandler(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<Result<ProductDTO2>> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();

        string sql = $@"SELECT ""Id"" AS ""IdProduct"",
                ""NameProduct"" AS ""NameProduct"",
                ""Price"" AS ""Price"",
                ""Description"" AS ""Description""
                     FROM public. ""products"" WHERE ""Id"" = '{request.id.ToString()}'";

        var result = await connection.QueryAsync<ProductDTO2>(sql);

        var groupedREsult = result.FirstOrDefault();

        return groupedREsult;
    }
}
