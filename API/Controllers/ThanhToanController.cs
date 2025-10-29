using Microsoft.AspNetCore.Mvc;
using MyWebAPI.BLL.Services;
using MyWebAPI.DTO;
using System.Threading.Tasks;

namespace MyWebAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThanhToanController : ControllerBase
    {
        private readonly IThanhToanService _service;
        public ThanhToanController(IThanhToanService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var res = await _service.GetAllAsync();
            return res.Success ? Ok(res) : StatusCode(500, res);
        }

        [HttpGet("{maThanhToan}")]
        public async Task<IActionResult> GetById(string maThanhToan)
        {
            var res = await _service.GetByIdAsync(maThanhToan);
            if (!res.Success && res.Data == null) return NotFound(res);
            return Ok(res);
        }

        // POST: /api/ThanhToan/Phat
        [HttpPost("Phat")]
        public async Task<IActionResult> ThanhToanPhat([FromBody] ThanhToanPhatDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(new ResponseDTO<bool>
            {
                Success = false,
                Message = "Dữ liệu không hợp lệ",
                Data = false
            });

            var res = await _service.ThanhToanPhatAsync(dto);
            return res.Success ? Ok(res) : BadRequest(res);
        }
    }
}
