// ../js/AddBS.js

if (!window.API_BANSAO) {
  window.API_BANSAO = 'https://localhost:7151/api/BanSao';
}

window.initAddBS = function () {
  const form   = document.getElementById('form-add-bs');
  const msg    = document.getElementById('bs-add-msg');
  const backBtn= document.getElementById('goBackbtn');

  if (!form) return;

  if (backBtn) {
    backBtn.onclick = () => {
      if (typeof loadPage === 'function') {
        loadPage('./html/BanSao.html','initBanSaoPage');
      } else {
        window.history.back();
      }
    };
  }

  form.onsubmit = async (e) => {
    e.preventDefault();
    if (msg) { msg.style.color = 'black'; msg.textContent = 'Đang lưu...'; }

    const body = {
      MaBanSao: document.getElementById('MaBS').value.trim(),
      MaVach:   document.getElementById('MaVach').value.trim(),
      MaSach:   document.getElementById('MaSach').value.trim(),
      MaKe:     document.getElementById('MaKe').value.trim(),
      TrangThai:document.getElementById('TrangThai').value.trim()
    };

    if (!body.MaBanSao || !body.MaVach || !body.MaSach || !body.MaKe || !body.TrangThai) {
      if (msg) {
        msg.style.color = 'red';
        msg.textContent = 'Vui lòng nhập đầy đủ thông tin';
      }
      return;
    }

    try {
      const res = await fetch(window.API_BANSAO, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(body)
      });

      const data = await res.json().catch(() => ({}));
      if (!res.ok || data.success === false) {
        if (msg) {
          msg.style.color = 'red';
          msg.textContent = data.message || 'Thêm bản sao thất bại';
        }
        return;
      }

      if (msg) {
        msg.style.color = 'green';
        msg.textContent = 'Thêm bản sao thành công';
      }

      setTimeout(() => {
        if (typeof loadPage === 'function') {
          loadPage('../html/BanSao.html','initBanSaoPage');
        }
      }, 500);
    } catch (err) {
      console.error(err);
      if (msg) {
        msg.style.color = 'red';
        msg.textContent = 'Lỗi kết nối API';
      }
    }
  };
};

// fallback nếu mở standalone
document.addEventListener('DOMContentLoaded', () => {
  if (document.getElementById('form-add-bs') && typeof window.initAddBS === 'function') {
    window.initAddBS();
  }
});
