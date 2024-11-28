

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

seats.forEach(function (seat) {
    seat.addEventListener('click', function () {
        var imageElement = seat.querySelector('img');
        var currentTicketPrice = parseInt(ticketInfo.textContent.match(/\d+/))
        var spanText = seat.querySelector('span').textContent;
        if (imageElement.src.endsWith('seat-standard.png')) {
            // Lấy giá trị hiện tại

            imageElement.src = './img/seat-selected.png';
            selectedSeatInfo.innerHTML += ' ' + spanText;
        } else {

            imageElement.src = './img/seat-standard.png';
            selectedSeatInfo.innerHTML = selectedSeatInfo.innerHTML.replace(spanText, '');
        }
    });
});


$('.numberInput').on('input', function () {
    var input = $(this);
    var itemName = input.data('name');
    var quantity = input.val();
    var filmDescList = $('#filmDescList');
    var existingItem = filmDescList.find('li:contains("' + itemName + '")');

    if (quantity > 0) {
        if (existingItem.length > 0) {
            existingItem.text(itemName + ': ' + quantity);
        } else {
            filmDescList.append('<li>' + itemName + ': ' + quantity + '</li>');
        }
    } else {
        existingItem.remove();
    }
    updateTotalPrice();
});

function updateTotalPrice() {
    var totalPrice = 0;
    $('.numberInput').each(function () {
        var input = $(this);
        var price = parseFloat(input.closest('.desc-col').find('p').eq(1).text().replace(/[^0-9.-]+/g, ""));
        var quantity = parseInt(input.val());
        if (!isNaN(quantity) && quantity > 0) {
            totalPrice += price * quantity;
        }
    });
    $('.price').text('Tổng tiền: ' + totalPrice.toLocaleString('vi-VN') + ' VNĐ');
}