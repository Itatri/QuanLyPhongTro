-- Create the Test database
CREATE DATABASE QL_ChungCu_17_10_2024;
GO
USE QL_ChungCu_17_10_2024;
GO

-- Table DangNhap
CREATE TABLE DangNhap
(
	ID varchar(50) PRIMARY KEY,
	PassWord varchar(50),
	MaKhuVuc varchar(50),
	TrangThai bit -- 0: vô hiệu tài khoản, 1: cho phép sử dụng
);
GO

CREATE TABLE DichVu
(
	MaDichVu varchar(50) PRIMARY KEY,
	TenDichVu nvarchar(max),
	DonGia float,
	TrangThai bit -- 0: vô hiệu dịch vụ, 1: dịch vụ còn hoạt động
);
GO

CREATE TABLE KhuVuc
(
	MaKhuVuc varchar(50) PRIMARY KEY,
	TenKhuVuc nvarchar(max),
	TrangThai bit
);
GO

CREATE TABLE Phong
(
	MaPhong varchar(50) PRIMARY KEY,
	MaKhuVuc varchar(50),
	TenPhong nvarchar(max),
	NgayVao date,
	TienCoc float,
	TienPhong float,
	Dien float,
	Nuoc float,
	CongNo float,
	HanTro date,
	TrangThai bit,
	GhiChu nvarchar(100)
);
GO
CREATE TABLE PhieuThu
(
	MaPT varchar(50) PRIMARY KEY,
	MaPhong varchar(50),
	NgayLap date,
	NgayThu date,
	TienNha float,
	DienCu float,
	DienMoi float,
	TienDien float,
	NuocCu float,
	NuocMoi float,
	TienNuoc float,
	TienDV float,
	TongTien float,
	ThanhToan float,
	TrangThai bit,
);
GO

