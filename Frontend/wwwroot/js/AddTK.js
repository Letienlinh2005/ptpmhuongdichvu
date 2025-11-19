// ../js/AddTK.js
const API_TK = 'https://localhost:7151/api/taikhoan';

window.initAddTK = function () {
  const form = document.getElementById('form-add-tk');
  const msg  = document.getElementById('tk-add-msg');
  if (!form) return;

  form.onsubmit = async (e) => {
    e.preventDefault();
    msg.textContent = 'Đang lưu...';

    const body = {
      maTaiKhoan:  document.getElementById('maTaiKhoan').value.trim(),
      tenDangNhap: document.getElementById('tenDangNhap').value.trim(),
      matKhau:     document.getElementById('matKhau').value.trim(),
      vaiTro:      document.getElementById('vaiTro').value.trim() || 'BanDoc',
      maBanDoc:    document.getElementById('maBanDoc').value.trim() || null
    };

    try {
      const res = await fetch(API_TK, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(body)
      });
      const data = await res.json().catch(() => ({}));

      if (!res.ok || data.success === false) {
        msg.style.color = 'red';
        msg.textContent = data.message || 'Thêm tài khoản thất bại';
        return;
      }

      msg.style.color = 'green';
      msg.textContent = 'Thêm thành công';
      setTimeout(() => {
        loadPage('../html/TaiKhoan.html', 'initTaiKhoanPage');
      }, 500);
    } catch (err) {
      console.error(err);
      msg.style.color = 'red';
      msg.textContent = 'Lỗi kết nối API';
    }
  };
};
