-- Create the Test database
CREATE DATABASE Test;
GO
USE Test;
GO

-- Table DangNhap
CREATE TABLE DangNhap
(
	ID varchar(10) PRIMARY KEY,
	PassWord varchar(10),
	MaKhuVuc varchar(10),
	TrangThai bit -- 0: vô hiệu tài khoản, 1: cho phép sử dụng
);
GO

CREATE TABLE DichVu
(
	MaDichVu varchar(10) PRIMARY KEY,
	TenDichVu nvarchar(50),
	DonGia float,
	TrangThai bit -- 0: vô hiệu dịch vụ, 1: dịch vụ còn hoạt động
);
GO

CREATE TABLE KhuVuc
(
	MaKhuVuc varchar(10) PRIMARY KEY,
	TenKhuVuc nvarchar(50),
	TrangThai bit
);
GO

CREATE TABLE LoaiPhong
(
	MaLoaiPhong varchar(10) PRIMARY KEY,
	TenLoaiPhong nvarchar(50),
	DienTichPhong float,
	DonGia float,
	GhiChu nvarchar(100)
);
GO

CREATE TABLE Phong
(
	MaPhong varchar(10) PRIMARY KEY,
	MaLoaiPhong varchar(10),
	MaKhuVuc varchar(10),
	TenPhong nvarchar(10),
	NgayVao date,
	TienCoc float,
	Dien float,
	Nuoc float,
	HanTro date,
	TrangThai bit,
	GhiChu nvarchar(100)
);
GO

CREATE TABLE PhieuThu
(
	MaPT varchar(10) PRIMARY KEY,
	MaPhong varchar(10),
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
	TrangThai bit
);
GO

CREATE TABLE ThongTinKhach
(
	MaKhachTro varchar(10) PRIMARY KEY,
	HoTen nvarchar(30),
	GioiTinh nvarchar(7),
	AnhNhanDien varchar(max),
	NgaySinh date,
	Cccd varchar(15),
	Phone varchar(15),
	QueQuan nvarchar(200),
	QuanHe nvarchar(50), --- sẽ có chủ hộ, con, vợ chồng,...
	ChuKy varchar(30),
	MaPhong varchar(10),
	TrangThai int
);
GO
CREATE TABLE CCCD
(
	Cccd varchar(15) PRIMARY KEY,
	NgayCap date,
	NoiCap nvarchar(200)
)
GO
CREATE TABLE TraPhong
(
	ID varchar(10) PRIMARY KEY,
	MaKhachTro varchar(10),
	MaPhong varchar(10),
	NgayThue date,
	NgayTra date
);
GO

CREATE TABLE FeedBack
(
	MaFB varchar(10) PRIMARY KEY,
	MaPhong varchar(10),
	MoTa nvarchar(100)
);
GO

CREATE TABLE ChiTietDichVu
(
	MaPhong varchar(10),
	MaDichVu varchar(10),
	PRIMARY KEY(MaPhong, MaDichVu),
	SoLuong int,
	TongTien float
);
GO

CREATE TABLE UserPhong
(
	ID varchar(10) PRIMARY KEY,
	MatKhau varchar(20),
	MaPhong varchar(10),
	TrangThai int
);
GO


ALTER TABLE Phong
ADD CONSTRAINT fk_Phong_KhuVuc
  FOREIGN KEY (MaKhuVuc)
  REFERENCES KhuVuc(MaKhuVuc);
GO

ALTER TABLE Phong
ADD CONSTRAINT fk_Phong_LoaiPhong
  FOREIGN KEY (MaLoaiPhong)
  REFERENCES LoaiPhong(MaLoaiPhong);
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

ALTER TABLE ChiTietDichVu
ADD CONSTRAINT fk_ChiTietDichVu_Phong
  FOREIGN KEY (MaPhong)
  REFERENCES Phong(MaPhong);
GO

ALTER TABLE ChiTietDichVu
ADD CONSTRAINT fk_ChiTietDichVu_DichVu
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

ALTER TABLE ThongTinKhach
ADD CONSTRAINT fk_ThongTinKhach_CCCD
  FOREIGN KEY (Cccd)
  REFERENCES CCCD(Cccd);
GO

-- Thêm dữ liệu vào bảng KhuVuc
INSERT INTO KhuVuc (MaKhuVuc, TenKhuVuc, TrangThai)
VALUES
('KV01', N'Khu vực A', 1),
('KV02', N'Khu vực B', 1);

