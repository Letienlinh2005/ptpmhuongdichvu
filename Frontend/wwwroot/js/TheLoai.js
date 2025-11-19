// ../js/TheLoai.js
const API_THE_LOAI = 'https://localhost:7151/api/theloai';

window.initTheLoaiPage = function () {
  const tbody     = document.getElementById('tl-body');
  const btnSearch = document.getElementById('en');
  const inpSearch = document.getElementById('search-bd');
  if (!tbody) return;

  let allTL = [];

  const normalize = (p) => {
    if (Array.isArray(p)) return p;
    if (p && Array.isArray(p.data)) return p.data;
    return p ? [p] : [];
  };

  const render = (rows) => {
    if (!rows || !rows.length) {
      tbody.innerHTML = '<tr><td colspan="5" style="text-align:center">Không có dữ liệu</td></tr>';
      return;
    }

    tbody.innerHTML = rows.map((x, i) => {
      const ma  = x.maTheLoai ?? x.MaTheLoai ?? '';
      const ten = x.tenTheLoai ?? x.TenTheLoai ?? '';
      const mo  = x.moTa ?? x.MoTa ?? '';

      return `
        <tr data-id="${ma}">
          <td>${i + 1}</td>
          <td>${ma}</td>
          <td>${ten}</td>
          <td>${mo}</td>
          <td>
            <button class="btn-sm" data-act="edit" data-id="${ma}">Sửa</button>
            <button class="btn-sm" data-act="delete" data-id="${ma}">Xoá</button>
          </td>
        </tr>
      `;
    }).join('');
  };

  const doSearch = () => {
    const key = (inpSearch?.value || '').trim().toLowerCase();
    if (!key) {
      render(allTL);
      return;
    }
    const filtered = allTL.filter(x => {
      const ma  = (x.maTheLoai ?? x.MaTheLoai ?? '').toLowerCase();
      const ten = (x.tenTheLoai ?? x.TenTheLoai ?? '').toLowerCase();
      return ma.includes(key) || ten.includes(key);
    });
    render(filtered);
  };

  fetch(API_THE_LOAI)
    .then(r => { if (!r.ok) throw new Error(`HTTP ${r.status}`); return r.json(); })
    .then(p => {
      allTL = normalize(p);
      render(allTL);
    })
    .catch(err => {
      console.error(err);
      tbody.innerHTML = '<tr><td colspan="5" style="text-align:center;color:#c00">Lỗi tải dữ liệu</td></tr>';
    });

  if (btnSearch) btnSearch.onclick = doSearch;
  if (inpSearch) inpSearch.onkeyup = (e) => { if (e.key === 'Enter') doSearch(); };

  tbody.onclick = (e) => {
    const btn = e.target.closest('button[data-act]');
    if (!btn) return;
    const id = btn.dataset.id;

    if (btn.dataset.act === 'edit') {
      sessionStorage.setItem('editTL', id);
      if (typeof loadPage === 'function') {
        loadPage('../html/FixTL.html', 'initFixTL');
      }
    }

    if (btn.dataset.act === 'delete') {
      if (typeof window.deleteTL === 'function') {
        window.deleteTL(id, () => {
          fetch(API_THE_LOAI)
            .then(r => r.json())
            .then(p => {
              allTL = normalize(p);
              doSearch();
            });
        });
      }
    }
  };
};
