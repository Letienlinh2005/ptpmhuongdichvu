using Microsoft.AspNetCore.Mvc;
using MyWebAPI.BLL.Services;
using MyWebAPI.DTO;

namespace MyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SachController : ControllerBase
    {
        private readonly ISachService _sachService;

        public SachController(ISachService sachService)
        {
            _sachService = sachService;
        }

        // GET api/sach
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _sachService.GetAllAsync();

            if (response.Success)
                return Ok(response);

            return StatusCode(500, response);
        }

        // GET api/sach/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _sachService.GetByIdAsync(id);

            if (response.Success)
                return Ok(response);

            return NotFound(response);
        }

        // POST api/sach
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSachRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _sachService.CreateAsync(request);

            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }

        // PUT api/sach/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateSachRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _sachService.UpdateAsync(id, request);

            if (response.Success)
                return Ok(response);

            return NotFound(response);
        }

        // DELETE api/sach/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _sachService.DeleteAsync(id);

            if (response.Success)
                return Ok(response);

            return NotFound(response);
        }
    }
}