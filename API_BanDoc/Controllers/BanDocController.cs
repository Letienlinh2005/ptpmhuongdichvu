using Microsoft.AspNetCore.Mvc;
using MyWebAPI.BLL.Services;
using MyWebAPI.DTO;

namespace MyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BanDocController : ControllerBase
    {
        private readonly IBanDocService _banDocService;

        public BanDocController(IBanDocService banDocService)
        {
            _banDocService = banDocService;
        }

        // GET api/bandoc
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _banDocService.GetAllAsync();

            if (response.Success)
                return Ok(response);

            return StatusCode(500, response);
        }

        // GET api/bandoc/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _banDocService.GetByIdAsync(id);

            if (response.Success)
                return Ok(response);

            return NotFound(response);
        }

        // POST api/bandoc
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBanDocRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _banDocService.CreateAsync(request);

            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }

        // PUT api/bandoc/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateBanDocRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _banDocService.UpdateAsync(id, request);

            if (response.Success)
                return Ok(response);

            return NotFound(response);
        }

        // DELETE api/bandoc/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _banDocService.DeleteAsync(id);

            if (response.Success)
                return Ok(response);

            return NotFound(response);
        }
    }
}