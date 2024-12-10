var gridButton = document.querySelector(".grid");
var listButton = document.querySelector(".list");
var productsWrapper = document.querySelector(".products-area-wrapper");
var model = document.getElementById("modal");
var form = document.getElementById("product-form");
var submitButton = document.getElementById("submit");

var maPhimInput = document.getElementById("maPhimChieu");
var maLoaiPhimInput = document.getElementById("maLoaiPhim");
var tenPhimInput = document.getElementById("tenPhim");
var daoDienInput = document.getElementById("daoDien");
var doTuoiYeuCauInput = document.getElementById("doTuoiYeuCau");
var ngayKhoiChieuInput = document.getElementById("ngayKhoiChieu");
var thoiLuongInput = document.getElementById("thoiLuong");
var tinhTrangInput = document.getElementById("tinhTrang");
var hinhDaiDienInput = document.getElementById("hinhDaiDien");
var hinhDaiDienFileInput = document.getElementById("hinhDaiDienFile");
var hinhImg = document.getElementById("hinhUp");
var videoInput = document.getElementById("video");
var moTaInput = document.getElementById("moTa");

var lableMaPhim = document.getElementById("labelphim");



var title = document.getElementById("title-form");

if (gridButton && listButton && productsWrapper) {
    gridButton.addEventListener("click", function () {
        listButton.classList.remove("active");
        gridButton.classList.add("active");
        productsWrapper.classList.add("gridView");
        productsWrapper.classList.remove("tableView");
    });

    listButton.addEventListener("click", function () {
        listButton.classList.add("active");
        gridButton.classList.remove("active");
        productsWrapper.classList.remove("gridView");
        productsWrapper.classList.add("tableView");
    });
} else {
    console.error("Không tìm thấy phần tử cần thiết trong DOM.");
}

function showPhimForm(maPhim, maLoaiPhim, tenPhim, daoDien, doTuoiYeuCau, ngayKhoiChieu, thoiLuong, tinhTrang, hinhDaiDien, video, moTa) {
    hinhDaiDienFileInput.addEventListener('change', function (event) {
        const file = event.target.files[0];

        // Kiểm tra xem có file nào được chọn không
        if (file) {
            const reader = new FileReader();

            // Khi file được đọc thành công, cập nhật src cho hình ảnh
            reader.onload = function (e) {
                hinhImg.src = e.target.result; // Đây là URL tạm thời của file đã chọn
            };

            reader.readAsDataURL(file);
        } else {
            hinhImg.style.display = "none";
        }
    });
    hinhDaiDienFileInput.value = ""; 
    if (maPhim != null) {
        // Gán giá trị cho các input
        maPhimInput.value = maPhim;
        tenPhimInput.value = tenPhim;
        daoDienInput.value = daoDien;
        doTuoiYeuCauInput.value = doTuoiYeuCau;
        hinhDaiDienInput.value = hinhDaiDien;
        if (maLoaiPhim) {
            for (var i = 0; i < maLoaiPhimInput.options.length; i++) {
                var option = maLoaiPhimInput.options[i];

                if (option.value === maLoaiPhim) {
                    maLoaiPhimInput.selectedIndex = i; 
                    break; 
                }
            }
        }

        thoiLuongInput.value = thoiLuong;
        tinhTrangInput.checked = tinhTrang; // Checkbox
        hinhImg.src = hinhDaiDien;

        videoInput.value = video;
        moTaInput.value = moTa;

        const [month, day, year] = ngayKhoiChieu.split(" ")[0].split("/");

        const ngaySinhFormatted = `${year}-${month.padStart(2, "0")}-${day.padStart(2, "0")}`;

        ngayKhoiChieuInput.value = ngaySinhFormatted;

        maPhimInput.style.display = "block";
        lableMaPhim.style.display = "block";

        // Thiết lập readonly cho trường mã phim nếu chỉnh sửa
        maPhimInput.setAttribute("readonly", "readonly");

        title.textContent = "Cập nhật phim";
        submitButton.textContent = "Lưu chỉnh sửa";
    } else {
        // Làm trống các input khi thêm mới
        maPhimInput.value = "";

      
        maLoaiPhimInput.selectedIndex = 0;
        tenPhimInput.value = "";
        daoDienInput.value = "";
        doTuoiYeuCauInput.value = "";
        ngayKhoiChieuInput.value = "";
        thoiLuongInput.value = "";
        tinhTrangInput.checked = false;

        
        hinhImg.src =  "";

        videoInput.value = "";
        moTaInput.value = "";

        maPhimInput.style.display = "none"; 
        lableMaPhim.style.display = "none"; 

        title.textContent = "Thêm mới phim";
        submitButton.textContent = "Lưu thông tin";
    }

    // Hiển thị form và modal
    form.classList.add("show");
    modal.classList.add("show");
}




model.addEventListener("click", function () {
    form.classList.remove("show");
    model.classList.remove("show");
})


if (submitButton) {
    submitButton.addEventListener("click", function (event) {

        // Cập nhật giá trị cho trường ẩn
        var madanhmucHiddenInput = document.querySelector("input[name='NhanVienMoi.maNhanVien']");
        madanhmucHiddenInput.value = maDanhMucInput.value;

    });
} else {
    console.error("Nút gửi không tìm thấy trong DOM");
}

