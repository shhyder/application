





// NOTE: This code remains here for historical reasons, but it has been
// entirely superceded by the file found at:

// http://uwmike.com/maps/manhattan2/labeled_marker.js





/* Constructor for an extended Marker class */
function LabeledMarker(latlng, options) {
    
    this.latlng = latlng;
    this.labelText = options.labelText || "";
    this.labelClass = options.labelClass || "markerLabel";
    this.labelOffset = options.labelOffset || new google.maps.Size(0, 0);


    google.maps.Marker.apply(this, arguments);


    //alert("label marker");
   



    //this.google.maps.Marker
////    google.maps.event.addListener(this, "click", function() {

////    infoBubble = new InfoBubble({
////        maxWidth: 300
////    });


////    infoBubble2 = new InfoBubble({
////        map: map,
////        content: '<div class="phoneytext">Some label</div>',
////        position: new google.maps.LatLng(-35, 151),
////        shadowStyle: 1,
////        padding: 0,
////        backgroundColor: 'rgb(57,57,57)',
////        borderRadius: 4,
////        arrowSize: 10,
////        borderWidth: 1,
////        borderColor: '#2c2c2c',
////        disableAutoPan: true,
////        hideCloseButton: true,
////        arrowPosition: 30,
////        backgroundClassName: 'phoney',
////        arrowStyle: 2
////    });

//    infoBubble.open(map, this);
//    infoBubble2.open();

//    var div = document.createElement('DIV');
//    div.innerHTML = 'Hello';

//    infoBubble.addTab('A Tab', div);
//    infoBubble.addTab('Uluru', 'contentString');
        
//        
//        
//        
//        
//        
//        
//        
//        
//        
//        
//        alert("fsda");
//    });
   
}


/* It's a limitation of JavaScript inheritance that we can't conveniently
extend google.maps.Marker without having to run its constructor. In order for the
constructor to run, it requires some dummy GLatLng. */
LabeledMarker.prototype = new google.maps.Marker(new google.maps.LatLng(0, 0));





// Creates the text div that goes over the marker.
LabeledMarker.prototype.initialize = function(map) {
// Do the google.maps.Marker constructor first.
alert("map");
    google.maps.Marker.prototype.initialize.call(this, map);
    
    var div = document.createElement("div");
    div.className = this.labelClass;
    div.innerHTML = this.labelText;
    div.style.position = "absolute";
    div.style.zIndex = 1;
    div.setAttribute('style', 'pointer-events :none;position:absolute');
    map.getPane(G_MAP_MARKER_PANE).appendChild(div);
    this.map = map;
    this.div = div;
}

// Redraw the rectangle based on the current projection and zoom level
LabeledMarker.prototype.redraw = function(force) {
    alert("force");
    google.maps.Marker.prototype.redraw.call(this, map);

    // We only need to do anything if the coordinate system has changed
    if (!force) return;

    // Calculate the DIV coordinates of two opposite corners of our bounds to
    // get the size and position of our rectangle
    alert("label marker");
    var p = this.map.fromLatLngToDivPixel(this.latlng);
    var z = google.maps.Overlay.getZIndex(this.latlng.lat());

    // Now position our DIV based on the DIV coordinates of our bounds
    this.div.style.left = (p.x + this.labelOffset.width) + "px";
    this.div.style.top = (p.y + this.labelOffset.height) + "px";
    this.div.style.zIndex = z + 1; // in front of the marker
}

// Remove the main DIV from the map pane
LabeledMarker.prototype.remove = function() {
    this.div.parentNode.removeChild(this.div);
    this.div = null;
    google.maps.Marker.prototype.remove.call(this);
}