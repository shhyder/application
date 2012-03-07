<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.Collections.Generic"  %>
<%@ Import Namespace="System.Data"  %>
<%@ Import Namespace="DataSet"  %>
<%@ Import Namespace="System.Net"  %>
<%@ Import Namespace="System.Xml"  %>
<%
    string heading = "";
    DSParameter ds2 = (DSParameter)System.Web.HttpContext.Current.Cache["data"];
    DataRow[] rows = ds2.Product_Type.Select(" Is_Active = true ");  
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
<table width="800px" >
    <tr>
        <td class="textBlack">
        <b >Find stores located</b>
        <select id="cobDistance" name="cobDistance" onchange="javascript:onSearchClick();">
                        <option value="5">5 miles</option><option value="10">10 miles</option><option value="25">25 miles</option><option value="50">50 miles</option><option value="75">75 miles</option><option value="100">100 miles</option><option value="250" selected="">250 miles</option><option value="500">500 miles</option><option value="12000">any distance</option>
                    </select>
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
                    <div id="slider">
                    
                         <% foreach (DataRow row  in rows)
                         {
                             vdd[UISearch.hidProduct_Type_ID.ToString()] = row["Product_Type_ID"];
                             Html.RenderPartial("Critaria", vdd);
                                
                        } %>
                    </div>
			        </div>
            </div>

<div id="mapPanel" style="display:none;text-align:right" >
<table border="0" width="800px">
    <tr>
        <td class="header"  valign="top">
       <b style="font-size:medium">Local Stores</b> 
       </td> 
       <td style="width:280px" colspan="3" align="right">
            <span class="editor-label">Results per page: </span>  <select id="cobRows" name="cobRows" onchange="javascript:rowsSizeChange();">
                        <option value="5">5</option><option value="10">10</option><option value="25">25</option><option value="50">50</option></select>
         </td>
    </tr>
    
    <tr >
        <td class="header" id="ListHeader" colspan="4" valign="top"></td>
    </tr>

    <tr>
        <td id="dealerList" valign="top" style="width:350px;"></td>
        <td colspan="2" valign="top">
             <div id="map-pane" style="position: relative;width: 450px; height: 480px" >
                    <div id="map-loading" class="loading"><img  src='<%=  Url.Content("~/images/ajax-loader.gif")%>' width="32" height="32" /><strong>Loading</strong></div>
                
                    <div id="map" style="width: 450px; height: 480px"></div> 
                </div>
                <div id="Navigator" class="footer" style="text-align: right;padding:  0.5em  0.5em 0.5em  0.5em"></div>
        </td>
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




jQuery(document).ready(function() {
    jQuery('input').checkBox();

});


</script>