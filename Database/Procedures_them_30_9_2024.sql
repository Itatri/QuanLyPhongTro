USE QL_ChungCU;
GO

--------------------------------------------------------------------------------------------------------------
-- thủ tục hiển thị danh sách nhân viên
CREATE PROC ShowDichVu as
select * from DichVu;
go
exec ShowDichVu
go

--------------------------------------------------------------------------------------------------------------
-- thủ tục tìm kiếm nhân viên
create proc TimDichVu (@GiaTriCanTim nvarchar(50)) as
SELECT * FROM DichVu WHERE (MaDichVu = @GiaTriCanTim OR TenDichVu LIKE N'%' + @GiaTriCanTim + '%');
go
exec TimDichVu 'DV04'
go

--------------------------------------------------------------------------------------------------------------
--Thủ tục thêm dịch vụ
CREATE PROC ThemDichVu(@MaDV nvarchar(10), @tenDV nvarchar(50), @dongia float) as
insert into DichVu values (@MaDV, @tenDV, @dongia, 0);
GO
exec ThemDichVu 'DV06',N'Giữ thú cưng 1', '1000'
GO
--------------------------------------------------------------------------------------------------------------
-- thủ tục sửa dịch vụ
create proc CapNhatDichVu(@MaDV nvarchar(10), @tenDV nvarchar(50), @dongia float, @trangthai bit) as
update DichVu set @tenDV = @tenDV, DonGia = @dongia, TrangThai = @trangthai  where  MaDichVu= @MaDV;
GO
exec CapNhatDichVu 'DV03','Dich vu ve sinh 1',100000,1
GO
--------------------------------------------------------------------------------------------------------------
-- Thủ tục xóa dịch vụ
CREATE PROC XoaDichVu(@MaDV CHAR(10)) AS
DELETE FROM DichVu WHERE MaDichVu= @MaDV;
GO
EXEC XoaDichVu 'DV04';
GO


select * from DichVu
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
SELECT * FROM PHONG




select MaLoaiPhong from LoaiPhong





----------------------------------
select * from ChiTietDichVu
select * from DangNhap
select * from DichVu
select * from FeedBack
select * from KhuVuc
select * from LoaiPhong
select * from PhieuThu
select * from Phong
select * from ThongTinKhach
select * from TraPhong
select * from UserPhong


Delete from Phong where tenphong = 'A3'

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

select*from TaiKhoanPhong

---------------------------------------------------------------
-- sinh mã phòng tự động
CREATE PROCEDURE SinhMaPhong01
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
