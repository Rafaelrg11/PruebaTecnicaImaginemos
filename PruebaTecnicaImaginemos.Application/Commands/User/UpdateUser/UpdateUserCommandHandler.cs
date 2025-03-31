using MediatR;
using PruebaTecnicaImaginemos.Application.Abstraction.Data;
using PruebaTecnicaImaginemos.Application.Abstraction.Messaging;
using PruebaTecnicaImaginemos.Application.Commands.User.GetUser;
using PruebaTecnicaImaginemos.Domain.Abstractions;
using PruebaTecnicaImaginemos.Domain.DTOs.User;
using PruebaTecnicaImaginemos.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Application.Commands.User.UpdateUser;

internal class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand, UserDTO>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISender _sender;
    private readonly IUserRepository _userRepository;

    public UpdateUserCommandHandler(IUnitOfWork unitOfWork, 
        ISender sender, IUserRepository userRepository)
    {
        _unitOfWork = unitOfWork;
        _sender = sender;
        _userRepository = userRepository;
    }

    public async Task<Result<UserDTO>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        Guid id;

        if(request.User is not null)
        {
            id = (Guid)request.User.IdUser;
        }

        var model = await _userRepository.GetByIdAsync(request.User.IdUser);

        if (model is null) 
        {
            return null;
        }

        model.Update(request.User);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var data = await _sender.Send(new GetUserQuery(model.Id));

        return data.Value;
    }
}
