var gridButton = document.querySelector(".grid");
var listButton = document.querySelector(".list");
var productsWrapper = document.querySelector(".products-area-wrapper");
var model = document.getElementById("modal");
var form = document.getElementById("product-form");
var submitButton = document.getElementById("submit");

var lableMaKhachHang = document.getElementById("labelkhachhang");

var maKHInput = document.getElementById("maKHForm");
var hoTenInput = document.getElementById("hoTen");
var sdtInput = document.getElementById("sdt");
var ngaySinhInput = document.getElementById("ngaySinh");
var emailInput = document.getElementById("email");
var tinhTrangInput = document.getElementById("tinhTrang");
var tenTKInput = document.getElementById("tenTK");
var MKInput = document.getElementById("matKhau");



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

function showKhachHangForm(maKH, hoTen, sdt, ngaySinh, email, tinhTrang, tenTK, mk) {
    if (maKH != null) {
        // Gán giá trị cho các input
        maKHInput.value = maKH;
        hoTenInput.value = hoTen;
        sdtInput.value = sdt;

        const [month, day, year] = ngaySinh.split(" ")[0].split("/");

        const ngaySinhFormatted = `${year}-${month.padStart(2, "0")}-${day.padStart(2, "0")}`;

        ngaySinhInput.value = ngaySinhFormatted;

        emailInput.value = email;
        tinhTrangInput.value = tinhTrang;
        tenTKInput.value = tenTK;
        MKInput.value = mk;

        maKHInput.style.display = "block";
        lableMaKhachHang.style.display = "block";

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
        MKInput.value = "";

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

       
    });
} else {
    console.error("Nút gửi không tìm thấy trong DOM");
}

function confirmDelete() {
    return confirm("Bạn có chắc chắn muốn xóa sản phẩm này?");
}