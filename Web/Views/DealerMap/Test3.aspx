<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index test 3
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript" src='<%=  Url.Content("~/js/jquery-1.4.2.min.js")%>' ></script> 
<script type="text/javascript" src='<%=  Url.Content("~/js/jquery-ui-1.8.1.custom.min.js")%>' ></script>
<script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=true"></script> 
<%--<script src="http://maps.google.com/maps?file=api&amp;v=2&amp;key=ABQIAAAAj2HTBsK69zEZ11d7jUHdihQDqmC9gwE6KXkUAz76Dg2Yk-m0iBSDyYT_w5Jw1Xb667V3RjOuUt78vg" type="text/javascript"></script> --%>
<script src= '<%=  Url.Content("~/MapJS/labeled_marker.js")%>'  type="text/javascript"></script> 
<script src='<%=  Url.Content("~/MapJS/map_functions.js")%>' type="text/javascript"></script> 

<script src='<%=  Url.Content("~/MapJS/markermanager.js")%>' type="text/javascript"></script> 
<script src="http://ajax.googleapis.com/ajax/libs/mootools/1.2.1/mootools.js" type="text/javascript"></script><script type="text/javascript" src="<%=  Url.Content("~/MapJS/infobubble.js")%>"></script> 
<link href="style.css" rel="stylesheet" type="text/css" /> 

<script type="text/javascript">
    //<![CDATA[

    var IMAGES = ['sun', 'rain', 'snow', 'storm'];
    var ICONS = [];
    var map = null;
    var mgr = null;


    function init() {
        var myOptions = {
            zoom: 4,
            center: new google.maps.LatLng(48.25, 11.00),
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };
        map = new google.maps.Map(document.getElementById('map'), myOptions);

        var listener = google.maps.event.addListener(map, 'bounds_changed', function() {
            setupWeatherMarkers();
            google.maps.event.removeListener(listener);
        });
    }

    function getWeatherIcon() {
        var i = Math.floor(IMAGES.length * Math.random());
        if (!ICONS[i]) {
            var iconImage = new google.maps.MarkerImage('images/' + IMAGES[i] + '.png',
            new google.maps.Size(32, 32),
            new google.maps.Point(0, 0),
            new google.maps.Point(0, 32)
        );

            var iconShadow = new google.maps.MarkerImage('images/' + IMAGES[i] + '.png',
            new google.maps.Size(32, 59),
            new google.maps.Point(0, 0),
            new google.maps.Point(0, 32)
        );

            var iconShape = {
                coord: [1, 1, 1, 32, 32, 32, 32, 1],
                type: 'poly'
            };

            ICONS[i] = {
                icon: iconImage,
                shadow: iconShadow,
                shape: iconShape
            };

        }
        return ICONS[i];
    }

    function getRandomPoint() {
        var lat = 48.25 + (Math.random() - 0.5) * 14.5;
        var lng = 11.00 + (Math.random() - 0.5) * 36.0;
        return new google.maps.LatLng(Math.round(lat * 10) / 10, Math.round(lng * 10) / 10);
    }

    function getWeatherMarkers(n) {
        var batch = [];
        for (var i = 0; i < n; ++i) {
            var tmpIcon = getWeatherIcon();

            batch.push(new google.maps.Marker({
                position: getRandomPoint(),
                shadow: tmpIcon.shadow,
                icon: tmpIcon.icon,
                shape: tmpIcon.shape,
                title: 'Weather marker'
            })
        );
        }
        return batch;
    }

    function setupWeatherMarkers() {
        mgr = new MarkerManager(map);

        google.maps.event.addListener(mgr, 'loaded', function() {
            mgr.addMarkers(getWeatherMarkers(20), 3);
            mgr.addMarkers(getWeatherMarkers(200), 6);
            mgr.addMarkers(getWeatherMarkers(1000), 8);

            mgr.refresh();
        });
    }
    //]]>

    //window.onload = init;
    </script> 

<script type="text/javascript">

