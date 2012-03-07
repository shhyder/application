





function Action(actions) {

    for (var i=0; i < actions.length; i++) {
        switch( actions[i]  )
        {
            case '1':
                jQuery('#slider').fadeTo("slow", 0.5, function() { });
              break;
          case '2':
              jQuery('#slider').fadeTo("slow", 1.0, function() { });
                break;
            case 3:
                jQuery('#content1').fadeOut('slow', function() { });
                break;

            case 4:
                jQuery('#mapPanel').fadeIn(2000);
                break;

            case 5:
                jQuery('#content1').fadeIn(2000);
                break;

            case 6:
                jQuery('#mapPanel').fadeOut(2000);
                break;

            case 7:
                jQuery('#dealerList').fadeTo("slow", 0.5, function() { });
                break;

            case 8:
                jQuery('#dealerList').fadeTo("slow", 1.0, function() { });
                break;

            case 9:
                jQuery('#specificAddress').fadeOut(2000);
                break;

            case 10:
                jQuery('#specificAddress').fadeIn(2000);
                break;

            case 11:
                jQuery('#pnlDirection').fadeOut(2000);
                break;

            case 12:
                jQuery('#pnlDirection').fadeIn(2000);
                break;

            case 13:
                jQuery('#directionMap').fadeIn(2000);
                jQuery('#fb-root').fadeIn(2000);
                
                break;

            case 14:
                jQuery('#directionMap').fadeOut(2000);
                jQuery('#fb-root').fadeOut(2000);
                break;

            case 15:
                jQuery('#detail').fadeIn(2000);
                break;

            case 16:
                jQuery('#detail').fadeOut(2000);
                break;

                
        }
    }



}


function makeQuerystring(is_From_Navigation) {
    moreinfo = '';
    var count = 0;
    var id;
   
    if (document.getElementById("txtzipcode").value.length == 0) {
        Action([2]);
        jQuery("#resultsProductsAndDealers").html(ErrorAdorn("Enter valid zip code"));
        return;
    }




    jQuery('.cbs input[type=checkbox]').each(
      function() {
          if (jQuery(this).is(':checked')) {
              name = jQuery(this).attr('name');
              id = "pro-" + jQuery(this).attr('id');
              moreinfo += '&' + id + '=1';
              _gaq.push(['_trackEvent', 'Search Product', 'Select', name, id, jQuery(this).attr('id')]);
              count++;
          }
      });



    if (count == 0) {
        jQuery("#resultsProductsAndDealers").html(ErrorAdorn("select at least a product to search"));
        Action([2]);
        return;
    }

    var queryString = "";
    queryString = jQuery("#searchForm").formSerialize(false) + moreinfo; 

    GetDealerList(1, queryString, is_From_Navigation);
}



function press() {
   makeQuerystring(false);
}

function checkBoxClicked() {
    if (document.getElementById("is_Auto_Search_Enabled").checked) {
        makeQuerystring(false);
    }
    else {
        document.getElementById('resultsProductsAndDealers').innerHTML = '&nbsp;';
    }

}


function resetSearch() {
    if (confirm("Reset this form?")) {
        jQuery('input[type=checkbox]').removeAttr('checked');
    }
    document.getElementById('resultsProductsAndDealers').innerHTML = '&nbsp;';
    Action([11,5,6, 15]);
}

var startLatitude;
var startLongitude;

jQuery(document).ready(function() {
    geocoder = new google.maps.Geocoder();
    var input = document.getElementById('address');
    var autocomplete = new google.maps.places.Autocomplete(input);
    google.maps.event.addListener(autocomplete, 'place_changed', function() {

        var place = autocomplete.getPlace();

        var startLatitude = place.geometry.location.lat();
        var startLongitude = place.geometry.location.lng();

        document.getElementById("startLatitude").value = startLatitude;
        document.getElementById("startLongitude").value = startLongitude;
        document.getElementById("hidAddress").value = document.getElementById('address').value;

        Action([9, 13, 15]);
        _gaq.push(['_trackEvent', 'Entered Address', 'Entered', 'Address', document.getElementById('address').value]);
   
        if (directionMap == null || directionsDisplay == null) {
            initDirection(startLatitude, startLongitude, endLatitude, endLongitude, 1);
            calcRoute(startLatitude, startLongitude, endLatitude, endLongitude);
        }
        else {
            //initDirection(startLatitude, startLongitude, endLatitude, endLongitude, 1);
            calcRoute(startLatitude, startLongitude, endLatitude, endLongitude);
        }


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


    });


});



