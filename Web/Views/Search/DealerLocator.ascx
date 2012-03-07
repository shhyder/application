<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
 


<span ><%=  ViewData["Total_Dealers"].ToString() + " Stores found" %></span>
<span >
<a target="_blank"  href="<%=  Url.Content("~/SearchDealer/Result?" +  ViewData["Querystring"].ToString())%>">Show Result</a>
</span>

<span id="Span1"><%=  ViewData["Total_Products"].ToString() + " Product selected" %></span>
<span id="Span2">
<a target="_blank" href="<%=  Url.Content("~/SearchProduct/Index?" +  ViewData["Querystring"].ToString())%>">Show Result</a>
</span>
