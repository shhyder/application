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




    string _avl_Products = "";
    DataRow[] rows = ds.Product.Select(  " Product_ID in ( " + ViewData["product_IDs"].ToString() + ")" );
    foreach (DataRow row in rows)
    {
         _avl_Products += row["Product"].ToString() + ", ";

    }

    _avl_Products = _avl_Products.Remove(_avl_Products.Length - 2);
     
%>


<%--<td class="header" colspan=4><a href="javascript:OnSearchFromList()" >Search</a>--%>
            <table>
                <tr>
                    <td><b style="font-size: small">Displaying <%= sr_No %>-<%= lastrow %> of <%= ViewData["Count"]%> Stores found</b></td>
                    <td style="text-align:right"><a href="javascript:OnSearchFromList()" >Select Product</a></td>
                </tr>
                <tr>
                    <td colspan="2">
                     Selected Products : <%= _avl_Products %> <br />
            Each store below carries at least one of the products
            in your search. Click "Jatai Products" for a list of all Jatai products carried.
            <br />
            <b >Please contact store to ensure product availability</b><br />
                    </td>
                
                </tr>
            
            </table>
           
           
    <%--    </td>--%>

