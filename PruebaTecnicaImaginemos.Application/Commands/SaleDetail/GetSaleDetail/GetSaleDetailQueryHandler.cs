using Dapper;
using PruebaTecnicaImaginemos.Application.Abstraction.Data;
using PruebaTecnicaImaginemos.Application.Abstraction.Messaging;
using PruebaTecnicaImaginemos.Domain.Abstractions;
using PruebaTecnicaImaginemos.Domain.DTOs.SaleDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Application.Commands.SaleDetail.GetSaleDetail;

internal class GetSaleDetailQueryHandler : IQueryHandler<GetSaleDetailQuery, SaleDetailDTO>
{
    private readonly ISqlConnectionFactory _connectionFactory;

    public GetSaleDetailQueryHandler(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<Result<SaleDetailDTO>> Handle(GetSaleDetailQuery request, CancellationToken cancellationToken)
    {
        using var _connection = _connectionFactory.CreateConnection();

        string sql = $@"SELECT ""Id"" AS ""IdSaleDetail"", ""IdProduct"", ""IdSale"", ""Amount"", ""UnitPrice"", ""Total""
                     FROM public. ""detail_sale"" WHERE ""Id"" = '{request.id.ToString()}'";

        var result = await _connection.QueryAsync<SaleDetailDTO>(sql);
        
        var module = result.FirstOrDefault();

        return module;

    }
}
