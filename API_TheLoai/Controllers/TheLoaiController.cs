using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_TheLoai.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TheLoaiController : ControllerBase
    {
        private readonly ITheLoaiService _theLoaiService;
        public TheLoaiController(ITheLoaiService theLoaiService)
        {
            _theLoaiService = theLoaiService;
        }

        // GET api/theloai
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _theLoaiService.GetAllAsync();
            if (response.Success)
                return Ok(response);
            return StatusCode(500, response);
        }

        // GET api/theloai/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _theLoaiService.GetByIdAsync(id);
            if (response.Success)
                return Ok(response);
            return NotFound(response);
        }

        // POST api/theloai
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTheLoaiRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var response = await _theLoaiService.CreateAsync(request);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }

        // PUT api/theloai/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateTheLoaiRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var response = await _theLoaiService.UpdateAsync(id, request);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }

        // DELETE api/theloai/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _theLoaiService.DeleteAsync(id);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }
    }
}
