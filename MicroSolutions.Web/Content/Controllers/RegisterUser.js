function EditUser(userId) {
	
	$.ajax({
		url: "RegisterUsers/EditUser/" + userId,
		type: "GET",
		contentType: "application/json;charset=UTF-8",
		dataType: "Json",
		success: function (result) {
			$("#user-Id").val("" + userId);
			$("#Edit-User-Name").val("" + result.UserName);
			$("#User-Role-select").val(result.UserRole);
			$('#active-user').prop('checked', result.IsActiveUserState);
			$('#edit-registered-users-modal').modal('show');
		},
		error: function (error) {
			alert("error");
		}
	});
}

function UpdateUser() {
	var user = {
		UserId: $("#user-Id").val(),
		UserName: $("#Edit-User-Name").val(),
		UserRole: $("#User-Role-select").val(),
		IsActiveUserState: $('#active-user').is(':checked')
	}
	$.ajax({
		url: "RegisterUsers/UpdateUser",
		data: JSON.stringify(user),
		type: "POST",
		contentType: "application/json;charset=UTF-8",
		dataType: "Json",
		success: function (result) {
			$("#user-Id").val("" + result.userId);
			$("#Edit-User-Name").val("" + result.UserName);
			$("#User-Role-select").val(result.UserRoleId);
			$('#active-user').prop('checked', result.IsActiveUserState);
			$('#edit-registered-users-modal').modal('hide');
		},
		error: function (error) {
			alert("error");
		}
	});
}

function AddUser() {
	$("#add-fail-message").html('');
	$('#add-new-user-modal').modal('show');
}

function SaveUser() {

	if (!($("#Add-User-Name").val().length > 0)) {
		ErrorMessage("#result-message", "User name is required.");
		return;
	}
	if (!($("#Add-User-Password").val().length > 0)) {
		ErrorMessage("#result-message", "Password is required.");
		return;
	}

	var user = {
		UserName: $("#Add-User-Name").val(),
		Password: $("#Add-User-Password").val(),
		UserRole: $("#User-Role-select").val()
	}

	$.ajax({
		url: "RegisterUsers/AddNewUser",
		data: JSON.stringify(user),
		type: "POST",
		contentType: "application/json;charset=UTF-8",
		dataType: "Json",
		success: function (result) {
			$('#add-new-user-modal').modal('hide');
		},
		error: function (error) {
			alert("error");
		}
	});
}