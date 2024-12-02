create database Cinema_version4;
go
use Cinema_version4
go

set dateformat dmy
create table QuanLi(
	maQL varchar(20) primary key,
	hoTen nvarchar(50) not null,
	sdt char(10) not null ,
	gioiTinh bit,
	ngaySinh date,
	diaChi nvarchar(50),
	cccd bigint,
	tinhTrang bit default 1,
	tenTK varchar(30) not null ,
	matKhau varchar(200) not null,
	CONSTRAINT U_ql_user unique(tenTK,matKhau),
	CONSTRAINT U_ql_sdt unique(sdt),
	CONSTRAINT U_ql_cccd unique(cccd),
	CONSTRAINT CK_ql_sdt CHECK(sdt LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]')
)
create table NhanVien(
	maNV varchar(20) primary key,
	hoTen nvarchar(50) not null,
	sdt char(10) not null ,
	gioiTinh bit,
	ngaySinh date,
	diaChi nvarchar(50),
	cccd bigint,
	tinhTrang bit default 1,
	tenTK varchar(30) not null ,
	matKhau varchar(200) not null,
	maQL varchar(20) not null,
	foreign key (maQL) references QuanLi(maQL) on update cascade on delete cascade,--thêm mã quản lí vào table nhân viên 
	CONSTRAINT U_nv_user unique(tenTK,matKhau),
	CONSTRAINT U_nv_sdt unique(sdt),
	CONSTRAINT U_nv_cccd unique(cccd),
	CONSTRAINT CK_nv_sdt CHECK(sdt LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]')
)

create table KhachHang(
	maKH varchar(20) primary key,
	hoTen nvarchar(50) null,
	sdt char(10) null,
	ngaySinh date null,
	email varchar(50) null,
	tinhTrang bit default 1 null,
	tenTK varchar(30) null,
	matKhau varchar(200) null,
	diemThuong int default 0,--diem thuong va voucher
	soLuongVoucher int default 0,
	diemDanh int default 0,
	ngayDiemDanhCuoi date null,
	CONSTRAINT U_kh_KH unique(sdt,tenTK,matKhau),
	CONSTRAINT U_kh_user unique(tenTK,matKhau),
	CONSTRAINT U_kh_sdt unique(sdt),
	CONSTRAINT CK_kh_sdt CHECK(sdt LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]')
)
create table TheLoaiPhim(
	maLPhim varchar(20) primary key,
	tenLPhim nvarchar(50) not null,
	moTaLP nvarchar(100)
)

create table Phim(
	maPhim varchar(20) primary key,
	maLPhim varchar(20),
	tenPhim nvarchar(50) not null,
	daoDien nvarchar(50)not null,
	doTuoiYeuCau int not null,
	ngayKhoiChieu date not null,
	thoiLuong int not null,
	tinhTrang bit default 1,
	hinhDaiDien varchar(MAX) null,
	video nvarchar(max),
	moTa nvarchar(max),
	foreign key (maLPhim) references TheLoaiPhim(maLPhim) ON UPDATE CASCADE ON DELETE CASCADE
)

create table BinhLuan(
	maBinhLuan INT PRIMARY KEY IDENTITY(1,1),
	maPhim varchar(20) not null,
	maKH varchar(20) not null,
	gio datetime  not null,
	noiDung nvarchar(max) not null,
	tinhTrang bit default 1,
	foreign key (maPhim) references Phim(maPhim) ON UPDATE CASCADE ON DELETE CASCADE ,
	foreign key (maKH) references KhachHang(maKH) ON UPDATE CASCADE ON DELETE CASCADE 
)

create table PhongChieu(
	maPhong varchar(20) primary key,
	tongSoGhe int not null,
	loaiMayChieu char(1),
	loaiAmThanh char(1),
	dienTich decimal,
	tinhTrang bit default 1,
	CONSTRAINT CK_mayChieu check (loaiMayChieu in ('A','B','C')),
	CONSTRAINT CK_amThanh check (loaiAmThanh in ('A','B','C'))
)
create table CaChieu(
	maCa varchar(20) primary key,
	tenCa varchar(20), 
	thoiGianBatDau Time,
	thoiGianKetThuc Time,
	CONSTRAINT U_caChieu unique(tenCa),
	CONSTRAINt CK_thoiGianCa check(thoiGianBatDau < thoiGianKetThuc)
)
create table SuatChieu(
	maSuat varchar(20) primary key,
	maPhim varchar(20) not null,
	maPhong varchar(20) not null,
	maCa varchar(20),
	ngayChieu date not null,
	tinhTrang bit not null, 
	foreign key (maCa) references CaChieu(maCa) ON UPDATE CASCADE ON DELETE CASCADE,
	foreign key (maPhim) references Phim(maPhim) ON UPDATE CASCADE ON DELETE CASCADE,
	foreign key (maPhong) references PhongChieu(maPhong) ON UPDATE CASCADE ON DELETE CASCADE
)
create table Ghe(
	maGhe varchar(20) primary key,
	tinhTrang bit not null
)


--bảng vé thêm số lượng tối đa
create table Ve(
	maVe varchar(20) primary key,
	maPhim varchar(20) not null,
	maNV varchar(20) null,
	tinhTrang bit default 1,
	soLuongToiDa int default 1,
	soLuongDaBan int default 1,
	tien float null,
	unique(maPhim)
)

--nối bảng nhân viên với ghế vô vô bookve
create table BookVe(
	maBook varchar(20) primary key,
	maKH varchar(20)  null,
	maNV varchar(20)  null,
	maSuat varchar(20) not null,
	maVe varchar(20)  null,
	tongTien float  null,
	suDungVoucher int default 0,
	ngayMua date null,
	soLuongVeDaDat int default 0,
	foreign key (maKH) references KhachHang(maKH) ON UPDATE CASCADE ON DELETE CASCADE,
	foreign key (maNV) references Nhanvien(maNV) ON UPDATE CASCADE ON DELETE CASCADE,
	foreign key (maSuat) references SuatChieu(maSuat) ON UPDATE CASCADE ON DELETE CASCADE,
	foreign key (maVe) references Ve(maVe) ON UPDATE CASCADE ON DELETE CASCADE
	
)

CREATE TABLE BookGhe (
    maGhe varchar(20),
    maBook varchar(20),
    PRIMARY KEY (maGhe, maBook),
    FOREIGN KEY (maGhe) REFERENCES Ghe(maGhe) ON UPDATE CASCADE ON DELETE CASCADE,
    FOREIGN KEY (maBook) REFERENCES BookVe(maBook) ON UPDATE CASCADE ON DELETE CASCADE
);

GO
CREATE TRIGGER TG_CreIdNhanVien
ON NhanVien
INSTEAD OF INSERT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Prefix VARCHAR(20) = 'NV'; 

    INSERT INTO NhanVien(maNV,hoTen,sdt,gioiTinh,ngaySinh,diaChi,cccd,tinhTrang,tenTK,matKhau,maQL)
    SELECT 
        @Prefix + CONVERT(VARCHAR(20), COALESCE((SELECT MAX(CAST(SUBSTRING(maNV, 3, LEN(maNV)) AS INT)) FROM NhanVien), 0) + ROW_NUMBER() OVER (ORDER BY (SELECT NULL))),
        hoTen,sdt,gioiTinh,ngaySinh,diaChi,cccd,tinhTrang,tenTK,matKhau,maQL
    FROM inserted;
