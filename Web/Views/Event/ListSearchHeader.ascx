<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<%@ Import Namespace="System.Collections.Generic"  %>
<%@ Import Namespace="System.Data"  %>
<%@ Import Namespace="System"  %>
<%@ Import Namespace="DataSet"  %>
 <% int sr_No = Convert.ToInt32( ViewData["start"] ) ;
    int lastrow = sr_No + Convert.ToInt32( ViewData["PageSize"] ) - 1;
    int total = Convert.ToInt32(ViewData["Count"]);
    DSParameter ds = (DSParameter)System.Web.HttpContext.Current.Cache["data"];
    if (lastrow > total)
    {
        lastrow = total - sr_No + sr_No;
    }
%>



            <table>
                <tr>
                    <td><b style="font-size: small">Displaying <%= sr_No %>-<%= lastrow %> of <%= ViewData["Count"]%> Jatai Events found</b></td>
                </tr>
                <tr>
                    <td colspan="2">
            <b >Please click an event to get its detail</b><br />
                    </td>
                
                </tr>
            
            </table>
           
           
