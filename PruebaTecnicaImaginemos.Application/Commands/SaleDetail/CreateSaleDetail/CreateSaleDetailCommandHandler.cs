using PruebaTecnicaImaginemos.Application.Abstraction.Messaging;
using PruebaTecnicaImaginemos.Domain.Abstractions;
using PruebaTecnicaImaginemos.Domain.sale_detail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Application.Commands.SaleDetail.CreateSaleDetail;

internal class CreateSaleDetailCommandHandler : ICommandHandler<CreateDetailSaleCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateSaleDetailCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateDetailSaleCommand request, CancellationToken cancellationToken)
    {

        var saleD = DetailSale.create(request.product, request.idSale, request.amount, request.unitPrice);

        _unitOfWork.Add(saleD);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return saleD.Id;
    }
}
