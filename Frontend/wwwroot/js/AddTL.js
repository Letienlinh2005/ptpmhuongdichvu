// ../js/AddTL.js
const API_THE_LOAI = 'https://localhost:7151/api/theloai';

window.initAddTL = function () {
  const form = document.getElementById('form-add-tl');
  const msg  = document.getElementById('tl-add-msg');
  if (!form) return;

  form.onsubmit = async (e) => {
    e.preventDefault();
    msg.textContent = 'Đang lưu...';

    const body = {
      maTheLoai:  document.getElementById('maTheLoai').value.trim(),
      tenTheLoai: document.getElementById('tenTheLoai').value.trim(),
      moTa:       document.getElementById('moTa').value.trim() || null
    };

    try {
      const res = await fetch(API_THE_LOAI, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(body)
      });
      const data = await res.json().catch(() => ({}));

      if (!res.ok || data.success === false) {
        msg.style.color = 'red';
        msg.textContent = data.message || 'Thêm thể loại thất bại';
        return;
      }

      msg.style.color = 'green';
      msg.textContent = 'Thêm thành công';
      setTimeout(() => {
        loadPage('../html/TheLoai.html', 'initTheLoaiPage');
      }, 500);
    } catch (err) {
      console.error(err);
      msg.style.color = 'red';
      msg.textContent = 'Lỗi kết nối API';
    }
  };
};
