
var ID = undefined;
var UserId = undefined;
var RoomId_ = undefined;
var Price = undefined;
$(document).ready(function ($) {
    $("#btnShowModalRegister").click(function () {
        $("#loginModal").modal('show');
        $(".modal-title").text("Register");
        $("#login").css("display", "none");
        $("#register").css("display", "block");       
    });

    $("#btnShowModalLogin").click(function () {
        $("#loginModal").modal('show');
        $(".modal-title").text("Log in");
        $("#login").css("display", "block");
        $("#register").css("display", "none");
    });
});

$(".purch").click(function () {
    var beginDate = document.getElementById('datebegin').value;
    var endDate = document.getElementById('dateend').value;
    var d = this.parentNode.parentNode.parentNode.parentNode.childNodes[1].id;
    var r = this.parentNode.parentNode.childNodes[1].id;
    var room_name = this.parentNode.parentNode.childNodes[3].id;
    var room_price = this.parentNode.parentNode.childNodes[5].id;
    var data = { 'HotelId': d, 'RoomId':r, 'BeginDate': beginDate, 'EndDate':endDate};
    $.ajax({
        url: 'Purchases/Purchase',
        data: data,
        contentType: 'application/json',
        dataType: "json",
        success: function (result) {
            $("#purchcases").modal('show');
            var bd = new Date(document.getElementById('datebegin').value);
            var ed = new Date(document.getElementById('dateend').value);
            var time = ed - bd;
            var countOfDay = time / (1000 * 60 * 60 * 24) + 1;
            var arrtime = (result.DateBegin).split("-");
            var day = arrtime[2].split('T');
            var arrtimeEnd = (result.DateEnd).split("-");
            var dayEnd = arrtimeEnd[2].split('T');
            document.getElementById("purchcases").innerHTML = "<div class=\"modal-dialog modal-lg\">" +
        "<div class=\"modal-content\">" +
            "<div class=\"modal-header\">" +
                "<button type=\"button\" class=\"close\" data-dismiss=\"modal\">" +
                   "X" +
                "</button>" +
                "<h4 class=\"modal-title\"> Перевірте дані замовлення</h4>" +
            "</div>" +
            "<div class=\"modal-body confirm-text\">" +
                "<div>" + "Назва готелю: " + result.Hotel.Name +
                "</div>" +
                "<div>" + "Рівень комформу номера: " + room_name +
                "</div>" +
                 "<div>" + "Кількість днів: " + countOfDay +
                "</div>" +
                "<div>" + "Загальна ціна: " + room_price*countOfDay +
                "</div>" +
                "<div>" + "Початок бронювання: " + day[0] + "-" + arrtime[1] + "-" + arrtime[0]+
                "</div>" +
                "<div>" + "Кінець бронювання: " + dayEnd[0] + "-" + arrtimeEnd[1] + "-" + arrtimeEnd[0] +
                "</div></div>" +
                "<button type=\"submit\" class=\"btn btn-dark \" onclick=\"ConfirmFunc()\" data-dismiss=\"modal\" id=\"confirm\">Підтвердити</button>" +
                "<span> </span>" +
                "<button type=\"submit\" class=\"btn btn-dark \" data-dismiss=\"modal\">Скасувати</button>" +
            "</div>" +
        "</div>" +
    "</div>"
            ID = result.HotelsID;
            UserId = result.UserId;
            RoomId_ = result.RoomId;
            Price = room_price * countOfDay;
        }
    })
});

function ConfirmFunc() {
    var beginDate = document.getElementById('datebegin').value;
    var endDate = document.getElementById('dateend').value;
    var data = { 'HotelId': ID, 'RoomId': RoomId_,'Price':Price, 'BeginDate': beginDate, 'EndDate': endDate };
    $.ajax({
        url: 'Purchases/Create',
        data: data,
        contentType: 'application/json',
        dataType: "json",
        success: function () {
        }
    });  
};

//function SentEmail() {
//    var data = { 'UserId': UserId };
//    $.ajax({
//        url: 'Home/SendMessage',
//        data: data,
//        contentType: 'application/json',
//        dataType: 'json',
//        succes: function (result) {
//            alert("Повідомлення надісланно");
//        }
//    })
//}

$(function () {
    var page = 0;
    var _inCallback = false;
    function loadItems() {
        if (page > -1 && !_inCallback) {
            _inCallback = true;
            page++;

            $.ajax({
                type: 'GET',
                url: '/Home/Index/' + page,
                success: function (data, textstatus) {
                    if (data != '') {
                        $("#scrolList").append(data);
                    }
                    else {
                        page = -1;
                    }
                    _inCallback = false;
                }
            });
        }
    }
    $(window).scroll(function () {
        if ($(window).scrollTop() == $(document).height() - $(window).height()) {

            loadItems();
        }
    });
})