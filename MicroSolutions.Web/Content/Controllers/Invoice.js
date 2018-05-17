function AddNewPartToInvoice() {
	var availableNumberOfPaers = ($('#invoice-parts tr').length - 1);//$("#part-for-invoice-model > div").length;
	var vendorId = $("#vendor-id").val();
	var itemTypeId = $("#item-type-id").val();
	var partNumber = $("#part-number-value").val();
	var quantity = $("#quantity-value").val();
	var serialNumbers = $("#serial-numbers-value").val();
	var supplierId = $("#supplier-id").val();
	var partDescription = $("#part-description-value").val();
	var expirationPeriodId = $("#expiration-period-id").val();
	var startDate = $("#start-date-value").val();
	var endDate = $("#end-date-value").val();
	var vendorName = $('#vendor-id :selected').text();
	var itemTypeName = $('#item-type-id :selected').text();
	var supplierName = $('#supplier-id :selected').text();
	var expirationPeriodName = $('#expiration-period-id').text();

	//var list = '<div> ' +
	//	'<input type="hidden" name="PartsForInvoiceViewModel[' + availableNumberOfPaers + '].ItemTypeId" value=' + itemTypeName + '>' +
	//	'<input type="hidden" name="PartsForInvoiceViewModel[' + availableNumberOfPaers + '].PartNumber" value=' + partNumber + '>' +
	//	'<input type="hidden" name="PartsForInvoiceViewModel[' + availableNumberOfPaers + '].VendorId" value=' + vendorName + '>' +
	//	'<input type="hidden" name="PartsForInvoiceViewModel[' + availableNumberOfPaers + '].SerialNumber" value=' + serialNumbers + '>' +
	//	'<input type="hidden" name="PartsForInvoiceViewModel[' + availableNumberOfPaers + '].Quantity" value=' + quantity + '>' +
	//	'<input type="hidden" name="PartsForInvoiceViewModel[' + availableNumberOfPaers + '].Supplier" value=' + supplierName + '>' +
	//	'<input type="hidden" name="PartsForInvoiceViewModel[' + availableNumberOfPaers + '].Description" value=' + partDescription + '>' +
	//	'<input type="hidden" name="PartsForInvoiceViewModel[' + availableNumberOfPaers + '].ExpirationPeriodId" value=' + expirationPeriodName + '>' +
	//	'<input type="hidden" name="PartsForInvoiceViewModel[' + availableNumberOfPaers + '].StartingDate" value=' + startDate + '>' +
	//	'<input type="hidden" name="PartsForInvoiceViewModel[' + availableNumberOfPaers + '].EndDate" value=' + endDate + '>' +
	//	vendorId + ' - ' + itemTypeId + ' - ' + partNumber + ' - ' + quantity + '</div>';
	//$('#parts-for-invoice').append(list);

	var row = '<tr id="' + availableNumberOfPaers+'">' +
		'<td><input type="hidden" name="PartsForInvoiceViewModel[' + availableNumberOfPaers + '].ItemTypeId" value=' + itemTypeId + '>' + itemTypeName +'</td>' +
		'<td><input type="hidden" name="PartsForInvoiceViewModel[' + availableNumberOfPaers + '].PartNumber" value=' + partNumber + '>' + partNumber +'</td>' +
		'<td><input type="hidden" name="PartsForInvoiceViewModel[' + availableNumberOfPaers + '].VendorId" value=' + vendorId + '>' + vendorName +'</td>' +
		'<td><input type="hidden" name="PartsForInvoiceViewModel[' + availableNumberOfPaers + '].SerialNumber" value=' + serialNumbers + '>' + serialNumbers +'</td>' +
		'<td><input type="hidden" name="PartsForInvoiceViewModel[' + availableNumberOfPaers + '].Quantity" value=' + quantity + '>' + quantity +'</td>' +
		'<td><input type="hidden" name="PartsForInvoiceViewModel[' + availableNumberOfPaers + '].SupplierId" value=' + supplierId + '>' + supplierName +'</td>' +
		'<td><input type="hidden" name="PartsForInvoiceViewModel[' + availableNumberOfPaers + '].Description" value=' + partDescription + '>' + partDescription +'</td>' +
		'<td><input type="hidden" name="PartsForInvoiceViewModel[' + availableNumberOfPaers + '].ExpirationPeriodId" value=' + expirationPeriodId + '>' + expirationPeriodName +'</td>' +
		'<td><input type="hidden" name="PartsForInvoiceViewModel[' + availableNumberOfPaers + '].StartingDate" value=' + startDate + '>' + startDate +'</td>' +
		'<td><input type="hidden" name="PartsForInvoiceViewModel[' + availableNumberOfPaers + '].EndDate" value=' + endDate + '>' + endDate + '</td>' +
		'<td><input type="hidden" name="PartsForInvoiceViewModel[' + availableNumberOfPaers + '].Status" value="true" ><input type="hidden" name="PartsForInvoiceViewModel[' + availableNumberOfPaers + '].IsNew" value="true" ><i class="fa fa-times" aria-hidden="true" onclick="RemoveRowFromTable(' + availableNumberOfPaers+');"></i></td>' +
		'</tr>';
    $('#invoice-parts tr:last').after(row);
    $('#add-parts-for-invoice-modal').modal('hide');
}

function RemoveRowFromTable(rowNumber) {
	$("#"+rowNumber).remove();
}
