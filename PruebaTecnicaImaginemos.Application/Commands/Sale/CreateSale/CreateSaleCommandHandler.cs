using PruebaTecnicaImaginemos.Application.Abstraction.Messaging;
using PruebaTecnicaImaginemos.Domain.Abstractions;
using PruebaTecnicaImaginemos.Domain.Sale;
using PruebaTecnicaImaginemos.Domain.sale_detail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Application.Commands.Sale.CreateSale;

internal class CreateSaleCommandHandler : ICommandHandler<CreateSaleCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateSaleCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {

        var sale = Sales.Create(request.idUser);

        _unitOfWork.Add(sale);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(sale.Id);
    }
}
