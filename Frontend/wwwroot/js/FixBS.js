// ../js/FixBS.js

if (!window.API_BANSAO) {
  window.API_BANSAO = 'https://localhost:7151/api/BanSao';
}

window.initFixBS = async function () {
  const id = sessionStorage.getItem('editBS');
  const msg = document.getElementById('bs-fix-msg');

  if (!id) {
    alert('Không có mã bản sao để sửa');
    return;
  }

  try {
    const res = await fetch(`${window.API_BANSAO}/${encodeURIComponent(id)}`, { cache: 'no-store' });
    if (!res.ok) {
      console.error('API lỗi', res.status);
      alert('Không tải được bản sao');
      return;
    }

    let bs = await res.json();
    if (bs && typeof bs === 'object' && bs.data) bs = bs.data;

    fillFixBSForm(bs);
    attachFixBSHandlers();
  } catch (err) {
    console.error(err);
    alert('Lỗi tải dữ liệu bản sao');
  }
};

function fillFixBSForm(bs) {
  if (!bs) return;
  const maBS   = document.getElementById('MaBS');
  const maVach = document.getElementById('MaVach');
  const maSach = document.getElementById('MaSach');
  const maKe   = document.getElementById('MaKe');
  const tt     = document.getElementById('TrangThai');

  if (maBS)   maBS.value   = bs.MaBanSao ?? bs.maBanSao ?? '';
  if (maVach) maVach.value = bs.MaVach   ?? bs.maVach   ?? '';
  if (maSach) maSach.value = bs.MaSach   ?? bs.maSach   ?? '';
  if (maKe)   maKe.value   = bs.MaKe     ?? bs.maKe     ?? '';
  if (tt)     tt.value     = bs.TrangThai?? bs.trangThai?? '';
}

function attachFixBSHandlers() {
  const btnSave = document.getElementById('saveBtn');
  const btnBack = document.getElementById('goBackbtn');
  const msg     = document.getElementById('bs-fix-msg');

  if (btnBack) {
    btnBack.onclick = () => {
      if (typeof loadPage === 'function') {
        loadPage('./html/BanSao.html','initBanSaoPage');
      } else {
        history.back();
      }
    };
  }

  if (btnSave) {
    btnSave.onclick = async () => {
      const maBS   = document.getElementById('MaBS').value.trim();
      const maVach = document.getElementById('MaVach').value.trim();
      const maSach = document.getElementById('MaSach').value.trim();
      const maKe   = document.getElementById('MaKe').value.trim();
      const tt     = document.getElementById('TrangThai').value.trim();

      if (!maBS) { alert('Thiếu mã bản sao'); return; }

      const payload = {
        MaBanSao: maBS,
        MaVach:   maVach,
        MaSach:   maSach,
        MaKe:     maKe,
        TrangThai:tt
      };

      try {
        const res = await fetch(`${window.API_BANSAO}/${encodeURIComponent(maBS)}`, {
          method: 'PUT',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify(payload)
        });

        const text = await res.text();
        if (!res.ok) {
          console.error('PUT lỗi:', res.status, text);
          if (msg) { msg.style.color = 'red'; msg.textContent = 'Lưu thất bại: ' + text; }
          return;
        }

        if (msg) { msg.style.color = 'green'; msg.textContent = 'Lưu bản sao thành công'; }

        setTimeout(() => {
          if (typeof loadPage === 'function') {
            loadPage('../html/BanSao.html','initBanSaoPage');
          }
        }, 500);
      } catch (err) {
        console.error(err);
        if (msg) { msg.style.color = 'red'; msg.textContent = 'Lỗi gọi API'; }
      }
    };
  }
}

// fallback nếu mở standalone
document.addEventListener('DOMContentLoaded', () => {
  if (document.getElementById('Fix') && typeof window.initFixBS === 'function') {
    window.initFixBS();
  }
});
