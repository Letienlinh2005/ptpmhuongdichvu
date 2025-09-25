// ==== Cấu hình API (đổi nếu BE của bạn chạy port khác) ====
const API = "https://localhost:44358/api/sach"; 

// Chuẩn hoá thuộc tính (BE có thể trả PascalCase/camelCase)
const norm = b => ({
    maSach: b.maSach ?? b.MaSach ?? "",
    tenSach: b.tenSach ?? b.TenSach ?? b.tieuDe ?? b.TieuDe ?? "",
    tacGia: b.tacGia ?? b.TacGia ?? "",
    namXuatBan: b.namXuatBan ?? b.NamXuatBan ?? null,
    theLoai: b.theLoai ?? b.TheLoai ?? b.maTheLoai ?? b.MaTheLoai ?? ""
});

// Biến global
let all = []; // dữ liệu gốc (đã normalize)

// DOM elements
const $books = document.getElementById("books");
const $count = document.getElementById("count");
const $error = document.getElementById("error");
const $search = document.getElementById("search-btn");

// Utility functions
function showError(msg) {
    $error.textContent = msg;
    $error.style.display = "block";
}

function clearError() {
    $error.style.display = "none";
    $error.textContent = "";
}

function esc(s) {
    return String(s).replace(/[&<>\"']/g, m => ({
        '&': '&amp;',
        '<': '&lt;',
        '>': '&gt;',
        '"': '&quot;',
        "'": '&#39;'
    }[m]));
}

// Render function
function render() {
    clearError();
    const kw = ($search.value || "").trim().toLowerCase();

    const list = kw
        ? all.filter(x =>
            (x.maSach || "").toLowerCase().includes(kw) ||
            (x.tenSach || "").toLowerCase().includes(kw) ||
            (x.tacGia || "").toLowerCase().includes(kw) ||
            (x.theLoai || "").toLowerCase().includes(kw)
        )
        : all;

    $count.textContent = `Có ${list.length} sách${kw ? ` (lọc theo: "${kw}")` : ""}`;

    if (!list.length) {
        $books.innerHTML = `<div class="muted">Không có dữ liệu để hiển thị.</div>`;
        return;
    }

    // Tạo các thẻ div cho từng cuốn sách
    $books.innerHTML = list.map(x => `
    <div class="book">
      <div class="title">${esc(x.tenSach)}</div>
      <div class="meta">Tác giả: <strong>${esc(x.tacGia)}</strong></div>
      <div class="meta">Năm XB: ${x.namXuatBan ?? "—"}</div>
      <div class="meta">Thể loại: <span class="tag">${esc(x.theLoai || "—")}</span></div>
      <div class="meta">Mã: <code>${esc(x.maSach || "mới")}</code></div>
    </div>
  `).join("");
}

// Load data from API
async function load() {
    $count.textContent = "Đang tải...";
    try {
        const res = await fetch(API, {
            headers: { "Accept": "application/json" }
        });
        if (!res.ok) throw new Error(`HTTP ${res.status}`);
        const data = await res.json();
        all = Array.isArray(data) ? data.map(norm) : [];
        render();
    } catch (err) {
        showError("Không tải được dữ liệu. " + err.message + ". Nếu frontend và API khác domain, hãy bật CORS ở backend.");
        $count.textContent = "Lỗi tải dữ liệu";
    }
}

// Event listeners
function initializeEventListeners() {
    // Lọc theo từ khoá khi gõ vào ô search
    $search.addEventListener("input", render);

    // Có thể thêm các event listener khác ở đây
    // Ví dụ: login button
    const loginBtn = document.getElementById("login-btn");
    if (loginBtn) {
        loginBtn.addEventListener("click", function () {
            // Xử lý đăng nhập
            console.log("Đăng nhập được click");
        });
    }
}

// Initialize application
function init() {
    // Đợi DOM load xong
    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', function () {
            initializeEventListeners();
            load();
        });
    } else {
        initializeEventListeners();
        load();
    }
}

// Khởi động ứng dụng
init();