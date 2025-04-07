using MediatR;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaImaginemos.Application.Commands.Product.GetAllProduct;
using PruebaTecnicaImaginemos.Application.Commands.SaleDetail.UpdateSaleDetail;
using PruebaTecnicaImaginemos.Application.Commands.User;
using PruebaTecnicaImaginemos.Application.Commands.User.CreateUser;
using PruebaTecnicaImaginemos.Application.Commands.User.DeleteUser;
using PruebaTecnicaImaginemos.Application.Commands.User.GetUser;
using PruebaTecnicaImaginemos.Application.Commands.User.GetUsers;
using PruebaTecnicaImaginemos.Application.Commands.User.PaginationUser;
using PruebaTecnicaImaginemos.Application.Commands.User.UpdateUser;
using PruebaTecnicaImaginemos.Domain.DTOs.Responses;
using PruebaTecnicaImaginemos.Domain.DTOs.SaleDetail;
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

        var result = await _sender.Send(userC);

        return Ok(result);
    }

    [HttpGet("GetAllUsers")]
    public async Task<IActionResult> GetAllUsers()
    {
        try
        {
            var result = await _sender.Send(new GetAllUsersQuery());

            // Verifica si la operación fue exitosa
            if (result.IsSuccess)
            {
                var (users, totalCount) = result.Value; // Desestructura el tuple

                return Ok(new
                {
                    Users = users,
                    TotalCount = totalCount
                });
            }

            return BadRequest(result.Error);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("GetUser")]
    public async Task<IActionResult> GetUser(Guid guid)
    {
        try
        {
            var user = new GetUserQuery(guid);

            await _sender.Send(user);

            return Ok(user);
        }
        catch (Exception ex)
        {
            {
                return BadRequest(ex.Message);
            }
        }
    }

    [HttpPut("UpdateUser")]
    public async Task<IActionResult> UpdateUser(UserDTO2 user, CancellationToken cancellationToken)
    {
        try
        {
            var detail = await _sender.Send(new UpdateUserCommand(user));

            return Ok(detail);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("PaginationUser")]
    public async Task<IActionResult> PaginationUser(int skip = 0, int limit = 12)
    {
        try
        {
            var result = await _sender.Send(new PaginationUserQuery(limit, skip));

            if (result.IsSuccess)
            {
                var (users, totalCount) = result.Value; 

                return Ok(new
                {
                    Users = users,
                    TotalCount = totalCount
                });
            }

            return BadRequest(result.Error);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
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