var markers;
var map;
var bounds;
var opts;
var listener;
var markersArray = [];
var geocoder;
var pinkParksStyles = [
  {
    featureType: "water",
    stylers: [
      { gamma: 0.7 },
      { visibility: "on" },
      { saturation: 54 },
      { hue: "#003bff" },
      { lightness: 43 }
    ]
  },
  {
      featureType: "poi.park",
      stylers: [
      { lightness: 50 },
      { saturation: 68 },
      { gamma: 0.4 },
      { visibility: "simplified" },
      { hue: "#00ffd5" }
    ]
  }
];


function initialize(centerLatitude, centerLongitude, startZoom) {
    startZoom = 10;
    opts = null;
    opts = {
        zoom: startZoom,
        center: new google.maps.LatLng(centerLatitude, centerLongitude),
        scrollwheel: false,
        streetViewControl: false,
        disableDefaultUI: true,
        navigationControl: false,
        noClear:false,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    }


    

   

    if (map == null) { 
        
       
        
        map = null;
        map = new google.maps.Map(document.getElementById("map"), opts);
        map.getMapType = google.maps.MapTypeId.ROADMAP;
        map.setOptions({ styles: pinkParksStyles });

    

        batch = [];

        for (i = 0; i < markers.length; i++) {
             if (markers[i] != null) {
                batch.push(createMarker(markers[i]));
            }
        }


      

            bounds = null;
            bounds = new google.maps.LatLngBounds();
            for (var i = 0; i < markers.length; i++) {
                if (markers[i] != null) {
                    bounds.extend(new google.maps.LatLng(markers[i].latitude, markers[i].longitude)); 
                }
            }

            map.fitBounds(bounds);

        
    }
    else {


        
        deleteOverlays();
        for (i = 0; i < markers.length; i++) {
            if (markers[i] != null) {
                batch.push(createMarker(markers[i]));
            }
        }


        bounds = null;
        bounds = new google.maps.LatLngBounds();

        var count = 0;
        var detail = "";
       
        for (var i = 0; i < markers.length; i++) {
            if (markers[i].title != null) {
                bounds.extend(new google.maps.LatLng(markers[i].latitude, markers[i].longitude));
                detail += " [" + markers[i].latitude + "," + markers[i].longitude + " ] ";
                count++; 
            }
        }
        map.fitBounds(bounds);
    }
}
// Deletes all markers in the array by removing references to them
function deleteOverlays() {




    for (i = 0; i < markersArray.length; i++) {
        if (markersArray[i] != null) {
            markersArray[i].setMap(null);
        }
    }
    markersArray.length = 0;
    
    

}

function createMarker(pointData) {
    var marker = new google.maps.Marker({
        position: new google.maps.LatLng(pointData.latitude, pointData.longitude),
        map: map,
        title: pointData.title,
        clickable: true,
        icon: pointData.icon,
        zIndex: pointData.zIndex
    });

    
   
    if (marker.getIcon() != undefined) {

        var aPosition = marker.getIcon().indexOf("home");
        if (aPosition < 0) {
            google.maps.event.addListener(marker, 'mouseover', function() {
                //this.setIcon(this.getIcon() + "&Col=yellow")
                this.setIcon(mutateImageLink(this.getIcon(), true));
                this.setZIndex(5);
            });


            google.maps.event.addListener(marker, 'mouseout', function() {
                //this.setIcon(this.getIcon().replace("&Col=yellow", ""))
                this.setIcon(mutateImageLink(this.getIcon(), false));
                this.setZIndex(1);
            });

            google.maps.event.addListener(marker, 'click', function() {
                
            });
        }
    }
    marker.setMap(map);
    markersArray.push(marker);
    return marker;
}

