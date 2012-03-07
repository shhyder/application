

function isNumberKey(evt, target) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    var isnum = true;
    if (!evt.ctrlKey && charCode > 31 && (charCode < 48 || charCode > 57)) {
        isnum = false;
        if (evt.which)
            evt.preventDefault();
        else
            evt.returnValue = false;
    }
    return fireDefault(evt, target) && isnum;
}

function fireDefault(evt, target) {

    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode == 13 && target != undefined) {
        evt.cancelBubble = true;
        if (evt.stopPropagation) {
            evt.stopPropagation();
            evt.preventDefault();
        }
        var btn = document.getElementById(target);
        btn.click();
        return false;
    }
    return true;
}


function SetAddress() {

   
    geocoder = new google.maps.Geocoder();
    var input = document.getElementById('txtAddress');
    var autocomplete = new google.maps.places.Autocomplete(input);
    google.maps.event.addListener(autocomplete, 'place_changed', function() {

        var place = autocomplete.getPlace();

        var startLatitude = place.geometry.location.lat();
        var startLongitude = place.geometry.location.lng();


        document.getElementById("hidLatitude").value = startLatitude;
        document.getElementById("hidLongitude").value = startLongitude;

        var address = '';
        if (place.address_components) {
            address = [(place.address_components[0] &&
                        place.address_components[0].short_name || ''),
                       (place.address_components[1] &&
                        place.address_components[1].short_name || ''),
                       (place.address_components[2] &&
                        place.address_components[2].short_name || '')
                      ].join(' ');
        }

        $("#txtFullAddress").val(place.formatted_address);

        var imagelink = "http://maps.google.com/maps/api/staticmap?center=" + startLatitude + "," + startLongitude + "&zoom=15&size=250x180&maptype=roadmap&" +
         "markers=size:mid%7Ccolor:green%7Clabel:D%7C" + startLatitude + "," + startLongitude + "&sensor=false";
        imagelink = "<img border='0' src=" + imagelink + " alt='image could not be found' />";
        $("#MapImage").html(imagelink);

        var arr = place.address_components;


        $("#txtStreetName").val('');
        $("#txtCountry").val('');
        $("#txtState").val('');
        $("#txtCity").val('');
        $("#txtZipCode").val('');
        $("#txtStreetNo").val('');


        var holder = "";
        var type = "";
        for (var i = 0; i < arr.length; i++) {
            type = arr[i].types;

       
            if (type.indexOf("street_address") != -1) {
                $("#txtStreetName").val(arr[i].long_name);
            }

            if (type.indexOf("country") != -1) {
                $("#txtCountry").val(arr[i].short_name);
            }

            if (type.indexOf("administrative_area_level_2") != -1) {
                $("#txtCounty").val( arr[i].short_name);
            }

            if (type.indexOf("locality") != -1) {
                $("#txtCity").val(arr[i].short_name);
            }

            if (type.indexOf("administrative_area_level_1") != -1) {
                $("#txtState").val(arr[i].short_name);
            }

            if (type.indexOf("administrative_area_level_3") != -1) {
                alert(type + "     " + arr[i].long_name + "   " + arr[i].short_name);
            }

            if (type.indexOf("postal_code") != -1) {
                $("#txtZipCode").val(arr[i].long_name);
            }

            if (type.indexOf("street_number") != -1) {
                $("#txtStreetNo").val(arr[i].long_name);
            }



        }

    });
    GetMap();
}


function GetMap() {
    if (  $("#hidState").val() != "3" ) {
        var imagelink = "http://maps.google.com/maps/api/staticmap?center=" + $("#hidLatitude").val() + "," + $("#hidLongitude").val() + "&zoom=15&size=250x180&maptype=roadmap&" +
                 "markers=size:mid%7Ccolor:green%7Clabel:D%7C" + $("#hidLatitude").val() + "," + $("#hidLongitude").val() + "&sensor=false";
        imagelink = "<img border='0' src=" + imagelink + " alt='image could not be found' />";
        $("#MapImage").html(imagelink);
    }
}







