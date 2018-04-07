function editVendor(id) {
	$.ajax({
		url: "Settings/EditVendor/" + id,
		type: "GET",
		contentType: "application/json;charset=UTF-8",
		dataType: "Json",
		success: function (result) {
			console.log(result);
			$("#Edit-Vendor-Id").val("" + id);
			$("#Edit-Vendor-Name").val("" + result.VenderName);
			$('#edit-vendor-modal').modal('show');
		},
		error: function (error) {
			console.log(error);
			alert(error);
		}
	});
}

function updateVendor() {
	var vendor =
		{
			Id: $("#Edit-Vendor-Id").val(),
			VenderName: $("#Edit-Vendor-Name").val()
		}

	$.ajax({
		url: "Settings/UpdateVendor/",
		data: JSON.stringify(vendor),
		type: "POST",
		contentType: "application/json;charset=UTF-8",
		dataType: "Json",
		success: function (result) {
			$('#edit-vendor-modal').modal('hide');
			location.reload();
		},
		error: function (error) {
			console.log(error);
			alert(error);
		}
	});
}

function confirmDeleteVendor(id) {
	$.ajax({
		url: "Settings/EditVendor/" + id,
		type: "GET",
		contentType: "application/json;charset=UTF-8",
		dataType: "Json",
		success: function (result) {
			console.log(result);
			$("#Delete-Vendor-Id").val("" + id);
			$("#Delete-Vendor-Name").val("" + result.VenderName);
			$('#delete-vendor-modal').modal('show');
		},
		error: function (error) {
			console.log(error);
			alert(error);
		}
	});
}

function deleteVendor() {
	var vendor =
		{
			Id: $("#Delete-Vendor-Id").val(),
			VenderName: $("#Delete-Vendor-Name").val()
		}

	$.ajax({
		url: "Settings/DeleteVendor/",
		data: JSON.stringify(vendor),
		type: "POST",
		contentType: "application/json;charset=UTF-8",
		dataType: "Json",
		success: function (result) {
			$('#delete-vendor-modal').modal('hide');
			location.reload();
		},
		error: function (error) {
			console.log(error);
			alert(error);
		}
	});
}

function vendorAddModel() {
	$('#add-vendor-modal').modal('show');
}

function addVendor() {
	var vendor =
		{
			VenderName: $("#Add-Vendor-Name").val()
		}
	$.ajax({
		url: "Settings/AddVendor/",
		data: JSON.stringify(vendor),
		type: "POST",
		contentType: "application/json;charset=UTF-8",
		dataType: "Json",
		success: function (result) {
			$('#add-vendor-modal').modal('hide');
			location.reload();
		},
		error: function (error) {
			console.log(error);
			alert(error);
		}
	});
}


function purchasedItemAddModel() {
	$('#add-purchased-item-modal').modal('show');
}

function addPurchasedItem() {
	var purchasedItem =
		{
			PurchasedItemName: $("#Add-PurchasedItem-Name").val()
		}

	$.ajax({
		url: "Settings/AddPurchasedItem/",
		data: JSON.stringify(purchasedItem),
		type: "POST",
		contentType: "application/json;charset=UTF-8",
		dataType: "Json",
		success: function (result) {
			$('#add-purchased-item-modal').modal('hide');
			location.reload();
		},
		error: function (error) {
			console.log(error);
			alert(error);
		}
	});
}
