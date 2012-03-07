<%@ Page Title="" MasterPageFile="~/Views/Shared/Site.Master" Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="System.Collections.Generic"  %>
<%@ Import Namespace="System.Data"  %>
<%@ Import Namespace="System"  %>
<%@ Import Namespace="DataSet"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%
     DSParameter ds = (DSParameter)System.Web.HttpContext.Current.Cache["data"];
     string querystring = ""; 
    %>

    <p>
        <%= Html.ActionLink("Search", "Index", "Search")%>
    </p>
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN"
  "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
    
    <html>
<head>
<meta name="viewport" content="initial-scale=1.0, user-scalable=no" />
<script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=false"></script>
<script type="text/javascript">
    var directionsDisplay;
    var directionsService = new google.maps.DirectionsService();
    var map;
    var oldDirections = [];
    var currentDirections = null;

    function initialize() {
        var myOptions = {
            zoom: 13,
            center: new google.maps.LatLng('<%=  ViewData["startLatitude"].ToString() %>', '<%= ViewData["startLongitude"].ToString() %>'),
            mapTypeId: google.maps.MapTypeId.ROADMAP
        }
        map = new google.maps.Map(document.getElementById("map_canvas"), myOptions);

        directionsDisplay = new google.maps.DirectionsRenderer({
            'map': map,
            'preserveViewport': true,
            'draggable': false
        });
        directionsDisplay.setPanel(document.getElementById("directions_panel"));

        google.maps.event.addListener(directionsDisplay, 'directions_changed',
      function() {
          if (currentDirections) {
              oldDirections.push(currentDirections);

          }
          currentDirections = directionsDisplay.getDirections();
      });



        calcRoute();
    }

    function calcRoute() {
        
        var start = new google.maps.LatLng('<%=  ViewData["startLatitude"].ToString() %>', '<%= ViewData["startLongitude"].ToString() %>');
        var end = new google.maps.LatLng('<%=  ViewData["endLatitude"].ToString() %>', '<%= ViewData["endLongitude"].ToString() %>');


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
    }
</script>
</head>
<body onload="initialize()">
    <table style="width: 100%" onload="initialize()">
    <tr>
        <td >
                    
                        <fieldset>
                            <% if (Session["QSCurDL"] != null)
                               {%>
                            <a href="<%=  Url.Content("~/SearchDealer/Index?" +  Session["QSCurDL"].ToString())%>">
                                Back</a>
                            <%}%>
                            <legend><b>
                                <%= ViewData["Distributor"].ToString()%></b></legend>
                            <div >
                                <b>Address : </b>
                                <%= ViewData["Address"].ToString() %>
                            </div>
                            <div >
                                <b>Phone1 : </b>
                                <%= ViewData["Phone1"].ToString()%>
                            </div>
                            <div >
                                <b>Phone2 : </b>
                                <%= ViewData["Phone2"].ToString()%>
                            </div>
                           
                            <div >
                                <b>Email : </b>
                                <%= ViewData["Email"]%>
                            </div>
                            <div >
                                <b>Website : </b>
                                <%= ViewData["Website"]%>
                            </div>
                            
                      
                        <div style="font-family:Arial, Helvetica, sans-serif; font-size: x-small; color:#F60;" >
                            <b>Available products : </b>
                            <% 
                                //int distributor_ID = Convert.ToInt32(ViewData["Distributor_ID"].ToString());
                                //DataRow[] rows = ds.Product_Distributor.Select(" Distributor_ID = " + distributor_ID.ToString() + " and Product_ID in ( " + ViewData["product_IDs"].ToString()  + ")" );
                                DataRow[] rows = ds.Product_Distributor.Select(" Distributor_ID = " + ViewData["Distributor_ID"].ToString());
                               
                                string _avl_Products = "";
                                foreach (DataRow row in rows)
                                {
                                    //_avl_Products += ds.Product.FindByProduct_ID(Convert.ToInt32(row["Product_ID"] )).Product + ", ";

                                    //if (ds.Product_Distributor.Select(" Distributor_ID = " + distributor_ID.ToString() + " and Product_ID in ( " + ViewData["product_IDs"].ToString() + ") and Product_ID = " + row["Product_ID"].ToString()).Length == 0)
                                    //{
                                        _avl_Products += ds.Product.FindByProduct_ID(Convert.ToInt32(row["Product_ID"])).Product + ", ";
                                    //}
                                    //else
                                    //{
                                    //    _avl_Products += "<b>" + ds.Product.FindByProduct_ID(Convert.ToInt32(row["Product_ID"])).Product + "</b>, ";
                                    //}
                                    
                                }
                            %>
                                <%= _avl_Products %>
                        </div>
                            
                        </fieldset>
                    
        </td>
    </tr>
  </table>


   
  <div style="position:relative; border: 1px; width: 100%; height: 400px;">
    <div id="map_canvas" style="border: 1px solid black; position:absolute; width:60%; height:398px"></div>
    <div id="directions_panel" style="position:absolute; left: 61%; width:40%; height:400px; overflow: auto"></div>

  </div>

</body>
</html>
<script type="text/javascript">
    initialize();
 </script>
    
    
    
    
    
    
    
    
</asp:Content>
