using MediatR;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaImaginemos.Application.Commands.Sale.CreateSale;
using PruebaTecnicaImaginemos.Application.Commands.Sale.DeleteSale;
using PruebaTecnicaImaginemos.Application.Commands.Sale.GetSale;
using PruebaTecnicaImaginemos.Application.Commands.Sale.GetSales;
using PruebaTecnicaImaginemos.Application.Commands.Sale.PaginationSale;
using PruebaTecnicaImaginemos.Domain.DTOs.Sale;

namespace PruebaTecnicaImaginemos.ApiView.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SaleController : Controller
{
    private readonly ISender _sender;

    public SaleController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("CreateSale")]
    public async Task<IActionResult> CreateSale([FromBody] SaleDTO2 saledto)
    {
        try
        {
            var sale = await _sender.Send(new CreateSaleCommand(saledto.UserId));

            return Ok(sale);
        }
        catch (Exception ex) 
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("GetAllSales")]
    public async Task<IActionResult> GetSales()
    {
        try
        {
            var sale = await _sender.Send(new GetSalesQuery());

            return Ok(sale);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("GetSale")]
    public async Task<IActionResult> GetSale(Guid guid)
    {
        try
        {
            var sale = await _sender.Send(new GetSaleQuery(guid));

            return Ok(sale);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("PaginationSale")]
    public async Task<IActionResult> PaginationSale(int limit = 12, int skip = 0)
    {
        try
        {
            var sale = await _sender.Send(new PaginationSaleQuery(skip, limit));

            return Ok(sale);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("DeleteSale")]
    public async Task<IActionResult> DeleteSale(Guid guid, CancellationToken cancellationToken)
    {
        try
        {
            var sale = await _sender.Send(new DeleteSaleCommand(guid));

            return Ok(sale);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
