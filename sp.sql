-- Store Procedure Sach
CREATE PROCEDURE sp_GetAllSach
AS
BEGIN
    SELECT MaSach, TieuDe, TacGia, NamXuatBan, MaTheLoai
    FROM Sach;
END
GO

CREATE PROCEDURE sp_GetSachById
    @MaSach NVARCHAR(20)
AS
BEGIN
    SELECT MaSach, TieuDe, TacGia, NamXuatBan, MaTheLoai
    FROM Sach
    WHERE MaSach = @MaSach;
END
GO
CREATE PROCEDURE sp_InsertSach
    @MaSach NVARCHAR(20),
    @TieuDe NVARCHAR(255),
    @TacGia NVARCHAR(255),
    @NamXuatBan INT = NULL,
    @MaTheLoai NVARCHAR(20) = NULL
AS
BEGIN
    INSERT INTO Sach (MaSach, TieuDe, TacGia, NamXuatBan, MaTheLoai)
    VALUES (@MaSach, @TieuDe, @TacGia, @NamXuatBan, @MaTheLoai);
END
GO
CREATE PROCEDURE sp_UpdateSach
    @MaSach NVARCHAR(20),
    @TieuDe NVARCHAR(255),
    @TacGia NVARCHAR(255),
    @NamXuatBan INT = NULL,
    @MaTheLoai NVARCHAR(20) = NULL
AS
BEGIN
    UPDATE Sach
    SET TieuDe = @TieuDe,
        TacGia = @TacGia,
        NamXuatBan = @NamXuatBan,
        MaTheLoai = @MaTheLoai
    WHERE MaSach = @MaSach;
END
GO
CREATE PROCEDURE sp_DeleteSach
    @MaSach NVARCHAR(20)
AS
BEGIN
    DELETE FROM Sach
    WHERE MaSach = @MaSach;
END
GO
-- SP TaiKhoan
CREATE PROCEDURE sp_GetAllTaiKhoan
AS 
BEGIN
	SELECT MaTaiKhoan, TenDangNhap, MatKhau, VaiTro
	FROM TaiKhoan
END
GO
CREATE PROCEDURE sp_Register
	@MaTaiKhoan NVARCHAR(20),
	@TenDangNhap NVARCHAR(100),
	@MatKhau NVARCHAR(255),
	@VaiTro NVARCHAR(20)
AS
BEGIN
	INSERT INTO TaiKhoan(MaTaiKhoan, TenDangNhap, MatKhau, VaiTro)
	VALUES (@MaTaiKhoan, @TenDangNhap, @MatKhau, @VaiTro)
END
GO
-- SP BanDoc
CREATE PROCEDURE sp_GetAllBanDoc
AS
BEGIN
	SELECT MaBanDoc, HoTen, SoThe, Email, DienThoai, HanThe, TrangThaiThe, DuNo
	FROM BanDoc
END
GO
CREATE PROCEDURE sp_GetBanDocById
	@MaBanDoc NVARCHAR(20)
AS
BEGIN
	SELECT MaBanDoc, HoTen, SoThe, Email, DienThoai, HanThe, TrangThaiThe, DuNo
	FROM BanDoc
	WHERE MaBanDoc = @MaBanDoc
END
GO
CREATE PROCEDURE sp_CreateBanDoc
	@MaBanDoc NVARCHAR(20),
	@SoThe NVARCHAR(10),
	@HoTen NVARCHAR(100),
	@Email NVARCHAR(100),
	@DienThoai NVARCHAR(10),
	@HanThe Date,
	@TrangThaiThe NVARCHAR(20),
	@DuNo DECIMAL(12, 2)
AS
BEGIN
	INSERT INTO BanDoc(MaBanDoc, SoThe, HoTen, Email, DienThoai, HanThe, TrangThaiThe, DuNo)
	VALUES(@MaBanDoc, @SoThe, @HoTen, @Email, @DienThoai, @HanThe, @TrangThaiThe, @DuNo)
END
GO
CREATE PROCEDURE sp_UpdateBanDoc
	@MaBanDoc NVARCHAR(20),
	@HoTen NVARCHAR(100),
	@Email NVARCHAR(100),
	@DienThoai NVARCHAR(10),
	@HanThe Date,
	@TrangThaiThe NVARCHAR(20),
	@DuNo DECIMAL(12, 2)
AS
BEGIN
	UPDATE BanDoc
	SET HoTen = @HoTen,
		Email = @Email,
		DienThoai = @DienThoai,
		HanThe = @HanThe,
		TrangThaiThe = @TrangThaiThe,
		DuNo = @DuNo
	WHERE MaBanDoc = @MaBanDoc;
END
GO
CREATE PROCEDURE sp_DeleteBanDoc
	@MaBanDoc NVARCHAR(20)
AS
BEGIN
	DELETE FROM BanDoc
	WHERE MaBanDoc = @MaBanDoc;
END
GO
-- Đăng nhập
CREATE PROCEDURE sp_Login
  @TenDangNhap NVARCHAR(100)
AS
BEGIN
  SET NOCOUNT ON;
  SELECT TOP (1)
    MaTaiKhoan,
    TenDangNhap,
    VaiTro,
    MatKhau          
  FROM dbo.TaiKhoan
  WHERE TenDangNhap = @TenDangNhap;
END
GO
-- Bản sao
CREATE PROCEDURE sp_GetAllBanSao
AS
BEGIN
	SELECT MaBanSao, MaVach, MaSach, MaKe, TrangThai
	FROM BanSao
