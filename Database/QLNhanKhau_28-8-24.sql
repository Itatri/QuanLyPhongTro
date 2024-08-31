CREATE DATABASE Test
GO
USE Test
GO
CREATE TABLE DangNhap
(
	ID varchar(10) primary key,
	PassWord varchar(10),
	TrangThai bit -- 0 là vô hiệu tài khoản 1: cho phép sử dụng
)
CREATE TABLE DichVu
(
	MaDichVu varchar(10) primary key,
	TenDichVu varchar(50),
	DonGia float,
	TrangThai bit -- 0 là vô hiệu dịch vụ 1: dịch vụ còn hoạt động
)
GO
CREATE TABLE KhuVuc
(
	MaKhuVuc varchar(10) primary key,
	TenKhuVuc varchar(50),
	TrangThai bit
)
GO
CREATE TABLE LoaiPhong
(
	MaLoaiPhong varchar(10) primary key,
	TenLoaiPhong varchar(50),
	DienTichPhong float,
	DonGia float,
	GhiChu varchar(100)
)
GO
CREATE TABLE Phong
(
	MaPhong varchar(10) primary key,
	MaLoaiPhong varchar(10),
	MaKhuVuc varchar(10),
	TenPhong varchar(10),
	Day int,
	TrangThai bit,
	GhiChu varchar(100)
)
GO
CREATE TABLE PhieuThu
(
	MaPT varchar(10) primary key,
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
)
GO
CREATE TABLE ThongTinKhach
(
	MaKhachTro varchar(10) primary key,
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
)
GO
CREATE TABLE TraPhong
(
	ID varchar(10) primary key,
	MaKhachTro varchar(10),
	MaPhong varchar(10),
	NgayThue date,
	NgayTra date,
)
GO
---------------khóa ngoại
ALTER TABLE Phong
ADD CONSTRAINT fk_Phong_KhuVuc
  FOREIGN KEY (MaKhuVuc)
  REFERENCES KhuVuc(MaKhuVuc)
GO
ALTER TABLE Phong
ADD CONSTRAINT fk_Phong_LoaiPhong
  FOREIGN KEY (MaLoaiPhong)
  REFERENCES LoaiPhong(MaLoaiPhong)
GO
ALTER TABLE PhieuThu
ADD CONSTRAINT fk_PhieuThu_Phong
  FOREIGN KEY (MaPhong)
  REFERENCES Phong(MaPhong)
GO
ALTER TABLE ThongTinKhach
ADD CONSTRAINT fk_ThongTinKhach_Phong
  FOREIGN KEY (MaPhong)
  REFERENCES Phong(MaPhong)
GO
ALTER TABLE TraPhong
ADD CONSTRAINT fk_TraPhong_ThongTinKhach
  FOREIGN KEY (MaKhachTro)
  REFERENCES ThongTinKhach(MaKhachTro)
GO
ALTER TABLE TraPhong
ADD CONSTRAINT fk_TraPhong_Phong
  FOREIGN KEY (MaPhong)
  REFERENCES Phong(MaPhong)
GO
---- THÊM
GO
CREATE TABLE FeedBack
(
    MaFB varchar(10) primary key,
    MaPhong varchar(10),
    MoTa varchar(100),
    CONSTRAINT fk_FeedBack_Phong FOREIGN KEY (MaPhong)
    REFERENCES Phong(MaPhong)
)
GO
CREATE TABLE ChiTietDichVu
(
    MaPhong varchar(10),
    MaDichVu varchar(10),
	 primary key(MaPhong,MaDichVu),
    SoLuong int,
    TongTien float,
    CONSTRAINT fk_Phong_ThongTinKhach FOREIGN KEY (MaPhong)
    REFERENCES Phong(MaPhong),
    CONSTRAINT fk_ChiTietDichVu_DichVu FOREIGN KEY (MaDichVu)
    REFERENCES DichVu(MaDichVu)
)
GO
CREATE TABLE ChiTietPTDichVu
(
	MaDichVu varchar(10),
	MaPT varchar(10),
	primary key(MaDichVu,MaPT),
    CONSTRAINT fk_ChiTietPTDichVu_DichVu FOREIGN KEY (MaDichVu)
    REFERENCES DichVu(MaDichVu),
    CONSTRAINT fk_ChiTietPTDichVu_PhieuThu FOREIGN KEY (MaPT)
    REFERENCES PhieuThu(MaPT)
)
CREATE TABLE UserPhong
(
	ID varchar(10) primary key,
	MatKhau varchar(20),
	MaPhong varchar(10),
	TrangThai int,
	CONSTRAINT fk_UserPhong_PhieuThu FOREIGN KEY (MaPhong)
    REFERENCES Phong(MaPhong)
)
