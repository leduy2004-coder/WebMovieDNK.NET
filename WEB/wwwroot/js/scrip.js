
var modeSwitch = document.querySelector('.mode-switch');
modeSwitch.addEventListener('click', function () {
    document.documentElement.classList.toggle('light');
    modeSwitch.classList.toggle('active');
});
function getUrlParameter(name) {
    const results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href);
    return results ? results[1] : null;
}

const accountInfoWrapper = document.querySelector('.account-info-wrapper');

accountInfoWrapper?.addEventListener('click', () => {
    accountInfoWrapper.classList.toggle('active');
});

// Đóng menu khi nhấp ra ngoài
document.addEventListener('click', (event) => {
    if (!accountInfoWrapper.contains(event.target)) {
        accountInfoWrapper.classList.remove('active');
    }
});


$(document).ready(function () {
    // Kiểm tra nếu có thông số 'SaveSuccess' trong URL
    if (getUrlParameter('SaveSuccess') != null) {
        const actionType = getUrlParameter('actionType');
        const saveSuccess = getUrlParameter('SaveSuccess');

        // Lưu trạng thái vào sessionStorage trước khi reload
        sessionStorage.setItem('SaveSuccess', saveSuccess);
        sessionStorage.setItem('actionType', actionType);

        // Xóa tham số URL để tránh lặp lại xử lý
        removeUrl();

        // Tải lại trang
        location.reload();
    }

    // Kiểm tra trạng thái thông báo sau khi tải lại
    const saveSuccess = sessionStorage.getItem('SaveSuccess');
    const actionType = sessionStorage.getItem('actionType');
    if (saveSuccess != null) {
        // Hiển thị thông báo dựa trên trạng thái đã lưu
        setTimeout(function () {
            if (saveSuccess === "True") {
                if (actionType === "create") {
                    showCustomAlert("Thêm thành công !!", "success");
                } else if (actionType === "update") {
                    showCustomAlert("Cập nhật thành công !!", "success");
                } else if (actionType === "delete") {
                    showCustomAlert("Xóa thành công !!", "success");
                }
            } else if (saveSuccess === "False") {
                if (actionType === "create") {
                    showCustomAlert("Thêm thất bại. Vui lòng thử lại!", "error");
                } else if (actionType === "update") {
                    showCustomAlert("Cập nhật thất bại. Vui lòng thử lại!", "error");
                } else if (actionType === "delete") {
                    showCustomAlert("Xóa thất bại. Vui lòng thử lại!", "error");
                }
            }

            // Xóa trạng thái thông báo trong sessionStorage
            sessionStorage.removeItem('SaveSuccess');
            sessionStorage.removeItem('actionType');
        }, 500); // Đợi 500ms để chắc chắn trang đã tải xong
    }
});


function removeUrl() {
    // Lấy URL hiện tại
    var currentUrl = window.location.href;

    // Tạo đối tượng URL từ URL hiện tại
    var url = new URL(currentUrl);

    // Lấy phần đường dẫn trước tham số truy vấn (ví dụ: /Admin_Home/Index)
    var pathname = url.pathname;

    // Tạo URL mới chỉ với phần giao thức, tên miền và phần đường dẫn (không có tham số truy vấn)
    var newUrl = url.protocol + '//' + url.host + pathname;

    // Cập nhật URL trong thanh địa chỉ mà không làm mới trang
    history.replaceState(null, '', newUrl);
}


function showCustomAlert(message, state) {
    const alertDiv = document.getElementById("customAlert");
    alertDiv.innerHTML = message;
    alertDiv.style.display = "block";

    if (state === "success") {
        alertDiv.style.backgroundColor = "#4CAF50";
    } else if (state === "error") {
        alertDiv.style.backgroundColor = "#f44336";
    }

    setTimeout(() => {
        alertDiv.style.display = "none";
    }, 3000);
}

