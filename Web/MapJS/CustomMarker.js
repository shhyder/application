﻿function CustomMarker(latlng, map) {
    google.maps.OverlayView.call(this);

    this.latlng_ = latlng;

    this.hasan = 'dfasdf';

    // Once the LatLng and text are set, add the overlay to the map.  This will
    // trigger a call to panes_changed which should in turn call draw.
    this.setMap(map);
}

CustomMarker.prototype = new google.maps.OverlayView();

CustomMarker.prototype.draw = function() {
    var me = this;

    // Check if the div has been created.
    var div = this.div_;
    if (!div) {
        // Create a overlay text DIV
        div = this.div_ = document.createElement('DIV');
        // Create the DIV representing our CustomMarker
        div.style.border = "1px solid none";
        div.style.position = "absolute";
        div.style.paddingLeft = "0px";
        div.style.cursor = 'pointer';
        div.style.height = '52 px';
        div.style.width = '52 px';

        //        icon.iconSize = new google.maps.Size(52, 37);
        //        icon.iconAnchor = new google.maps.Point(16, 16);
        div.innerHTML = "this.labelText";
        var img = document.createElement("img");
        //img.src = "http://gmaps-samples.googlecode.com/svn/trunk/markers/circular/bluecirclemarker.png";
        div.appendChild(img);
        google.maps.event.addDomListener(div, "click", function(event) {
            google.maps.event.trigger(me, "click");
        });

        // Then add the overlay to the DOM
        var panes = this.getPanes();
        panes.overlayLayer.appendChild(div);
    }

    // Position the overlay 
    var point = this.get_projection().fromLatLngToDivPixel(this.latlng_);
    if (point) {
        div.style.left = point.x + 'px';
        div.style.top = point.y + 'px';
    }
};

CustomMarker.prototype.remove = function() {
    // Check if the overlay was on the map and needs to be removed.
    if (this.div_) {
        this.div_.parentNode.removeChild(this.div_);
        this.div_ = null;
    }
};

var map;
var overlay;
function initialize() {
    var opts = {
        zoom: 9,
        center: new google.maps.LatLng(-34.397, 150.644),
        mapTypeId: google.maps.MapTypeId.ROADMAP
    }
    map = new google.maps.Map(document.getElementById("map"), opts);

    overlay = new CustomMarker(map.get_center(), map);
    google.maps.event.addListener(overlay, "click", function() {
        alert("You clicked me! Yippee!");
    });
}

function addOverlay() {
    overlay.set_map(map);
}

function removeOverlay() {
    overlay.set_map(null);
}