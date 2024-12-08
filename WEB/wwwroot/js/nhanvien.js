var gridButton = document.querySelector(".grid");
var listButton = document.querySelector(".list");
var productsWrapper = document.querySelector(".products-area-wrapper");
var model = document.getElementById("modal");
var form = document.getElementById("product-form");
var submitButton = document.getElementById("submit");

var maNhanVienInput = document.getElementById("maNhanVien");
var labelMaNhanVien = document.getElementById("labelnhanvien");
var tenNhanVienInput = document.getElementById("tenNhanVien");
var sdtInput = document.getElementById("sdt");
var manInput = document.getElementById("man");
var womenInput = document.getElementById("woman");
var ngaySinhInput = document.getElementById("ngaySinh");
var diaChiInput = document.getElementById("diaChi");
var cccdInput = document.getElementById("cccd");
var tinhTrangInput = document.getElementById("tinhTrang");
var tenTaiKhoanInput = document.getElementById("tenTaiKhoan");
var matKhauInput = document.getElementById("matKhau");


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

function showNhanVienForm(maNV, hoTen, sdt, gioiTinh, ngaySinh, diaChi, cccd, tinhTrang, tenTK, matKhau) {
    if (maNV != null) {
        // Gán giá trị cho các input
        maNhanVienInput.value = maNV;
        tenNhanVienInput.value = hoTen;
        sdtInput.value = sdt;
        if (gioiTinh) {
            manInput.checked = true;  
            womenInput.checked = false;  
        } else {
            manInput.checked = false; 
            womenInput.checked = true;  
        }

        const [month, day, year] = ngaySinh.split(" ")[0].split("/");

        const ngaySinhFormatted = `${year}-${month.padStart(2, "0")}-${day.padStart(2, "0")}`;

        ngaySinhInput.value = ngaySinhFormatted;

        diaChiInput.value = diaChi;
        cccdInput.value = cccd;
        tinhTrangInput.value = tinhTrang;
        tenTaiKhoanInput.value = tenTK;
        matKhauInput.value = matKhau;


        // Thiết lập readonly cho trường mã nhân viên nếu chỉnh sửa
        maNhanVienInput.setAttribute("readonly", "readonly");

        title.textContent = "Cập nhật nhân viên";
        submitButton.textContent = "Lưu chỉnh sửa";
    } else {
        // Làm trống các input khi thêm mới
        maNhanVienInput.value = "";
        tenNhanVienInput.value = "";
        sdtInput.value = "";
        manInput.checked = true;
        womenInput.checked = false;  
        ngaySinhInput.value = "";
        diaChiInput.value = "";
        cccdInput.value = "";
        tinhTrangInput.value = "";
        tenTaiKhoanInput.value = "";
        matKhauInput.value = "";


        // Bỏ readonly cho trường mã nhân viên
        maNhanVienInput.style.display = "none";
        labelMaNhanVien.style.display = "none";
        title.textContent = "Thêm mới nhân viên";
        submitButton.textContent = "Lưu thông tin";

    }
    // Hiển thị form và modal
    form.classList.add("show");
    model.classList.add("show");
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

function confirmDelete() {
    return confirm("Bạn có chắc chắn muốn xóa sản phẩm này?");
}