<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <%= Html.ActionLink("Search", "Index", "Search")%>
        <a href="<%=  Url.Content("~/SearchProduct/Index" )%>">View all Products</a>
    </p>
    
      <div id="mainSearchPanel" >
    <% Html.RenderPartial( "List");%>
    </div>


</asp:Content>