END;

go
CREATE TRIGGER TG_CreIdQuanLi
ON QuanLi
INSTEAD OF INSERT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Prefix VARCHAR(20) = 'QL'; 

    INSERT INTO QuanLi(maQL,hoTen,sdt,gioiTinh,ngaySinh,diaChi,cccd,tinhTrang,tenTK,matKhau )
    SELECT 
        @Prefix + CONVERT(VARCHAR(20), COALESCE((SELECT MAX(CAST(SUBSTRING(maQL, 3, LEN(maQL)) AS INT)) FROM QuanLi), 0) + ROW_NUMBER() OVER (ORDER BY (SELECT NULL))),
        hoTen,sdt,gioiTinh,ngaySinh,diaChi,cccd,tinhTrang,tenTK,matKhau
    FROM inserted;
END;

go
GO
CREATE TRIGGER TG_CreIdKhachHang
ON KhachHang
INSTEAD OF INSERT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Prefix VARCHAR(20) = 'KH';

    INSERT INTO KhachHang(maKH, hoTen, sdt, ngaySinh, email, tinhTrang, tenTK, matKhau, diemThuong, soLuongVoucher, diemDanh,ngayDiemDanhCuoi)
    SELECT 
        @Prefix + CONVERT(VARCHAR(20), COALESCE((SELECT MAX(CAST(SUBSTRING(maKH, 3, LEN(maKH)) AS INT)) FROM KhachHang), 0) + ROW_NUMBER() OVER (ORDER BY (SELECT NULL))),
        hoTen, sdt, ngaySinh, email, tinhTrang, tenTK, matKhau, diemThuong, soLuongVoucher, diemDanh, ngayDiemDanhCuoi
    FROM inserted;
END;

go
CREATE TRIGGER TG_CreIdTLPhim
ON TheLoaiPhim
INSTEAD OF INSERT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Prefix VARCHAR(20) = 'LP'; 

    INSERT INTO TheLoaiPhim(maLPhim,tenLPhim,moTaLP)
    SELECT 
        @Prefix + CONVERT(VARCHAR(20), COALESCE((SELECT MAX(CAST(SUBSTRING(maLPhim, 2, LEN(maLPhim)) AS INT)) FROM TheLoaiPhim), 0) + ROW_NUMBER() OVER (ORDER BY (SELECT NULL))),
         tenLPhim,moTaLP
    FROM inserted;
END;

go
CREATE TRIGGER TG_CreIdPhim
ON Phim
INSTEAD OF INSERT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Prefix VARCHAR(20) = 'P'; 

    INSERT INTO Phim(maPhim,tenPhim,daoDien,maLPhim,doTuoiYeuCau,ngayKhoiChieu,thoiLuong,tinhTrang,hinhDaiDien,video,moTa)
    SELECT 
        @Prefix + CONVERT(VARCHAR(20), COALESCE((SELECT MAX(CAST(SUBSTRING(maPhim, 2, LEN(maPhim)) AS INT)) FROM Phim), 0) + ROW_NUMBER() OVER (ORDER BY (SELECT NULL))),
         tenPhim,daoDien,maLPhim,doTuoiYeuCau,ngayKhoiChieu,thoiLuong,tinhTrang,hinhDaiDien,video,moTa
    FROM inserted;
END;

go
CREATE TRIGGER TG_CreIdPhongChieu
ON PhongChieu
INSTEAD OF INSERT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Prefix VARCHAR(20) = 'PC'; 

    INSERT INTO PhongChieu(maPhong, tongSoGhe, loaiMayChieu, loaiAmThanh, dienTich,tinhTrang)
    SELECT 
        @Prefix + CONVERT(VARCHAR(20), COALESCE((SELECT MAX(CAST(SUBSTRING(maPhong, 3, LEN(maPhong)) AS INT)) FROM PhongChieu), 0) + ROW_NUMBER() OVER (ORDER BY (SELECT NULL))),
        tongSoGhe, loaiMayChieu, loaiAmThanh, dienTich,tinhTrang
    FROM inserted;
END;

go
CREATE TRIGGER TG_CreIdCaChieu
ON CaChieu
INSTEAD OF INSERT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Prefix VARCHAR(20) = 'CC'; 

    INSERT INTO CaChieu(maCa,tenCa,thoiGianBatDau,thoiGianKetThuc)
    SELECT 
        @Prefix + CONVERT(VARCHAR(20), COALESCE((SELECT MAX(CAST(SUBSTRING(maCa, 3, LEN(maCa)) AS INT)) FROM CaChieu), 0) + ROW_NUMBER() OVER (ORDER BY (SELECT NULL))),
         tenCa,thoiGianBatDau,thoiGianKetThuc
    FROM inserted;
END;

go
CREATE TRIGGER TG_CreIdSuatChieu
ON SuatChieu
INSTEAD OF INSERT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Prefix VARCHAR(20) = 'SC'; 

    INSERT INTO SuatChieu(maSuat,maPhim,maPhong,maCa,ngayChieu,tinhTrang)
    SELECT 
        @Prefix + CONVERT(VARCHAR(20), COALESCE((SELECT MAX(CAST(SUBSTRING(maSuat, 3, LEN(maSuat)) AS INT)) FROM SuatChieu), 0) + ROW_NUMBER() OVER (ORDER BY (SELECT NULL))),
         maPhim,maPhong,maCa,ngayChieu,tinhTrang
    FROM inserted;
END;

--thay doi 
go
CREATE TRIGGER TG_CreIdBookVe
ON BookVe
INSTEAD OF INSERT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Prefix VARCHAR(20) = 'BV'; 

	CREATE TABLE #InsertedValues (
        maBook VARCHAR(50) -- Thay đổi kiểu dữ liệu nếu cần thiết
    );

    INSERT INTO BookVe(maBook, maKH,maNV, maSuat,maVe, tongTien, ngayMua,soLuongVeDaDat,suDungVoucher )
	OUTPUT inserted.maBook INTO #InsertedValues
    SELECT 
        @Prefix + CONVERT(VARCHAR(20), COALESCE((SELECT MAX(CAST(SUBSTRING(maBook, 3, LEN(maBook)) AS INT)) FROM BookVe), 0) + ROW_NUMBER() OVER (ORDER BY (SELECT NULL))),
         maKH,maNV, maSuat ,maVe, tongTien, ngayMua,soLuongVeDaDat,suDungVoucher
    FROM inserted;

	-- Lấy các giá trị từ bảng tạm thời
    SELECT maBook FROM #InsertedValues;

    -- Xóa bảng tạm thời sau khi sử dụng
    DROP TABLE #InsertedValues;
END;
go
CREATE TRIGGER TG_CreIdVe
ON Ve
INSTEAD OF INSERT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Prefix VARCHAR(20) = 'V'; 

    INSERT INTO Ve(maVe,maPhim,maNV,soLuongToiDa,soLuongDaBan,tinhTrang, tien)
    SELECT 
        @Prefix + CONVERT(VARCHAR(20), COALESCE((SELECT MAX(CAST(SUBSTRING(maVe, 2, LEN(maVe)) AS INT)) FROM Ve), 0) + ROW_NUMBER() OVER (ORDER BY (SELECT NULL))),
         maPhim,maNV,soLuongToiDa,soLuongDaBan,tinhTrang, tien
    FROM inserted;
