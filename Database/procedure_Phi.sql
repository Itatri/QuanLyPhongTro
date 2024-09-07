GO
CREATE PROCEDURE ThongKePhieuThuTheoThangVaNam
    @Thang INT,
    @Nam INT
AS
BEGIN
    SELECT 
        PT.MaPT,
        PT.MaPhong,
        PT.NgayLap,
        PT.NgayThu,
        PT.TienNha,
        (PT.DienMoi - PT.DienCu) AS SoKiDienSuDung, 
        PT.TienDien,
        (PT.NuocMoi - PT.NuocCu) AS SoKiNuocSuDung,  
        PT.TienNuoc,
        ISNULL(COUNT(CTDV.MaDichVu), 0) AS TongSoDichVu,  
        PT.TienDV,
        PT.TongTien,
        PT.TrangThai
    FROM 
        PhieuThu PT
    LEFT JOIN 
        ChiTietDichVu CTDV ON PT.MaPhong = CTDV.MaPhong
    WHERE 
        MONTH(PT.NgayLap) = @Thang         
        AND YEAR(PT.NgayLap) = @Nam
        AND PT.TrangThai = 1  
    GROUP BY 
        PT.MaPT,
        PT.MaPhong,
        PT.NgayLap,
        PT.NgayThu,
        PT.TienNha,
        PT.DienMoi,  
        PT.DienCu,  
        PT.TienDien,
        PT.NuocMoi,  
        PT.NuocCu,  
        PT.TienNuoc,
        PT.TienDV,
        PT.TongTien,
        PT.TrangThai
    ORDER BY 
        PT.NgayLap, PT.MaPT;
END;
GO
GO
CREATE PROCEDURE ThongKeTongHopTheoThang
    @Nam INT
