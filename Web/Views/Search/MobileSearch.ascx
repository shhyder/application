<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<%@ Import Namespace="System.Collections.Generic"  %>
<%@ Import Namespace="System.Data"  %>
<%@ Import Namespace="DataSet"  %>
<%@ Import Namespace="System.Net"  %>
<%@ Import Namespace="System.Xml"  %>
<%
    string heading = "";
    DSParameter ds2 = (DSParameter)System.Web.HttpContext.Current.Cache["data"];
    DataRow[] rows = ds2.Product_Type.Select(" Product_Type_ID <= 20"); // 
    ViewData["ZipCode"] = "";
    
    
%>
<script src='<%=  Url.Content("~/Scripts/MicrosoftAjax.js")%>' type="text/javascript"></script>
<script src='<%=  Url.Content("~/Scripts/MicrosoftMvcAjax.js")%>' type="text/javascript"></script>
<link href='<%=  Url.Content("~/Content/Search.css")%>' rel="stylesheet" type="text/css"/>
<script src='<%=  Url.Content("~/MapJS/Search.js")%>' type="text/javascript"></script>
<script src='<%=  Url.Content("~/js/jquery.ui.widget.js")%>' type="text/javascript"></script>
<script src='<%=  Url.Content("~/js/ui.checkbox.js")%>' type="text/javascript"></script>
<script src='<%=  Url.Content("~/js/jquery.usermode.js")%>' type="text/javascript"></script>


<script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=false"></script>
<script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?libraries=places&sensor=false"></script>




        <script type="text/javascript">
            $(function() {
                $('input').checkBox();
            });
        </script>
<form id="searchForm" name="searchForm"   >
<table width="100%" >
    <tr style="width:100%;">
        <td class="textBlack" style="width:100%;">
        <b >Find stores located</b>
        <select id="cobDistance" name="cobDistance" onchange="javascript:onSearchClick();">
                        <option value="5">5 miles</option><option value="10">10 miles</option><option value="25">25 miles</option><option value="50">50 miles</option><option value="75">75 miles</option><option value="100">100 miles</option><option value="250" selected="">250 miles</option><option value="500">500 miles</option><option value="12000">any distance</option>
                    </select>
        </td>
        </tr>
   <tr>
        <td>
                    from ZIP&nbsp;<input type="text" name="txtzipcode" id="txtzipcode" maxlength="5"  onkeypress="javascript:isNumberKey(event);" size="8" onkeyup="if ((isNumberKey(event)) &amp;&amp; (this.value.length==5)) { this.blur(); }" value="<%= ViewData["ZipCode"].ToString().Trim()  %>">
                    <a class="gradbutton" onclick="javascript:onSearchClick();">Search</a>
                     <input id="initial-zip" name="initial-zip" type="hidden" id="initial-zip" value="<%= ViewData["ZipCode"].ToString()  %>"/>
	                 <input id="initial-isoas" name="initial-isoas" type="hidden" id="initial-isoas" value="false"/>
	                 <input id="last-valid-zip" name="last-valid-zip" type="hidden" id="last-valid-zip" value="<%= ViewData["ZipCode"].ToString()  %>"/>					
	                 <a class="gradbutton" href="javascript:resetSearch();">Reset</a>
	                 <span id="resultsProductsAndDealers" valign="top"  style="height:23px;" >
                     &nbsp;
                    </span>
	     </td>
    </tr>
</table>
<div id="content1">
                    <div id="content">
                   <div>
                    <input name='is_Auto_Search_Enabled'  id='is_Auto_Search_Enabled'  type="checkbox"/>
                    <label>Enable Autosearch</label>
                    </div>
	                 <% ViewDataDictionary vdd = new ViewDataDictionary();%>				
                    <div id="slider" style="width:100%;">
                    
                         <% foreach (DataRow row  in rows)
                         {
                             vdd[UISearch.hidProduct_Type_ID.ToString()] = row["Product_Type_ID"];
                             Html.RenderPartial("MobileCritaria", vdd);
                                
                        } %>
                    </div>
			        </div>
            </div>

<div id="mapPanel" width="100%" style="display:none;text-align:right" >
<table border="0" width="100%">
    <tr>
        <td class="header"  valign="top">
       <b style="font-size:medium">Local Stores</b> 
       </td> 
       <td  colspan="3" align="right">
            <span class="editor-label">Results per page: </span>  <select id="cobRows" name="cobRows" onchange="javascript:rowsSizeChange();">
                        <option value="5">5</option><option value="10">10</option><option value="25">25</option><option value="50">50</option></select>
         </td>
    </tr>
    
    <tr >
        <td class="header" id="ListHeader" colspan="4" valign="top"></td>
    </tr>
    
    <tr >
        <td colspan="4">
                    <div id="map-pane" style="position: relative;width: 100%; height: 280px" >
                    <div id="map-loading" class="loading"><img  src='<%=  Url.Content("~/images/ajax-loader.gif")%>' width="32" height="32" /><strong>Loading</strong></div>
                
                    <div id="map" style="margin: 5px auto; width:100%; height: 280px"></div> 
                </div>
                <div id="Navigator" class="footer" style="width: 100%;text-align: right;padding:  0.5em  0.5em 0.5em  0.5em"></div>
        
        </td>
    </tr>

    <tr >
        <td id="dealerList" colspan="3" valign="top" style="width:100%;"></td>
    </tr>
    <tr>
        
    </tr>
    
    
   
</table>


 
</div>

</form>    

<script>
    jQuery(".cbs input, [checkbox]").click(function(event) {
    checkBoxClicked();
});



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


</script>