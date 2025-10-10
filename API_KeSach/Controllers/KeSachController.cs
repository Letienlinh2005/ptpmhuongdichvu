﻿using Microsoft.AspNetCore.Mvc;
using MyWebAPI.BLL.Services;
using MyWebAPI.DTO;

namespace MyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KeSachController : ControllerBase
    {
        private readonly IKeSachService _keSachService;

        public KeSachController(IKeSachService keSachService)
        {
            _keSachService = keSachService;
        }

        // GET api/kesach
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _keSachService.GetAllAsync();
            return response.Success ? Ok(response) : StatusCode(500, response);
        }

        // GET api/kesach/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _keSachService.GetByIdAsync(id);
            return response.Success ? Ok(response) : NotFound(response);
        }

        // POST api/kesach
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateKeSachRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var response = await _keSachService.CreateAsync(request);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        // PUT api/kesach/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateKeSachRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var response = await _keSachService.UpdateAsync(id, request);
            return response.Success ? Ok(response) : NotFound(response);
        }

        // DELETE api/kesach/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _keSachService.DeleteAsync(id);
            return response.Success ? Ok(response) : NotFound(response);
        }
    }
}