using Microsoft.AspNetCore.Mvc;
using MyWebAPI.BLL.Services;
using MyWebAPI.DTO;

namespace API_PhieuMuon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhieuMuonController : ControllerBase
    {
        private readonly IPhieuMuonService _phieuMuonService;
        public PhieuMuonController(IPhieuMuonService phieuMuonService)
        {
            _phieuMuonService = phieuMuonService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _phieuMuonService.GetAllAsync();

            if (response.Success)
                return Ok(response);
            return StatusCode(500, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _phieuMuonService.GetByIdAsync(id);
            if (response.Success)
                return Ok(response);
            return NotFound(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePhieuMuonRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var response = await _phieuMuonService.CreateAsync(request);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdatePhieuMuonRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var response = await _phieuMuonService.UpdateAsync(id, request);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _phieuMuonService.DeleteAsync(id);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }
    }
}