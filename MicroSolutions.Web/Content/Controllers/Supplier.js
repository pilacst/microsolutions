function editSupplier(id) {
	$.ajax({
		url: "Settings/EditSupplier/" + id,
		type: "GET",
		contentType: "application/json;charset=UTF-8",
		dataType: "Json",
		success: function (result) {
			console.log(result);
			$("#Edit-Supplier-Id").val("" + id);
			$("#Edit-Supplier-Name").val("" + result.SupplierName);
			$('#edit-supplier-modal').modal('show');
		},
		error: function (error) {
			console.log(error);
			alert(error);
		}
	});
}

function updateSupplier() {
	var Supplier =
		{
			Id: $("#Edit-Supplier-Id").val(),
			SupplierName: $("#Edit-Supplier-Name").val()
		}

	$.ajax({
		url: "Settings/UpdateSupplier/",
		data: JSON.stringify(Supplier),
		type: "POST",
		contentType: "application/json;charset=UTF-8",
		dataType: "Json",
		success: function (result) {
			$('#edit-Supplier-modal').modal('hide');
			location.reload();
		},
		error: function (error) {
			console.log(error);
			alert(error);
		}
	});
}

function confirmDeleteSupplier(id) {
	$.ajax({
		url: "Settings/EditSupplier/" + id,
		type: "GET",
		contentType: "application/json;charset=UTF-8",
		dataType: "Json",
		success: function (result) {
			console.log(result);
			$("#Delete-Supplier-Id").val("" + id);
			$("#Delete-Supplier-Name").val("" + result.SupplierName);
			$('#delete-supplier-modal').modal('show');
		},
		error: function (error) {
			console.log(error);
			alert(error);
		}
	});
}

function deleteSupplier() {
	var Supplier =
		{
			Id: $("#Delete-Supplier-Id").val(),
			SupplierName: $("#Delete-Supplier-Name").val()
		}

	$.ajax({
		url: "Settings/DeleteSupplier/",
		data: JSON.stringify(Supplier),
		type: "POST",
		contentType: "application/json;charset=UTF-8",
		dataType: "Json",
		success: function (result) {
			$('#delete-Supplier-modal').modal('hide');
			location.reload();
		},
		error: function (error) {
			console.log(error);
			alert(error);
		}
	});
}

function SupplierAddModel() {
	$('#add-supplier-modal').modal('show');
}

function addSupplier() {
	var Supplier =
		{
			SupplierName: $("#Add-Supplier-Name").val()
		}
	$.ajax({
		url: "Settings/AddSupplier/",
		data: JSON.stringify(Supplier),
		type: "POST",
		contentType: "application/json;charset=UTF-8",
		dataType: "Json",
		success: function (result) {
			$('#add-Supplier-modal').modal('hide');
			location.reload();
		},
		error: function (error) {
			console.log(error);
			alert(error);
		}
	});
}
