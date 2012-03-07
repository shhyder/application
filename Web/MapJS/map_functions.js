var map, manager;
var centerLatitude = 40, centerLongitude = -98, startZoom = 4;


function createMarker(pointData) {
    alert("create marker");
    var latlng = new google.maps.LatLng(pointData.latitude, pointData.longitude);
    var icon = new google.maps.MarkerImage();
    //icon.image = 'http://cdn4.iconfinder.com/data/icons/softwaredemo/PNG/128x128/Box_Blue.png';  //'http://uwmike.com/maps/manhattan/img/red-marker.png';
    icon.iconSize = new google.maps.Size(52, 37);
    icon.iconAnchor = new google.maps.Point(16, 16);
    opts = {
        "icon": icon,
        "clickable": true,
        "draggable": false,
        "labelText": pointData.abbr,
        "labelOffset": new google.maps.Size(-16, -16)
    };
    var marker = new LabeledMarker(latlng, opts); //CustomMarker(latlng, map); //
    

//    google.maps.event.addListener(marker, 'click', function() {
//        alert(pointData.abbr);
//    });

//    var listItem = document.createElement('li');
//    listItem.innerHTML = '<div class="label">' + pointData.abbr + '</div><a href="' + pointData.wp + '">' + pointData.name + '</a>';

//    document.getElementById('sidebar-list').appendChild(listItem);

    return marker;
}

function windowHeight() {
    // Standard browsers (Mozilla, Safari, etc.)
    if (self.innerHeight)
        return self.innerHeight;
    // IE 6
    if (document.documentElement && document.documentElement.clientHeight)
        return document.documentElement.clientHeight;
    // IE 5
    if (document.body)
        return document.body.clientHeight;
    // Just in case. 
    return 0;
}

function handleResize() {
    var height = windowHeight(); //- document.getElementById('toolbar').offsetHeight - 30;
    document.getElementById('map').style.height = height + 'px';
    //document.getElementById('sidebar').style.height = height + 'px';
}

function init() {
    handleResize();
//    var mgrOptions = { borderPadding: 50, maxZoom: 15, trackMarkers: true, 
//        center: new google.maps.LatLng(48.25, 11.00),
//        mapTypeId: google.maps.MapTypeId.ROADMAP
//    };

    var mgrOptions = {
        zoom: 4,
        center: new google.maps.LatLng(centerLatitude, centerLongitude),
        mapTypeId: google.maps.MapTypeId.ROADMAP,
        borderPadding: 50, maxZoom: 15, trackMarkers: true
    };
    
    map = new google.maps.Map(document.getElementById("map"), mgrOptions);
//    map.addControl(new google.maps.SmallMapControl());
    //map.setCenter(new google.maps.LatLng(centerLatitude, centerLongitude), startZoom);
//    map.addControl(new google.maps.MapTypeControl());


    var listener = google.maps.event.addListener(map, 'bounds_changed', function() {

        alert("bound");
        manager = new MarkerManager(map, mgrOptions);

        google.maps.event.addListener(manager, 'loaded', function() {

            // This is a sorting trick, don't worry too much about it.
            markers.sort(function(a, b) { return (a.abbr > b.abbr) ? +1 : -1; });

            batch = [];
            for (id in markers) {
                batch.push(createMarker(markers[id]));
            }
            manager.addMarkers(batch, 3);
            manager.refresh();
        });



        google.maps.event.removeListener(listener);
    });

   
//    GEvent.addListener(map, 'click', function() {
//        alert("check");
//        alert("marker" + marker.labelText);

//    });
}

//window.onresize = handleResize;
window.onload = init;
//window.onunload = google.maps.Unload;