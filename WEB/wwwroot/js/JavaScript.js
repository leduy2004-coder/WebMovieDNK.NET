

// Lấy tất cả các ghế
var seats = document.querySelectorAll('.seat');




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
			imageElement.src = './img/seat-selected.png';
			selectedSeatInfo.innerHTML += ' ' + spanText;
		} else if (imageElement.src.endsWith('seat-selected.png')) {
			var newTicketPrice = currentTicketPrice - moneyValue; // Giảm giá vé đi 60.000 VNĐ
			ticketInfo.innerHTML = '<b>Tổng tiền: </b>' + newTicketPrice + ' VNĐ'; // Cập nhật lại giá trị trong thẻ <li>
			imageElement.src = './img/seat-standard.png';
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
const formAccout = document.querySelector('.accout .form-option');


filmOp.addEventListener('click', function (event) {
	// Ngăn chặn sự kiện "click" lan truyền lên các phần tử cha
	event.stopPropagation();

	// Thực hiện toggle lớp 'selected'
	formFilm.classList.toggle('selected');
});

accoutImg.addEventListener('click', function () {
	// Kiểm tra nếu accout-select đã có lớp active thì xóa lớp active, ngược lại thêm lớp active
	formAccout.classList.toggle('selected');
});

function closeForm(form) {
	form.classList.remove('selected');
}

// Thêm sự kiện "click" vào document
document.addEventListener('click', function (event) {
	const clickedElement = event.target;

	// Kiểm tra nếu click không phải là trên formFilm
	if (!formFilm.contains(clickedElement) && clickedElement !== filmOp) {
		closeForm(formFilm);
		console.log(1)
	}

	// Kiểm tra nếu click không phải là trên formAccout
	if (!formAccout.contains(clickedElement) && clickedElement !== accoutImg) {
		console.log(1)
		closeForm(formAccout);

	}
});