END;
GO



--Không cho tentk matkhau null đc vì hai dòng null nó tính trùng nhau
insert into KhachHang (hoTen,sdt,ngaySinh,email,tinhTrang,tenTK,matKhau,soLuongVoucher)
values	(N'Lê A','0900000001','5/5/2004','A0900000001@gmail.com',  1,'leduy','$2b$10$PQiWRRTwcmRHGTqO8wTZyOgbP1wi0lMt/qLJQcBBIJWb44z1JfwgG',10),
		(N'Lê B','0900000002','5/5/2004','B0900000002@gmail.com',  1,'B0900000002','B09002',1),
		(N'Lê C','0900000003','5/5/2004','C0900000003@gmail.com',  0,'C0900000003','C09003',1),
		(N'Lê D','0900000004','5/5/2004','D0900000004@gmail.com',  1,'D0900000004','D09004',1),
		(N'Lê E','0900000005','5/5/2004','E0900000005@gmail.com',  1,'E0900000005','E09005',2),
		(N'Lê F','0900000006','5/5/2004','F0900000006@gmail.com',1,'F0900000006','F09006',4),
		(N'Lê G','0900000007','5/5/2004','G0900000007@gmail.com',1,'G0900000007','G09007',5),
		(N'Lê H','0900000008','5/5/2004','H0900000008@gmail.com',1,'H0900000008','H09008',1),
		(N'Lê I','0900000009','5/5/2004','I0900000009@gmail.com',0,'I0900000009','I09009',0),
		(N'Lê K','0900000010','5/5/2004','K0900000010@gmail.com',1,'K0900000010','K09010',1),
		(N'Lê L','0900000011','5/5/2004','L0900000011@gmail.com',1,'L0900000011','L09011',4),
		(N'Nguyễn Văn B','0900000013','5/5/2004','nB0900000013@gmail.com',  1,'nA0900000012','nA0912',4),
		(N'Nguyễn Văn C','0900000014','5/5/2004','nC0900000014@gmail.com',  0,'nB0900000013','nB0913',6),
		(N'Nguyễn Văn D','0900000015','5/5/2004','nD0900000015@gmail.com',  1,'nC0900000014','nC0914',3),
		(N'Nguyễn Văn E','0900000016','5/5/2004','nE0900000016@gmail.com',  1,'nD0900000015','nD0915',7),
		(N'Nguyễn Văn F','0900000017','5/5/2004','nF0900000017@gmail.com',1,'nE0900000016','nE0916',8),
		(N'Nguyễn Văn G','0900000018','5/5/2004','nG0900000018@gmail.com',1,'nF0900000017','nF0917',1),
		(N'Nguyễn Văn H','0900000019','5/5/2004','nH0900000019@gmail.com',1,'nG0900000018','nG0918',9),
		(N'Nguyễn Văn I','0900000020','5/5/2004','nI0900000020@gmail.com',0,'nH0900000019','nH0919',1),
		(N'Nguyễn Văn K','0900000021','5/5/2004','nK0900000021@gmail.com',1,'nI0900000020','nI0920',6),
		(N'Nguyễn Văn L','0900000022','5/5/2004','nL0900000022@gmail.com',1,'nK0900000021','nK0921',4)

insert into QuanLi(hoTen,sdt,gioiTinh,ngaySinh,diaChi,cccd,tinhTrang,tenTK,matKhau)
values	(N'Lê Ha Ha','0910000001',1,'5/5/2004','Đà Nẵng',04920492 , 1,'admin','ad123'),
		(N'Lê Hi Hi','0920000002',1,'5/5/2004','Đà Nẵng',04920493 , 1,'Hi0920000002','Hi092002')

insert into CaChieu(tenCa,thoiGianBatDau)
values	('Ca 1','7:00:00'),
		('Ca 2','9:00:00'),
		('Ca 3','11:00:00'),
		('Ca 4','14:00:00'),
		('Ca 5','16:00:00'),
		('Ca 6','19:00:00')

insert into NhanVien (hoTen,sdt,gioiTinh,ngaySinh,diaChi,cccd,tinhTrang,tenTK,matKhau,maQL)
values	(N'Trần A','0100000001',1,'1-1-2001',N'Đà Nẵng',04920481,1,'leduy','ad123','QL1'),
		(N'Trần D','0100000002',0,'1-3-2001',N'Đà Nẵng',04920482,1,'D0100000002','D012','QL1'),
		(N'Trần C','0100000003',1,'1-9-2001',N'Đà Nẵng',04920483,0,'C0100000003','C013','QL1'),
		(N'Trần B','0100000004',1,'1-5-2001',N'Đà Nẵng',04920484,1,'B0100000004','B014','QL2'),
		(N'Trần X','0100000014',1,'1-5-2001',N'Đà Nẵng',04928884,1,'X0100000014','X0114','QL2'),
		(N'Trần Y','0100000024',1,'1-5-2001',N'Đà Nẵng',04924444,1,'Y0100000024','Y0124','QL1'),
		(N'Trần Z','0100000034',1,'1-5-2001',N'Đà Nẵng',04929950,1,'Z0100000034','Z0134','QL1')
		
insert into TheLoaiPhim(tenLPhim)
values  (N'Tình cảm'),
		(N'Hành động'),
		(N'Kinh dị'),
		(N'Hài kịch'),
		(N'Chính kịch'),
		(N'Hoạt hình'),
		(N'Viễn tưởng')

insert into PhongChieu (tongSoGhe, loaiMayChieu, loaiAmThanh, dienTich,tinhTrang)
values	(15,'a','a',70,1),
		(15,'b','b',70,1),
		(15,'c','a',70,1),
		(15,'a','c',70,1)

insert into Ghe (maGhe,tinhTrang)
values  ('A1',1),
		('A2',1),
		('A3',1),
		('A4',1),
		('A5',1),
		('B1',1),
		('B2',1),
		('B3',1),
		('B4',1),
		('B5',1),
		('C1',1),
		('C2',1),
		('C3',1),
		('C4',1),
		('C5',1)

