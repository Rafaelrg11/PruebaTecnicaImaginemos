using MediatR;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaImaginemos.Application.Commands.User;
using PruebaTecnicaImaginemos.Application.Commands.User.CreateUser;
using PruebaTecnicaImaginemos.Application.Commands.User.DeleteUser;
using PruebaTecnicaImaginemos.Application.Commands.User.GetUser;
using PruebaTecnicaImaginemos.Application.Commands.User.GetUsers;
using PruebaTecnicaImaginemos.Application.Commands.User.PaginationUser;
using PruebaTecnicaImaginemos.Domain.DTOs.Responses;
using PruebaTecnicaImaginemos.Domain.DTOs.User;

namespace PruebaTecnicaImaginemos.ApiView.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ISender _sender;

    public UserController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("AddUser")]
    public async Task<IActionResult> CreateUSer([FromBody] UserDTO2 user)
    {
        var userC = new CreateUserCommand(user.Name, user.DNI);

        await _sender.Send(userC);

        return Ok(userC);
    }

    [HttpGet("GetAllUsers")]
    public async Task<IActionResult> GetAllUsers(Guid guid)
    {
        var user = new GetAllUsersQuery();

        await _sender.Send(user);

        return Ok(user);
    }

    [HttpGet("GetUser")]
    public async Task<IActionResult> GetUser(Guid guid)
    {
        var user = new GetUserQuery(guid);

        await _sender.Send(user);

        return Ok(user);
    }

    [HttpGet("PaginationUser")]
    public async Task<IActionResult> PaginationUser(int skip = 0, int limit = 12)
    {
        var user = await _sender.Send(new PaginationUserQuery(limit, skip));

        var response = new ResponseStandar<List<UserResponse>>()
        {
            data = user.Value.Item1,
            total = user.Value.Item2
        };

        return Ok(response);
    }

    [HttpDelete("DeleteUser")]
    public async Task<IActionResult> DeleteUser([FromQuery] Guid guid, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _sender.Send(new DeleteUserCommand(guid));

            return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
