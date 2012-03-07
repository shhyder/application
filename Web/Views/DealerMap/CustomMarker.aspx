<%@ Page Title="" Language="C#"  Inherits="System.Web.Mvc.ViewPage" %>
    <h2>CustomMarker</h2>
    <html>
<head>
<title>Google Maps JavaScript API v3: Custom Marker Demo</title>
<script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=false"></script>
<script src='<%=  Url.Content("~/MapJS/markermanager.js")%>' type="text/javascript"></script> 
<script type="text/javascript" src="<%=  Url.Content("~/MapJS/infobubble.js")%>"></script> 
<script type="text/javascript" src='<%=  Url.Content("~/js/jquery-1.4.2.min.js")%>' ></script> 
<script type="text/javascript" src='<%=  Url.Content("~/js/jquery-ui-1.8.1.custom.min.js")%>' ></script>
<script src='<%=  Url.Content("~/Scripts/MicrosoftAjax.js")%>' type="text/javascript"></script>
<script src='<%=  Url.Content("~/Scripts/MicrosoftMvcAjax.js")%>' type="text/javascript"></script>
<script type="text/javascript">
  var markers = <%=  ViewData["Array"].ToString() %>
  var map,infoBubble;
  var overlay;
  var centerLatitude = 40, centerLongitude = -98, startZoom = 4;
  
  function initialize() {
    var opts = {
      zoom: startZoom,
      center: new google.maps.LatLng(centerLatitude, centerLongitude),
      mapTypeId: google.maps.MapTypeId.ROADMAP
    }

    map = new google.maps.Map(document.getElementById("map"), opts);
    var listener = google.maps.event.addListener(map, 'bounds_changed', function() {
        manager = new MarkerManager(map, opts);
        google.maps.event.addListener(manager, 'loaded', function() {

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
  }
  
  function createMarker(pointData) {
    var marker = new google.maps.Marker({  
      position: new google.maps.LatLng(pointData.latitude, pointData.longitude),  
      map: map,  
      title: pointData.title,  
      clickable: true,  
      icon: 'http://google-maps-icons.googlecode.com/files/factory.png'  
    }); 

  google.maps.event.addListener(marker, "click", function() {
    
    if( infoBubble != null )
        {
            infoBubble.close();
            infoBubble = null;
            infoBubble2 = null;
        }
    
    infoBubble = new InfoBubble({
          maxWidth: 300
        });
 
        infoBubble2 = new InfoBubble({
          map: map,content: '<div class="phoneytext">Some label</div>',position: new google.maps.LatLng(-35, 151),
          shadowStyle: 1,padding: 0,backgroundColor: 'rgb(57,57,57)',borderRadius: 4,
          arrowSize: 80,borderWidth: 1,borderColor: '#2c2c2c',disableAutoPan: true,
          hideCloseButton: true,arrowPosition: 50,backgroundClassName: 'phoney',arrowStyle: 2,minWidth: 100,
          minHeight: 300
        });
 
        infoBubble.setMinHeight(200);
        infoBubble.setMinWidth(400);
        infoBubble.setMaxHeight(200);
        infoBubble.setMaxWidth(400);
 
        infoBubble.open(map, marker);
        infoBubble2.open();
        
         // profile div   
        var div = document.createElement('DIV');
        div.id = 'contentPanel';
        div.innerHTML = pointData.state;
        infoBubble.addTab('Profile', SetProfile(pointData));
     
         // Dealers div
        var div2 = document.createElement('DIV');
        div2.id = 'contentPanel';
        div2.innerHTML = pointData.state;
        infoBubble.addTab('Dealers', div2);
        
         // Cities div
        div3 = document.createElement('DIV');
        div3.id = 'contentPanel';
        div3.innerHTML = pointData.state;
        infoBubble.addTab('Cities', div3);
        
        var qstr = "state=" + pointData.abbr;
        GetDealerPage(1,qstr);
        
         str = "<%= Web.Model.Utility.Get_Path() %>/SearchProduct/List/";
        jQuery.ajax({type: "Get",url:str ,success: function(result) {
                 if (result.isOk == false) {
                     infoBubble.tabs_[2].content.innerHTML = result.message;
                 }
                 else {
                     infoBubble.tabs_[2].content.innerHTML = result;
                 }
             },async: true
         });
  });
  return marker;
}

function UpdateDealerList(data) {
        infoBubble.tabs_[1].content.innerHTML = data;
}

function SetProfile(pointData)
{
    var str = '';
    str = "<span class='text-main-products'><div><fieldset><legend><b>" + pointData.state + "</b></legend>" +
         "<div class='editor-label'><b>Region : </b>" + pointData.region  + "</div>" +
         "<div class='editor-label'><b>Total Cities : </b>" + pointData.totalCities  + "</div>" +
         "<div class='editor-label'><b>Jatai Cities : </b>" + pointData.totalJatiaCities  + "</div>" +
         "<div class='editor-label'><b>Total Outlets : </b>" + pointData.totalOutlets  + "</div>" +
         "<div class='editor-label'><b>Total Jatai Dealers : </b>" + pointData.totalDealers + "</div>" +
         "</fieldset></div></span>";
    return str;
}

function GetDealerPage(page,queryString)
{
     str = "<%= Web.Model.Utility.Get_Path() %>/SearchDealer/ListPopup/" + page ;
     jQuery.post(str, queryString, UpdateDealerList);
}
</script>
</head>
<body onload="initialize()">
  <div id="map" style="width: 820px; height: 680px;">map div</div>
  <div>
      <input type="button" value="Add Marker" onclick="addOverlay()">
      <input type="button" value="Remove Marker" onclick="removeOverlay()">
  </div>

</body>
</html>
    
    
    
    
    
    
    
    