insert into Phim (maLPhim,tenPhim,daoDien,doTuoiYeuCau,ngayKhoiChieu,thoiLuong,tinhTrang,hinhDaiDien,video,moTa)--dang chieu tu ngay 1/1
values	('LP1',N'Titanic',N'Ngọc Huân',18 ,'1-1-2024',145 ,1,'https://res.cloudinary.com/dyzx3s8h1/image/upload/v1732356874/mmmc09monnroiknfn3y0.jpg','F2RnxZnubCM',N'"Titanic" là một bộ phim kể về câu chuyện tình yêu giữa Jack và Rose, hai nhân vật chính, trên tàu Titanic, một trong những sự kiện lịch sử nổi tiếng nhất. Jack là một người nghèo lang thang, còn Rose là một phụ nữ giàu có và đang bị hòa mình vào cuộc hôn nhân không hạnh phúc. Hai người gặp nhau trên con tàu huyền thoại này và phải đối mặt với thảm kịch khi tàu đâm vào một tảng băng. Phim kết thúc với sự hy sinh của Jack để cứu Rose, trong khi tàu chìm dần vào đáy biển. "Titanic" không chỉ là một bộ phim về tình yêu, mà còn là một tác phẩm nghệ thuật đầy cảm xúc và lôi cuốn, với những hình ảnh ấn tượng và âm nhạc đậm chất cảm xúc.'),
		('LP2',N'Avenger',N'Phước Lý',06 ,'1-1-2024',130 ,1,'https://res.cloudinary.com/dyzx3s8h1/image/upload/v1732356850/pxshcg7guvc1u6umy5ij.jpg','F_mhWxOjxp4',N'Avenger là câu truyện về Loki, người em trai nuôi độc ác của Thor đến từ hành tinh Asgard xa xôi, đột nhập vào căn cứ của S.H.I.E.L.D để chiếm khối Tesseract chứa nguồn năng lượng vô hạn. Hắn còn âm mưu dẫn một đội quân tới Trái đất thôn tính và biến loài người thành nô lệ. Nick Fury, giám đốc của S.H.I.E.L.D nỗ lực tập hợp một đội quân tinh nhuệ nhất để bảo vệ trái đất khỏi âm mưu của Loki. Tuy nhiên, anh và người cộng sự lâu năm là điệp viên Coulson phải tìm cách thuyết phục các siêu anh hùng phối hợp thành một đội thay vì chống lại nhau...'),
		('LP7',N'Avatar',N'Tam Quốc',18 ,'1-1-2024',180 ,1,'https://res.cloudinary.com/dyzx3s8h1/image/upload/v1732356821/tefevricbuhhttjh7w0z.jpg','bDHD1ueL4a4',N'Avatar là câu chuyện về người anh hùng “bất đắc dĩ” Jake Sully – một cựu sĩ quan thủy quân lục chiến bị liệt nửa thân. Người anh em sinh đôi của anh được chọn để tham gia vào chương trình cấy gien với người ngoài hành tinh Na’vi nhằm tạo ra một giống loài mới có thể hít thở không khí tại hành tinh Pandora. Giống người mới này được gọi là Avatar. Sau khi anh của Jake bị giết, Jake được chọn để thay thế anh mình và đã trở thành một Avatar, Jake có nhiệm vụ đi tìm hiểu và nghiên cứu hành tinh Pandora. Những thông tin mà anh thu thập được rất có giá trị cho chiến dịch xâm chiếm hành tinh xanh thứ hai này của loài người.'),
		--sap chieu 31/12
		('LP5',N'Mắc biếc',N'Victor Vũ',18 ,'31-12-2024',125 ,1,'https://res.cloudinary.com/dyzx3s8h1/image/upload/v1732356895/emghfckrgbmzhr5cxjqk.jpg','HsgTIMDA6ps',N'Mắt Biếc là một bộ phim điện ảnh Việt Nam sản xuất vào năm 2019, được chuyển thể từ tiểu thuyết cùng tên của nhà văn Nguyễn Nhật Ánh. Bộ phim được đạo diễn bởi Victor Vũ và đã tạo nên một cơn sốt và trở thành một trong những bộ phim thành công nhất trong lịch sử điện ảnh Việt Nam.Cốt truyện của "Mắt Biếc" xoay quanh câu chuyện tình yêu đẹp và đầy cảm xúc giữa người anh mù Đoàn (do Trần Ngọc Hiếu thủ vai) và cô bạn gái xinh đẹp chuyển đến hàng xóm tên Lan (do Trúc Anh thủ vai). Tình yêu trong "Mắt Biếc" là một tình yêu thuần khiết và sâu sắc, được hình thành từ những chia sẻ, hiểu biết và tôn trọng lẫn nhau.Bên cạnh câu chuyện tình yêu chính, bộ phim còn tái hiện hình ảnh cuộc sống thôn quê, những gia đình thân thương, và tạo nên một thế giới đẹp màu sắc và ngọt ngào.'),
		('LP2',N'Transformers: Kỷ Nguyên Hủy Diệt'	  ,N'James Cameron',18 ,'31-12-2024',145 ,1,'https://res.cloudinary.com/dyzx3s8h1/image/upload/v1732356919/bykxnsrmkbmca26kyfx3.jpg','1oY-23R9PKw',N'Robot Đại Chiến 4: Kỷ Nguyên Hủy Diệt - Tranformers 4: Age Of Extinction tiếp tục là một thế giới với đầy rẫy những robot. Kỷ Nguyên Huỷ Diệt bắt đầu sau một trận chiến lịch sử để lại một thành phố đổ nát. Với những mảnh ghép còn xót lại, một nhóm những robot cho thấy khả năng của họ có khả năng thay đổi lịch sử. Dẫn đầu nhóm autorobot là Optimus Prime chiến đấu với Dinobot, một cuộc chiến giữa thiện và ác. Sự đa dạng của Dinobot với robot khủng long, robot thú sẽ làm cho màn đối kháng trở nên kịch tích và ấp dẫn hơn bao giờ hết. Liệu Autobot và con người có thể chống lại Dinobot và thoát khỏi thảm họa diệt vong? Chúc các bạn xem phim Transformers 4: Kỷ Nguyên Hủy Diệt')/*,
		('LP2',N'Kong: Đảo Đầu lâu'				      ,N'Lê Công Khánh',16 ,'9-10-2024',145 ,1,null,'b',N'Phim Kong: Đảo Đầu Lâu nói về quái vật huyền thoại của Hollywood kể từ King Kong (2005). Phim lấy bối cảnh chủ yếu tại hòn đảo Đầu Lâu, quê hương của vua loài khỉ.Kong: Skull Islands được quay ngoại cảnh ở Tràng An, Vân Long, Tam Cốc, Vịnh Hạ Long và Động Phong Nha-Kẻ Bàng cùng các địa danh quốc tế như Hawaii, Úc...'),
		('LP2',N'Captain America: Chiến binh mùa đông',N'Dark Sunsine ',12 ,'3-9-2023',145 ,1,null,'b',N'Chiến Binh Mùa Đông - Captain America: The Winter Soldier là phần tiếp theo của Captain America: The First Avenger. Steve Rogers muốn thấu hiểu một nước Mỹ hiện đại sau khi bị đông lạnh 50 năm trong nước đá. Một mối đe dọa mới chống lại S.H.I.E.L.D xuất hiện. Đó chính là Chiến Binh Mùa Đông. Trong lúc nguồn gốc của Chiến Binh Mùa Đông không rõ ràng, mối đe dọa của SHEILD đến từ tổ chức Hydra. Đặc biệt là sau khi thỏa hiệp với Nick Fury và chất Romanoff, SHIELD của rơi vào tình trạng bị thu hồi và bị cho là những kẻ phản bội có tổ chức. Captain America mở một cuộc điều tra nguồn gốc của SHIELD và anh trở về nơi anh từng trải qua đào tạo cơ bản gần 100 năm trước đây. Những gì anh và Romanoff khám phá ở đó tạo ra một sự phát triển rất đáng ngạc nhiên với SHIELD, một dự án bí mật được gọi là "Chiến dịch Cái nhìn sâu sắc", và sự ra mắt của thế hệ tiếp theo của Helicarrier. Họ có thể ngăn chặn nó trước khi quá muộn?'),
		('LP2',N'Người Dơi'							  ,N'Phonix Hot'   ,16 ,'11-10-2022',145 ,1,null,'b',N'Sau cái chết của luật sư Harvey Dent, Batman nhận trách nhiệm cho tội ác của Dent để bảo vệ danh tiếng cho Dent và sau đó bị đuổi bắt bởi Cục cảnh sát thành phố Gotham. Tám năm sau các sự kiện của The Dark Knight, Batman trở lại Gotham, nơi anh gặp cô Selina Kyle bí ẩn trong khi ngăn chặn kế hoạch nhằm tiêu diệt thành phố của nhân vật phản diện Bane...'),
		('LP2',N'Đại Chiến Hành Tinh Khỉ',N'Lý Hải',18 ,'10-6-2024',145 ,1,null,'https://www.youtube.com/embed/8ZFsTMe9JDw?si=y-f9UvG7fth0nEwa',N'Đại Chiến Hành Tinh Khỉ War for the Planet of the Apes 2017 Full HD Vietsub
		Thuyết Minh War for the Planet of the Apes, War for the Planet of the Apes 2017 War for the Planet of the Apes, bộ phim thứ ba và cũng là phần cuối cùng 
		của bộ ba Planet of the Apes. Tiếp nối phần trước, sau khi quân đội biết được sự tồn tại của xã hội vượn dưới sự chỉ huy của Caesar, họ quyết định chiến 
		đấu chống lại Caesar, một nhóm binh lính do một vị tướng tàn bạo lãnh đạo. Về phần vua khỉ của chúng ta, sau những mất mát của mình, anh ta dần trở nên đen 
		tối hơn, có những suy nghĩ độc đoán hơn về con người và cách thức thống trị của mình. Số phận của hai chủng tộc này phụ thuộc vào trận chiến cuối cùng này!')
		*/

