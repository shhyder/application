<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>


<%@ Import Namespace="System.Collections.Generic"  %>
<%@ Import Namespace="System.Data"  %>
<%@ Import Namespace="DataSet"  %>
<%@ Import Namespace="System.Net"  %>
<%@ Import Namespace="System.Xml"  %>
<%
    string heading = "";
    DSParameter ds2 = (DSParameter)System.Web.HttpContext.Current.Cache["data"];
    DataRow[] rows = ds2.Product_Type.Select(" Product_Type_ID < 20"); // 
    ViewData["ZipCode"] = "";
%>
<script src='<%=  Url.Content("~/Scripts/MicrosoftAjax.js")%>' type="text/javascript"></script>
<script src='<%=  Url.Content("~/Scripts/MicrosoftMvcAjax.js")%>' type="text/javascript"></script>
<link href='<%=  Url.Content("~/Content/Search.css")%>' rel="stylesheet" type="text/css"/>
<script src='<%=  Url.Content("~/MapJS/Search.js")%>' type="text/javascript"></script>
<script src='<%=  Url.Content("~/MapJS/Event.js")%>' type="text/javascript"></script>
<script src='<%=  Url.Content("~/MapJS/Route.js")%>' type="text/javascript"></script>
<script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=false"></script>
<script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?libraries=places&sensor=false"></script>
     
 
     
     
<form id="searchForm" name="searchForm"   >
<table width="600px" >
    <tr>
        <td class="textBlack">
        <b >Find stores located</b>
        <select id="cobDistance" name="cobDistance" onchange="javascript:onEventSearchClick();">
                        <option value="25">25 miles</option><option value="50">50 miles</option><option value="75">75 miles</option><option value="100">100 miles</option><option value="250" selected="">250 miles</option><option value="500">500 miles</option><option value="12000">any distance</option>
                    </select>
                    from ZIP&nbsp;<input type="text" name="txtzipcode" id="txtzipcode" maxlength="5" onblur="javascript:onEventSearchClick();" onkeypress="javascript:isNumberKey(event);" size="8" onkeyup="if ((isNumberKey(event)) &amp;&amp; (this.value.length==5)) { this.blur(); }" value="<%= ViewData["ZipCode"].ToString().Trim()  %>">
                    <select id="cobPeriod" name="cobPeriod" onchange="javascript:pressEvent();">
                        <option value="1">Up-comming Events</option><option value="-1">Past Events</option><option value="0">All Events</option>
                    </select>
                    <%--<input id="Search" type="button" value="Search" class="gradbutton" onclick="javascript:onSearchClick();" > </input>--%>
                    <a class="gradbutton" onclick="javascript:onEventSearchClick();">Search</a>
                     <input id="initial-zip" name="initial-zip" type="hidden" id="initial-zip" value="<%= ViewData["ZipCode"].ToString()  %>"/>
	                 <input id="initial-isoas" name="initial-isoas" type="hidden" id="initial-isoas" value="false"/>
	                 <input id="last-valid-zip" name="last-valid-zip" type="hidden" id="last-valid-zip" value="<%= ViewData["ZipCode"].ToString()  %>"/>					
	                 <span id="resultsProductsAndDealers" valign="top"  style="height:23px;" class="header" >
                     &nbsp;
                    </span>
	     </td>
    </tr>
</table>


		
<div id="content1">
                    <div id="content">
                     <div id="slider">
                    
                       
                    </div>
			        </div>
            </div>

<div id="mapPanel" style="display:none;text-align:right" >
<table border="0" width="600px">
    <tr>
        <td class="header"  valign="top">
       <b style="font-size:medium">Local Stores</b> 
       </td> 
       <td style="width:280px" colspan="3" align="right">
            <span class="editor-label">Results per page: </span>  <select id="cobRows" name="cobRows" onchange="javascript:rowsEventSizeChange();">
                        <option value="5">5</option><option value="10">10</option><option value="25">25</option><option value="50">50</option></select>
         </td>
    </tr>
    
    <tr >
        <td class="header" id="ListHeader" colspan="4" valign="top"></td>
    </tr>

    <tr>
        <td id="dealerList" valign="top" style="width:350px;"></td>
        <td colspan="2" valign="top">
             <div id="map-pane" style="position: relative;width: 250px; height: 280px" >
                    <div id="map-loading" class="loading"><img  src='<%=  Url.Content("~/images/ajax-loader.gif")%>' width="32" height="32" /><strong>Loading</strong></div>
                
                    <div id="map" style="margin: 5px auto; width: 250px; height: 280px"></div> 
                </div>
                <div id="Navigator" class="footer" style="text-align: right;padding:  0.5em  0.5em 0.5em  0.5em"></div>
        </td>
    </tr>
    <tr>
        
    </tr>
    
    
   
</table>


 
</div>

</form>    










<%--


<table>
<tr>
<td>
    <form id="searchForm" name="searchForm"   >
     <select id="cobDistance" name="cobDistance" onchange="javascript:pressEvent();">
                        <option value="25">25 miles</option><option value="50">50 miles</option><option value="75">75 miles</option><option value="100">100 miles</option><option value="250" selected="">250 miles</option><option value="500">500 miles</option><option value="12000">any distance</option>
                    </select>
                    from ZIP&nbsp;
                    <span id="ctl01_ucLocation_upZip">
	                    <input type="text" name="txtzipcode" id="txtzipcode" maxlength="5" onblur="javascript:pressEvent();" onkeypress="javascript:isNumberKey(event);" size="8" onkeyup="if ((isNumberKey(event)) &amp;&amp; (this.value.length==5)) { this.blur(); }" value="<%= ViewData["ZipCode"].ToString().Trim()  %>">
                    </span>
                    
                    <select id="cobPeriod" name="cobPeriod" onchange="javascript:pressEvent();">
                        <option value="1">Up-comming Events</option><option value="-1">Past Events</option><option value="0">All Events</option>
                    </select>
                    
                            <br />
                    			&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type=button value="Search" onclick="javascript:pressEvent();" />					
	                    </div>
							                    <div id="ctl01_upError" class="error">
                    			                    &nbsp;


<div id="page" style=" font-family:Arial, Helvetica, sans-serif;">


		
            <div id="content">
                    
            </div>
        </form>
   
   
   
   
</div>
</td>
</tr>
</table>
   <div id="map-pane" style="position: relative;" >
                    <div id="map-loading" class="loading"><img  src='<%=  Url.Content("~/images/ajax-loader.gif")%>' width="32" height="32" /><strong>Loading</strong></div>
  
<table>
            
    <tr>
        <td valign="top" style="width:370px;"><div id="dealerList" ></div></td>
        <td valign= "bottom" >
                
                   
                    <div id="map" style="margin: 5px auto; width: 450px; height: 480px"></div> 
                
        </td>
    </tr>
   
</table>
 </div>
 
 
 --%>
 
 
 
 <script type="text/javascript">

     function OnSearchFromList() {
         Action([6, 5]);
     }

     function OnSearchFromDirection() {
         Action([11, 5]);
     }


     function OnBackToListFromDirection() {
         Action([11, 4]);
     }

     function GetEventList(page, queryString, is_From_Navigation) {
         document.getElementById('resultsProductsAndDealers').innerHTML = '<div id="loadingUpper" ><img  src="' + pfxURL.staticVar + "/images/ajax-loader_Upper.gif" + '"  /><strong>Loading</strong></div>';

         jQuery.ajax({
             url: "<%= Web.Model.Utility.Get_Path() %>/Event/GetEvent/" + page,
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
                     
                     //jQuery("#pnlFacebook").html(markers[0].facebook);
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
     
     
     
</script>


