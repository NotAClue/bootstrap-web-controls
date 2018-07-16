$(document).ready(function ()
{
	$.cookie('bootstrap-tabs-Customers-last_tab', null, { path: '/' });
	$('#bootstrap-tabs-Customers a[data-toggle=tab]').on('shown.bs.tab',
		function (e)
		{
			//save the latest tab using a cookie:
			$.cookie('bootstrap-tabs-Customers-last_tab', $(e.target).attr('href'));
		}
	);

	//activate last selected tab, if it exists:
	var lastTab = $.cookie('bootstrap-tabs-Customers-last_tab');
	if (lastTab)
	{
		$('a[href=' + lastTab + ']').tab('show');
	}
	else
	{
		// Set the first tab if cookie does not exist
		$('#bootstrap-tabs-Customers a[data-toggle="tab"]:first').tab('show');
	}
});
