function editItemType(id) {
	$.ajax({
		url: "Settings/EditItemTypeItem/" + id,
		type: "GET",
		contentType: "application/json;charset=UTF-8",
		dataType: "Json",
		success: function (result) {
			$("#ItemType-Id").val("" + id);
			$("#ItemType-Name").val("" + result.ItemTypeName);
			$('#edit-item-type-modal').modal('show');
		},
		error: function (error) {
			console.log(error);
			alert(error);
		}
	});
}

function updateItemType() {
	var ItemType =
		{
			Id: $("#ItemType-Id").val(),
			ItemTypeName: $("#ItemType-Name").val()
		}

	$.ajax({
		url: "Settings/UpdateItemType/",
		data: JSON.stringify(ItemType),
		type: "POST",
		contentType: "application/json;charset=UTF-8",
		dataType: "Json",
		success: function (result) {
			$('#edit-item-type-modal').modal('hide');
			location.reload();
		},
		error: function (error) {
			console.log(error);
			alert(error);
		}
	});
}

function confirmDeleteItemType(id) {
	$.ajax({
		url: "Settings/EditItemTypeItem/" + id,
		type: "GET",
		contentType: "application/json;charset=UTF-8",
		dataType: "Json",
		success: function (result) {
			$("#Delete-ItemType-Id").val("" + id);
			$("#Delete-ItemType-Name").val("" + result.ItemTypeName);
			$('#delete-item-type-modal').modal('show');
		},
		error: function (error) {
			alert(error);
		}
	});
}

function deleteItemType() {
	var itemtype =
		{
			Id: $("#Delete-ItemType-Id").val(),
			ItemTypeName: $("#Delete-ItemType-Name").val()
		}

	$.ajax({
		url: "Settings/DeleteItemType/",
		data: JSON.stringify(itemtype),
		type: "POST",
		contentType: "application/json;charset=UTF-8",
		dataType: "Json",
		success: function (result) {
			$('#delete-item-type-modal').modal('hide');
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


function ItemTypeAddModel() {
	$('#add-item-type-modal').modal('show');
}

function addItemType() {
	var itemtype =
		{
			ItemTypeName: $("#Add-ItemType-Name").val()
		}

	$.ajax({
		url: "Settings/AddItemType/",
		data: JSON.stringify(itemtype),
		type: "POST",
		contentType: "application/json;charset=UTF-8",
		dataType: "Json",
		success: function (result) {
			$('#add-item-type-modal').modal('hide');
			location.reload();
		},
		error: function (error) {
			console.log(error);
			alert(error);
		}
	});
}
