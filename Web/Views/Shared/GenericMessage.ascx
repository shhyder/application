<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<p>
        <%= Html.ActionLink("Search", "Index", "Search")%>
    </p>

<table border="0" style="width: 100%; height: 339px;">
    <tr>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td align="center" valign="middle"><h1>
            <%= ViewData["GenericMessage"].ToString()%></h1></td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
</table>

