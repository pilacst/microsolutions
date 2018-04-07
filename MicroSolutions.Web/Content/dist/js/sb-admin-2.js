/*!
 * Start Bootstrap - SB Admin 2 v3.3.7+1 (http://startbootstrap.com/template-overviews/sb-admin-2)
 * Copyright 2013-2016 Start Bootstrap
 * Licensed under MIT (https://github.com/BlackrockDigital/startbootstrap/blob/gh-pages/LICENSE)
 */
$(function() {
    $('#side-menu').metisMenu();
});

//Loads the correct sidebar on window load,
//collapses the sidebar on window resize.
// Sets the min-height of #page-wrapper to window size
$(function() {
    $(window).bind("load resize", function() {
        var topOffset = 50;
        var width = (this.window.innerWidth > 0) ? this.window.innerWidth : this.screen.width;
        if (width < 768) {
            $('div.navbar-collapse').addClass('collapse');
            topOffset = 100; // 2-row-menu
        } else {
            $('div.navbar-collapse').removeClass('collapse');
        }

        var height = ((this.window.innerHeight > 0) ? this.window.innerHeight : this.screen.height) - 1;
        height = height - topOffset;
        if (height < 1) height = 1;
        if (height > topOffset) {
            $("#page-wrapper").css("min-height", (height) + "px");
        }
    });

    var url = window.location;
    // var element = $('ul.nav a').filter(function() {
    //     return this.href == url;
    // }).addClass('active').parent().parent().addClass('in').parent();
    var element = $('ul.nav a').filter(function() {
        return this.href == url;
    }).addClass('active').parent();

    while (true) {
        if (element.is('li')) {
            element = element.parent().addClass('in').parent();
        } else {
            break;
        }
    }
});

$(function () {
	$('#registered-users').DataTable({
		responsive: true
	});

	$('#customer-table').DataTable({
		responsive: true
	});

	$('#expiration-period-table').DataTable({
		responsive: true
	});

	$('#item-type-table').DataTable({
		responsive: true
	});

	$('#vendors-table').DataTable({
		responsive: true
	});
	$('#invoice-table').DataTable({
		responsive: true
	});
	$('#supplier-table').DataTable({
		responsive: true
	});
	$('#invoice-parts').DataTable({
		responsive: true
	});
	$('#expired-invoice-table').DataTable({
		responsive: true
	});
	
});

function ErrorMessage(element, message) {
	$(element).html('<div class="alert alert-danger alert-dismissable" id="add-fail-message"><button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>'
				+message+'</div >');
}

function SuccessMessage(element, message) {
	$(element).html('<div class="alert alert-success alert-dismissable" id="add-fail-message"><button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>'
		+ message + '</div >');
}


