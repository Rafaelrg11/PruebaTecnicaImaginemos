using MediatR;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaImaginemos.Application.Commands.Product.GetAllProduct;
using PruebaTecnicaImaginemos.Application.Commands.Sale.CreateSale;
using PruebaTecnicaImaginemos.Application.Commands.Sale.DeleteSale;
using PruebaTecnicaImaginemos.Application.Commands.Sale.GetSale;
using PruebaTecnicaImaginemos.Application.Commands.Sale.GetSales;
using PruebaTecnicaImaginemos.Application.Commands.Sale.PaginationSale;
using PruebaTecnicaImaginemos.Application.Commands.Sale.UpdateSale;
using PruebaTecnicaImaginemos.Application.Commands.SaleDetail.UpdateSaleDetail;
using PruebaTecnicaImaginemos.Domain.Abstractions;
using PruebaTecnicaImaginemos.Domain.DTOs.Sale;
using PruebaTecnicaImaginemos.Domain.DTOs.SaleDetail;

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
            var result = await _sender.Send(new GetSalesQuery());

            if (result.IsSuccess)
            {
                var sales = result.Value; 

                return Ok(new
                {
                    Sales = sales
                });
            }
                return BadRequest(result.Error);
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

    [HttpPut("UpdateSale")]
    public async Task<IActionResult> UpdateSale(SaleDTO2 sale, CancellationToken cancellationToken)
    {
        try
        {
            var detail = await _sender.Send(new UpdateSaleCommand(sale));

            return Ok(detail);
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

            if (sale.IsSuccess)
            {
                var (Sales, totalCount) = sale.Value; // Desestructura el tuple

                return Ok(new
                {
                    Sales = Sales,
                    TotalCount = totalCount
                });
            }

            return BadRequest(sale.Error);
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
