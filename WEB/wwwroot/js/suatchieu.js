var gridButton = document.querySelector(".grid");
var listButton = document.querySelector(".list");
var productsWrapper = document.querySelector(".products-area-wrapper");
var model = document.getElementById("modal");
var form = document.getElementById("product-form");
var submitButton = document.getElementById("submit");

var maSuatInput = document.getElementById("maSuat");
var maPhimInput = document.getElementById("maPhim");
var maPhongInput = document.getElementById("maPhong");
var maCaInput = document.getElementById("maCa");
var ngayChieuInput = document.getElementById("ngayChieu");
var tinhTrangInput = document.getElementById("tinhTrang");


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

function showSuatChieuForm(maSuat, maPhim, maPhong, maCa, ngayChieu, tinhTrang) {
    if (maSuat != null) {
        // Gán giá trị cho các input
        maSuatInput.value = maSuat;
        maPhimInput.value = maPhim;
        maPhongInput.value = maPhong;
        maCaInput.value = maCa;
        ngayChieuInput.value = ngayChieu;
        tinhTrangInput.value = tinhTrang;

        // Thiết lập readonly cho trường mã suất nếu chỉnh sửa
        maSuatInput.setAttribute("readonly", "readonly");

        title.textContent = "Cập nhật suất chiếu";
        submitButton.textContent = "Lưu chỉnh sửa";
    } else {
        // Làm trống các input khi thêm mới
        maSuatInput.value = "";
        maPhimInput.value = "";
        maPhongInput.value = "";
        maCaInput.value = "";
        ngayChieuInput.value = "";
        tinhTrangInput.value = "";

        // Bỏ readonly cho trường mã suất
        maSuatInput.removeAttribute("readonly");

        title.textContent = "Thêm mới suất chiếu";
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