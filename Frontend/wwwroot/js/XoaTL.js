// ../js/XoaTL.js
const API_THE_LOAI = 'https://localhost:7151/api/theloai';

window.deleteTL = async function (id, onDone) {
  if (!id) return;
  if (!confirm('Xoá thể loại ' + id + '?')) return;

  try {
    const res = await fetch(`${API_THE_LOAI}/${encodeURIComponent(id)}`, {
      method: 'DELETE'
    });
    if (!res.ok) throw new Error('HTTP ' + res.status);
    if (typeof onDone === 'function') onDone();
  } catch (err) {
    console.error(err);
    alert('Xoá thất bại');
  }
};
