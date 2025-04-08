using Dapper;
using PruebaTecnicaImaginemos.Application.Abstraction.Data;
using PruebaTecnicaImaginemos.Application.Abstraction.Messaging;
using PruebaTecnicaImaginemos.Application.Responses;
using PruebaTecnicaImaginemos.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Application.Commands.SaleDetail.PaginationSaleDetail;

internal class PaginationSaleDetailQueryHandler : IQueryHandler<PaginationSaleDetailQuery, (List<SailDetailResponse2>, long)>
{
    private readonly ISqlConnectionFactory _connectionFactory;

    public PaginationSaleDetailQueryHandler(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<Result<(List<SailDetailResponse2>, long)>> Handle(PaginationSaleDetailQuery request, CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();

        var sql = $@"SELECT 
            sd.""Id"" AS ""IdSaleDetail"",
            sd.""IdProduct"" AS ""IdProduct"",
            sd.""IdSale"" AS ""IdSale"",
            sd.""Amount"", 
            sd.""UnitPrice"",
            sd.""Total"",

            p.""Id"" AS ""IdProduct"",
            p.""NameProduct"" AS ""NameProduct"", 
            p.""Price"", 
            p.""Description"",

            s.""Id"" AS ""IdSale"", 
            s.""TimeSale"", 
            s.""UserId"",
            u.""Id"" AS ""UserId"", 
            u.""Name"" AS ""Name"", 
            u.""DNI"" AS ""DNI""

        FROM public.""detail_sale"" sd
        LEFT JOIN public.""products"" p ON sd.""IdProduct"" = p.""Id""
        LEFT JOIN public.""sale"" s ON sd.""IdSale"" = s.""Id""
        LEFT JOIN public.""users"" u ON s.""UserId"" = u.""Id""
        LIMIT {request.limit}
        OFFSET {request.skip}
        ;";

        var details = new List<SailDetailResponse2>();

        await connection.QueryAsync<SailDetailResponse2, ProductResponse2, SaleResponse3, UserResponse2, SailDetailResponse2>(
            sql,
            (detail, product, sale, user) =>
            {
                detail.Product = product;
                detail.sale = sale;
                detail.sale.user = user;
                details.Add(detail);
                return detail;
            },
            splitOn: "IdProduct,IdSale,UserId"
        );

        const string countSql = @"SELECT COUNT(*) FROM public.""detail_sale""";

        var totalCount = await connection.ExecuteScalarAsync<long>(countSql);

        return Result.Success((details, totalCount));
    }
}