insert into SuatChieu (maPhim,maPhong,maCa,ngayChieu,tinhTrang)--ngay 28 cho dang chieu 
values	('P1' ,'PC1','CC3' ,'28-12-2024',1),--phim 1 ca 3 
		('P1' ,'PC3','CC5' ,'28-12-2024',1),-- phim 1 ca 5
		('P1' ,'PC4','CC5' ,'28-12-2024',1),
		('P1' ,'PC3','CC6' ,'28-12-2024',1),-- phim 1 ca 6
								 
		('P2' ,'PC4','CC2' ,'28-12-2024',1),
		('P2' ,'PC1','CC5' ,'28-12-2024',1),--phim 2 ca 5
		('P3' ,'PC1','CC4' ,'28-12-2024',1),--phim 3 ca 4
								 
		('P3' ,'PC1','CC6' ,'28-12-2024',1)--phim 3 ca 6


/*insert into SuatChieu (maPhim,maPhong,maCa,ngayChieu,tinhTrang)--ngay 30 cho sap chieu
values	('P1' ,'PC1','CC3' ,'30-10-2024',1),--phim 1 ca 3 
		('P1' ,'PC2','CC3' ,'30-10-2024',1),
		('P1 ','PC3','CC3' ,'30-10-2024',1),
		('P1' ,'PC4','CC3' ,'30-10-2024',1),
		('P1' ,'PC3','CC5' ,'30-10-2024',1),-- phim 1 ca 5
		('P1' ,'PC4','CC5' ,'30-10-2024',1),
		('P1' ,'PC3','CC6' ,'30-10-2024',1),-- phim 1 ca 6
		('P1' ,'PC4','CC6' ,'30-10-2024',1),
		('P2' ,'PC1','CC2' ,'30-10-2024',1),--phim 2 ca 2
		('P2' ,'PC2','CC2' ,'30-10-2024',1),
		('P2 ','PC3','CC2' ,'30-10-2024',1),
		('P2' ,'PC4','CC2' ,'30-10-2024',1),
		('P2' ,'PC1','CC5' ,'30-10-2024',1),--phim 2 ca 5
		('P2' ,'PC2','CC5' ,'30-10-2024',1),
		('P3' ,'PC1','CC4' ,'30-10-2024',1),--phim 3 ca 4
		('P3' ,'PC2','CC4' ,'30-10-2024',1),
		('P3 ','PC3','CC4' ,'30-10-2024',1),
		('P3' ,'PC4','CC4' ,'30-10-2024',1),
		('P3' ,'PC1','CC6' ,'30-10-2024',1),--phim 3 ca 6
		('P3' ,'PC2','CC6' ,'30-10-2024',1) */


insert into SuatChieu (maPhim,maPhong,maCa,ngayChieu,tinhTrang)--ngay 31/12/2025 cho sap chieu
values	('P4' ,'PC1','CC3' ,'31/12/2025',1),--phim 1 ca 3 
		('P4' ,'PC3','CC6' ,'31/12/2025',1),-- phim 1 ca 6
		('P5' ,'PC1','CC2' ,'31/12/2025',1),--phim 2 ca 2
		('P5' ,'PC1','CC6' ,'31/12/2025',1)--phim 3 ca 6 

--select * from SuatChieu
--where ngayChieu = '28-10-2024'

insert into Ve (maPhim, maNV,soLuongToiDa, tinhTrang, tien)--ngay 26
values  ('P1','NV1',100, 1,60000),--sc1
		('P2','NV2',150, 1,50000),--sc9
		('P3','NV2',100, 1,70000),--sc15
		('P4','NV1',100, 1,60000),--sc1
		('P5','NV2',100, 1,40000)

set dateformat dmy
insert into BookVe (maKH,maVe,maSuat, tongTien,soLuongVeDaDat, ngayMua)
values	('KH2','V1','SC1',60000,1, '20/12/2024');

		


/*insert into BookGhe (maBook,maGhe)
values	('BV1' ,'A1'),
		('BV2' ,'A2'),
		('BV3' ,'A3'),
		('BV4' ,'A4'),
		('BV5' ,'A5'),
		('BV6' ,'B1'),
		('BV7' ,'B2'),
		('BV8' ,'B3'),
		('BV9' ,'B4') */
		
--next insert 
--27/10


--end 27/10
--28/10


	--hàm tính tuổi của khách hàng từ ngày sinh của khách hàng
go
create function fTinhTuoi(
	@maKH varchar(20)
)
returns int
as 
begin 
	declare @tuoi int 
	select @tuoi= datediff(MONTH,KhachHang.ngaySinh ,GETDATE())
	from KhachHang
	where maKH=@maKH
	return @tuoi
end 

--trigger đảm bảo tuổi đủ mới được mua vé 
go
create trigger tDuTuoi
on BookVe
after insert
as 
begin 
	declare @check int, @doTuoi int  
	select @check=dbo.fTinhTuoi(i.maKH)
	from  inserted i
	select @doTuoi=doTuoiYeuCau
	from Phim p
	if(@check < @doTuoi)
	begin
		print N'Chưa đủ tuổi!'
		rollback;
	end
end
go
--trigger cap nhat ngay mua 
create trigger tNgayMua
on BookVe
after insert 
as
begin
	update BookVe
	set ngayMua = GETDATE()
	from BookVe, inserted
	where BookVe.maBook = inserted.maBook
end

go
--trigger tinh tong tien khi book ve 
/*create trigger tTongTien
on BookVe
after insert 
as 
begin
	update BookVe
	set tongTien= 50000*i.tongSoVe
	from BookVe b, inserted i
	where b.maBook = i.maBook
end 
go*/

-- số lượng phim đã và đang chiếu 
--select count(maPhim) as soPhimDaChieu
--from Phim 

--khách hàng từ trước đến nay
--select count(maKH) as soKhachHangDaDatVe
--from KhachHang 

--nhân viên đang làm việc
--select maNV, hoTen
--from NhanVien
--where tinhTrang = 1

