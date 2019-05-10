$('#find').click(function () {
    var beginpurch_ = document.getElementById('beginpurch').value;
    var endpurch_ = document.getElementById('endpurch').value;
    var data = { 'beginPurch': beginpurch_, 'endPurch': endpurch_ };
    $.ajax({
        url: 'Purchases/PurchTable',
        data: data,
        contentType: 'application/json',
        dataType: "json",
        success: function (result) {
            alert(result);
        }
    })
});