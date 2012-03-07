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
    <% foreach (var consumer in (ViewData[ UIStore.listStore.ToString()] as List< System.Data.DataRow>))
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
            
            <td class="<%=rowStyle %> LTextAlign">
                <%= consumer["Customer_ID"].ToString()%> 
            </td>
            <td class="<%=rowStyle %> LTextAlign" >
               <%= consumer["Customer"].ToString()%> 
            </td>
            <td class="<%=rowStyle %> LTextAlign">
                <%= consumer["Address"].ToString()%> 
            </td>
            <td class="<%=rowStyle %> CTextAlign">
                <%= consumer["City"].ToString()%> 
            </td>
            <td class="<%=rowStyle %> CTextAlign">
                <%= consumer["State"].ToString()%> 
            </td>
            <td class="<%=rowStyle %>">
                <%= consumer["Email"].ToString()%> 
            </td>
            <td class="<%=rowStyle %>">
                <%= consumer["Phone1"].ToString()%> 
            </td>
            <td class="<%=rowStyle %> CTextAlign">
                <%if ( Convert.ToBoolean( consumer["Is_Active"] ) )
                  { %>
                    &nbsp;&nbsp;<a  href="##" onclick="javascript:SetCustomerStatus(this,'<%= consumer["Customer_ID"].ToString()%>');" title="Update Product status from AccPac's database" >De-Activate</a>
                    
                <%}else
                { %>
                    &nbsp;&nbsp;<a  href="##" onclick="javascript:SetCustomerStatus(this,'<%= consumer["Customer_ID"].ToString()%>');" title="Update Product status from AccPac's database" >Activate</a>
                    
                <%} %>
            </td>
            <td class="<%=rowStyle %> CTextAlign">
                
            </td>
            
          </tr>
           <tr class="<%=rowStyle %> gridContent">
                <td colspan="10" class="<%=rowStyle %> CTextAlign">
                    <div id='<%= "Con" + sr_No.ToString() %>' style="padding-left:20px"></div>                                        
                </td>
            </tr>
          <% sr_No++; %>
    <% } %>
    <tr class="gridContent">
            <td colspan="10"  align="left" class="GridFooter" >
                <br />
                <% if ((string)ViewData["pageLinks"] != "" && Convert.ToInt16(ViewData["Count"]) > 0)
                   { %>
           <%= ViewData["pageLinks"]%>
           <br /><br />
        <% } %>
            </td>
    </tr>

