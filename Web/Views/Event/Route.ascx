<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
 <%if (ViewData["Is_Direct_Link"] == null )
{%>
    <span>
        <a href="javascript:OnBackToListFromDirection()" >Back to List</a>
        <a href="javascript:OnChangeAddress()" >Change Address</a>
    </span>
<%} %>    
    <div style="width:600px" id="detail">
    
    </div>
   
  
  <table id="specificAddress" class="editor-label">
        <tr><td colspan="2">&nbsp;</td></tr>
        <tr>
            <td>Starting Address </td>
            <td><input id="address" type="text" size="70" placeholder="Type in your starting location and select your location from the dropdown list" /></td>
        </tr>
        <tr>
            <td></td>
            <td><span class="editor-field">e.g 6980 Hermosa Circle, Buena Park, CA 90620</span></td>
        </tr>
         <input id="startLatitude" name="startLatitude" type="hidden" value="" />
        <input id="startLongitude" name="startLongitude" type="hidden" value="" />
        <input id="endLatitude" name="endLatitude" type="hidden" value="" />
        <input id="endLongitude" name="endLongitude" type="hidden" value="" />
        <input id="hidAddress" name="hidAddress" type="hidden" value="" />
  </table>
<div style=" display:none; position:relative; border: 1px; width: 600px; height: 400px;" id="directionMap">
    <div id="map_Direction_Canvas" style="border: 1px solid black; position:absolute; width:60%; height:398px"></div>
    <div id="directions_panel" style="position:absolute; left: 61%; width:40%; height:400px; overflow: auto" class="editor-field"></div>
</div>
 <%if (ViewData["Is_Direct_Link"] == null )
{%>
    <div id="fb-root" style="text-align: left;vertical-align: top;font-family: Arial,Helvetica,sans-serif;" ></div>    
<%} %>
  
   
 