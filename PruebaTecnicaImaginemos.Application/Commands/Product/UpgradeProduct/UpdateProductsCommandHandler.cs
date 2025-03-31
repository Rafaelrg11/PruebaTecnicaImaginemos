using MediatR;
using PruebaTecnicaImaginemos.Application.Abstraction.Data;
using PruebaTecnicaImaginemos.Application.Abstraction.Messaging;
using PruebaTecnicaImaginemos.Application.Commands.Product.GetProduct;
using PruebaTecnicaImaginemos.Domain.Abstractions;
using PruebaTecnicaImaginemos.Domain.DTOs.Product;
using PruebaTecnicaImaginemos.Domain.Interfaces;


namespace PruebaTecnicaImaginemos.Application.Commands.Product.UpgradeProduct;

internal class UpdateProductsCommandHandler : ICommandHandler<UpdateProductsCommand, ProductDTO2>
{
    private readonly ISqlConnectionFactory _connectionFactory;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISender _sender;
    private readonly IProductRepository _productRepository;

    public UpdateProductsCommandHandler(ISqlConnectionFactory connectionFactory, 
        IUnitOfWork unitOfWork, ISender sender, 
        IProductRepository productRepository)
    {
        _connectionFactory = connectionFactory;
        _unitOfWork = unitOfWork;
        _sender = sender;
        _productRepository = productRepository;
    }

    public async Task<Result<ProductDTO2>> Handle(UpdateProductsCommand request, CancellationToken cancellationToken)
    {
        Guid id;

        if (request.Product is not null) 
        {
            id = (Guid)request.Product.IdProduct;
        }

        var model = await _productRepository.GetByIdAsync(request.Product.IdProduct);

        if (model is null) 
        {
            return null;
        }

        model.update(request.Product);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var data = await _sender.Send(new GetProductQuery(model.Id));

        return data.Value;

    }
}
