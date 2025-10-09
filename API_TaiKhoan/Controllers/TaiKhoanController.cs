using Microsoft.AspNetCore.Mvc;
using MyWebAPI.BLL.Services;
using MyWebAPI.DTO;

namespace MyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaiKhoanController : ControllerBase
    {
        private readonly ITaiKhoanService _taiKhoanService;

        public TaiKhoanController(ITaiKhoanService taiKhoanService)
        {
            _taiKhoanService = taiKhoanService;
        }

        // GET api/taikhoan
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _taiKhoanService.GetAllAsync();

            if (response.Success)
                return Ok(response);

            return StatusCode(500, response);
        }

        // GET api/taikhoan/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _taiKhoanService.GetByIdAsync(id);

            if (response.Success)
                return Ok(response);

            return NotFound(response);
        }

        // POST api/taikhoan
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTaiKhoanRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _taiKhoanService.CreateAsync(request);

            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }

        // PUT api/taikhoan/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateTaiKhoanRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _taiKhoanService.UpdateAsync(id, request);

            if (response.Success)
                return Ok(response);

            return NotFound(response);
        }

        // DELETE api/taikhoan/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _taiKhoanService.DeleteAsync(id);

            if (response.Success)
                return Ok(response);

            return NotFound(response);
        }
    }
}