go
CREATE FUNCTION TongHopThongKe
(
    @ngayBatDau DATE,
    @ngayKetThuc DATE
)
RETURNS TABLE
AS 
RETURN (
    SELECT tenPhim,
        COUNT(DISTINCT SuatChieu.ngayChieu) AS tongSoNgayChieu,
        ISNULL(COUNT(BookGhe.maBook),0) AS tongSoVeDaBan,
        ISNULL(COUNT(BookGhe.maBook),0) * 60000 AS doanhThu
    FROM 
        SuatChieu
    LEFT JOIN
        BookVe ON SuatChieu.maSuat= BookVe.maSuat
	LEFT JOIN
        BookGhe ON BookGhe.maBook= BookVe.maBook
	LEFT JOIN 
		Phim ON Phim.maPhim = SuatChieu.maPhim
    WHERE 
        SuatChieu.ngayChieu BETWEEN @ngayBatDau AND @ngayKetThuc
    GROUP BY
        tenPhim
);

go
--SELECT * 
--FROM TongHopThongKe('2-6-2024', '3-6-2024');

--trong 1 tháng trong năm có bao nhiêu vé được bán
GO
CREATE FUNCTION fThongKeTungThangTrongNam
(@nam int)
RETURNS @ThongKe TABLE (
    Thang int,
    TongSoVe int
)
AS
BEGIN
    DECLARE @i int = 1;
    
    WHILE (@i <= 12)
    BEGIN
        INSERT INTO @ThongKe (Thang, TongSoVe)
        SELECT @i, isnull(COUNT(maBook),0) as TongSoVe
        FROM BookVe
        WHERE YEAR(ngayMua) = @nam AND MONTH(ngayMua) = @i;

        SET @i = @i + 1;
    END;

    RETURN;
END;
go
select *
from dbo.fThongKeTungThangTrongNam(2024)

/*viết function 
trả về true/false kiểm tra ngày chiếu đã full 6 ca hay chưa
*/
GO
create function fKTfullCa
(@ngayKiemTra date)
returns int
as
begin
	declare @kq int
	if exists(
		select *
		from SuatChieu
		where ngayChieu = @ngayKiemTra and
			maCa = 'Ca 1' and
			maCa = 'Ca 2'  and
			maCa = 'Ca 3'  and
			maCa = 'Ca 4'  and
			maCa = 'Ca 5'  and
			maCa = 'Ca 6'  
			)
	begin
		set @kq= 1;
	end
	else
	begin
		set @kq= 0;
	end
	return @kq
end
go
--declare @kt int
--set @kt= dbo.fKTfullCa('2-6-2024')
--print @kt

/*trả về bảng chứa các ca chưa chiếu khi nhập ngày chiếu
*/
GO
create function fCaChuaChieu
( @ngayKT date)
returns table
as return 
(
	select maCa, tenCa
	from CaChieu
	where maCa not in(select maca
						from SuatChieu
						where ngayChieu= @ngayKT)
)
go
--select *
--from fCaChuaChieu('2-6-2024')

--trả về bảng chứa các phòng chưa chiếu khi nhập ngày và ca chiếu
GO
CREATE FUNCTION fPhongChuaChieu
(	
    @ngayKT DATE,
    @caKT VARCHAR(20)
)
RETURNS TABLE
AS 
RETURN 
(
    SELECT PhongChieu.maPhong
    FROM PhongChieu
    WHERE PhongChieu.maPhong NOT IN
    (
        SELECT SuatChieu.maPhong
        FROM SuatChieu
        WHERE SuatChieu.ngayChieu = @ngayKT
        AND SuatChieu.maCa = @caKT
    )
);

go
--select *
--from fPhongChuaChieu('2-6-2024','CC1')

--viết function khi nhập ngày chiếu thì xuất các ca chiếu mà tại ca chiếu nớ chưa full 4 phòng chiếu
go
CREATE FUNCTION fKTTheoCa
(
    @ngayKT DATE
)
RETURNS TABLE
AS 
RETURN (
    SELECT c.maCa,c.tenCa, c.thoiGianBatDau
    FROM CaChieu c
    LEFT JOIN SuatChieu s ON c.maCa = s.maCa AND s.ngayChieu = @ngayKT
    GROUP BY c.maCa,c.tenCa, c.thoiGianBatDau
    HAVING COUNT(s.maPhong) < (SELECT COUNT(*) FROM PhongChieu)
);
GO

go
--select *
--from fKTTheoCa('4-4-2024')
-----------------------------------------------------------------------------------------------------///////////////////////////////////////////////////////////////////////

/*ADD SOLUONGTOIDA VAO TABLE VE TRIGGER = TONG SO GHE TABLE PHONG CHIEU , BO TONG SO VE TRONG BOOKVE*/ 
--trigger bang so luong ghe
go 
create trigger tLaySoLuongGhe
on SuatChieu
after insert
as
begin
	update Ve
	set soLuongToiDa = p.tongSoGhe
	from Ve v, inserted i, PhongChieu p,BookVe b
	where v.maVe = b.maVe and b.maSuat = i.maSuat and p.maPhong = i.maPhong
end
go
--insert into SuatChieu (maPhim,maPhong,maCa,ngayChieu)
--values	('P1','PC1','CC4','6-2-2024')

--trigger giam so luong ghe 
go
CREATE TRIGGER tGiamSoGhe
ON Ve
AFTER INSERT
AS 
BEGIN
    UPDATE Ve
    SET soLuongDaBan = soLuongDaBan + (
        SELECT COUNT(i.maVe)
        FROM inserted i
        JOIN BookVe b ON i.maVe = b.maVe
        WHERE i.maVe = ve.maVe 
    )
    FROM Ve
    JOIN BookVe ON BookVe.maVe = ve.maVe
END;

go
--suat chieu nhap vao da co bao nhieu ghe da dat 
go
create function fSoGheDaDat
( @maSuat varchar(20) )
returns table  
as 
return ( 
	select bg.maGhe, g.tinhTrang
	from SuatChieu s, BookGhe bg, BookVe bv, Ghe g
	where s.maSuat = bv.maSuat 
			and bv.maBook = bg.maBook 
			and g.maGhe = bg.maGhe
			and s.maSuat = @maSuat 
); 
go

--declare @masuatTEST varchar(20)
--set @masuatTEST = 'SC1'
--select *
--from dbo.fSoGheDaDat(@masuatTEST)
--

--khi nhập số điện thoại thì xuất ra tên kh, ghế, maSC
go
CREATE function fXuatTT
( @sdt char(10) )
returns table
as 
return(
	select k.hoTen, g.maGhe,b.ngayMua ,s.maCa, s.maPhim, s.maPhong
	from KhachHang k, SuatChieu s, BookGhe g,BookVe b
	where k.sdt = @sdt and 
		b.maKH = k.maKH and
		b.maSuat = s.maSuat and
		b.maBook = g.maBook
);
go
--declare @sdt char(10)
--set @sdt= '0900000008'
--
--select * 
--from dbo.fXuatTT(@sdt)

--Lấy tất cả các ngày đang chiếu
go

CREATE PROCEDURE GetNgayChieu
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT DISTINCT s.ngayChieu
    FROM SuatChieu s
    INNER JOIN Phim p ON s.maPhim = p.maPhim 
    WHERE s.ngayChieu >= GETDATE() 
    AND GETDATE() >= p.ngayKhoiChieu
	AND s.tinhTrang = 1 AND p.tinhTrang=1;
