<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MobileSite.Master" Inherits="System.Web.Mvc.ViewPage" %>



<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%--<script type="text/javascript" src='<%=  Url.Content("~/js/jquery-1.4.2.min.js")%>'></script>--%>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.6.1/jquery.min.js" type="text/javascript"></script>	<script type="text/javascript" src='<%=  Url.Content("~/Advanced Search_files/prototype.js")%>'></script>
	<script type="text/javascript" src='<%=  Url.Content("~/Advanced Search_files/slider.js")%>'></script>
    <link id="siteThemeLink" href='<%=  Url.Content("~/Pro_files/jquery-ui.css")%>' rel="stylesheet" type="text/css"/>
    <script src='<%=  Url.Content("~/Pro_files/jquery_002.js")%>' type="text/javascript"></script>
    <script src='<%=  Url.Content("~/Pro_files/Awesome.js")%>' type="text/javascript"></script>
    <script src='<%=  Url.Content("~/Pro_files/style.js")%>' type="text/javascript"></script>
    <script src='<%=  Url.Content("~/MapJS/Route.js")%>' type="text/javascript"></script>
	<link rel="stylesheet" type="text/css" media="all" href='<%=  Url.Content("~/Content/MobilemultiSelect.css")%>'/>
	<meta http-equiv="expires" content="-1"/>
	<meta http-equiv="pragma" content="no-cache"/>
	<meta http-equiv="cache-control" content="no-cache"/>
	<% Html.RenderPartial("FacebookInit");%>
<div id="pnlPersonal" style="text-align: left;vertical-align: top;font-family: Arial,Helvetica,sans-serif; width:100%;" >
   <% Html.RenderPartial("MobileSearch");%>
</div>

<div id="pnlDirection" style="display:none;text-align: left;vertical-align: top;font-family: Arial,Helvetica,sans-serif;" >
   <% Html.RenderPartial("MobileRoute");%>
</div>
</asp:Content>