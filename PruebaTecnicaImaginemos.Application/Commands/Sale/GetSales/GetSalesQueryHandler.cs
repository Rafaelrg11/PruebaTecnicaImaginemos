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

namespace PruebaTecnicaImaginemos.Application.Commands.Sale.GetSales;

internal class GetSalesQueryHandler : IQueryHandler<GetSalesQuery, List<SaleResponse2>>
{
    private readonly ISqlConnectionFactory _connectionFactory;

    public GetSalesQueryHandler(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<Result<List<SaleResponse2>>> Handle(GetSalesQuery request, CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();

        var sql = @"
        SELECT 
            s.""Id"" AS IdSale, 
            s.""TimeSale"", 
            s.""UserId"",
            u.""Id"" AS IdUser, 
            u.""Name"" AS Name, 
            u.""DNI"" AS DNI
        FROM public.""sale"" s
        LEFT JOIN public.""users"" u ON s.""UserId"" = u.""Id"";
    ";

        var sales = await connection.QueryAsync<SaleResponse2, UserResponse2, SaleResponse2>(
            sql,
            (sale, user) =>
            {
                sale.user = user; 
                return sale;
            },
            splitOn: "IdUser");

        var resultList = sales.ToList();

        return Result<List<SaleResponse>>.Success((resultList));
    }
}
