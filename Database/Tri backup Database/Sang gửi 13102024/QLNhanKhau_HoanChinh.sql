-- Create the Test database
CREATE DATABASE QL_ChungCu;
GO
USE QL_ChungCu;
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
	MoTa nvarchar(max)
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
CREATE TABLE LuuTru
(
	MaLuuTru varchar(50) primary key,
	MaPhong varchar(50),
	TenFile varchar(max), --- chỗ lưu trữ
	TrangThai int ---  1: Hợp đồng, 2: CT01
);
GO
CREATE TABLE DuongDan
(
	ID int primary key,
	DDChuKy varchar(50), --- chữ ký thường admin và khách
	DDFile varchar(50), ----- hợp đồng, ct01, cam kết
);
GO
CREATE TABLE DichVuPhieuThu
(
	ID int primary key,
	MaPT varchar(50),
	TenDichVu varchar(max),
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
ALTER TABLE LuuTru
ADD CONSTRAINT fk_LuuTru_Phong
  FOREIGN KEY (MaPhong)
  REFERENCES Phong(MaPhong);
GO
ALTER TABLE DichVuPhieuThu
ADD CONSTRAINT fk_PhieuThu_DichVuPhieuThu
  FOREIGN KEY (MaPT)
  REFERENCES PhieuThu(MaPT);
GO