CREATE TABLE ThongTinKhach
(
	MaKhachTro varchar(50) PRIMARY KEY,
	HoTen nvarchar(50),
	GioiTinh nvarchar(50),
	NgaySinh date,
	Cccd varchar(50),
	NgayCap date,
	NoiCap nvarchar(max),
	Phone varchar(50),
	Email varchar(max),
	QueQuan nvarchar(max),
	QuanHe nvarchar(50), --- sẽ có chủ hộ, con, vợ chồng, bạn cùng phòng (mặc định nhập web đầu tiên sẽ là chủ hộ, sẽ được tùy chọn chủ hộ
	ChuKy varchar(50),
	MaPhong varchar(50),
	TrangThai int
);
GO
CREATE TABLE TraPhong
(
	ID varchar(50) PRIMARY KEY,
	MaKhachTro varchar(50),
	MaPhong varchar(50),
	NgayThue date,
	NgayTra date
);
GO

CREATE TABLE FeedBack
(
	MaFB varchar(50) PRIMARY KEY,
	MaPhong varchar(50),
	MoTa nvarchar(max),
	NgayGui datetime,
	PhanHoi nvarchar(max),
	NgayPhanHoi datetime,
	TrangThai int
);
GO

CREATE TABLE DichVuPhong
(
	MaPhong varchar(50),
	MaDichVu varchar(50),
	PRIMARY KEY(MaPhong, MaDichVu),
);
GO

CREATE TABLE UserPhong
(
	ID varchar(50) PRIMARY KEY,
	MatKhau varchar(50),
	MaPhong varchar(50),
	TrangThai int
);
GO
CREATE TABLE ThongTinAdmin
(
	MaAdmin varchar(50) PRIMARY KEY,
	HoTen nvarchar(50),
	GioiTinh nvarchar(50),
	NgaySinh date,
	Cccd varchar(50),
	Phone varchar(50),
	QueQuan nvarchar(200),
	ChuKy varchar(max),
	IdUser varchar(50),
	TrangThai int
);
GO
CREATE TABLE DuongDan
(
	ID int primary key IDENTITY,
	DDChuKy varchar(50), --- chữ ký thường admin và khách
	DDFile varchar(50), ----- hợp đồng, ct01, cam kết
);
GO
CREATE TABLE DichVuPhieuThu
(
	ID int primary key IDENTITY,
	MaPT varchar(50),
	TenDichVu nvarchar(max),
	DonGia float
);

ALTER TABLE Phong
ADD CONSTRAINT fk_Phong_KhuVuc
  FOREIGN KEY (MaKhuVuc)
  REFERENCES KhuVuc(MaKhuVuc);
GO

ALTER TABLE PhieuThu
ADD CONSTRAINT fk_PhieuThu_Phong
  FOREIGN KEY (MaPhong)
  REFERENCES Phong(MaPhong);
GO

ALTER TABLE ThongTinKhach
ADD CONSTRAINT fk_ThongTinKhach_Phong
  FOREIGN KEY (MaPhong)
  REFERENCES Phong(MaPhong);
GO

ALTER TABLE TraPhong
ADD CONSTRAINT fk_TraPhong_ThongTinKhach
  FOREIGN KEY (MaKhachTro)
  REFERENCES ThongTinKhach(MaKhachTro);
GO

ALTER TABLE TraPhong
ADD CONSTRAINT fk_TraPhong_Phong
  FOREIGN KEY (MaPhong)
  REFERENCES Phong(MaPhong);
GO

ALTER TABLE FeedBack
ADD CONSTRAINT fk_FeedBack_Phong
  FOREIGN KEY (MaPhong)
  REFERENCES Phong(MaPhong);
GO

ALTER TABLE DichVuPhong
ADD CONSTRAINT fk_DichVuPhong_Phong
  FOREIGN KEY (MaPhong)
  REFERENCES Phong(MaPhong);
GO

ALTER TABLE DichVuPhong
ADD CONSTRAINT fk_DichVuPhong_DichVu
  FOREIGN KEY (MaDichVu)
  REFERENCES DichVu(MaDichVu);
GO


ALTER TABLE UserPhong
ADD CONSTRAINT fk_UserPhong_Phong
  FOREIGN KEY (MaPhong)
  REFERENCES Phong(MaPhong);
GO

ALTER TABLE DangNhap
ADD CONSTRAINT fk_DangNhap_KhuVuc
  FOREIGN KEY (MaKhuVuc)
  REFERENCES KhuVuc(MaKhuVuc);
GO

ALTER TABLE ThongTinAdmin
ADD CONSTRAINT fk_ThongTinAdmin_DangNhap
  FOREIGN KEY (IdUser)
  REFERENCES DangNhap(ID);
GO
GO
ALTER TABLE DichVuPhieuThu
ADD CONSTRAINT fk_PhieuThu_DichVuPhieuThu
  FOREIGN KEY (MaPT)
  REFERENCES PhieuThu(MaPT);
GO
-- Thêm dữ liệu vào bảng KhuVuc
INSERT INTO KhuVuc (MaKhuVuc, TenKhuVuc, TrangThai) VALUES 
('KV001', N'Khu A', 1),
('KV002', N'Khu B', 1);

-- Thêm dữ liệu vào bảng DangNhap
INSERT INTO DangNhap (ID, PassWord, MaKhuVuc, TrangThai) VALUES 
('admin1', '1', 'KV001', 1),
('user001', 'userpass', 'KV002', 1);

-- Thêm dữ liệu vào bảng DichVu
INSERT INTO DichVu (MaDichVu, TenDichVu, DonGia, TrangThai) VALUES 
('DV001', N'Dịch vụ điện', 3000, 1),
('DV002', N'Dịch vụ nước', 2000, 1);


-- Thêm dữ liệu vào bảng Phong
INSERT INTO Phong (MaPhong, MaKhuVuc, TenPhong, NgayVao, TienCoc, TienPhong, Dien, Nuoc, CongNo, HanTro, TrangThai, GhiChu) VALUES 
('P001', 'KV001', N'Phòng 101', '2024-09-01', 1000000, 2000000, 120, 15, 0, '2024-12-31', 1, N'Phòng còn trống'),
('P002', 'KV002', N'Phòng 202', '2024-10-01', 1000000, 2200000, 150, 20, 0, '2024-12-31', 1, N'Phòng đã có khách');
-- Thêm dữ liệu vào bảng DichVuPhong
INSERT INTO DichVuPhong (MaPhong, MaDichVu) VALUES 
('P001', 'DV001'),
('P001', 'DV002');

-- Thêm dữ liệu vào bảng PhieuThu
INSERT INTO PhieuThu (MaPT, MaPhong, NgayLap, NgayThu, TienNha, DienCu, DienMoi, TienDien, NuocCu, NuocMoi, TienNuoc, TienDV, TongTien, ThanhToan, TrangThai) VALUES 
('PT001', 'P001', '2024-10-01', '2024-10-10', 2000000, 100, 120, 60000, 10, 15, 10000, 3000000, 3000000,3000000, 1);

-- Thêm dữ liệu vào bảng ThongTinKhach
INSERT INTO ThongTinKhach (MaKhachTro, HoTen, GioiTinh, NgaySinh, Cccd, NgayCap, NoiCap, Phone, Email, QueQuan, QuanHe, ChuKy, MaPhong, TrangThai) VALUES 
('KT001', N'Nguyễn Văn A', N'Nam', '1990-01-01', '123456789', '2010-01-01', N'Hà Nội', '0912345678', 'a@gmail.com', N'Hà Nội', N'Chủ hộ', 'chuky1', 'P001', 1);

-- Thêm dữ liệu vào bảng TraPhong
INSERT INTO TraPhong (ID, MaKhachTro, MaPhong, NgayThue, NgayTra) VALUES 
('TP001', 'KT001', 'P001', '2024-09-01', '2024-12-01');

-- Thêm dữ liệu vào bảng FeedBack
INSERT INTO FeedBack (MaFB, MaPhong, MoTa, NgayGui, PhanHoi, NgayPhanHoi, TrangThai) VALUES 
('FB001', 'P001', N'Tiếng ồn quá lớn', '2024-10-01 08:30:00', N'Sẽ kiểm tra và giải quyết', '2024-10-02 10:00:00', 1);


-- Thêm dữ liệu vào bảng UserPhong
INSERT INTO UserPhong (ID, MatKhau, MaPhong, TrangThai) VALUES 
('UP001', 'userpass123', 'P001', 1);

-- Thêm dữ liệu vào bảng ThongTinAdmin
INSERT INTO ThongTinAdmin (MaAdmin, HoTen, GioiTinh, NgaySinh, Cccd, Phone, QueQuan, ChuKy, IdUser, TrangThai) VALUES 
('AD001', N'Trần Văn B', N'Nam', '1985-05-05', '987654321', '0987654321', N'Hồ Chí Minh', 'chukyadmin', 'admin1', 1);

-- Thêm dữ liệu vào bảng DuongDan
INSERT INTO DuongDan (DDChuKy, DDFile) VALUES 
('path/to/chuky', 'path/to/hopdong');

-- Thêm dữ liệu vào bảng DichVuPhieuThu
INSERT INTO DichVuPhieuThu (MaPT, TenDichVu, DonGia) VALUES 
('PT001', N'Dịch vụ điện', 60000),
('PT001', N'Dịch vụ nước', 10000);



--------------------------------------------------------------------------------------------------------------
-- thủ tục hiển thị danh sách nhân viên
CREATE PROC ShowDichVu as
select * from DichVu;
go


--------------------------------------------------------------------------------------------------------------
-- thủ tục tìm kiếm nhân viên
create proc TimDichVu (@GiaTriCanTim nvarchar(50)) as
SELECT * FROM DichVu WHERE (MaDichVu = @GiaTriCanTim OR TenDichVu LIKE N'%' + @GiaTriCanTim + '%');
go


--------------------------------------------------------------------------------------------------------------
--Thủ tục thêm dịch vụ
CREATE PROC ThemDichVu(@MaDV nvarchar(10), @tenDV nvarchar(50), @dongia float) as
insert into DichVu values (@MaDV, @tenDV, @dongia, 0);
GO

--------------------------------------------------------------------------------------------------------------
-- thủ tục sửa dịch vụ
create proc CapNhatDichVu(@MaDV nvarchar(10), @tenDV nvarchar(50), @dongia float, @trangthai bit) as
update DichVu set @tenDV = @tenDV, DonGia = @dongia, TrangThai = @trangthai  where  MaDichVu= @MaDV;
GO

--------------------------------------------------------------------------------------------------------------
-- Thủ tục xóa dịch vụ
CREATE PROC XoaDichVu(@MaDV CHAR(10)) AS
DELETE FROM DichVu WHERE MaDichVu= @MaDV;
GO


--------------------------------------------------------------------------------------------------------------
---- Thủ tục sinh mã dịch vụ
CREATE PROCEDURE SinhMaDichVu AS
BEGIN
	DECLARE @MaDichVuCuoi varchar(10);
	DECLARE @SoCuoi int;
	DECLARE @MaDichVuMoi varchar(10);

	-- Lấy mã dịch vụ cuối cùng từ bảng DichVu
	SELECT TOP 1 @MaDichVuCuoi = MaDichVu
	FROM DichVu
	ORDER BY MaDichVu DESC;

	IF @MaDichVuCuoi IS NULL
		SET @MaDichVuCuoi = 'DV0000'; -- Nếu không có dữ liệu, gán mã mặc định
	ELSE
	BEGIN
		SET @SoCuoi = CAST(RIGHT(@MaDichVuCuoi, LEN(@MaDichVuCuoi) - 2) AS int);
		SET @MaDichVuMoi = 'DV' + FORMAT(@SoCuoi + 1, '0000');
	END

	SELECT @MaDichVuMoi AS MaDichVuMoi; -- Trả về mã dịch vụ mới
END;
GO

----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--TỚI TABLE PHÒNG
---------------------------
--CREATE TABLE TaiKhoanPhong
--(
--    ID char(10) PRIMARY KEY,
--    tenphong nvarchar(50),
--    Matkhau nvarchar(20),
--    Trangthai bit,
--    MaPhong varchar(10),
--    FOREIGN KEY (MaPhong) REFERENCES Phong(MaPhong)
--);
--GO



---------------------------------------------------------------
-- sinh mã phòng tự động
CREATE PROCEDURE SinhMaPhong
AS
BEGIN
    DECLARE @newMaPhong varchar(10);
    DECLARE @maxMaPhong varchar(10);

    -- Lấy mã phòng lớn nhất hiện tại
    SELECT @maxMaPhong = MAX(MaPhong) FROM Phong;

    -- Nếu không có mã phòng nào, bắt đầu từ 'P001'
    IF @maxMaPhong IS NULL
    BEGIN
        SET @newMaPhong = 'P001';
    END
    ELSE
    BEGIN
        -- Tăng số thứ tự lên 1 so với mã phòng lớn nhất hiện tại
        SET @newMaPhong = 'P' + RIGHT('000' + CAST(CAST(SUBSTRING(@maxMaPhong, 2, LEN(@maxMaPhong) - 1) AS int) + 1 AS varchar), 3);
    END

    -- Trả về mã phòng mới sinh ra
    SELECT @newMaPhong AS NewMaPhong;
END
GO
------------------------------

--Bạn có thể sử dụng trigger trong SQL Server để ngăn chặn việc xóa các bản ghi 
--với MaDichVu là DV0000 hoặc DV0001. Dưới đây là câu lệnh SQL để tạo trigger đó:
CREATE TRIGGER trg_PreventDeleteDichVu
ON DichVu
INSTEAD OF DELETE
AS
BEGIN
    IF EXISTS (SELECT * FROM deleted WHERE MaDichVu IN ('DV0000', 'DV0001'))
    BEGIN
        PRINT 'Không thể xóa dịch vụ có MaDichVu là DV0000 hoặc DV0001.';
        RETURN;
    END

    -- Nếu không phải MaDichVu cấm, thực hiện xóa
    DELETE FROM DichVu
    WHERE MaDichVu IN (SELECT MaDichVu FROM deleted);
END;
GO


--------------------------------------------------------------
CREATE TRIGGER trg_UpdateHanTro
ON Phong
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- Cập nhật hạn trọ bằng ngày vào cộng thêm 2 năm
    UPDATE p
    SET p.HanTro = DATEADD(YEAR, 2, i.NgayVao)
    FROM Phong p
    INNER JOIN inserted i ON p.MaPhong = i.MaPhong;
END;
GO
