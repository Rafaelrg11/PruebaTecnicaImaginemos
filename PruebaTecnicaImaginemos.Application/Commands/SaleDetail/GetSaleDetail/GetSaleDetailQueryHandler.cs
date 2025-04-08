using Dapper;
using PruebaTecnicaImaginemos.Application.Abstraction.Data;
using PruebaTecnicaImaginemos.Application.Abstraction.Messaging;
using PruebaTecnicaImaginemos.Application.Responses;
using PruebaTecnicaImaginemos.Domain.Abstractions;
using PruebaTecnicaImaginemos.Domain.DTOs.SaleDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace PruebaTecnicaImaginemos.Application.Commands.SaleDetail.GetSaleDetail;

internal class GetSaleDetailQueryHandler : IQueryHandler<GetSaleDetailQuery, SailDetailResponse2>
{
    private readonly ISqlConnectionFactory _connectionFactory;

    public GetSaleDetailQueryHandler(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<Result<SailDetailResponse2>> Handle(GetSaleDetailQuery request, CancellationToken cancellationToken)
    {
        using var _connection = _connectionFactory.CreateConnection();

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
        WHERE sd.""Id"" = '{request.id}';"
        ;

        var details = new SailDetailResponse2();

        await _connection.QueryAsync<SailDetailResponse2, ProductResponse2, SaleResponse3, UserResponse2, SailDetailResponse2>(
            sql,
            (detail, product, sale, user) =>
            {
                detail.Product = product;
                detail.sale = sale;
                detail.sale.user = user;
                details = detail;
                return detail;
            },
            splitOn: "IdProduct,IdSale,UserId"
        );

        return details;
    }
}
