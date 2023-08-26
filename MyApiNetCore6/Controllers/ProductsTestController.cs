using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApiNetCore6.Models;
using MyApiNetCore6.Repositories;

namespace MyApiNetCore6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsTestController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        public ProductsTestController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBooksAsync()
        {
            try
            {
                // Ok là phương thức trả về response với HttpStatus là 200
                return Ok(await _bookRepository.GetAllBooksAsync());
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBooksAsync(int id)
        {
            var book = await _bookRepository.GetBooksAsync(id);
            return book == null ? NotFound() : Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> AddBookdAsync(BookModel model)
        {
            try
            {
                var newBookId = await _bookRepository.AddBookdAsync(model);
                var book = await _bookRepository.GetBooksAsync(newBookId);
                return book == null ? NotFound() : Ok(book);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBookdAsync(int id, BookModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }
            await _bookRepository.UpdateBookdAsync(id, model);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBookAsync(int id)
        {
            await _bookRepository.DeleteBookAsync(id);
            return Ok();
        }
    }
}
