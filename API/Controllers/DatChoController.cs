using Microsoft.AspNetCore.Mvc;
using MyWebAPI.BLL;
using MyWebAPI.DTO;

[ApiController]
[Route("api/DatCho")]
public class DatChoController : ControllerBase
{
    private readonly IDatChoService _svc;
    public DatChoController(IDatChoService svc) => _svc = svc;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] DatChoCreateRequest req)
    {
        var id = await _svc.TaoDatChoAsync(req);
        var dto = await _svc.GetByIdAsync(id);
        return CreatedAtAction(nameof(GetById), new { id }, dto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
        => (await _svc.GetByIdAsync(id)) is { } dto ? Ok(dto) : NotFound();

    [HttpGet]
    public async Task<IActionResult> List([FromQuery] string? maBanDoc, [FromQuery] string? maSach, [FromQuery] string? trangThai)
        => Ok(await _svc.ListAsync(maBanDoc, maSach, trangThai));

    [HttpPost("ready")]
    public async Task<IActionResult> SetReady([FromBody] DatChoReadyRequest req)
        => Ok(new { maDatChoReady = await _svc.ChuyenSangGiuAsync(req.MaSach, req.GiuTrongGio) });

    [HttpPost("{id}/cancel")]
    public async Task<IActionResult> Cancel(string id)
        => Ok(new { affected = await _svc.HuyAsync(id) });

    [HttpPost("expire")]
    public async Task<IActionResult> Expire()
        => Ok(new { expired = await _svc.HetHanAsync() });
}
