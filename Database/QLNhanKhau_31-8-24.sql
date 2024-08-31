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

-- Table DichVu
CREATE TABLE DichVu
(
	MaDichVu varchar(10) PRIMARY KEY,
	TenDichVu varchar(50),
	DonGia float,
	TrangThai bit -- 0: vô hiệu dịch vụ, 1: dịch vụ còn hoạt động
);
GO

-- Table KhuVuc
CREATE TABLE KhuVuc
(
	MaKhuVuc varchar(10) PRIMARY KEY,
	TenKhuVuc varchar(50),
	TrangThai bit
);
GO

-- Table LoaiPhong
CREATE TABLE LoaiPhong
(
	MaLoaiPhong varchar(10) PRIMARY KEY,
	TenLoaiPhong varchar(50),
	DienTichPhong float,
	DonGia float,
	GhiChu varchar(100)
);
GO

-- Table Phong
CREATE TABLE Phong
(
	MaPhong varchar(10) PRIMARY KEY,
	MaLoaiPhong varchar(10),
	MaKhuVuc varchar(10),
	TenPhong varchar(10),
	Day int,
	TrangThai bit,
	GhiChu varchar(100)
);
GO

-- Table PhieuThu
CREATE TABLE PhieuThu
(
	MaPT varchar(10) PRIMARY KEY,
	MaPhong varchar(10),
	NgayLap date,
	NgayThu date,
	TienNha float,
	SkDien float,
	TienDien float,
	SkNuoc float,
	TienNuoc float,
	TongTien float,
	TrangThai bit
);
GO

-- Table ThongTinKhach
CREATE TABLE ThongTinKhach
(
	MaKhachTro varchar(10) PRIMARY KEY,
	HoTen varchar(30),
	GioiTinh varchar(5),
	AnhNhanDien varchar(10),
	NgaySinh date,
	CCCD varchar(12),
	Phone varchar(12),
	QueQuan varchar(10),
	ChuKy varchar(10),
	MaPhong varchar(10),
	TrangThai int
);
GO

-- Table TraPhong
CREATE TABLE TraPhong
(
	ID varchar(10) PRIMARY KEY,
	MaKhachTro varchar(10),
	MaPhong varchar(10),
	NgayThue date,
	NgayTra date
);
GO

-- Table FeedBack
CREATE TABLE FeedBack
(
	MaFB varchar(10) PRIMARY KEY,
	MaPhong varchar(10),
	MoTa varchar(100)
);
GO

-- Table ChiTietDichVu
CREATE TABLE ChiTietDichVu
(
	MaPhong varchar(10),
	MaDichVu varchar(10),
	PRIMARY KEY(MaPhong, MaDichVu),
	SoLuong int,
	TongTien float
);
GO

-- Table ChiTietPTDichVu
CREATE TABLE ChiTietPTDichVu
(
	MaDichVu varchar(10),
	MaPT varchar(10),
	PRIMARY KEY(MaDichVu, MaPT)
);
GO

-- Table UserPhong
CREATE TABLE UserPhong
(
	ID varchar(10) PRIMARY KEY,
	MatKhau varchar(20),
	MaPhong varchar(10),
	TrangThai int
);
GO

-- Add Foreign Key Constraints

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

ALTER TABLE ChiTietPTDichVu
ADD CONSTRAINT fk_ChiTietPTDichVu_DichVu
  FOREIGN KEY (MaDichVu)
  REFERENCES DichVu(MaDichVu);
GO

ALTER TABLE ChiTietPTDichVu
ADD CONSTRAINT fk_ChiTietPTDichVu_PhieuThu
  FOREIGN KEY (MaPT)
  REFERENCES PhieuThu(MaPT);
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

-- Insert data into KhuVuc
INSERT INTO KhuVuc (MaKhuVuc, TenKhuVuc, TrangThai) VALUES
('KV01', 'Khu A', 1),
('KV02', 'Khu B', 1),
('KV03', 'Khu C', 0);