END;
--Lấy tất cả các phim có suất chiếu có ngày chiếu bằng ngày hiện tại với có ngày chiếu lớn hơn
go

CREATE PROCEDURE GetSuatChieu
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT DISTINCT s.maPhim
    FROM SuatChieu s
    INNER JOIN Phim p ON s.maPhim = p.maPhim 
    WHERE s.ngayChieu >= GETDATE() 
    AND GETDATE() >= p.ngayKhoiChieu
	AND s.tinhTrang = 1 AND p.tinhTrang=1;
END;

go

CREATE PROCEDURE GetSuatChuaChieu
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT DISTINCT s.maPhim
    FROM SuatChieu s
    INNER JOIN Phim p ON s.maPhim = p.maPhim 
    WHERE s.ngayChieu >= GETDATE() 
    AND GETDATE() < p.ngayKhoiChieu
	AND s.tinhTrang = 1 AND p.tinhTrang=1;
END;
GO


/*set dateformat dmy
insert into Phim (maLPhim,tenPhim,daoDien,doTuoiYeuCau,ngayKhoiChieu,thoiLuong,tinhTrang,hinhDaiDien,video,moTa)
values	('LP2',N'King dom of the planet of apes',N'Lý Hải',18 ,'10-6-2024',145 ,1,null,'b1','c1')*/

/*insert into SuatChieu (maPhim,maPhong,maCa,ngayChieu,tinhTrang)
values	('P9','PC1','CC5','10-6-2024',1),--ngay 10 / 6
		('P9','PC2','CC5','10-6-2024',1),
		('P9','PC3','CC5','10-6-2024',1),
		('P9','PC4','CC5','10-6-2024',1)*/
EXECUTE GetNgayChieu;
EXECUTE GetSuatChieu;
EXECUTE GetSuatChuaChieu;

go
create function fXuatNgayChieu
( @id nvarchar(50) )
returns table
as 
return(
	SELECT DISTINCT  s.ngayChieu
    FROM SuatChieu s
    INNER JOIN Phim p ON s.maPhim = p.maPhim 
    WHERE s.ngayChieu >= GETDATE() 
    AND GETDATE() >= p.ngayKhoiChieu
	AND s.tinhTrang = 1 AND p.tinhTrang=1
	AND p.maPhim = @id
);
go

create function fXuatPhimChieu
( @ngay nvarchar(50) )
returns table
as 
return(
	SELECT DISTINCT  s.maPhim
    FROM SuatChieu s
    INNER JOIN Phim p ON s.maPhim = p.maPhim 
    WHERE s.ngayChieu >= GETDATE() 
    AND GETDATE() >= p.ngayKhoiChieu
	AND s.tinhTrang = 1 AND p.tinhTrang=1
	AND s.ngayChieu = @ngay
);
go
select *
from dbo.fXuatNgayChieu(N'P1')
go
select *
from dbo.fXuatPhimChieu('2024-12-28')
--khi nhập tên phim và ngày chiếu sẽ xuất ra bảng thời gian bắt đầu ở bảng ca chiếu
select * from SuatChieu
where maPhim ='P15'
select * from SuatChieu
where maPhim ='P3'

go
CREATE function fXuatThoiGianChieu
( @id nvarchar(50), @ngayChieu date)
returns table
as 
return(
	select DISTINCT c.thoiGianBatDau, c.maCa, s.maSuat
	from Phim p , SuatChieu s, CaChieu c
	where p.maPhim = s.maPhim and c.maCa= s.maCa and
			p.maPhim = @id and s.ngayChieu = @ngayChieu
);
go
select *
from dbo.fXuatThoiGianChieu(N'P1','30-10-2024')

----khach hang : ve da dat ,ten phim , tong tien, thoi gian dat 
GO
CREATE FUNCTION fLichSuKH
(
    @maKH VARCHAR(20)
)
RETURNS TABLE
AS 
RETURN 
(
    SELECT 
        k.maKH,                        -- Mã khách hàng
        p.maPhim,                      -- Mã phim
        p.tenPhim,                     -- Tên phim
        bv.tongTien,                   -- Tổng tiền vé
        bv.ngayMua,                    -- Ngày mua vé
        bv.maBook                      -- Mã đặt vé
    FROM 
        KhachHang k
    INNER JOIN 
        BookVe bv ON k.maKH = bv.maKH
    INNER JOIN 
        Ve v ON bv.maVe = v.maVe
    INNER JOIN 
        Phim p ON v.maPhim = p.maPhim
    WHERE 
        k.maKH = @maKH
    GROUP BY 
        k.maKH, p.maPhim, p.tenPhim, bv.tongTien, bv.ngayMua, bv.maBook
);




--select *
--from dbo.fLichSuKH('KH1')

--SELECT Phim.maPhim,tenPhim FROM Phim where maPhim not in (
--	select maPhim from Ve)


--1 là khi thêm vào bảng bookGhe thì đếm số lượng dòng mới thêm vào nớ rồi update soLuongVeDaDat trong bảng BookVe dựa vào maBook
go
CREATE TRIGGER tDemSoLuong
ON BookGhe
AFTER insert 
AS 
BEGIN
    DECLARE @maBook nvarchar(50);  
    SELECT @maBook = maBook FROM inserted; 

    DECLARE @soLuong INT; 

    -- Tính tổng số lượng dựa trên số lượng hàng đã đặt trong BookGhe
    SELECT @soLuong = COUNT(g.maBook)
    FROM BookGhe g
	WHERE @maBook = g.maBook

    -- Cập nhật số lượng vào bảng BookVe
    UPDATE BookVe
    SET soLuongVeDaDat = @soLuong
    WHERE maBook = @maBook;
END;

go
--2 là cập nhật soLuongDaBan trong bảng Vé bằng với soLuongVeDaDat trong BookVe theo maVe
go
CREATE TRIGGER tCNSoLuongDaBan
ON BookVe
AFTER UPDATE 
AS 
BEGIN
    UPDATE Ve
    SET soLuongDaBan = Ve.soLuongDaBan + (i.soLuongVeDaDat - d.soLuongVeDaDat)
    FROM BookVe v
    INNER JOIN inserted i ON v.maVe = i.maVe
    INNER JOIN deleted d ON v.maVe = d.maVe;
END;
go


--Khi nhập vào ngày và ca chiếu thì kiểm tra xem full ghế hay chưa
go
CREATE FUNCTION dbo.fKiemTraGheTheoCa
( 
@maPhim VARCHAR(20),
    @ngay DATE, 
    @maCa VARCHAR(20)
   
)
RETURNS BIT 
AS 
BEGIN
    DECLARE @SoLuongVeDaDat INT;

    SELECT @SoLuongVeDaDat = SUM(bv.soLuongVeDaDat) 
    FROM SuatChieu sc
    INNER JOIN BookVe bv ON sc.maSuat = bv.maSuat
    WHERE sc.ngayChieu = @ngay AND sc.maCa = @maCa AND sc.maPhim = @maPhim;

    IF @SoLuongVeDaDat = 15
        RETURN 1; 

    RETURN 0; 
END;

go

set dateformat dmy
select dbo.fKiemTraGheTheoCa('P1','1-5-2024','CC3') as 'check'



GO

