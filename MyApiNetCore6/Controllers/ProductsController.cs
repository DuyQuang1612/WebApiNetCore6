using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApiNetCore6.Repositories;

namespace MyApiNetCore6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IHangHoaRepository _hanghoarepository;
        public ProductsController(IHangHoaRepository hanghoarepository)
        {
            _hanghoarepository = hanghoarepository;
        }

        [HttpGet]
        public IActionResult GetAllProducts(string? search, double? from, double? to, string? sortBy, int page = 1)
        {
            try
            {
                var Products = _hanghoarepository.GetAll(search, from, to, sortBy, page);
                return Ok(Products);
            }
            catch (Exception)   
            {

                return BadRequest("We cant get Products");
            }
        }
    }
}
