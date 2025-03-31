using Dapper;
using PruebaTecnicaImaginemos.Application.Abstraction.Data;
using PruebaTecnicaImaginemos.Application.Abstraction.Messaging;
using PruebaTecnicaImaginemos.Domain.Abstractions;
using PruebaTecnicaImaginemos.Domain.DTOs.Sale;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Application.Commands.Sale.GetSale;

internal class GetSaleQueryHandler : IQueryHandler<GetSaleQuery, SaleDTO>
{
    private readonly ISqlConnectionFactory _connectionFactory;

    public GetSaleQueryHandler(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<Result<SaleDTO>> Handle(GetSaleQuery request, CancellationToken cancellationToken)
    {
        using var _connection = _connectionFactory.CreateConnection();

        string sql = $@"SELECT ""Id"" AS ""IdSale"",
            ""TimeSale"" AS ""TimeSale"",
            ""Total"" AS ""Total"",
            ""UserId AS ""UserId""
            
            FROM public. ""sale""
                     FROM public. ""sale"" WHERE Id = '{request.guid.ToString()}'";

        var result = await _connection.QueryAsync<SaleDTO>(sql);

        var module = result.FirstOrDefault();

        return module;
    }
}
