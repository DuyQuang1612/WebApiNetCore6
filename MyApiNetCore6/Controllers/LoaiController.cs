using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApiNetCore6.Data;
using MyApiNetCore6.Models;
using MyApiNetCore6.Services;

namespace MyApiNetCore6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaiController : ControllerBase
    {
        private readonly ILoaiRepository _loaiRepository;
        public LoaiController(ILoaiRepository loaiRepository)
        {
            _loaiRepository = loaiRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_loaiRepository.GetAll());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var loai = _loaiRepository.GetById(id);
                if (loai != null)
                {
                    return Ok(loai);
                }
                else
                    return NotFound();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpPost]
        public IActionResult Add(LoaiModel model)
        {
            try
            {
                return Ok(_loaiRepository.Add(model));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }



        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, LoaiVM loai)
        {
            if(id != loai.MaLoai)
            {
                return BadRequest();
            }
            try
            {
                _loaiRepository.Update(loai);
                return NoContent();

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);   
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _loaiRepository.Delete(id);
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
