<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=false"></script> 
    <script type="text/javascript" src='<%=  Url.Content("~/js/jquery-1.4.2.min.js")%>' ></script> 
    <script type="text/javascript" src='<%=  Url.Content("~/js/jquery-ui-1.8.1.custom.min.js")%>' ></script> 
    <script src='<%=  Url.Content("~/Scripts/MicrosoftAjax.js")%>' type="text/javascript"></script>
    <script src='<%=  Url.Content("~/Scripts/MicrosoftMvcAjax.js")%>' type="text/javascript"></script>
    
<div id="pnlPersonal" style="text-align: left;vertical-align: top;" >
   <% Html.RenderPartial("Address");%>
</div>
</asp:Content>