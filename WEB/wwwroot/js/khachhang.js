var gridButton = document.querySelector(".grid");
var listButton = document.querySelector(".list");
var productsWrapper = document.querySelector(".products-area-wrapper");
var model = document.getElementById("modal");
var form = document.getElementById("product-form");
var submitButton = document.getElementById("submit");

var maKHInput = document.getElementById("maKH");
var hoTenInput = document.getElementById("hoTen");
var lableMaKhachHang = document.getElementById("labelkhachhang");
var sdtInput = document.getElementById("sdt");
var ngaySinhInput = document.getElementById("ngaySinh");
var emailInput = document.getElementById("email");
var tinhTrangInput = document.getElementById("tinhTrang");
var tenTKInput = document.getElementById("tenTK");


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

function showKhachHangForm(maKH, hoTen, sdt, ngaySinh, email, tinhTrang, tenTK) {
    if (maKH != null) {
        // Gán giá trị cho các input
        maKHInput.value = maKH;
        hoTenInput.value = hoTen;
        sdtInput.value = sdt;
        ngaySinhInput.value = ngaySinh;
        emailInput.value = email;
        tinhTrangInput.value = tinhTrang;
        tenTKInput.value = tenTK;

        // Thiết lập readonly cho trường mã khách hàng nếu chỉnh sửa
        maKHInput.setAttribute("readonly", "readonly");

        title.textContent = "Cập nhật khách hàng";
        submitButton.textContent = "Lưu chỉnh sửa";
    } else {
        // Làm trống các input khi thêm mới
        maKHInput.value = "";
        hoTenInput.value = "";
        sdtInput.value = "";
        ngaySinhInput.value = "";
        emailInput.value = "";
        tinhTrangInput.value = "";
        tenTKInput.value = "";

        // Bỏ readonly cho trường mã khách hàng
        maKHInput.style.display="none";
        lableMaKhachHang.style.display = "none";

        title.textContent = "Thêm mới khách hàng";
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