function mutateImageLink(link,is_MouseOver_Icon) {
    
    if (is_MouseOver_Icon) {
        link = link + "&Col=yellow"

    }
    else {
        link = link.replace("&Col=yellow", "")
    }
    return link;
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




    jQuery('input[type=checkbox]').each(
      function() {
          if (jQuery(this).is(':checked')) {
              name = jQuery(this).attr('name');
              moreinfo += '&' + name + '=1';
              count++;
          }
      });



//    if (count == 0) {
//        jQuery("#ctl01_upError").html("select at least a product to search");
//        document.getElementById("map").style.display = 'none';
//        document.getElementById("map-pane").style.display = 'none';
//        document.getElementById("dealerList").style.display = 'none';
//        return;
//    }



 
    var queryString = jQuery('#searchForm').formSerialize(true) + moreinfo;
    GetEventList(1, queryString);



}


function ErrorAdorn(str) {
    return '<div class="error">' + str + '</div>';
}


function itemMouseOver(i,ele) {
    google.maps.event.trigger(markersArray[i], "mouseover");
    jQuery(ele).find('img')[0].src = mutateImageLink(jQuery(ele).find('img')[0].src, true)
}


function itemMouseOut(i,ele) {
    google.maps.event.trigger(markersArray[i], "mouseout");
    jQuery(ele).find('img')[0].src = mutateImageLink(jQuery(ele).find('img')[0].src, false)
}

function itemMouseClick(i) {

    google.maps.event.trigger(markersArray[i], "click");
}


function rowsSizeChange() {
    Action([7]);
    makeQuerystring(true);
}   


function onSearchClick() {
    Action([1, 11, 6]);
    press();
}

function OnChangeAddress() {
    document.getElementById("startLatitude").value = '';
    document.getElementById("startLongitude").value = '';

    getDirection(endLatitude, endLongitude, 1);

}








   



function GetDealerListFromNavigator(page, queryString) {
    Action([7]);
    GetDealerList(page, queryString,true)
}


function GetDealerList(page, queryString,is_From_Navigation) {

    document.getElementById('resultsProductsAndDealers').innerHTML = '<div id="loadingUpper" ><img  src="' + pfxURL.staticVar + "/images/ajax-loader_Upper.gif" + '"  /><strong>Loading</strong></div>';




    jQuery.ajax({
        url: pfxURL.staticVar + "/Search/GetDealer/" + page,
        dataType: 'json',
        type: "POST",
        data: queryString,
        success: function(json) {

            if (json.totalMarker != 0) {

                if (is_From_Navigation) {
                    Action([8]);
                }
                else {
                    Action([3, 4]);
                }

                document.getElementById('resultsProductsAndDealers').innerHTML = '&nbsp;';
                markers = json.Markers;
                //document.getElementById("resultsProductsAndDealers").innerHTML = json.DealerLocator;
                initialize(json.latitude, json.longitude, 5);

                
                document.getElementById("dealerList").innerHTML = json.DealerList;
                document.getElementById("ListHeader").innerHTML = json.DealerListHeader;
                document.getElementById("Navigator").innerHTML = json.DealerListNavigator;


            }
            else {
                Action([2, 5, 11]);
                document.getElementById("resultsProductsAndDealers").innerHTML = ErrorAdorn(json.Error);
            }
            json = null;
        }
    });
}

jQuery("#map-pane").ajaxStart(function() {
    var width = jQuery(this).width();
    var height = jQuery(this).height();
    jQuery("#map-loading").css({
        top: ((height / 2) - 25),
        left: ((width / 2) - 50)
    }).fadeIn(200);    // fast fade in of 200 mili-seconds
}).ajaxStop(function() {
    jQuery("#map-loading", this).fadeOut(1000);    // slow fade out of 1 second
});


function OnSearchFromList() {
    Action([6, 5]);
}

function OnSearchFromDirection() {
    Action([11, 5]);
}


function OnBackToListFromDirection() {
    Action([11,4]);
}

function OnBackToList() {
    Action([3, 4]);
}



// Removes the overlays from the map, but keeps them in the array
function clearOverlays() {
    if (markersArray) {
        for (i in markersArray) {
            markersArray[i].setMap(null);
        }
    }
}

jQuery(document).ready(function() {
    jQuery('input').checkBox();

});