CREATE FUNCTION fChiTietTicket (@maBook VARCHAR(20))
RETURNS TABLE
AS 
RETURN 
(
    SELECT 
        k.maKH,                        -- Mã khách hàng
        p.tenPhim,                     -- Tên phim
        p.maPhim,                      -- Mã phim
        p.daoDien,                     -- Đạo diễn
        p.ngayKhoiChieu,               -- Ngày khởi chiếu
        p.thoiLuong,                   -- Thời lượng phim
        bv.soLuongVeDaDat,             -- Số lượng vé đã đặt
        bv.tongTien,                    -- Tổng tiền vé
        bv.ngayMua,                     -- Ngày mua vé
        STRING_AGG(g.maGhe, ', ') AS maGhe -- Gộp mã ghế dựa vào mã vé
    FROM 
        KhachHang k
    INNER JOIN 
        BookVe bv ON k.maKH = bv.maKH
    INNER JOIN 
        Ve v ON bv.maVe = v.maVe
    INNER JOIN 
        Phim p ON v.maPhim = p.maPhim
    INNER JOIN 
        BookGhe bg ON bv.maBook = bg.maBook  -- Kết nối với bảng BookGhe
    INNER JOIN 
        Ghe g ON bg.maGhe = g.maGhe  -- Kết nối với bảng Ghe
    WHERE 
        bv.maBook = @maBook  -- Thay đổi điều kiện để lọc theo maBook
    GROUP BY 
        k.maKH, p.tenPhim, p.maPhim, p.daoDien, p.ngayKhoiChieu, p.thoiLuong, 
        bv.soLuongVeDaDat, bv.tongTien, bv.ngayMua
);
GO
select *
from dbo.fChiTietTicket('BV2')

--28/10


go
CREATE TRIGGER TG_UpdateSoLuongVoucher
ON KhachHang
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- Chỉ cập nhật khi `diemDanh` đạt giá trị 6
    UPDATE KhachHang
    SET 
        soLuongVoucher = ISNULL(kh.soLuongVoucher, 0) + 1, -- Tăng `soLuongVoucher` lên 1
        diemDanh = 0                                   -- Đặt lại `diemDanh` về 0
    FROM KhachHang kh
    INNER JOIN inserted i ON kh.maKH = i.maKH
    WHERE i.diemDanh = 6; -- Điều kiện kích hoạt khi `diemDanh` đạt 6
END;
go

go
-- Lấy top 5 user có nhiều lượt mua vé nhất
CREATE FUNCTION GetTopCustomersByYear
(
    @Year INT  
)
RETURNS TABLE
AS
RETURN
(
    WITH RankedCustomers AS (
        SELECT 
            b.maKH,
            SUM(b.soLuongVeDaDat) AS tongSoLuongVe,
            SUM(b.tongTien) AS tongTien,
            RANK() OVER (ORDER BY SUM(b.soLuongVeDaDat) DESC) AS RankPosition
        FROM BookVe b
        WHERE YEAR(b.ngayMua) = @Year 
        GROUP BY b.maKH
    )
    SELECT 
        kh.hoTen AS TenKhachHang,  
        kh.sdt AS SoDienThoai,    
        kh.email AS Email,        
        rc.tongSoLuongVe AS TongSoVeMua, 
        rc.tongTien AS TongTien          
    FROM RankedCustomers rc
    JOIN KhachHang kh ON rc.maKH = kh.maKH
    WHERE rc.RankPosition <= 5   
);

SELECT * 
FROM dbo.GetTopCustomersByYear(2024);

-- tong ve trong nam 
go
CREATE FUNCTION ftongVeTrongNam(
    @nam INT
)
RETURNS INT
AS 
BEGIN
    DECLARE @tongVe INT;

    SELECT @tongVe = SUM(v.soLuongDaBan)
    FROM Ve v
    JOIN BookVe bv ON v.maVe = bv.maVe
    JOIN SuatChieu s ON bv.maSuat = s.maSuat
    WHERE YEAR(s.ngayChieu) = @nam;

    RETURN isnull(@tongVe,0);
END;
go

SELECT dbo.ftongVeTrongNam(2024) AS TongSoLuongVe;

--doanh thu 
go 
CREATE FUNCTION ftongDoanhThuTheoNam(
    @nam INT
)
RETURNS FLOAT
AS
BEGIN
    DECLARE @tongDoanhThu FLOAT;

    SELECT @tongDoanhThu = SUM(bv.tongTien)
    FROM BookVe bv
    WHERE YEAR(bv.ngayMua) = @nam;

    RETURN ISNULL(@tongDoanhThu, 0);
END;
go

SELECT dbo.fTongDoanhThuTheoNam(2024) AS TongDoanhThu;

--so luong phim da chieu
go
CREATE FUNCTION fsoLuongPhimDaChieuTrongNam(
    @nam INT
)
RETURNS INT
AS
BEGIN
    DECLARE @soLuongPhim INT;

    SELECT @soLuongPhim = COUNT(DISTINCT s.maPhim)
    FROM SuatChieu s
    WHERE YEAR(s.ngayChieu) = @nam;

    RETURN ISNULL(@soLuongPhim, 0);
END;
go 
SELECT dbo.fSoLuongPhimDaChieuTrongNam(2024) AS SoLuongPhim;
go

set dateformat dmy
insert into BookVe (maKH,maVe,maSuat, tongTien,soLuongVeDaDat, ngayMua)
values	('KH3','V2','SC3',60000,1, '20/12/2024'),
		('KH4','V3','SC4',160000,1, '20/12/2024'),
		('KH5','V4','SC5',100000,1, '20/12/2024'),
		('KH6','V5','SC6',900000,1, '20/12/2024')

		-- mat biec 
insert into BookVe (maKH,maVe,maSuat, tongTien,soLuongVeDaDat, ngayMua)
values	('KH3','V4','SC3',60000,1, '20/12/2024'),
		('KH4','V4','SC4',160000,1, '20/12/2024'),
		('KH5','V4','SC5',100000,1, '20/12/2024'),
		('KH6','V4','SC6',900000,1, '20/12/2024'),
		('KH3','V4','SC3',60000,1, '20/12/2024'),
		('KH4','V4','SC4',160000,1, '20/12/2024'),
		('KH5','V4','SC5',100000,1, '20/12/2024'),
		('KH6','V4','SC6',900000,1, '20/12/2024'),
		('KH3','V4','SC3',60000,1, '20/12/2024'),
		('KH4','V4','SC4',160000,1, '20/12/2024'),
		('KH5','V4','SC5',100000,1, '20/12/2024'),
		('KH6','V4','SC6',900000,1, '20/12/2024'),
		('KH3','V4','SC3',60000,1, '20/12/2024'),
		('KH4','V4','SC4',160000,1, '20/12/2024'),
		('KH5','V4','SC5',100000,1, '20/12/2024'),
		('KH6','V4','SC6',900000,1, '20/12/2024'),
		('KH3','V4','SC3',60000,1, '20/12/2024'),
		('KH4','V4','SC4',160000,1, '20/12/2024'),
		('KH5','V4','SC5',100000,1, '20/12/2024'),
		('KH6','V4','SC6',900000,1, '20/12/2024')

		SELECT dbo.fTongDoanhThuTheoNam(2024) AS TongDoanhThu;
		SELECT dbo.ftongVeTrongNam(2024) AS TongSoLuongVe;