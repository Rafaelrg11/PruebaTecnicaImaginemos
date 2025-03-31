using MediatR;
using PruebaTecnicaImaginemos.Application.Abstraction.Messaging;
using PruebaTecnicaImaginemos.Application.Commands.Sale.GetSale;
using PruebaTecnicaImaginemos.Domain.Abstractions;
using PruebaTecnicaImaginemos.Domain.DTOs.Sale;
using PruebaTecnicaImaginemos.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Application.Commands.Sale.UpdateSale;

internal class UpdateSaleCommandHandler : ICommandHandler<UpdateSaleCommand, SaleDTO>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISender _sender;
    private readonly ISaleRepository _saleRepository;

    public UpdateSaleCommandHandler(IUnitOfWork unitOfWork, 
        ISender sender, ISaleRepository saleRepository)
    {
        _unitOfWork = unitOfWork;
        _sender = sender;
        _saleRepository = saleRepository;
    }

    public async Task<Result<SaleDTO>> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
    {
        Guid guid;

        if (request.Sale is not null) 
        {
            guid = (Guid)request.Sale.IdSale;
        }

        var model = await _saleRepository.GetByIdAsync(request.Sale.IdSale);

        if (model is null) 
        {
            return null;
        }

        model.Update(request.Sale);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var data = await _sender.Send(new GetSaleQuery(model.Id));

        return data.Value;
    }
}
