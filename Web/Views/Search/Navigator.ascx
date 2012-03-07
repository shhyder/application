<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>


<%if (Convert.ToInt16(ViewData["Count"]) != 0)
{
      if ((string)ViewData["pageLinks"] != "" && Convert.ToInt16(ViewData["Count"]) > 0)
      { %>
           <%= ViewData["pageLinks"]%>
           
      <% } 
} %>
