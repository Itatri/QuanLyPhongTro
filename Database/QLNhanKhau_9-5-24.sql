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
	CCCD varchar(15),
	Phone varchar(15),
	QueQuan nvarchar(200),
	ChuKy varchar(30),
	MaPhong varchar(10),
	TrangThai int
);
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


INSERT INTO KhuVuc (MaKhuVuc, TenKhuVuc, TrangThai)
VALUES ('KV01', N'Khu Vực 1', 1), 
       ('KV02', N'Khu Vực 2', 1);

INSERT INTO LoaiPhong (MaLoaiPhong, TenLoaiPhong, DienTichPhong, DonGia, GhiChu)
VALUES ('LP01', N'Loại 1', 30.5, 5000000, N'Phòng đơn'),
       ('LP02', N'Loại 2', 45.0, 7000000, N'Phòng đôi');

INSERT INTO Phong (MaPhong, MaLoaiPhong, MaKhuVuc, TenPhong, NgayVao, TienCoc, Dien, Nuoc, HanTro, TrangThai, GhiChu)
VALUES ('P01', 'LP01', 'KV01', N'Phòng 101', '2024-01-01', 5000000, 150, 100, '2024-12-31', 1, N'Sạch'),
       ('P02', 'LP02', 'KV02', N'Phòng 102', '2024-02-01', 7000000, 200, 150, '2024-12-31', 1, N'Sạch');

INSERT INTO DichVu (MaDichVu, TenDichVu, DonGia, TrangThai)
VALUES ('DV01', N'Dịch vụ điện', 3000, 1), 
       ('DV02', N'Dịch vụ nước', 5000, 1);

INSERT INTO ChiTietDichVu (MaPhong, MaDichVu, SoLuong, TongTien)
VALUES ('P01', 'DV01', 100, 300000),
       ('P02', 'DV02', 50, 250000);

INSERT INTO PhieuThu (MaPT, MaPhong, NgayLap, NgayThu, TienNha, DienCu, DienMoi, TienDien, NuocCu, NuocMoi, TienNuoc, TienDV, TongTien, TrangThai)
VALUES 
('PT01', 'P01', '2024-08-01', '2024-08-10', 5000000, 150, 160, 30000, 100, 110, 50000, 300000, 5380000, 1),
('PT02', 'P02', '2024-08-01', '2024-08-10', 7000000, 200, 210, 30000, 150, 160, 50000, 250000, 7580000, 1);

INSERT INTO PhieuThu (MaPT, MaPhong, NgayLap, NgayThu, TienNha, DienCu, DienMoi, TienDien, NuocCu, NuocMoi, TienNuoc, TienDV, TongTien, TrangThai)
VALUES 
('PT03', 'P01', '2024-09-01', '2024-09-10', 5000000, 160, 170, 30000, 110, 120, 50000, 300000, 5380000, 1),
('PT04', 'P02', '2024-09-01', '2024-09-10', 7000000, 210, 220, 30000, 160, 170, 50000, 250000, 7580000, 1);

INSERT INTO DangNhap (ID, PassWord, MaKhuVuc, TrangThai)
VALUES ('DN01', 'pass123', 'KV01', 1), 
       ('DN02', 'pass456', 'KV02', 1);

INSERT INTO ThongTinKhach (MaKhachTro, HoTen, GioiTinh, AnhNhanDien, NgaySinh, CCCD, Phone, QueQuan, ChuKy, MaPhong, TrangThai)
VALUES ('KH01', N'Nguyễn Văn A', N'Nam', NULL, '1990-01-01', '123456789012', '0901234567', N'Hà Nội', 'ChuKy1', 'P01', 1),
       ('KH02', N'Trần Thị B', N'Nữ', NULL, '1992-02-02', '987654321098', '0912345678', N'Hồ Chí Minh', 'ChuKy2', 'P02', 1);

INSERT INTO TraPhong (ID, MaKhachTro, MaPhong, NgayThue, NgayTra)
VALUES ('TP01', 'KH01', 'P01', '2024-01-01', '2024-09-01'),
       ('TP02', 'KH02', 'P02', '2024-02-01', '2024-09-02');

INSERT INTO FeedBack (MaFB, MaPhong, MoTa)
VALUES ('FB01', 'P01', N'Phòng sạch sẽ, dịch vụ tốt'),
       ('FB02', 'P02', N'Nhân viên hỗ trợ nhiệt tình');

INSERT INTO UserPhong (ID, MatKhau, MaPhong, TrangThai)
VALUES ('UP01', 'userpass1', 'P01', 1),
       ('UP02', 'userpass2', 'P02', 1);
