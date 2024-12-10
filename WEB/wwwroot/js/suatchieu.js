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
var labelMaSuat = document.getElementById("labelIdSuatChieu");

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
document.addEventListener("DOMContentLoaded", function () {
   
    console.log(maSuatInput); // Không còn null
});
function showSuatChieuForm(maSuat, maPhim, maPhong, maCa, ngayChieu, tinhTrang) {
    console.log(maSuat, maPhim, maPhong, maCa, ngayChieu, tinhTrang);
    if (maSuat != null) {
        const [month, day, year] = ngayChieu.split(" ")[0].split("/");
        const ngayChieuFormatted = `${year}-${month.padStart(2, "0")}-${day.padStart(2, "0")}`;
        getAvailableCaChieu(maPhim, ngayChieuFormatted)
        getAvailablePhongChieu(maCa, ngayChieuFormatted)
        // Gán giá trị cho các input
        maSuatInput.value = maSuat;
        maPhimInput.value = maPhim;
        maPhongInput.value = maPhong;
        maCaInput.value = maCa;

       

        ngayChieuInput.value = ngayChieuFormatted;

        tinhTrangInput.value = tinhTrang;
        console.log(maPhimInput.value, maPhongInput.value, maCaInput.value, ngayChieuInput.value,)
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
        maSuatInput.style.display = "none";
        labelMaSuat.style.display = "none";


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

    });
} else {
    console.error("Nút gửi không tìm thấy trong DOM");
}


//===================================

// Biến lưu trạng thái hiện tại
let selectedNgayChieu = null;
let selectedMaPhim = null;
let selectedMaCa = null;


// Kiểm tra và gọi getAvailableCaChieu
function checkAndFetchCaChieu() {
    if (selectedNgayChieu && selectedMaPhim) {
        getAvailableCaChieu(selectedMaPhim, selectedNgayChieu);
    }
}

// Kiểm tra và gọi getAvailablePhongChieu
function checkAndFetchPhongChieu() {
    if (selectedMaCa && selectedNgayChieu) {
        getAvailablePhongChieu(selectedMaCa, selectedNgayChieu);
    }
}





// Lắng nghe sự kiện thay đổi trên maPhim
document.getElementById("maPhim").addEventListener("change", function () {
    const phimId = this.value; // Lấy giá trị phim đã chọn
    const ngayChieu = document.getElementById("ngayChieu").value; // Lấy giá trị ngày chiếu

    // Nếu cả phimId và ngayChieu đều có giá trị thì gọi hàm
    if (phimId && ngayChieu) {
        getAvailableCaChieu(phimId, ngayChieu);
    }
});

// Sự kiện khi chọn ngày chiếu
document.getElementById("ngayChieu").addEventListener("change", function () {
    const ngay = this.value;

    if (ngay) {
        selectedNgayChieu = ngay.replace(/\//g, '-'); // Định dạng ngày
    } else {
        selectedNgayChieu = null;
    }

    checkAndFetchCaChieu(); // Kiểm tra và gọi hàm lấy ca chiếu
    checkAndFetchPhongChieu(); // Kiểm tra và gọi hàm lấy phòng chiếu
});

// Sự kiện khi chọn mã phim
document.getElementById("maPhim").addEventListener("change", function () {
    const phimId = this.value;

    if (phimId) {
        selectedMaPhim = phimId;
    } else {
        selectedMaPhim = null;
    }

    checkAndFetchCaChieu(); // Kiểm tra và gọi hàm lấy ca chiếu
});

// Sự kiện khi chọn mã ca
document.getElementById("maCa").addEventListener("change", function () {
    const maCa = this.value;

    if (maCa) {
        selectedMaCa = maCa;
    } else {
        selectedMaCa = null;
    }

    checkAndFetchPhongChieu(); // Kiểm tra và gọi hàm lấy phòng chiếu
});

// Hàm fetch dữ liệu ca chiếu khả dụng
function getAvailableCaChieu(phimId, ngayChieu) {
    console.log("get ca chieu")
    fetch(`https://localhost:7079/api/Admin_QLSuatChieu/caChieuAvailable/${ngayChieu}/${phimId}`)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(data => {
            // Xóa hết các option cũ
            const caChieuSelect = document.getElementById("maCa");
            caChieuSelect.innerHTML = '<option>Chọn ca chiếu</option>';

            // Thêm các option ca chiếu mới
            data.forEach(caChieu => {
                const option = document.createElement("option");
                option.value = caChieu.maCa;
                option.textContent = caChieu.tenCa;
                caChieuSelect.appendChild(option);
            });

            // Kích hoạt select ca chiếu
            caChieuSelect.disabled = data.length === 0; // Disable nếu không có dữ liệu
        })
        .catch(error => {
            console.error('Có lỗi xảy ra khi lấy dữ liệu:', error);
        });
}

function getAvailablePhongChieu(maCa, ngayChieu) {
    console.log(" get phong chieu")
    fetch(`https://localhost:7079/api/Admin_QLSuatChieu/phongChieuAvailable/${maCa}/${ngayChieu}`)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(data => {
            // Xóa hết các option cũ
            const phongChieuSelect = document.getElementById("maPhong");
            phongChieuSelect.innerHTML = '<option>Chọn phòng chiếu</option>';

            // Thêm các option suất chiếu mới
            data.forEach(phongChieu => {
                const option = document.createElement("option");
                option.value = phongChieu.maPhong;
                option.textContent = phongChieu.maPhong;
                phongChieuSelect.appendChild(option);
            });

            // Kích hoạt select suất chiếu
            phongChieuSelect.disabled = false;
        })
        .catch(error => {
            console.error('Có lỗi xảy ra khi lấy dữ liệu:', error);
        });
}