using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebAPI.Models;



namespace MyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SachController : ControllerBase
    {
        public static List<SachVM> sachs = new List<SachVM>();

        [HttpGet]

        public IActionResult getAll()
        {
            return Ok(sachs);
        }

        [HttpGet("{id}")]
        public IActionResult getById(Guid id)
        {
            try
            {
                var sach = sachs.FirstOrDefault(c => c.MaSach == id);
                if (sach == null)
                {
                    return NotFound(new
                    {
                        message = "Không tìm thấy sách"
                    });
                }
                return Ok(sach);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpPost]

        public IActionResult Create(SachVM sachVM)
        {
            var Sach = new SachVM()
            {
                MaSach = Guid.NewGuid(),
                TenSach = sachVM.TenSach,
                TacGia = sachVM.TacGia,
                NamXuatBan = sachVM.NamXuatBan,
                TheLoai = sachVM.TheLoai,
            };
            sachs.Add(Sach);
            return Ok(new
            {
                message = "Thêm thành công",
                data = Sach
            });
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, SachVM sachVM)
        {
            try
            {
                var sach = sachs.FirstOrDefault(c => c.MaSach == id);
                if (sach == null)
                {
                    return NotFound(new
                    {
                        message = "Không tìm thấy sách"
                    });
                }
                if(id != sach.MaSach)
                {
                    return BadRequest();
                }
                sach.TenSach = sachVM.TenSach;
                sach.TacGia = sachVM.TacGia;
                sach.NamXuatBan = sachVM.NamXuatBan;
                sach.TheLoai = sachVM.TheLoai;
                return Ok(new
                {
                    message = "Cập nhật thành công",
                    data = sach
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var sach = sachs.FirstOrDefault(c => c.MaSach == id);
                if (sach == null)
                {
                    return NotFound(new
                    {
                        message = "Không tìm thấy sách"
                    });
                }
                sachs.Remove(sach);
                return Ok(new
                {
                    message = "Xoá thành công",
                    data = sach
                });
            }
            catch(Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }
    }
}
