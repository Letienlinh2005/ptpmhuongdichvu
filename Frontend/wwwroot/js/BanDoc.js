// ===== cấu hình API =====
const API_BANDOC = 'https://localhost:7151/api/BanDoc'; 
const API = window.API_BANDOC;


const fmtDate = (v) => {
  if (!v) return '';
  const d = new Date(v);
  if (isNaN(d)) return v; 
  return `${String(d.getDate()).padStart(2,'0')}/${String(d.getMonth()+1).padStart(2,'0')}/${d.getFullYear()}`;
};
const fmtMoney = (n) => {
  const x = Number(n);
  return isNaN(x) ? (n ?? '') : x.toLocaleString('vi-VN') + ' ₫';
};
const fmtStatus = (s) => {
  if (s === true || s === 1 || s === '1') return 'Hoạt động';
  if (s === false || s === 0 || s === '0') return 'Không hoạt động';
  return String(s ?? '');
};

// ===== gọi API =====
async function fetchBanDoc(params = {}) {
  const url = new URL(API_BANDOC);
  Object.entries(params).forEach(([k, v]) => {
    if (v !== undefined && v !== null && v !== '') url.searchParams.append(k, v);
  });
  const res = await fetch(url, { cache: 'no-store' });
  if (!res.ok) throw new Error(`HTTP ${res.status}: ` + await res.text());
  return res.json();
}

// ===== render vào đúng tbody#bd-body =====
async function renderBanDoc(params = {}) {
  const tbody = document.getElementById('bd-body');
  if (!tbody) return;

  tbody.innerHTML = `<tr><td colspan="10">Đang tải...</td></tr>`;

  try {
    // luôn gọi API lấy hết
    let data = await fetchBanDoc();  // 👈 không truyền params lên server nữa

    // nếu API bọc trong .data
    if (data && Array.isArray(data.data)) data = data.data;

    if (!Array.isArray(data) || data.length === 0) {
      tbody.innerHTML = `<tr><td colspan="10">Không có dữ liệu</td></tr>`;
      return;
    }

    // nếu có params.q thì lọc ở đây
    if (params.q) {
      const key = params.q.toLowerCase();
      data = data.filter(bd => {
        const ma    = (bd.maBanDoc ?? bd.ma ?? bd.maBD ?? '').toString().toLowerCase();
        const soThe = (bd.soThe ?? '').toString().toLowerCase();
        const hoTen = (bd.hoTen ?? bd.ten ?? bd.fullName ?? '').toString().toLowerCase();
        const email = (bd.email ?? '').toString().toLowerCase();
        const sdt   = (bd.dienThoai ?? bd.sdt ?? '').toString().toLowerCase();
        return (
          ma.includes(key) ||
          soThe.includes(key) ||
          hoTen.includes(key) ||
          email.includes(key) ||
          sdt.includes(key)
        );
      });
    }

    if (data.length === 0) {
      tbody.innerHTML = `<tr><td colspan="10">Không có dữ liệu phù hợp</td></tr>`;
      return;
    }

    const rows = data.map((bd, i) => {
      const ma        = bd.maBanDoc ?? bd.ma ?? bd.maBD ?? '';
      const soThe     = bd.soThe ?? '';
      const hoTen     = bd.hoTen ?? bd.ten ?? bd.fullName ?? '';
      const email     = bd.email ?? '';
      const sdt       = bd.dienThoai ?? bd.sdt ?? '';
      const hanThe    = fmtDate(bd.hanThe ?? bd.ngayHetHan);
      const trangThai = fmtStatus(bd.trangThaiThe ?? bd.trangThai);
      const duNo      = fmtMoney(bd.duNo ?? bd.soTienNo ?? 0);

      return `
        <tr>
          <td>${i + 1}</td>
          <td>${ma}</td>
          <td>${soThe}</td>
          <td>${hoTen}</td>
          <td>${email}</td>
          <td>${sdt}</td>
          <td>${hanThe}</td>
          <td>${trangThai}</td>
          <td>${duNo}</td>
          <td>
            <a
              class="btn-sm btn-edit"
              data-id="${ma}"
              onclick="setEditBD('${ma}'); loadPage('../html/FixBD.html','initFixBD')"
            >Sửa</a>
            <a class="btn-sm" data-act="delete" data-id="${ma}">Xoá</a>
          </td>
        </tr>
      `;
    }).join('');

    tbody.innerHTML = rows;
  } catch (e) {
    console.error(e);
    tbody.innerHTML = `<tr><td colspan="10" style="color:red">Lỗi tải dữ liệu</td></tr>`;
  }
}


// ===== gọi sau khi trang con được inject vào Admin =====
window.initReaderPage = function () {
  renderBanDoc();

  const btnSearch = document.getElementById('en');
  const inpSearch = document.getElementById('search-bd');

  if (btnSearch && inpSearch) {
    btnSearch.onclick = () => {
      const q = inpSearch.value.trim();
      renderBanDoc(q ? { q } : {});
    };
  }

  if (inpSearch) {
    inpSearch.onkeyup = (e) => {
      if (e.key === 'Enter') {
        const q = inpSearch.value.trim();
        renderBanDoc(q ? { q } : {});
      }
    };
  }

  // mấy nút lọc
  const btnAll = document.getElementById('All');
  const btnActive = document.getElementById('Active');
  const btnNoActive = document.getElementById('No-active');
  if (btnAll)      btnAll.onclick    = () => renderBanDoc();
  if (btnActive)   btnActive.onclick = () => renderBanDoc({ status: 'active' });
  if (btnNoActive) btnNoActive.onclick = () => renderBanDoc({ status: 'inactive' });
  
};
// Sửa Bạn đọc 
function getQueryID() {
  const p = new URLSearchParams(window.location.search)
  return p.get('id')
}

async function loadDetail(ma) {
  const res = await fetch(`${API_BANDOC}/${encodeURIComponent(ma)}`);
  if (!res.ok) throw new Error('Không tải đc hehehe');
  return res.json();
} 
var maBD = document.getElementById('MaBD')
var soThe = document.getElementById('Sothe')
var hoTen = document.getElementById('Hoten')
var Email = document.getElementById('Email')
var sodt = document.getElementById('sodt')
var hanthe = document.getElementById('hanthe')
var trangThai = document.getElementById('TrangThai')
var duNo = document.getElementById('DuNo')


function fillForm(bd) {
  maBD.value = bd.MaBanDoc ?? '';
  soThe.value = bd.SoThe ?? '';
  hoTen.value = bd.HoTen ?? '';
  Email.value = bd.Email ?? '';
  sodt.value = bd.DienThoai ?? '';
  hanthe.value = (bd.HanThe || '').slice(0,10);
  trangThai.value = bd.trangThaiThe ?? 'Active';
  duNo.value = bd.DuNo ?? 0;
}
async function init() {
  const id = getQueryId();
  if (!id) { alert('Thiếu id bạn đọc'); return; }
  try {
    const data = await loadDetail(id);
    fillForm(data);
  } catch (e) {
    console.error(e);
    alert('Lỗi tải dữ liệu');
  }
}
