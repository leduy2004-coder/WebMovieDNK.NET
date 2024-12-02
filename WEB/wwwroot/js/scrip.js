
var modeSwitch = document.querySelector('.mode-switch');
modeSwitch.addEventListener('click', function () {
    document.documentElement.classList.toggle('light');
    modeSwitch.classList.toggle('active');
});
function getUrlParameter(name) {
    const results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href);
    return results ? results[1] : null;
}

$(document).ready(function () {
    if (window.location.href.indexOf('SaveProductSuccess') > -1) {
        const actionType = getUrlParameter('actionType');

        setTimeout(function () {
            removeUrl();

            if (actionType === "create") {
                $('.nav-link[data-url="/SanPham"]').click();
                showCustomAlert("Thêm sản phẩm thành công !!", "success");
            } else if (actionType === "update") {
                $('.nav-link[data-url="/SanPham"]').click();
                showCustomAlert("Cập nhật sản phẩm thành công !!", "success");
            } else if (actionType === "delete") {
                $('.nav-link[data-url="/SanPham"]').click();
                showCustomAlert("Xóa sản phẩm thành công !!", "success");
            }
        }, 100); // Đợi 100ms trước khi thực hiện click
    }

    $(document).on('click', '.nav-link', function (e) {

        var url = $(this).data('url');

        e.preventDefault();

        $('.sidebar-list-item').removeClass('active');


        $(this).closest('.sidebar-list-item').addClass('active');


        $.get(url, function (data) {
            $('#content').html(data);
        }).fail(function () {
            console.error('Lỗi khi tải dữ liệu');
        });

    });
});

function removeUrl() {
    // Xóa 'CreateSuccess' khỏi URL
    var currentUrl = window.location.href;

    // Tạo một URL đối tượng từ URL hiện tại
    var url = new URL(currentUrl);

    // Tạo URL mới chỉ với phần giao thức và tên miền
    var newUrl = url.protocol + '//' + url.host + '/';

    // Thay đổi URL trong thanh địa chỉ mà không làm mới trang
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

