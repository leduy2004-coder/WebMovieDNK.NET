﻿

// Lấy tất cả các ghế
var seats = document.querySelectorAll('.seat');


function closeToast(element) {
	// Tìm phần tử cha (div.toast) và ẩn nó
	const toast = element.closest('.toast');
	if (toast) {
		toast.style.opacity = '0'; // Hiệu ứng mờ dần
		setTimeout(() => {
			toast.style.display = 'none'; // Ẩn hoàn toàn sau hiệu ứng
		}, 300); // Thời gian khớp với transition CSS nếu có
	}
}

// Lặp qua từng ghế và thêm sự kiện click
seats.forEach(function (seat) {
	seat.addEventListener('click', function () {
		changeSeatColor(this); // Gọi hàm changeSeatColor khi ghế được nhấp vào
	});
});

// Hàm thay đổi màu ghế
function changeSeatColor(seat) {
	seat.classList.toggle("selected"); // Toggle 'selected' class
}

var seats = document.querySelectorAll('.seat');
var ticketInfo = document.querySelector('.price');
var selectedSeatInfo = document.querySelector('.title-seat');

var liElement = document.querySelector('#money');

function setOrderDrink() {
	document.getElementById('datdoUong').value = "true";
}

seats.forEach(function (seat) {
	seat.addEventListener('click', function () {
		var moneyValue = parseInt(liElement.textContent || liElement.innerText);
		var imageElement = seat.querySelector('img');
		var currentTicketPrice = parseInt(ticketInfo.textContent.match(/\d+/))
		var spanText = seat.querySelector('span').textContent;
		if (imageElement.src.endsWith('seat-standard.png')) {
			// Lấy giá trị hiện tại
			var newTicketPrice = currentTicketPrice + moneyValue; // Tăng giá vé lên 60.000 VNĐ
			ticketInfo.innerHTML = '<b>Tổng tiền: </b>' + newTicketPrice + ' VNĐ'; // Cập nhật lại giá trị trong thẻ <li>
			imageElement.src = '/img/seat-selected.png';
			selectedSeatInfo.innerHTML += ' ' + spanText;
		} else if (imageElement.src.endsWith('seat-selected.png')) {
			var newTicketPrice = currentTicketPrice - moneyValue; // Giảm giá vé đi 60.000 VNĐ
			ticketInfo.innerHTML = '<b>Tổng tiền: </b>' + newTicketPrice + ' VNĐ'; // Cập nhật lại giá trị trong thẻ <li>
			imageElement.src = '/img/seat-standard.png';
			selectedSeatInfo.innerHTML = selectedSeatInfo.innerHTML.replace(spanText, '');
		}
		document.getElementById("totalMoney").value = newTicketPrice;
		var content = selectedSeatInfo.textContent;
		var firstSpaceIndex = content.indexOf(' ');
		if (firstSpaceIndex !== -1) {
			var result = content.substring(firstSpaceIndex + 1);
			document.getElementById("chairBook").value = result;
		} else {
			document.getElementById("chairBook").value = "";
		}

	});
});

const accoutImg = document.querySelector('.accout-img');
const filmOp = document.querySelector('.film-option');
const formFilm = document.querySelector('.film-option .form-option');
const formAccout = document.querySelector('.accout-select');


accoutImg.addEventListener('click', function () {
	event.stopPropagation();
	// Kiểm tra nếu accout-select đã có lớp active thì xóa lớp active, ngược lại thêm lớp active
	formAccout.classList.toggle('selected');
	// Đóng form khác nếu đang mở
	if (formFilm.classList.contains('selected')) {
		formFilm.classList.remove('selected');
	}
});
filmOp.addEventListener('click', function (event) {
	event.stopPropagation();

	// Toggle dropdown Phim
	formFilm.classList.toggle('selected');
	// Đóng form khác nếu đang mở
	if (formAccout.classList.contains('selected')) {
		formAccout.classList.remove('selected');
	}
});


// Đóng form khi click ngoài form
document.addEventListener('click', function () {
	if (formFilm.classList.contains('selected')) {
		formFilm.classList.remove('selected');
	}
	if (formAccout.classList.contains('selected')) {
		formAccout.classList.remove('selected');
	}
});




