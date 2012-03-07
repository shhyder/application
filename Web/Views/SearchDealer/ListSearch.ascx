<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.Collections.Generic"  %>
<%@ Import Namespace="System.Data"  %>
<%@ Import Namespace="System"  %>
<%@ Import Namespace="DataSet"  %>
<style type="text/css">
    .style1
    {
        width: 279px;
    }
    .style2
    {
        width: 82px;
    }
    .style3
    {
        width: 49px;
    }
    
    .header
    {
        font-family: Arial;
        font-size: x-large; 
        font-weight: bold; 
        font-style: normal; 
        color: #FFFFFF;
        background:#FFCC00;
        text-align:left;
    }
    
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
</style>
<script src='<%=  Url.Content("~/Scripts/MicrosoftAjax.js")%>' type="text/javascript"></script>
<script src='<%=  Url.Content("~/Scripts/MicrosoftMvcAjax.js")%>' type="text/javascript"></script>
<div id="divLoading"></div>
<%
    int grid_No = 1;
    string contentStyle = "";
%>
<% using (Ajax.BeginForm("List", "SearchDealer", new AjaxOptions { UpdateTargetId = "mainSearchPanel", InsertionMode = InsertionMode.Replace }, new { autocomplete = "off" }))
     { %>

    <table id = "toolTip_Container"  style="border-style: none; width: 76%;">
   <tr>
    <td >
    
    <table style="height: 118px; width: 101%" >
      
        
    <% string style = "background:White";%>
    <% string querystring = ""; %>
    <% int sr_No = Convert.ToInt32( ViewData["start"] ) ;  %>
    <tr>
        <td class="style3 header"><b>Sr. No.</b></td>
        <td class="header"><b>Distributor</b></td>
        <td class="style1 header" ><b>Address</b></td>
        <td class="style2 header" ><b>Distance</b></td>
        <td class="header"><b></b></td>
    </tr>
    
    
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
               querystring = "strLat=0&strLog=0&endLat=" + consumer["Latitude"].ToString() + "&endLog=" + consumer["Longitude"].ToString() + "&id=" + consumer["Distributor_ID"].ToString() + "&dist=12000" + ViewData["queryString"].ToString();
               querystring = HttpUtility.UrlEncode(querystring); 
            %>
            <tr>
                <td class="style3 <%= contentStyle %>"><%= sr_No.ToString()%></td><% sr_No++; %>
                <td class="<%= contentStyle %>"><%= consumer["Distributor"].ToString() %></td>
                <td class="style1 <%= contentStyle %>"><%= consumer["Address"].ToString() %></td>
                <td class="style2 <%= contentStyle %>"><%=  Convert.ToInt32( consumer["distance"] ).ToString()  + " miles"%></td>
                <td class="<%= contentStyle %>"><a target="_blank" href="<%=  Url.Content("~/SearchDealer/Route?" +  querystring)%>">Detail</a></td>
            </tr>
    <% } %>
     <%if (Convert.ToInt16(ViewData["Count"]) != 0)
    {%>
    <tr>
         <td colspan="5" class="footer" >
                <% if ((string)ViewData["pageLinks"] != "" && Convert.ToInt16(ViewData["Count"]) > 0)
                   { %>
           <%= ViewData["pageLinks"]%>
           
        <% } %>
         </td>
    </tr>
    <%} %>
    
    <%if (Convert.ToInt16(ViewData["Count"]) == 0)
    {%>
        <tr>
            <td colspan="5" class="gridAlternateContentStyle" >
            <p style="text-align:center; font-size:x-large">No dealer found</p>
            </td>
        </tr>
    <%} %>
    </table>
    </td>
    </tr>
    </table>  
    <%}%>