// ===== cấu hình API =====
const API_BANDOC = 'https://localhost:7151/api/BanDoc'; // dùng http nếu chưa trust cert

// ===== helpers nhỏ =====
const fmtDate = (v) => {
  if (!v) return '';
  const d = new Date(v);
  if (isNaN(d)) return v; // nếu backend trả sẵn "dd/MM/yyyy" thì giữ nguyên
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
  const tbody = document.getElementById('bd-body');      // <-- đúng body bạn cần
  if (!tbody) return;                                    // phòng khi DOM chưa gắn

  tbody.innerHTML = `<tr><td colspan="10">Đang tải...</td></tr>`;
  try {
    let data = await fetchBanDoc(params);
    // Nếu API trả {data:[...]} thì bóc ra
    if (data && Array.isArray(data.data)) data = data.data;

    if (!Array.isArray(data) || data.length === 0) {
      tbody.innerHTML = `<tr><td colspan="10">Không có dữ liệu</td></tr>`;
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
            <button class="btn-sm" data-act="edit"   data-id="${ma}">Sửa</button>
            <button class="btn-sm" data-act="delete" data-id="${ma}">Xoá</button>
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
  // lần đầu: tải tất cả
  renderBanDoc();

  // hook tìm kiếm & filter nếu có
  const btnSearch = document.getElementById('en');
  const inpSearch = document.getElementById('search-bd');
  if (btnSearch && inpSearch) {
    btnSearch.onclick = () => {
      const q = inpSearch.value.trim();
      renderBanDoc(q ? { q } : {}); // đổi key param theo API nếu cần
    };
  }

  const btnAll = document.getElementById('All');
  const btnActive = document.getElementById('Active');
  const btnNoActive = document.getElementById('No-active');
  if (btnAll)      btnAll.onclick = () => renderBanDoc();
  if (btnActive)   btnActive.onclick = () => renderBanDoc({ status: 'active' });   // đổi theo API thật
  if (btnNoActive) btnNoActive.onclick = () => renderBanDoc({ status: 'inactive' });
};
// Bản sao
