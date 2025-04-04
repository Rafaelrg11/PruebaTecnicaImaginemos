using MediatR;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaImaginemos.Application.Commands.Product.GetAllProduct;
using PruebaTecnicaImaginemos.Application.Commands.SaleDetail.CreateSaleDetail;
using PruebaTecnicaImaginemos.Application.Commands.SaleDetail.DeleteSaleDetail;
using PruebaTecnicaImaginemos.Application.Commands.SaleDetail.GetAllSaleDetail;
using PruebaTecnicaImaginemos.Application.Commands.SaleDetail.GetSaleDetail;
using PruebaTecnicaImaginemos.Application.Commands.SaleDetail.PaginationSaleDetail;
using PruebaTecnicaImaginemos.Application.Commands.SaleDetail.UpdateSaleDetail;
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
            var result = await _sender.Send(new GetAllSaleDetailQuery());

            // Verifica si la operación fue exitosa
            if (result.IsSuccess)
            {
                var (details, totalCount) = result.Value; // Desestructura el tuple

                return Ok(new
                {
                    details = details,
                });
            }

            return BadRequest(result.Error);
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

    [HttpPut("UpdateDetail")]
    public async Task<IActionResult> UpdateDetail(SaleDetailDTO2 detaildto, CancellationToken cancellationToken)
    {
        try
        {
            var detail = await _sender.Send(new UpdateSaleDetailCommand(detaildto));

            return Ok(detail);
        }catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("PaginationDetail")]
    public async Task<IActionResult> PaginationDetail([FromQuery] int limit = 12,[FromQuery] int skip = 0)
    {
        try
        {
            var result = await _sender.Send(new PaginationSaleDetailQuery(skip, limit));

            // Verifica si la operación fue exitosa
            if (result.IsSuccess)
            {
                var (details, totalCount) = result.Value; // Desestructura el tuple

                return Ok(new
                {
                    Details = details,
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
