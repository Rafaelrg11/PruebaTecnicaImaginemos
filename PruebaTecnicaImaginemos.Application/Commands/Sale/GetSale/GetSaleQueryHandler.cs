using Dapper;
using PruebaTecnicaImaginemos.Application.Abstraction.Data;
using PruebaTecnicaImaginemos.Application.Abstraction.Messaging;
using PruebaTecnicaImaginemos.Application.Commands.User;
using PruebaTecnicaImaginemos.Application.Responses;
using PruebaTecnicaImaginemos.Domain.Abstractions;
using PruebaTecnicaImaginemos.Domain.DTOs.Sale;
using PruebaTecnicaImaginemos.Domain.Sale;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Application.Commands.Sale.GetSale;

internal class GetSaleQueryHandler : IQueryHandler<GetSaleQuery, SaleResponse2>
{
    private readonly ISqlConnectionFactory _connectionFactory;

    public GetSaleQueryHandler(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<Result<SaleResponse2>> Handle(GetSaleQuery request, CancellationToken cancellationToken)
    {
        using var _connection = _connectionFactory.CreateConnection();

        var sql = @"
        SELECT 
            s.""Id"" AS IdSale, 
            s.""TimeSale"", 
            s.""UserId"",
            u.""Id"" AS ""IdUser"", 
            u.""Name"" AS ""Name"", 
            u.""DNI"" AS ""DNI""
        FROM public.""sale"" s
        LEFT JOIN public.""users"" u ON s.""UserId"" = u.""Id"";
    ";

        var sales = await _connection.QueryAsync<SaleResponse2, UserResponse2, SaleResponse2>(
                    sql,
                    (sale, user) =>
                    {
                        sale.user = user;
                        return sale;
                    },
                    splitOn: "IdUser");

        var sale = sales.FirstOrDefault();

        if (sales is null)
            return Result.Failure<SaleResponse2>("Venta no encontrada");

        return Result<SaleResponse2>.Success(sale);
    }
}
