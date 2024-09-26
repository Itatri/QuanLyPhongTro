-- Create the Test database
CREATE DATABASE QL_ChungCu;
GO
USE QL_ChungCU;
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

CREATE TABLE LoaiPhong
(
	MaLoaiPhong varchar(50) PRIMARY KEY,
	TenLoaiPhong nvarchar(max),
	DienTichPhong float,
	DonGia float,
	GhiChu nvarchar(max)
);
GO

CREATE TABLE Phong
(
	MaPhong varchar(50) PRIMARY KEY,
	MaLoaiPhong varchar(50),
	MaKhuVuc varchar(50),
	TenPhong nvarchar(max),
	NgayVao date,
	TienCoc float,
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
	CongNo float,
	TongTien float,
	TrangThai bit
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
	QueQuan nvarchar(max),
	QuanHe nvarchar(50), --- sẽ có chủ hộ, con, vợ chồng, bạn cùng phòng (mặc định nhập web đầu tiên sẽ là chủ hộ, sẽ được tùy chọn chủ hộ
	ChuKy varchar(50),
	ChuKyXacNhan varchar(max),
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
	MoTa nvarchar(max)
);
GO

CREATE TABLE ChiTietDichVu
(
	MaPhong varchar(50),
	MaDichVu varchar(50),
	PRIMARY KEY(MaPhong, MaDichVu),
	SoLuong int,
	TongTien float
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
	ChuKyXacNhan varchar(max),
	IdUser varchar(50),
	TrangThai int
);
GO
CREATE TABLE LuuTru
(
	MaLuuTru varchar(50) primary key,
	MaPhong varchar(50),
	UserPhong varchar(50),
	DiaChi varchar(max), --- chỗ lưu trữ
	TrangThai int ---  1: Hợp đồng, 2: CT01, 3: Cam Kết,
);
CREATE TABLE DuongDan
(
	DDChuKy varchar(50) primary key, --- chữ ký thường admin và khách
	DDFile varchar(50), ----- hợp đồng, ct01, cam kết
);
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

ALTER TABLE ThongTinAdmin
ADD CONSTRAINT fk_ThongTinAdmin_DangNhap
  FOREIGN KEY (IdUser)
  REFERENCES DangNhap(ID);
GO
ALTER TABLE LuuTru
ADD CONSTRAINT fk_LuuTru_Phong
  FOREIGN KEY (MaPhong)
  REFERENCES Phong(MaPhong);
GO
ALTER TABLE LuuTru
ADD CONSTRAINT fk_LuuTru_UserPhong
  FOREIGN KEY (UserPhong)
  REFERENCES UserPhong(ID);
GO

-- Insert data into KhuVuc
INSERT INTO KhuVuc (MaKhuVuc, TenKhuVuc, TrangThai)
VALUES 
('KV01', N'Khu Vực 1', 1),
('KV02', N'Khu Vực 2', 1);

-- Insert data into LoaiPhong
INSERT INTO LoaiPhong (MaLoaiPhong, TenLoaiPhong, DienTichPhong, DonGia, GhiChu)
VALUES
('LP01', N'Loại Phòng 1', 50.0, 1500000.0, N'Phòng tiêu chuẩn'),
('LP02', N'Loại Phòng 2', 75.0, 2500000.0, N'Phòng cao cấp');

-- Insert data into Phong
INSERT INTO Phong (MaPhong, MaLoaiPhong, MaKhuVuc, TenPhong, NgayVao, TienCoc, Dien, Nuoc, HanTro, TrangThai, GhiChu)
VALUES
('P01', 'LP01', 'KV01', N'Phòng 101', '2024-01-01', 1000000.0, 100, 200, '2024-12-31', 1, N'Phòng đẹp'),
('P02', 'LP02', 'KV02', N'Phòng 201', '2024-02-01', 2000000.0, 150, 250, '2024-12-31', 1, N'Phòng cao cấp');

-- Insert data into DichVu
INSERT INTO DichVu (MaDichVu, TenDichVu, DonGia, TrangThai)
VALUES
('DV01', N'Dịch vụ điện', 3500.0, 1),
('DV02', N'Dịch vụ nước', 15000.0, 1);

-- Insert data into PhieuThu
INSERT INTO PhieuThu (MaPT, MaPhong, NgayLap, NgayThu, TienNha, DienCu, DienMoi, TienDien, NuocCu, NuocMoi, TienNuoc, TienDV, TongTien, TrangThai)
VALUES
('PT01', 'P01', '2024-08-01', '2024-08-05', 1500000.0, 100, 120, 70000.0, 200, 220, 300000.0, 100000.0, 1970000.0, 1),
('PT02', 'P02', '2024-09-01', '2024-09-05', 2500000.0, 150, 170, 70000.0, 250, 270, 300000.0, 100000.0, 2970000.0, 1);

-- Insert data into ThongTinKhach
INSERT INTO ThongTinKhach (MaKhachTro, HoTen, GioiTinh, AnhNhanDien, NgaySinh, Cccd, NgayCap, NoiCap, MaDinhDanh, Phone, QueQuan, QuanHe, ChuKy, MaPhong, TrangThai)
VALUES
('KT01', N'Nguyen Van A', N'Nam', NULL, '1990-01-01', '0123456789', '2010-01-01', N'Hà Nội', 'MDD001', '0987654321', N'Hà Nội', N'Chủ hộ', NULL, 'P01', 1),
('KT02', N'Tran Thi B', N'Nữ', NULL, '1992-02-02', '9876543210', '2010-02-02', N'TP.HCM', 'MDD002', '0123456789', N'TP.HCM', N'Vợ', NULL, 'P02', 1);

-- Insert data into TraPhong
INSERT INTO TraPhong (ID, MaKhachTro, MaPhong, NgayThue, NgayTra)
VALUES
('TP01', 'KT01', 'P01', '2024-01-01', '2024-12-31'),
('TP02', 'KT02', 'P02', '2024-02-01', '2024-12-31');

-- Insert data into FeedBack
INSERT INTO FeedBack (MaFB, MaPhong, MoTa)
VALUES
('FB01', 'P01', N'Phòng sạch sẽ, dịch vụ tốt'),
('FB02', 'P02', N'Phòng rộng, tiện nghi');

-- Insert data into ChiTietDichVu
INSERT INTO ChiTietDichVu (MaPhong, MaDichVu, SoLuong, TongTien)
VALUES
('P01', 'DV01', 20, 70000.0),
('P01', 'DV02', 20, 300000.0),
('P02', 'DV01', 20, 70000.0),
('P02', 'DV02', 20, 300000.0);

-- Insert data into UserPhong
INSERT INTO UserPhong (ID, MatKhau, MaPhong, TrangThai)
VALUES
('U01', 'password1', 'P01', 1),
('U02', 'password2', 'P02', 1);
-- Insert data into DangNhap
INSERT INTO DangNhap (ID, PassWord, MaKhuVuc, TrangThai)
VALUES
('DN01', 'pass123', 'KV01', 1),
('DN02', 'pass456', 'KV02', 1);

-- Insert data into ThongTinAdmin
INSERT INTO ThongTinAdmin (MaAdmin, HoTen, GioiTinh, NgaySinh, Cccd, MaDinhDanh, Phone, QueQuan, ChuKy, ChuKyXacNhan, IdUser, TrangThai)
VALUES
('A01', N'Le Van C', N'Nam', '1985-05-15', '1234567890', 'MDD003', '0981234567', N'Hà Nội', NULL, NULL, 'DN01', 1),
('A02', N'Nguyen Thi D', N'Nữ', '1986-06-10', '9876543210', 'MDD004', '0912345678', N'TP.HCM', NULL, NULL, 'DN02', 1);

-- Insert data into LuuTru
INSERT INTO LuuTru (MaLuuTru, MaPhong, UserPhong, DiaChi, TrangThai)
VALUES
('LT01', 'P01', 'U01', N'Kho lưu trữ 1', 1),
('LT02', 'P02', 'U02', N'Kho lưu trữ 2', 1);

INSERT INTO LuuTru (MaLuuTru, MaPhong, UserPhong, DiaChi, TrangThai)
VALUES
('LT03', 'P01', 'U01', N'Kho lưu trữ 1', 2),
('LT04', 'P01', 'U01', N'Kho lưu trữ 1', 3)


