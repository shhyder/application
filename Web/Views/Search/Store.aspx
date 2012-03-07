<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.6.1/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=false"></script>
    <script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?libraries=places&sensor=false"></script>
    <script type="text/javascript" src='<%=  Url.Content("~/Advanced Search_files/jquery.js")%>'></script>
	<script type="text/javascript" src='<%=  Url.Content("~/Advanced Search_files/prototype.js")%>'></script>
	<script type="text/javascript" src='<%=  Url.Content("~/Advanced Search_files/slider.js")%>'></script>
    <link id="siteThemeLink" href='<%=  Url.Content("~/Pro_files/jquery-ui.css")%>' rel="stylesheet" type="text/css"/>
    <script src='<%=  Url.Content("~/Pro_files/jquery_002.js")%>' type="text/javascript"></script>
    <script src='<%=  Url.Content("~/Pro_files/Awesome.js")%>' type="text/javascript"></script>
    <script src='<%=  Url.Content("~/Pro_files/style.js")%>' type="text/javascript"></script>
    <link href='<%=  Url.Content("~/Content/Search.css")%>' rel="stylesheet" type="text/css"/>
    <script src='<%=  Url.Content("~/MapJS/Search.js")%>' type="text/javascript"></script>
    <script src='<%=  Url.Content("~/MapJS/Route.js")%>' type="text/javascript"></script>
	<link rel="stylesheet" type="text/css" media="all" href='<%=  Url.Content("~/Advanced Search_files/multiSelect.css")%>'\>
	<meta http-equiv="expires" content="-1"/>
	<meta http-equiv="pragma" content="no-cache"/>
	<meta http-equiv="cache-control" content="no-cache"/>
	<% Html.RenderPartial("FacebookInit");%>
<div id="pnlPersonal" style="text-align: left;vertical-align: top;font-family: Arial,Helvetica,sans-serif;" >
   <% Html.RenderPartial("Detail");%>
</div>

<div id="pnlDirection" style="text-align: left;vertical-align: top;font-family: Arial,Helvetica,sans-serif;" >
   <% Html.RenderPartial("Route");%>
</div>
<%if (ViewData["Is_Direct_Link"] != null )
{%>
    <div id="fb-root" style="text-align: left;vertical-align: top;font-family: Arial,Helvetica,sans-serif;" >
        <br />
        <% Html.RenderPartial("Facebook");%>
    </div>    
<%} %>


</asp:Content>