-- Thêm dữ liệu vào bảng LoaiPhong
INSERT INTO LoaiPhong (MaLoaiPhong, TenLoaiPhong, DienTichPhong, DonGia, GhiChu)
VALUES
('LP01', N'Phòng đơn', 20.5, 1500000, N'Phòng nhỏ, 1 giường'),
('LP02', N'Phòng đôi', 30.0, 2000000, N'Phòng lớn, 2 giường');

-- Thêm dữ liệu vào bảng Phong
INSERT INTO Phong (MaPhong, MaLoaiPhong, MaKhuVuc, TenPhong, NgayVao, TienCoc, Dien, Nuoc, HanTro, TrangThai, GhiChu)
VALUES
('P001', 'LP01', 'KV01', 'Phòng 1', '2023-06-15', 500000, 120, 30, '2024-12-31', 1, N'Đang cho thuê'),
('P002', 'LP02', 'KV02', 'Phòng 2', '2023-07-01', 1000000, 150, 40, '2024-12-31', 1, N'Đang cho thuê');

-- Thêm dữ liệu vào bảng DichVu
INSERT INTO DichVu (MaDichVu, TenDichVu, DonGia, TrangThai)
VALUES
('DV01', N'Dịch vụ giặt ủi', 50000, 1),
('DV02', N'Dịch vụ internet', 200000, 1);

-- Thêm dữ liệu vào bảng DangNhap
INSERT INTO DangNhap (ID, PassWord, MaKhuVuc, TrangThai)
VALUES
('DN01', 'pass123', 'KV01', 1),
('DN02', 'pass456', 'KV02', 1);
-- Thêm dữ liệu vào bảng CCCD
INSERT INTO CCCD (Cccd, NgayCap, NoiCap)
VALUES
('123456789', '2010-05-01', N'Công an Hà Nội'),
('987654321', '2015-08-01', N'Công an Hải Phòng');
-- Thêm dữ liệu vào bảng ThongTinKhach
INSERT INTO ThongTinKhach (MaKhachTro, HoTen, GioiTinh, AnhNhanDien, NgaySinh, CCCD, Phone, QueQuan, QuanHe, ChuKy, MaPhong, TrangThai)
VALUES
('KT01', N'Nguyen Van A', 'Nam', NULL, '1990-05-15', '123456789', '0905123456', N'Ha Noi', N'Chủ hộ', 'Chữ ký A', 'P001', 1),
('KT02', N'Tran Thi B', 'Nữ', NULL, '1995-08-20', '987654321', '0912345678', N'Hai Phong', N'Vợ', 'Chữ ký B', 'P002', 1);

-- Thêm dữ liệu vào bảng PhieuThu cho tháng 8 và tháng 9
INSERT INTO PhieuThu (MaPT, MaPhong, NgayLap, NgayThu, TienNha, DienCu, DienMoi, TienDien, NuocCu, NuocMoi, TienNuoc, TienDV, TongTien, TrangThai)
VALUES
-- Tháng 8/2024
('PT001', 'P001', '2024-08-01', '2024-08-05', 1500000, 120, 140, 300000, 30, 35, 25000, 50000, 1825000, 1),
('PT002', 'P002', '2024-08-01', '2024-08-06', 2000000, 150, 170, 400000, 40, 45, 30000, 200000, 2630000, 1),
-- Tháng 9/2024
('PT003', 'P001', '2024-09-01', '2024-09-05', 1500000, 140, 160, 300000, 35, 40, 25000, 50000, 1825000, 1),
('PT004', 'P002', '2024-09-01', '2024-09-06', 2000000, 170, 190, 400000, 45, 50, 30000, 200000, 2630000, 1);

-- Thêm dữ liệu vào bảng ChiTietDichVu
INSERT INTO ChiTietDichVu (MaPhong, MaDichVu, SoLuong, TongTien)
VALUES
('P001', 'DV01', 1, 50000),
('P002', 'DV02', 1, 200000);

-- Thêm dữ liệu vào bảng TraPhong
INSERT INTO TraPhong (ID, MaKhachTro, MaPhong, NgayThue, NgayTra)
VALUES
('TP001', 'KT01', 'P001', '2023-06-15', '2024-08-01'),
('TP002', 'KT02', 'P002', '2023-07-01', '2024-09-01');

-- Thêm dữ liệu vào bảng FeedBack
INSERT INTO FeedBack (MaFB, MaPhong, MoTa)
VALUES
('FB001', 'P001', N'Phòng sạch sẽ, thoải mái'),
('FB002', 'P002', N'Dịch vụ tốt, nhưng cần nâng cấp');

-- Thêm dữ liệu vào bảng UserPhong
INSERT INTO UserPhong (ID, MatKhau, MaPhong, TrangThai)
VALUES
('UP001', '123abc', 'P001', 1),
('UP002', '456def', 'P002', 1);

