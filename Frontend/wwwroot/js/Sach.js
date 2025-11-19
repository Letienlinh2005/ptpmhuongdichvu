// ./js/Sach.js

// ===== cấu hình API (dùng http giống ảnh bạn chụp) =====
const API_SACH = 'https://localhost:7151/api/sach';

// format nhỏ
const fmt = {
  text(v) {
    return v == null ? '' : String(v);
  },
  tomTat(v) {
    if (!v) return '';
    return v.length > 40 ? v.slice(0, 40) + '...' : v;
  }
};

// gọi API lấy toàn bộ sách
async function fetchSach() {
  const res = await fetch(API_SACH, { cache: 'no-store' });
  if (!res.ok) throw new Error(`HTTP ${res.status}`);
  return res.json();
}

// render vào tbody#sach-body
async function renderSach(params = {}) {
  const tbody = document.getElementById('sach-body');
  if (!tbody) return;

  tbody.innerHTML = `<tr><td colspan="9">Đang tải...</td></tr>`;

  try {
    // API của bạn trả {success, message, data: [...]}
    let data = await fetchSach();
    if (data && Array.isArray(data.data)) {
      data = data.data;
    }

    // lọc ở client nếu có tìm kiếm
    if (params.q) {
      const key = params.q.toLowerCase();
      data = data.filter(s => {
        const ma = (s.maSach ?? '').toLowerCase();
        const td = (s.tieuDe ?? '').toLowerCase();
        const tg = (s.tacGia ?? '').toLowerCase();
        const tl = (s.theLoai ?? '').toLowerCase();
        return (
          ma.includes(key) ||
          td.includes(key) ||
          tg.includes(key) ||
          tl.includes(key)
        );
      });
    }

    if (!data || !data.length) {
      tbody.innerHTML = `<tr><td colspan="9">Không có dữ liệu</td></tr>`;
      return;
    }

    const rows = data.map((s, i) => {
      const maSach    = s.maSach ?? '';
      const tieuDe    = s.tieuDe ?? '';
      const tacGia    = s.tacGia ?? '';
      const theLoai   = s.theLoai ?? '';
      const namXB     = s.namXuatBan ?? '';
      const ngonNgu   = s.ngonNgu ?? '';
      const tomTat    = fmt.tomTat(s.tomTat);

      return `
        <tr>
          <td>${i + 1}</td>
          <td>${maSach}</td>
          <td>${tieuDe}</td>
          <td>${tacGia}</td>
          <td>${theLoai}</td>
          <td>${namXB}</td>
          <td>${ngonNgu}</td>
          <td>${tomTat}</td>
          <td>
            <a class="btn-sm"
               onclick="setEditSach('${maSach}'); loadPage('../html/FixSach.html','initFixSach')">
               Sửa
            </a>
            <a class="btn-sm" data-act="delete-sach" data-id="${maSach}" style="color:red;cursor:pointer">
               Xoá
            </a>
          </td>
        </tr>
      `;
    }).join('');

    tbody.innerHTML = rows;
  } catch (err) {
    console.error(err);
    tbody.innerHTML = `<tr><td colspan="9" style="color:red">Lỗi tải dữ liệu</td></tr>`;
  }
}

// lưu mã sách để sang trang sửa dùng giống setEditBD
function setEditSach(ma) {
  sessionStorage.setItem('editSach', ma);
}

// ===== gọi sau khi trang Sách được inject vào Admin =====
window.initSachPage = function () {
  renderSach();

  // tìm kiếm giống bạn đọc
  const btnSearch = document.getElementById('en');
  const inpSearch = document.getElementById('search-bd'); // bạn đặt tên sẵn vậy rồi

  if (btnSearch && inpSearch) {
    btnSearch.onclick = () => {
      const q = inpSearch.value.trim();
      renderSach(q ? { q } : {});
    };
  }
  if (inpSearch) {
    inpSearch.onkeyup = (e) => {
      if (e.key === 'Enter') {
        const q = inpSearch.value.trim();
        renderSach(q ? { q } : {});
      }
    };
  }

  // nếu bạn muốn mấy nút All/Active/No-active làm gì đó với sách thì viết thêm ở đây
};
