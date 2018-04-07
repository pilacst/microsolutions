function editExpirationPeriod(id)
{
	$.ajax({
		url: "Settings/EditExpirationPeriod/" + id,
		type: "GET",
		contentType: "application/json;charset=UTF-8",
		dataType: "Json",
		success: function (result) {
			$("#ExpirationPeriod-Id").val("" + id);
			$("#ExpirationPeriod-Name").val("" + result.ExpirationPeriodName);
			$("#ExpirationPeriod-Value").val("" + result.ExpirationPeriodValue);
			$('#edit-expiration-period-modal').modal('show');
		},
		error: function (error) {
			console.log(error);
			alert(error);
		}
	});
}

function updateExpirationPeriod() {
	var expirationPeriod =
		{
			ExpirationPeriodId: $("#ExpirationPeriod-Id").val(),
			ExpirationPeriodName: $("#ExpirationPeriod-Name").val(),
			ExpirationPeriodValue: $("#ExpirationPeriod-Value").val()
		}

	$.ajax({
		url: "Settings/UpdateExpirationPeriod/",
		data: JSON.stringify(expirationPeriod),
		type: "POST",
		contentType: "application/json;charset=UTF-8",
		dataType: "Json",
		success: function (result) {
			$('#edit-expiration-period-modal').modal('hide');
			location.reload();
		},
		error: function (error) {
			console.log(error);
			alert(error);
		}
	});
}

function confirmDeleteExpirationPeriod(id) {
	$.ajax({
		url: "Settings/EditExpirationPeriod/" + id,
		type: "GET",
		contentType: "application/json;charset=UTF-8",
		dataType: "Json",
		success: function (result) {
			$("#Delete-ExpirationPeriod-Id").val("" + id);
			$("#Delete-ExpirationPeriod-Name").val("" + result.ExpirationPeriodName);
			$("#Delete-ExpirationPeriod-Value").val("" + result.ExpirationPeriodValue);
			$('#delete-expiration-period-modal').modal('show');
		},
		error: function (error) {
			alert(error);
		}
	});
}

function deleteExpirationPeriod() {
	var expirationPeriod =
		{
			ExpirationPeriodId: $("#Delete-ExpirationPeriod-Id").val(),
			ExpirationPeriodName: $("#Delete-ExpirationPeriod-Name").val(),
			ExpirationPeriodValue: $("#Delete-ExpirationPeriod-Value").val()
		}

	$.ajax({
		url: "Settings/DeleteExpirationPeriod/",
		data: JSON.stringify(expirationPeriod),
		type: "POST",
		contentType: "application/json;charset=UTF-8",
		dataType: "Json",
		success: function (result) {
			$('#delete-expiration-period-modal').modal('hide');
			location.reload();
		},
		error: function (error) {
			console.log(error);
			alert(error);
		}
	});
}

function expirationPeriodAddModel() {
	$('#add-expiration-period-modal').modal('show');
}

function addExpirationPeriod() {
	var expirationPeriod =
		{
			ExpirationPeriodName: $("#Add-ExpirationPeriod-Name").val(),
			ExpirationPeriodValue: $("#Add-ExpirationPeriod-Value").val()
		}

	$.ajax({
		url: "Settings/AddExpirationPeriod/",
		data: JSON.stringify(expirationPeriod),
		type: "POST",
		contentType: "application/json;charset=UTF-8",
		dataType: "Json",
		success: function (result) {
			$('#add-expiration-period-modal').modal('hide');
			location.reload();
		},
		error: function (error) {
			console.log(error);
			alert(error);
		}
	});
}
