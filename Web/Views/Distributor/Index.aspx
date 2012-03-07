<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    


    <script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=false"></script> 
    <script type="text/javascript" src='<%=  Url.Content("~/js/jquery-1.4.2.min.js")%>' ></script> 
    <script type="text/javascript" src='<%=  Url.Content("~/js/jquery-ui-1.8.1.custom.min.js")%>' ></script> 
    <script src='<%=  Url.Content("~/Scripts/MicrosoftAjax.js")%>' type="text/javascript"></script>
    <script src='<%=  Url.Content("~/Scripts/MicrosoftMvcAjax.js")%>' type="text/javascript"></script>
    
    
    
    
    
    
<script type="text/javascript">
    function Done()
    {
        try {

            initialize($get('<%= UIDistributor.hidLatitude.ToString()%>').value, $get('<%= UIDistributor.hidLongitude.ToString()%>').value);
           
        }
        catch (err) {
            SetkAutocomplete();
            //Handle errors here
        }
        //loadmap();
    }
</script>

<script type="text/javascript">



    var geocoder = new google.maps.Geocoder();
    var map;
    var marker;

    function initialize(latitude, longitude) {


        //MAP
        var latlng = new google.maps.LatLng(latitude, longitude); //41.659, -4.714);
        var options = {
            zoom: 20,
            center: latlng,
            mapTypeId: google.maps.MapTypeId.SATELLITE
        };

        map = new google.maps.Map(document.getElementById("map_canvas"), options);

        //GEOCODER
        geocoder = new google.maps.Geocoder();



        var image = new google.maps.MarkerImage(
            '<%=  Url.Content("~/marker-images/image.png")%>',
            new google.maps.Size(48, 48),
            new google.maps.Point(0, 0),
            new google.maps.Point(0, 48)
          );

        var shadow = new google.maps.MarkerImage(
            '<%=  Url.Content("~/marker-images/shadow.png")%>',
            new google.maps.Size(76, 48),
            new google.maps.Point(0, 0),
            new google.maps.Point(0, 48)
          );

        var shape = {
            coord: [0, 0, 48, 48],
            type: 'rect'
        };


        marker = new google.maps.Marker({
            draggable: true,
            icon: image,
            shadow: shadow,
            shape: shape,
            map: map,
            position: latlng
        });




        //Add listener to marker for reverse geocoding
        google.maps.event.addListener(marker, 'drag', function() {
            geocoder.geocode({ 'latLng': marker.getPosition() }, function(results, status) {
                if (status == google.maps.GeocoderStatus.OK) {
                    if (results[0]) {
                        $('#address22').val(results[0].formatted_address);
                        $("#<%= UIDistributor.hidLatitude.ToString()%>").val(marker.getPosition().lat());
                        $("#<%= UIDistributor.hidLongitude.ToString()%>").val(marker.getPosition().lng());
                        var arr = results[0].address_components;
                        setValues(arr);

                    }
                }
            });
        });


        geocoder.geocode({ 'latLng': marker.getPosition() }, function(results, status) {

            if (status == google.maps.GeocoderStatus.OK) {
                if (results[0]) {
                    $('#address22').val(results[0].formatted_address);
                    $("#<%= UIDistributor.hidLatitude.ToString()%>").val(marker.getPosition().lat());
                    $("#<%= UIDistributor.hidLongitude.ToString()%>").val(marker.getPosition().lng());
                    var arr = results[0].address_components;
                    setValues(arr);
                }
            }
        });
    }


    function setValues( arr ) {
            
            var holder = "";
            var type = "";
            for (var i = 0; i < arr.length; i++) {

                type = parseArray(arr[i].types)
                holder += "  " + arr[i].long_name + " [ " + type + " ] ,";
                if (type.indexOf("route") != -1) {
                    $("#txtStreet_Name").val(arr[i].long_name);
                }

                if (type.indexOf("country") != -1) {
                    $("#txtCountry").val(arr[i].long_name);
                }

                if (type.indexOf("administrative_area_level_1") != -1) {
                    $("#txtState").val(arr[i].short_name);
                }

                if (type.indexOf("administrative_area_level_2") != -1) {
                    $("#txtCity").val(arr[i].long_name);
                }

                if (type.indexOf("postal_code") != -1) {
                    $("#txtZipCode").val(arr[i].long_name);
                }

                if (type.indexOf("street_number") != -1) {
                    $("#txtStreet_No").val(arr[i].long_name);
                }
            }

           
    }




    function parseArray(arr) {
        var holder = "";
        for (var i = 0; i < arr.length; i++) {
            holder += arr[i] + " & ";

        }
        return holder;
    }



    function SetkAutocomplete() {
        $(function() {
            $("#address22").autocomplete({
                //This bit uses the geocoder to fetch address values
                source: function(request, response) {
                    //alert( "_address" );
                    var _address = request.term + ('USA' ? ', ' + 'USA' : '');
                    //alert( _address );
                    geocoder.geocode({ 'address': _address }, function(results, status) {
                        response($.map(results, function(item) {
                            return {
                                label: item.formatted_address,
                                value: item.formatted_address,
                                latitude: item.geometry.location.lat(),
                                longitude: item.geometry.location.lng()
                            }
                        }));
                    })
                },
                //This bit is executed upon selection of an address
                select: function(event, ui) {
                    $("#<%= UIDistributor.hidLatitude.ToString()%>").val(ui.item.latitude);
                    $("#<%= UIDistributor.hidLongitude.ToString()%>").val(ui.item.longitude);
                    //var location = new google.maps.LatLng(ui.item.latitude, ui.item.longitude);
                    //marker.setPosition(location);
                    //map.setCenter(location);

                }
            });
        });






    }

    SetkAutocomplete();

    //    $(document).ready(function() {

    //        //initialize();
    //        SetkAutocomplete();

    //    });


    function Is_ConsumptionChange() {
        alert("chage");

    }

</script> 
<div id="pnlPersonal" style="text-align: left;vertical-align: top;" >
   <% Html.RenderPartial("Address");%>
</div>
</asp:Content>