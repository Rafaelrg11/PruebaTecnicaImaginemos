using MediatR;
using PruebaTecnicaImaginemos.Application.Abstraction.Messaging;
using PruebaTecnicaImaginemos.Application.Commands.Sale.GetSale;
using PruebaTecnicaImaginemos.Application.Commands.SaleDetail.GetAllSaleDetail;
using PruebaTecnicaImaginemos.Application.Commands.SaleDetail.GetSaleDetail;
using PruebaTecnicaImaginemos.Domain.Abstractions;
using PruebaTecnicaImaginemos.Domain.DTOs.SaleDetail;
using PruebaTecnicaImaginemos.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Application.Commands.SaleDetail.UpdateSaleDetail;

internal class UpdateSaleDetailCommandHandler : ICommandHandler<UpdateSaleDetailCommand, SaleDetailDTO>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISender _sender;
    private readonly ISaleDetailRepository _saleDetailRepository;

    public UpdateSaleDetailCommandHandler(IUnitOfWork unitOfWork, ISender sender, 
        ISaleDetailRepository saleDetailRepository)
    {
        _unitOfWork = unitOfWork;
        _sender = sender;
        _saleDetailRepository = saleDetailRepository;
    }

    public async Task<Result<SaleDetailDTO>> Handle(UpdateSaleDetailCommand request, CancellationToken cancellationToken)
    {
        Guid guid;

        if (request.Sale is not null)
        {
            guid = (Guid)request.Sale.IdSaleDetail;
        }

        var model = await _saleDetailRepository.GetByIdAsync(request.Sale.IdSaleDetail);

        if (model is null)
        {
            return null;
        }

        model.Update(request.Sale);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var data = await _sender.Send(new GetSaleDetailQuery(model.Id));

        return data.Value;
    }
}
