<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.Collections.Generic"  %>
<%@ Import Namespace="System.Data"  %>
<%@ Import Namespace="System"  %>
<%@ Import Namespace="DataSet"  %>
<script src='<%=  Url.Content("~/Scripts/MicrosoftAjax.js")%>' type="text/javascript"></script>
<script src='<%=  Url.Content("~/Scripts/MicrosoftMvcAjax.js")%>' type="text/javascript"></script>
<style type="text/css">
    .header
    {
        font-family: Arial;
        font-size: x-small; 
        font-weight: normal; 
        font-style: normal; 
        color: #FFFFFF;
        background:#FFCC00;
        text-align:left;
    }
    
    .gridContentStyle
    {
        font-family: Arial;
        font-size: x-small; 
        font-weight: normal; 
        font-style: normal; 
        color: #FFFFFF;
        background:#FFCC00;
        text-align:left;
    }
    
    .gridAlternateContentStyle
    {
        font-family: Arial;
        font-size: x-small; 
        font-weight: normal; 
        font-style: normal; 
        color: Gray;
        background:#FFFFFF;
        text-align:left;
    }
    
    .footer
    {
        font-family: Arial;
        font-size: small; 
        font-weight: normal; 
        font-style: normal; 
        color: #FFFFFF;
        background:#FFCC00;
        text-align:right;
    }
    .style1
    {
        width: 164px;
    }
    .style2
    {
        width: 161px;
    }
</style>
<div id="divLoading"></div>
<%
    int grid_No = 1;
    string contentStyle = "";
%>
<div id='contentPanel'>

<% using (Ajax.BeginForm("List", "SearchDealer", new AjaxOptions { UpdateTargetId = "contentPanel", InsertionMode = InsertionMode.Replace }, new { autocomplete = "off" }))
     { %>
    <table id = "toolTip_Container"  style="border-style: none; width: 100%;">
   <tr>
    <td >
    <table >
    <tr>
        <td class="style1 header">Dealer</td>
        <td class="style2 header">Address</td>
        <td class="header"></td>
    </tr>
    <% string style = "background:White";%>
    <% string querystring = ""; %>
    <% foreach (var consumer in (ViewData[UIDealerSearch.listDealer.ToString()] as List<DataRow>))
       { %>
            <% if (grid_No % 2 == 0)
               {
                   contentStyle = "gridContentStyle";
               }
               else
               {
                   contentStyle = "gridAlternateContentStyle";
               }
               grid_No++;
               querystring = "strLat=0&strLog=0&endLat=" + consumer["Latitude"].ToString() + "&endLog=" + consumer["Longitude"].ToString() + "&id=" + consumer["Distributor_ID"].ToString() + "&dist=12000";
               querystring = HttpUtility.UrlEncode(querystring); 
            %>
            <tr>
               <td class="style1 <%= contentStyle %>"><%= consumer["Distributor"].ToString()%></td>
               <td title='<%= consumer["Address"].ToString() %>' class="style2 <%= contentStyle %>" ><%= consumer["Address"].ToString().Length > 40 ? consumer["Address"].ToString().Trim().Remove(40) + "..." : consumer["Address"].ToString()%></td>
               <td class="<%= contentStyle %>"><a target="_blank" href="<%=  Url.Content("~/SearchDealer/Route?" +  querystring)%>">Show Map</a></td>
            </tr>
    <% } %>
    <tr>
            <td colspan="3" class="footer" >
                <br />
                <% if ((string)ViewData["pageLinks"] != "" && Convert.ToInt16(ViewData["Count"]) > 0)
                   { %>
           <%= ViewData["pageLinks"]%>
           <br /><br />
        <% } %>
            </td>
       </tr>
    </table>
    <%if (Convert.ToInt16(ViewData["Count"]) == 0)
    {%>
        <p class="gridContentStyle">No dealer found</p>
    <%} %>
    </td>
    </tr>
    </table>  
    <%}%>
</div>
