<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<%@ Import Namespace="System.Collections.Generic"  %>


<%
    string _class = "ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only linkSimple  ";
    int grid_No = 1;
    string rowStyle = "";
    //DataSet.DSParameter ds2 = (DSParameter)System.Web.HttpContext.Current.Cache["data"];
    string link = "";
%>

 <% bool rowFlag = true; %>
    <% int sr_No = Convert.ToInt32( ViewData["start"] ) ;  %>
    <% foreach (var consumer in (ViewData[ UIProductType.listProductType.ToString()] as List< System.Data.DataRow>))
       { %>
        <% if (grid_No % 2 == 0)
               {
                   rowStyle = "GridNonAlt";
               }
               else
               {
                   rowStyle = "GridAlt";
               }
               grid_No++;
       %>
     
           <tr class="gridContent gridRow"  >
            
            <td class="<%=rowStyle %>">
                <%= sr_No.ToString()%>
            </td>
            
            <td class="<%=rowStyle %>">
                <%= consumer["Product_Type"].ToString()%> 
            </td>
            <td class="<%=rowStyle %>">
                <%if ( Convert.ToBoolean( consumer["Is_Active"] ) )
                  { %>
                    Active
                    
                <%}else
                { %>
                    In-Active
                    
                <%} %>
            </td>
            <td class="<%=rowStyle %>">
                <a href="javascript:OnEdit(<%= consumer["Product_Type_ID"].ToString()%>)" class="gradButton">Edit</a>
            </td>
          </tr>
           <tr class="<%=rowStyle %> gridContent">
                <td colspan="3" class="<%=rowStyle %>">
                    <div id='<%= "Con" + sr_No.ToString() %>' style="padding-left:20px"></div>                                        
                </td>
            </tr>
          <% sr_No++; %>
    <% } %>
    <tr class="gridContent">
            <td colspan="4" align="right" class="GridFooter" >
                <br />
                <% if ((string)ViewData["pageLinks"] != "" && Convert.ToInt16(ViewData["Count"]) > 0)
                   { %>
           <%= ViewData["pageLinks"]%>
           <br /><br />
        <% } %>
            </td>
    </tr>

