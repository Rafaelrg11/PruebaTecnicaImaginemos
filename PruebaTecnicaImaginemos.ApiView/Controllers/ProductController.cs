using MediatR;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaImaginemos.Application.Commands.Product.CreateProduct;
using PruebaTecnicaImaginemos.Application.Commands.Product.DeleteProduct;
using PruebaTecnicaImaginemos.Application.Commands.Product.GetAllProduct;
using PruebaTecnicaImaginemos.Application.Commands.Product.GetProduct;
using PruebaTecnicaImaginemos.Application.Commands.User.PaginationUser;
using PruebaTecnicaImaginemos.Domain.DTOs.Product;

namespace PruebaTecnicaImaginemos.ApiView.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : Controller
{
    private readonly ISender _sender;

    public ProductController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("CreateProduct")]
    public async Task<IActionResult> CreateProduct(ProductDTO2 productdto)
    {
        try
        {
            var product = await _sender.Send(new CreateProductCommand(productdto.NameProduct, 
                productdto.Price, productdto.Description));

            return Ok(product);
        }
        catch (Exception ex) 
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("GetAllProducts")]
    public async Task<IActionResult> GetAllProducts()
    {
        try
        {
            var product = await _sender.Send(new GetProductsQuery());

            return Ok(product);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("GetProduct")]
    public async Task<IActionResult> GetProduct(Guid guid)
    {
        try
        {
            var product = await _sender.Send(new GetProductQuery(guid));

            return Ok(product);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("PaginationProduct")]
    public async Task<IActionResult> PaginationProduct(int limit = 12, int skip = 0)
    {
        try
        {
            var product = await _sender.Send(new PaginationUserQuery(limit, skip));

            return Ok(product);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("DeleteProduct")]
    public async Task<IActionResult> DeleteProduct(Guid guid)
    {
        try
        {
            var product = await _sender.Send(new DeleteProductCommand(guid));

            return Ok(product);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
