using PruebaTecnicaImaginemos.Application.Abstraction.Messaging;
using PruebaTecnicaImaginemos.Domain.Abstractions;
using PruebaTecnicaImaginemos.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Application.Commands.SaleDetail.DeleteSaleDetail;

internal class DeleteSaleDetailCommandHandler : ICommandHandler<DeleteSaleDetailCommand, bool>
{
    private readonly ISaleDetailRepository _saleDetailRepository;

    public DeleteSaleDetailCommandHandler(ISaleDetailRepository saleDetailRepository)
    {
        _saleDetailRepository = saleDetailRepository;
    }

    public async Task<Result<bool>> Handle(DeleteSaleDetailCommand request, CancellationToken cancellationToken)
    {
        var detail = await _saleDetailRepository.GetByIdAsync(request.id);

        if (detail is null) 
        {
            return false;
        }

        _saleDetailRepository.Delete(detail);

        return true;
    }
}
