<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/App.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=false"></script>
     <script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?libraries=places&sensor=false"></script>
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    
    <script src='<%=  Url.Content("~/MapJS/Store.js")%>' type="text/javascript"></script>
    <link id="Link1" href='<%=  Url.Content("~/Pro_files/jquery-ui.css")%>' rel="stylesheet" type="text/css"/>
    <script src='<%=  Url.Content("~/Pro_files/jquery.js")%>' type="text/javascript"></script>
    <script src='<%=  Url.Content("~/Pro_files/jquery-ui.js")%>' type="text/javascript"></script>
    <script src='<%=  Url.Content("~/Pro_files/jquery_002.js")%>' type="text/javascript"></script>
    <script src='<%=  Url.Content("~/Pro_files/Awesome.js")%>' type="text/javascript"></script>
    <script src='<%=  Url.Content("~/Pro_files/style.js")%>' type="text/javascript"></script>
    
 
    


<table width="100%">
        <td align="left">
            &nbsp;<a onclick="javascript:AddLoading(this);" href="<%=  Url.Content("~/Product/Index")%>">Product Search</a>
            &nbsp;&nbsp;<a onclick="javascript:AddLoading(this);" href="<%=  Url.Content("~/Store/Index")%>">Store Search</a> 
        </td>
        <td align="right">
            <% Html.RenderPartial("LogOnUserControl");%>
        </td>
</table>
<h2>Visitor overview</h2>
<p>
 <div id='VisitorsOverview' style='width:100%; height:250px;clear:both;'></div>
 </p>
 
 
 <h2>Map Visit</h2>
 <p>
 <div id='WorldMap' style='width:100%; height:350px;clear:both;'></div>
 </p>
 <h2>Traffic source</h2>
 <p>
 <div id='TrafficSourcesOverview' style='width:100%; height:250px;clear:both;'></div>
 </p>
 <%=  ViewData["VisitorsOverview"]%>
 <%=  ViewData["TrafficSourcesOverview"]%>
 <%=  ViewData["WorldMap"]%>
 <%=  ViewData["ContentOverview"]%>
 
 
 
 
 	
 
 
</asp:Content>
