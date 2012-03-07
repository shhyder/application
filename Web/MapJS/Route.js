    var directionsDisplay;
    var directionsService; 
    var directionMap;
    var oldDirections = [];
    var currentDirections = null;
    var endLatitude;
    var endLongitude;
    
    function getDirection(endLat, endLog,id) {
        endLatitude = endLat;
        endLongitude = endLog;
        document.getElementById('detail').innerHTML = markers[id].detail;
        jQuery("#fb-root").html(markers[id].facebook);
        gapi.plusone.render("gPlus-root");
        FB.XFBML.parse(document.getElementById('fb-root'));
        
        if (document.getElementById("startLatitude").value.length != 0 && document.getElementById("startLongitude").value.length != 0) {
            Action([6, 12,13,15]);
            document.getElementById('address').value = document.getElementById("hidAddress").value;
            _gaq.push(['_trackEvent', 'Store detail', 'Select', 'Store', id, id]);
            calcRoute(document.getElementById("startLatitude").value, document.getElementById("startLongitude").value,
            endLatitude, endLongitude);
        }
        else {
            Action([6, 12,10,14,16]);
        }


    }

    function initDirection(strLat, strLog, endLat, endLog,id) {

    
        var myOptions = {
            zoom: 13,
            center: new google.maps.LatLng(strLat, strLog),
            mapTypeId: google.maps.MapTypeId.ROADMAP
        }

        directionMap = new google.maps.Map(document.getElementById("map_Direction_Canvas"), myOptions);
        directionsService = new google.maps.DirectionsService();
       

        directionsDisplay = new google.maps.DirectionsRenderer({
            'map': directionMap,
            'preserveViewport': false,
            'draggable': false
        });


        directionsDisplay.setPanel(document.getElementById("directions_panel"));
      
        calcRoute(strLat, strLog, endLat, endLog);
        
        
 

      bounds = null;
      bounds = new google.maps.LatLngBounds();
      bounds.extend(new google.maps.LatLng(strLat, strLog));
      bounds.extend(new google.maps.LatLng(endLat, endLog));
      directionMap.fitBounds(bounds);

      
    }

    function calcRoute(strLat, strLog, endLat, endLog) {
        
        var start = new google.maps.LatLng(strLat, strLog);
        var end = new google.maps.LatLng(endLat, endLog);
        directionMap.setCenter(new google.maps.LatLng(strLat, strLog));
        directionMap.setZoom(13);

        var request = {
            origin: start,
            destination: end,
            travelMode: google.maps.DirectionsTravelMode.DRIVING
        };
        directionsService.route(request, function(response, status) {
            if (status == google.maps.DirectionsStatus.OK) {
                directionsDisplay.setDirections(response);
            }
        });

        bounds = null;
        bounds = new google.maps.LatLngBounds();
        bounds.extend(new google.maps.LatLng(strLat, strLog));
        bounds.extend(new google.maps.LatLng(endLat, endLog));
        directionMap.fitBounds(bounds);

    }