AS
BEGIN
    CREATE TABLE #ThongKeTheoThang
    (
        Thang INT,
        TongSoPhong INT,
        TongSoDien FLOAT,
        TongTienDien FLOAT,
        TongSoNuoc FLOAT,
        TongTienNuoc FLOAT,
        TongTienDichVu FLOAT,
        TongTienNha FLOAT,      
        TongTienTATCA FLOAT        
    );

    DECLARE @Thang INT = 1;

    DECLARE @TongSoPhongNam INT = 0;
    DECLARE @TongSoDienNam FLOAT = 0;
    DECLARE @TongTienDienNam FLOAT = 0;
    DECLARE @TongSoNuocNam FLOAT = 0;
    DECLARE @TongTienNuocNam FLOAT = 0;
    DECLARE @TongTienDichVuNam FLOAT = 0;
    DECLARE @TongTienNhaNam FLOAT = 0;
    DECLARE @TongTienTATCANam FLOAT = 0;

    WHILE @Thang <= 12
    BEGIN
        DECLARE @TongSoPhong INT;
        DECLARE @TongSoDien FLOAT;
        DECLARE @TongTienDien FLOAT;
        DECLARE @TongSoNuoc FLOAT;
        DECLARE @TongTienNuoc FLOAT;
        DECLARE @TongTienDichVu FLOAT;
        DECLARE @TongTienNha FLOAT;
        DECLARE @TongTienTATCA FLOAT;

        SELECT @TongSoPhong = ISNULL(COUNT(DISTINCT PT.MaPhong), 0)
        FROM PhieuThu PT
        WHERE YEAR(PT.NgayLap) = @Nam 
          AND MONTH(PT.NgayLap) = @Thang
          AND PT.TrangThai = 1;  

        SELECT @TongSoDien = ISNULL(SUM(PT.DienMoi - PT.DienCu), 0)  
        FROM PhieuThu PT
        WHERE YEAR(PT.NgayLap) = @Nam 
          AND MONTH(PT.NgayLap) = @Thang
          AND PT.TrangThai = 1;  

        SELECT @TongTienDien = ISNULL(SUM(PT.TienDien), 0)
        FROM PhieuThu PT
        WHERE YEAR(PT.NgayLap) = @Nam 
          AND MONTH(PT.NgayLap) = @Thang
          AND PT.TrangThai = 1;  

        SELECT @TongSoNuoc = ISNULL(SUM(PT.NuocMoi - PT.NuocCu), 0)  
        FROM PhieuThu PT
        WHERE YEAR(PT.NgayLap) = @Nam 
          AND MONTH(PT.NgayLap) = @Thang
          AND PT.TrangThai = 1;  

        SELECT @TongTienNuoc = ISNULL(SUM(PT.TienNuoc), 0)
        FROM PhieuThu PT
        WHERE YEAR(PT.NgayLap) = @Nam 
          AND MONTH(PT.NgayLap) = @Thang
          AND PT.TrangThai = 1;  

        SELECT @TongTienDichVu = ISNULL(SUM(CTDV.TongTien), 0)
        FROM ChiTietDichVu CTDV
        INNER JOIN Phong P ON CTDV.MaPhong = P.MaPhong
        INNER JOIN PhieuThu PT ON P.MaPhong = PT.MaPhong
        WHERE YEAR(PT.NgayLap) = @Nam 
          AND MONTH(PT.NgayLap) = @Thang
          AND PT.TrangThai = 1;  

        SELECT @TongTienNha = ISNULL(SUM(PT.TienNha), 0)
        FROM PhieuThu PT
        WHERE YEAR(PT.NgayLap) = @Nam 
          AND MONTH(PT.NgayLap) = @Thang
          AND PT.TrangThai = 1;

        SET @TongTienTATCA = ISNULL(@TongTienDichVu, 0) + ISNULL(@TongTienDien, 0) + ISNULL(@TongTienNuoc, 0) + ISNULL(@TongTienNha, 0);

        -- Chèn kết quả vào bảng tạm
        INSERT INTO #ThongKeTheoThang 
        (Thang, TongSoPhong, TongSoDien, TongTienDien, TongSoNuoc, TongTienNuoc, TongTienDichVu, TongTienNha, TongTienTATCA)
        VALUES 
        (@Thang, @TongSoPhong, @TongSoDien, @TongTienDien, @TongSoNuoc, @TongTienNuoc, @TongTienDichVu, @TongTienNha, @TongTienTATCA);

        -- Cộng dồn giá trị vào tổng của cả năm
        SET @TongSoPhongNam = @TongSoPhongNam + @TongSoPhong;
        SET @TongSoDienNam = @TongSoDienNam + @TongSoDien;
        SET @TongTienDienNam = @TongTienDienNam + @TongTienDien;
        SET @TongSoNuocNam = @TongSoNuocNam + @TongSoNuoc;
        SET @TongTienNuocNam = @TongTienNuocNam + @TongTienNuoc;
        SET @TongTienDichVuNam = @TongTienDichVuNam + @TongTienDichVu;
        SET @TongTienNhaNam = @TongTienNhaNam + @TongTienNha;
        SET @TongTienTATCANam = @TongTienTATCANam + @TongTienTATCA;

        SET @Thang = @Thang + 1;
    END

    INSERT INTO #ThongKeTheoThang 
    (Thang, TongSoPhong, TongSoDien, TongTienDien, TongSoNuoc, TongTienNuoc, TongTienDichVu, TongTienNha, TongTienTATCA)
    VALUES 
    (13, @TongSoPhongNam, @TongSoDienNam, @TongTienDienNam, @TongSoNuocNam, @TongTienNuocNam, @TongTienDichVuNam, @TongTienNhaNam, @TongTienTATCANam);

    -- Xuất kết quả
    SELECT 
        CASE 
            WHEN Thang = 13 THEN 'Tổng' 
            ELSE CAST(Thang AS VARCHAR) 
        END AS Thang,
        TongSoPhong,
        TongSoDien,
        TongTienDien,
        TongSoNuoc,
        TongTienNuoc,
        TongTienDichVu,
        TongTienNha,
        TongTienTATCA
    FROM #ThongKeTheoThang;

    DROP TABLE #ThongKeTheoThang;
END;


GO
GO
CREATE PROCEDURE GetThongKePhieuThu
AS
BEGIN
    SELECT 
        PT.MaPT,
        PT.MaPhong,
        PT.NgayLap,
        PT.NgayThu,
        PT.TienNha,
        (PT.DienMoi - PT.DienCu) AS SoKiDien,  
        PT.TienDien,
        (PT.NuocMoi - PT.NuocCu) AS SoKiNuoc,  
        PT.TienNuoc,
        PT.TienDV,     
        PT.TongTien
    FROM 
        PhieuThu PT
    WHERE
        PT.NgayLap IS NOT NULL
    ORDER BY 
        PT.NgayLap DESC,  
        PT.MaPT DESC;     
END;



GO
GO
CREATE PROCEDURE GetPhongByName
    @TenPhong NVARCHAR(50)
AS
BEGIN
    SELECT 
        p.MaPhong,
        p.TenPhong,
        lp.DonGia,
        pt.DienMoi,      
        pt.NuocMoi       
    FROM 
        Phong p
    JOIN 
        LoaiPhong lp ON p.MaLoaiPhong = lp.MaLoaiPhong
    LEFT JOIN 
        PhieuThu pt ON p.MaPhong = pt.MaPhong  
    WHERE 
        p.TenPhong = @TenPhong
    ORDER BY 
        pt.NgayLap DESC;  
END;



