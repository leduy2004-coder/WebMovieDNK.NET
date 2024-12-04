var gridButton = document.querySelector(".grid");
var listButton = document.querySelector(".list");
var productsWrapper = document.querySelector(".products-area-wrapper");
var model = document.getElementById("modal");
var form = document.getElementById("product-form");
var submitButton = document.getElementById("submit");

var maPhimInput = document.getElementById("maPhim");
var maLoaiPhimInput = document.getElementById("maLoaiPhim");
var tenPhimInput = document.getElementById("tenPhim");
var daoDienInput = document.getElementById("daoDien");
var doTuoiYeuCauInput = document.getElementById("doTuoiYeuCau");
var ngayKhoiChieuInput = document.getElementById("ngayKhoiChieu");
var thoiLuongInput = document.getElementById("thoiLuong");
var tinhTrangInput = document.getElementById("tinhTrang");
var hinhDaiDienInput = document.getElementById("hinhDaiDien");
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
    if (maPhim != null) {
        // Gán giá trị cho các input
        maPhimInput.value = maPhim;
        maLoaiPhimInput.value = maLoaiPhim;
        tenPhimInput.value = tenPhim;
        daoDienInput.value = daoDien;
        doTuoiYeuCauInput.value = doTuoiYeuCau;
        ngayKhoiChieuInput.value = ngayKhoiChieu;
        thoiLuongInput.value = thoiLuong;
        tinhTrangInput.checked = tinhTrang; // Checkbox
        hinhDaiDienInput.value = hinhDaiDien;
        videoInput.value = video;
        moTaInput.value = moTa;

        // Thiết lập readonly cho trường mã phim nếu chỉnh sửa
        maPhimInput.style.display = "none";
        lableMaPhim.style.display = "none";

        title.textContent = "Cập nhật phim";
        submitButton.textContent = "Lưu chỉnh sửa";
    } else {
        // Làm trống các input khi thêm mới
        maPhimInput.value = "";
        maLoaiPhimInput.value = "";
        tenPhimInput.value = "";
        daoDienInput.value = "";
        doTuoiYeuCauInput.value = "";
        ngayKhoiChieuInput.value = "";
        thoiLuongInput.value = "";
        tinhTrangInput.checked = false;
        hinhDaiDienInput.value = "";
        videoInput.value = "";
        moTaInput.value = "";

        // Bỏ readonly cho trường mã phim
        maPhimInput.style.display = "none"; // Hiển thị lại trường mã phim
        lableMaPhim.style.display = "none"; // Hiển thị lại label mã phim

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