-- Insert data into LoaiPhong
INSERT INTO LoaiPhong (MaLoaiPhong, TenLoaiPhong, DienTichPhong, DonGia, GhiChu) VALUES
('LP01', 'Phong Don', 20.5, 1000000, 'Phong cho 1 nguoi'),
('LP02', 'Phong Doi', 30.0, 1500000, 'Phong cho 2 nguoi'),
('LP03', 'Phong Gia Dinh', 50.0, 2500000, 'Phong cho gia dinh');

-- Insert data into Phong
INSERT INTO Phong (MaPhong, MaLoaiPhong, MaKhuVuc, TenPhong, Day, TrangThai, GhiChu) VALUES
('P001', 'LP01', 'KV01', 'Phong 101', 1, 1, 'Phong view dep'),
('P002', 'LP02', 'KV01', 'Phong 102', 1, 1, 'Phong rong rai'),
('P003', 'LP03', 'KV02', 'Phong 201', 2, 0, 'Phong co bep');

-- Insert data into DichVu
INSERT INTO DichVu (MaDichVu, TenDichVu, DonGia, TrangThai) VALUES
('DV01', 'Dich vu Internet', 200000, 1),
('DV02', 'Dich vu giat ui', 100000, 1),
('DV03', 'Dich vu ve sinh', 150000, 0);

-- Insert data into DangNhap
INSERT INTO DangNhap (ID, PassWord, MaKhuVuc, TrangThai) VALUES
('user1', 'password1', 'KV01', 1),
('user2', 'password2', 'KV02', 0),
('user3', 'password3', 'KV03', 1);

-- Insert data into PhieuThu
INSERT INTO PhieuThu (MaPT, MaPhong, NgayLap, NgayThu, TienNha, SkDien, TienDien, SkNuoc, TienNuoc, TongTien, TrangThai) VALUES
('PT001', 'P001', '2024-08-01', '2024-08-10', 1000000, 50, 100000, 30, 50000, 1150000, 1),
('PT002', 'P002', '2024-08-02', '2024-08-11', 1500000, 60, 120000, 40, 60000, 1680000, 1);

-- Insert data into ThongTinKhach
INSERT INTO ThongTinKhach (MaKhachTro, HoTen, GioiTinh, AnhNhanDien, NgaySinh, CCCD, Phone, QueQuan, ChuKy, MaPhong, TrangThai) VALUES
('KH001', 'Nguyen Van A', 'Nam', 'a.jpg', '1990-05-10', '123456789012', '0123456789', 'Ha Noi', 'chuKy1.png', 'P001', 1),
('KH002', 'Tran Thi B', 'Nu', 'b.jpg', '1992-07-15', '987654321098', '0987654321', 'Hai Phong', 'chuKy2.png', 'P002', 1);

-- Insert data into TraPhong
INSERT INTO TraPhong (ID, MaKhachTro, MaPhong, NgayThue, NgayTra) VALUES
('TP001', 'KH001', 'P001', '2024-07-01', '2024-08-01'),
('TP002', 'KH002', 'P002', '2024-07-15', '2024-08-15');

-- Insert data into FeedBack
INSERT INTO FeedBack (MaFB, MaPhong, MoTa) VALUES
('FB001', 'P001', 'Phong sach se, an toan'),
('FB002', 'P002', 'Phong dep nhung wifi yeu');

-- Insert data into ChiTietDichVu
INSERT INTO ChiTietDichVu (MaPhong, MaDichVu, SoLuong, TongTien) VALUES
('P001', 'DV01', 1, 200000),
('P002', 'DV02', 2, 200000);

-- Insert data into ChiTietPTDichVu
INSERT INTO ChiTietPTDichVu (MaDichVu, MaPT) VALUES
('DV01', 'PT001'),
('DV02', 'PT002');

-- Insert data into UserPhong
INSERT INTO UserPhong (ID, MatKhau, MaPhong, TrangThai) VALUES
('UP001', 'userpass1', 'P001', 1),
('UP002', 'userpass2', 'P002', 0);