function init2() {
    handleResize();
    var mgrOptions = { borderPadding: 50, maxZoom: 15, trackMarkers: true };
    map = new google.maps.Map(document.getElementById("map"), mgrOptions);
//    map.addControl(new google.maps.SmallMapControl());
//    map.setCenter(new google.maps.LatLng(centerLatitude, centerLongitude), startZoom);
//    map.addControl(new google.maps.MapTypeControl());

    alert("--2");
    var listener = google.maps.event.addListener(map, 'bounds_changed', function() {
        alert("bound change");
        
        manager = new MarkerManager(map, mgrOptions);

        google.maps.event.addListener(manager, 'loaded', function() {

            alert("11 load");
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
    alert("init--");

    //    GEvent.addListener(map, 'click', function() {
    //        alert("check");
    //        alert("marker" + marker.labelText);

    //    });
}

function init3() {
        handleResize();
        var myOptions = {
            zoom: 4,
            center: new google.maps.LatLng(48.25, 11.00),
            mapTypeId: google.maps.MapTypeId.ROADMAP,
            borderPadding: 50, maxZoom: 15, trackMarkers: true
        };

//        var myOptions = { mapTypeId: google.maps.MapTypeId.ROADMAP };

        
        map = new google.maps.Map(document.getElementById('map'), myOptions);

        var listener = google.maps.event.addListener(map, 'bounds_changed', function() {
            setupWeatherMarkers();
            google.maps.event.removeListener(listener);
        });
    }


    window.onload = init3;
</script>

<script type="text/javascript">
    var markers = <%=  ViewData["Array"].ToString() %>
    var infoBubble;
</script>
<style>
html { overflow: hidden; }

html, body {
     margin: 0;
     padding: 0;
     height: 100%;
}

body { margin: 10px; }

#content {
     margin-top: 10px;
     position: relative;
}

#map {
     position: absolute;
     top: 0;
     left: 0;
     width: 100%;
     height: 100%;
     border: 1px solid #aaa;
}

#map-wrapper {
     position: relative;
     height: 100%;
}

#sidebar {
     position: absolute;
     border: 1px solid #aaa;
     top: 0;
     width: 220px;
     height: 100%;
     overflow: auto;
}

#sidebar p {
     margin: 10px;
}

#sidebar ul#sidebar-list {
     list-style: none;
     padding: 6px 0 0 0 ;
     margin: 0;
}

#sidebar ul#sidebar-list li {
	position: relative;
	padding: 2px 5px 2px 50px;
	clear:left;
}

#sidebar ul#sidebar-list li div.label {
	float: left;
	margin-left: -40px;
	width: 40px;
	position:relative;
	padding: 1px 3px 1px;
	background: red;
	color: white;
}

#sidebar ul#sidebar-list li a {
	font-family: Arial;
	font-size: 11px;
	color: #445555;
	text-decoration: none;
	padding: 2px 3px;
}


div.markerLabel {
	display: block;
	padding-top: 9px;
}

div.markerLabel,
#sidebar ul#sidebar-list li div.label {
	text-align: center;
	color: white;
	width: 52px;
	letter-spacing: 0px;
	font-size: 9px;
	font-family: Arial;
}


body.sidebar-right #map-wrapper { margin-right: 230px; }
body.sidebar-right #sidebar { right: 0; }

body.nosidebar #map-wrapper { margin: 0; }
body.nosidebar #sidebar { display: none; }

body.sidebar-right a#button-sidebar-show,
body.nosidebar a#button-sidebar-hide { display: none; }

#toolbar {
     background: white;
     padding: 9px;
     border: 1px solid black;
     position: relative;
}

/* holly hack for IE to get position:bottom right
   see: http://www.positioniseverything.net/abs_relbugs.html
 \*/
* html #toolbar { height: 1px; }
/* */

#toolbar h1 {
     margin: 0;
     font: bold 18px Helvetica, sans-serif;
}

#toolbar ul {
     list-style: none;
     padding: 0;
     margin: 0;
}

#toolbar ul#sidebar-controls li {
	position: absolute;
	right: 5px;
	bottom: 5px;
}

#toolbar ul li {
     display: inline;
}

#toolbar ul li a {
     padding: 3px 6px;
     color: #444;
     text-decoration: none;
     font-size: 12px;
}

#toolbar ul li a:hover {
     color: #aaa;
     background: #444;
}


</style>
   <%-- <div id="toolbar"> 
	</div> --%>
	<h2>Test2</h2>
    <div id="map" style="margin: 5px auto; width: 500px; height: 400px"></div> 
    <div style="text-align: center; font-size: large;"> 
      Random Weather Map
    </div> 
	
	<div></div>
</asp:Content>
