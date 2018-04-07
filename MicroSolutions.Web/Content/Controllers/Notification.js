function editNotification(id) {
	$.ajax({
		url: "Edit/" + id,
		type: "GET",
		contentType: "application/json;charset=UTF-8",
		dataType: "Json",
		success: function (result) {
			$("#Notification-Id").val("" + id);
			$("#Edit-Notification-InvoiceNumber").val("" + result.InvoiceNumber);
			$("#Edit-Notification-PartNumber").val("" + result.PartNumber);
			$("#Edit-Notification-Next-Notification-Date").val("");
			$('#edit-notification-modal').modal('show');
		},
		error: function (error) {
			console.log(error);
		}
	});
}

function UpdateNotification() {
	var Notification =
		{
			Id: $("#Notification-Id").val(),
			NextNotificationDate: $("#Edit-Notification-Next-Notification-Date").val()
		}

	$.ajax({
		url: "UpdateNotification/",
		data: JSON.stringify(Notification),
		type: "POST",
		contentType: "application/json;charset=UTF-8",
		dataType: "Json",
		success: function (result) {
			$('#edit-notification-modal').modal('hide');
			location.reload();
		},
		error: function (error) {
			console.log(error);
			alert(error);
		}
	});
}