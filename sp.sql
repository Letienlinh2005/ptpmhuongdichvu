-- Store Procedure Sach
CREATE PROCEDURE sp_GetAllSach
AS
BEGIN
    SELECT MaSach, TieuDe, TacGia, NamXuatBan, MaTheLoai
    FROM Sach;
END


CREATE PROCEDURE sp_GetSachById
    @MaSach NVARCHAR(20)
AS
BEGIN
    SELECT MaSach, TieuDe, TacGia, NamXuatBan, MaTheLoai
    FROM Sach
    WHERE MaSach = @MaSach;
END

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

CREATE PROCEDURE sp_DeleteSach
    @MaSach NVARCHAR(20)
AS
BEGIN
    DELETE FROM Sach
    WHERE MaSach = @MaSach;
END

-- SP TaiKhoan
CREATE PROCEDURE sp_GetAllTaiKhoan
AS 
BEGIN
	SELECT MaTaiKhoan, TenDangNhap, MatKhau, VaiTro
	FROM TaiKhoan
END

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

-- SP BanDoc
CREATE PROCEDURE sp_GetAllBanDoc
AS
BEGIN
	SELECT MaBanDoc, HoTen, SoThe, Email, DienThoai, HanThe, TrangThaiThe, DuNo
	FROM BanDoc
END

CREATE PROCEDURE sp_GetBanDocById
	@MaBanDoc NVARCHAR(20)
AS
BEGIN
	SELECT MaBanDoc, HoTen, SoThe, Email, DienThoai, HanThe, TrangThaiThe, DuNo
	FROM BanDoc
	WHERE MaBanDoc = @MaBanDoc
END

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

CREATE PROCEDURE sp_DeleteBanDoc
	@MaBanDoc NVARCHAR(20)
AS
BEGIN
	DELETE FROM BanDoc
	WHERE MaBanDoc = @MaBanDoc;
END

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

CREATE PROCEDURE sp_GetBanSaoById
	@MaBanSao NVARCHAR(20)
AS
BEGIN
	SELECT MaBanSao, MaVach, MaSach, MaKe, TrangThai
	FROM BanSao
	WHERE MaBanSao = @MaBanSao
END

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

CREATE PROCEDURE sp_DeleteBanSao
	@MaBanSao NVARCHAR(20)
AS
BEGIN
	DELETE FROM BanSao
	WHERE MaBanSao = @MaBanSao;
END

EXEC sp_Login letienlinh2005

	



SELECT * FROM TaiKhoan