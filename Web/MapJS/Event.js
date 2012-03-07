
function rowsEventSizeChange() {
    Action([7]);
    pressEvent();
} 


function onEventSearchClick() {
    Action([1, 11, 6]);
    _gaq.push(['_trackEvent', 'Search Store', 'Click', 'zipcode', jQuery("#txtzipcode").attr('value'), jQuery("#txtzipcode").attr('value')];
    pressEvent();
}



function pressEvent() {

    moreinfo = '';
    var count = 0;
    jQuery("#ctl01_upError").html("");


    if (document.getElementById("txtzipcode").value.length == 0) {
        //        jQuery('#ctl01_upLocation').fadeIn('slow', function() { });
        //        jQuery('#resultsProductsAndDealers').fadeIn('slow', function() { });
        //        jQuery("#ctl01_upError").html("Enter valid zip code");
        return;
    }


    var queryString = jQuery('#searchForm').formSerialize(true) + moreinfo;
    GetEventList(1, queryString);



}
