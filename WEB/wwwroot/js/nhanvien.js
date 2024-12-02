var gridButton = document.querySelector(".grid");
var listButton = document.querySelector(".list");
var productsWrapper = document.querySelector(".products-area-wrapper");
var model = document.getElementById("modal");
var form = document.getElementById("product-form");
var submitButton = document.getElementById("submit");

var maNhanVienInput = document.getElementById("maNhanVien");
var tenNhanVienInput = document.getElementById("tenNhanVien");
var sdtInput = document.getElementById("sdt");
var gioiTinhInput = document.getElementById("gioiTinh");
var ngaySinhInput = document.getElementById("ngaySinh");
var diaChiInput = document.getElementById("diaChi");
var cccdInput = document.getElementById("cccd");
var tinhTrangInput = document.getElementById("tinhTrang");
var tenTaiKhoanInput = document.getElementById("tenTaiKhoan");
var matKhauInput = document.getElementById("matKhau");
var maQLInput = document.getElementById("maQL");

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

function showPhimForm(maNV, hoTen, sdt, gioiTinh, ngaySinh, diaChi, cccd, tinhTrang, tenTK, matKhau, maQL) {
    if (maNV != null) {
        // Gán giá trị cho các input
        maNhanVienInput.value = maNV;
        tenNhanVienInput.value = hoTen;
        sdtInput.value = sdt;
        gioiTinhInput.value = gioiTinh;
        ngaySinhInput.value = ngaySinh;
        diaChiInput.value = diaChi;
        cccdInput.value = cccd;
        tinhTrangInput.value = tinhTrang;
        tenTaiKhoanInput.value = tenTK;
        matKhauInput.value = matKhau;
        maQLInput.value = maQL;

        // Thiết lập readonly cho trường mã nhân viên nếu chỉnh sửa
        maNhanVienInput.setAttribute("readonly", "readonly");

        title.textContent = "Cập nhật nhân viên";
        submitButton.textContent = "Lưu chỉnh sửa";
    } else {
        // Làm trống các input khi thêm mới
        maNhanVienInput.value = "";
        tenNhanVienInput.value = "";
        sdtInput.value = "";
        gioiTinhInput.value = "";
        ngaySinhInput.value = "";
        diaChiInput.value = "";
        cccdInput.value = "";
        tinhTrangInput.value = "";
        tenTaiKhoanInput.value = "";
        matKhauInput.value = "";
        maQLInput.value = "";

        // Bỏ readonly cho trường mã nhân viên
        maNhanVienInput.removeAttribute("readonly");

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

        /*  let isValid = true;
          let errors = [];
  
          if (!manganhInput.value) {
              isValid = false;
              errors.push("Mã ngành không được để trống");
              manganhInput.classList.add("is-invalid");
          } else {
              manganhInput.classList.remove("is-invalid");
          }
  
          if (!tennganhInput.value) {
              isValid = false;
              errors.push("Tên ngành không được để trống");
              tennganhInput.classList.add("is-invalid");
          } else {
              tennganhInput.classList.remove("is-invalid");
          }
  
          if (!namthanhlapInput.value) {
              isValid = false;
              errors.push("Năm không được để trống");
              namthanhlapInput.classList.add("is-invalid");
          } else {
              namthanhlapInput.classList.remove("is-invalid");
          }
          if (!makhoaInput.value) {
              isValid = false;
              errors.push("Bạn phải chọn một khoa.");
              makhoaInput.classList.add("is-invalid");
          } else {
              makhoaInput.classList.remove("is-invalid");
          }
  
          // Nếu có lỗi, hiển thị thông báo lỗi
          if (!isValid) {
              event.preventDefault();
              showCustomAlert(errors.join("<br>"), "error");
          }*/
    });
} else {
    console.error("Nút gửi không tìm thấy trong DOM");
}

function confirmDelete() {
    return confirm("Bạn có chắc chắn muốn xóa sản phẩm này?");
}