END
GO
CREATE PROCEDURE sp_GetBanSaoById
	@MaBanSao NVARCHAR(20)
AS
BEGIN
	SELECT MaBanSao, MaVach, MaSach, MaKe, TrangThai
	FROM BanSao
	WHERE MaBanSao = @MaBanSao
END
GO
CREATE PROCEDURE sp_InsertBanSao
	@MaBanSao NVARCHAR(20),
	@MaVach NVARCHAR(64),
	@MaSach NVARCHAR(20),
	@MaKe NVARCHAR(20),
	@TrangThai NVARCHAR(20)
AS
BEGIN
	INSERT INTO BanSao(MaBanSao, MaVach, MaSach, MaKe, TrangThai)
	VALUES(@MaBanSao,@MaVach, @MaSach, @MaKe, @TrangThai)
END

CREATE PROCEDURE sp_UpdateBanSao
	@MaBanSao NVARCHAR(20),
	@MaVach NVARCHAR(64),
	@MaSach NVARCHAR(20),
	@MaKe NVARCHAR(20),
	@TrangThai NVARCHAR(20)
AS
BEGIN
	UPDATE BanSao
	SET MaBanSao = @MaBanSao,
		MaVach = @MaVach,
		MaSach = @MaSach,
		MaKe = @MaKe,
		TrangThai = @TrangThai
	WHERE MaBanSao = @MaBanSao;
END
GO
CREATE PROCEDURE sp_DeleteBanSao
	@MaBanSao NVARCHAR(20)
AS
BEGIN
	DELETE FROM BanSao
	WHERE MaBanSao = @MaBanSao;
END
GO
CREATE PROCEDURE sp_GetAllPhieuMuon
AS
BEGIN
	SELECT MaPhieuMuon, MaBanSao, MaBanDoc, NgayMuon, HanTra, NgayTra, SoLanGiaHan, TrangThai
	FROM PhieuMuon
END
GO
CREATE PROCEDURE sp_GetPhieuMuonById
	@MaPhieuMuon NVARCHAR(20)
AS
BEGIN
	SELECT MaPhieuMuon, MaBanSao, MaBanDoc, NgayMuon, HanTra, NgayTra, SoLanGiaHan, TrangThai
	FROM PhieuMuon
	WHERE MaPhieuMuon = @MaPhieuMuon
END
GO
CREATE PROCEDURE sp_CreatePhieuMuon
	@MaPhieuMuon NVARCHAR(20),
	@MaBanSao NVARCHAR(20),
	@MaBanDoc NVARCHAR(20),
	@NgayMuon DATE,
	@HanTra DATE,
	@NgayTra DATE,
	@SoLanGiaHan INT,
	@TrangThai NVARCHAR(10)
AS
BEGIN
	INSERT INTO PhieuMuon(MaPhieuMuon, MaBanSao, MaBanDoc, NgayMuon, HanTra, NgayTra, SoLanGiaHan, TrangThai)
	VALUES(@MaPhieuMuon,@MaBanSao, @MaBanDoc, @NgayMuon, @HanTra,@NgayTra,@SoLanGiaHan,@TrangThai)
END
GO
CREATE PROCEDURE sp_UpdatePhieuMuon
	@MaPhieuMuon NVARCHAR(20),
	@MaBanSao NVARCHAR(20),
	@MaBanDoc NVARCHAR(20),
	@NgayMuon DATE,
	@HanTra DATE,
	@NgayTra DATE,
	@SoLanGiaHan INT,
	@TrangThai NVARCHAR(10)
AS
BEGIN
	UPDATE PhieuMuon
	SET MaPhieuMuon = @MaPhieuMuon,
		MaBanSao = @MaBanSao,
		MaBanDoc = @MaBanDoc,
		NgayMuon = @NgayMuon,
		HanTra = @HanTra,
		NgayTra= @NgayTra,
		SoLanGiaHan = @SoLanGiaHan,
		TrangThai = @TrangThai
	WHERE MaPhieuMuon = @MaPhieuMuon;
END
GO
CREATE PROCEDURE sp_GetAllTheLoai
AS
BEGIN
	SELECT MaTheLoai, TenTheLoai, MoTa
	FROM TheLoai
END
GO
CREATE PROCEDURE sp_GetTheLoaiById
	@MaTheLoai NVARCHAR(20)
AS
BEGIN
	SELECT MaTheLoai, TenTheLoai, MoTa
	FROM TheLoai
	WHERE MaTheLoai = @MaTheLoai
END
GO
CREATE PROCEDURE sp_InsertTheLoai
	@MaTheLoai NVARCHAR(20) ,
	@TenTheLoai NVARCHAR(100),
	@MoTa NVARCHAR(255) 
AS
BEGIN
	INSERT INTO TheLoai(MaTheLoai, TenTheLoai, MoTa)
	VALUES(@MaTheLoai, @TenTheLoai, @MoTa)
END
GO
CREATE PROCEDURE sp_UpdateTheLoai
	@MaTheLoai NVARCHAR(20) ,
	@TenTheLoai NVARCHAR(100),
	@MoTa NVARCHAR(255) 
AS
BEGIN
	UPDATE TheLoai
	SET MaTheLoai = @MaTheLoai,
		TenTheLoai = @TenTheLoai,
		MoTa = @MoTa
	WHERE MoTa = @MoTa;
END


EXEC sp_Login letienlinh2005

	



SELECT * FROM TaiKhoan