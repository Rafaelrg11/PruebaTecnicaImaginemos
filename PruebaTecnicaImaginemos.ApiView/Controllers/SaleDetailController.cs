using MediatR;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaImaginemos.Application.Commands.SaleDetail.CreateSaleDetail;
using PruebaTecnicaImaginemos.Application.Commands.SaleDetail.DeleteSaleDetail;
using PruebaTecnicaImaginemos.Application.Commands.SaleDetail.GetAllSaleDetail;
using PruebaTecnicaImaginemos.Application.Commands.SaleDetail.GetSaleDetail;
using PruebaTecnicaImaginemos.Application.Commands.SaleDetail.PaginationSaleDetail;
using PruebaTecnicaImaginemos.Domain.DTOs.SaleDetail;

namespace PruebaTecnicaImaginemos.ApiView.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SaleDetailController : Controller
{

    private readonly ISender _sender;

    public SaleDetailController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("CreateDetail")]
    public async Task<IActionResult> CreateDetail(SaleDetailDTO2 dTO2)
    {
        try
        {
            var sale = await _sender.Send(new CreateDetailSaleCommand(dTO2.IdProduct, dTO2.IdSale, dTO2.Amount, dTO2.UnitPrice));

            return Ok(sale);
        }
        catch (Exception ex) 
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("GetAllDetails")]
    public async Task<IActionResult> GetAllDetails()
    {
        try
        {
            var sale = await _sender.Send(new GetAllSaleDetailQuery());

            return Ok(sale);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("GetDetail")]
    public async Task<IActionResult> GetDetail([FromQuery]Guid guid)
    {
        try
        {
            var sale = await _sender.Send(new GetSaleDetailQuery(guid));

            return Ok(sale);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("PaginationDetail")]
    public async Task<IActionResult> PaginationDetail([FromBody] int limit = 12,[FromBody] int skip = 0)
    {
        try
        {
            var sale = await _sender.Send(new PaginationSaleDetailQuery(skip, limit));

            return Ok(sale);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("DeleteDetail")]
    public async Task<IActionResult> DeleteDetail(Guid guid)
    {
        try
        {
            var sale = await _sender.Send(new DeleteSaleDetailCommand(guid));

            return Ok(sale);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
