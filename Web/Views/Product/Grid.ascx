<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.Collections.Generic"  %>
<%
    string _class = "ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only linkSimple  ";
    int grid_No = 1;
    string rowStyle = "";
    DataSet.DSParameter ds2 = (DataSet.DSParameter)System.Web.HttpContext.Current.Cache["data"];
    string link = "";
%>

 <% bool rowFlag = true; %>
    <% int sr_No = Convert.ToInt32( ViewData["start"] ) ;  %>
    
   
    
                
   <tr class="gridContent GridFooter">
            <td colspan="5"  style="text-align:left;vertical-align:top;" class="GridFooter" > <%= ViewData["searchCritaria"].ToString() %> 
            </td>
            <td colspan="4" style="text-align:right;vertical-align:top;" class="GridFooter" >
                <% if ((string)ViewData["pageLinks"] != "" && Convert.ToInt16(ViewData["Count"]) > 0)
                   { %>
           <%= ViewData["pageLinks"]%>
        <% } %>
            </td>
            
            

    </tr>
    
    
    <% foreach (var consumer in (ViewData[ UIProduct.listProduct.ToString()] as List< System.Data.DataRow>))
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
     
           <tr class="gridContent" >
            
            <td class="<%=rowStyle %>" style="vertical-align:middle;text-align:center;">
                <%= sr_No.ToString()%>
            </td>
            
            <td class="<%=rowStyle %>" style="vertical-align:middle;text-align:center;">
                <img onError="this.src='<%=  Url.Content("~/images/no-product-image.jpg" )%>'"  src="<%=  Url.Content("~/ProdImg/" +  consumer["Product_Image_Link"].ToString())%>"  alt="" name="product_r9_c2" width="60px" height="60px" border="0" class="catfixed" id="product_r9_c2"  style="opacity: 1; ">
            </td>
            <td class="<%=rowStyle %>" style="vertical-align:middle;text-align:center;" >
               <%= consumer["Product_Code"].ToString()%> 
            </td>
            <td class="<%=rowStyle %>" style="vertical-align:middle;text-align:center;" >
               <%= consumer["Product_Code_To_Display"].ToString()%> 
            </td>
            <td class="<%=rowStyle %>" style="vertical-align:middle;text-align:center;" >
               <%= consumer["Product"].ToString()%> 
            </td>
            <td class="<%=rowStyle %>" style=" padding: 5px 5px 6px 5px; overflow:hidden;width:200px;">
                <%= consumer["Description"].ToString()%> 
            </td>
            <td class="<%=rowStyle %>" style="vertical-align:middle;text-align:center;">
                <%= ds2.Product_Type.FindByProduct_Type_ID(Convert.ToInt32(consumer["Product_Type_ID"])).Product_Type%> 
            </td>
            <td class="<%=rowStyle %>" style="vertical-align:middle;text-align:center;">
                <%if ( Convert.ToBoolean( consumer["Is_Active"] ) )
                  { %>
                    Active
                <%}else
                { %>
                    In-active
                <%} %>
            </td>
            <td class="<%=rowStyle %>" style="vertical-align:middle;text-align:center;">
            <a href="javascript:OnEdit('<%= consumer["Product_ID"].ToString()  %>')" class="gradButton">Edit</a>
            
            
           <%-- <%= Ajax.ActionLink("View", consumer["Case_ID"].ToString(),
                                    "Case/View/", new AjaxOptions { UpdateTargetId = "mainPanel", InsertionMode = InsertionMode.Replace, OnBegin = "onBeginPage", OnSuccess = "onCompletePage" })%>
                                   <%= Html.ActionLink("Schedule", "GenerateSchedule", "Employee", new { service_Authorization_ID = consumer["Auth_ID"].ToString(), employee_ID = consumer["Staff"], UCI = consumer["UCI"] }, new { @class = _class })%>                     --%>
                                  <%-- <% link = "/Employee/GenerateSchedule?service_Authorization_ID=" + consumer["Auth_ID"].ToString().Trim() + "&employee_ID=" + consumer["Staff"].ToString().Trim() + "&UCI=" + consumer["UCI"].ToString().Trim(); %>
                                   <a class="gradButton" onclick="javascript:GetPage('<%= link %>')" >Schedule</a>--%>
            </td>
            
          </tr>
           <tr class="<%=rowStyle %>">
                <td colspan="9" class="<%=rowStyle %>">
                    <div id='<%= "Con" + sr_No.ToString() %>' style="padding-left:20px"></div>                                        
                </td>
            </tr>
          <% sr_No++; %>
    <% } %>
    <tr class="gridContent GridFooter">
            <td colspan="9" align="right" class="GridFooter" >
                <% if ((string)ViewData["pageLinks"] != "" && Convert.ToInt16(ViewData["Count"]) > 0)
                   { %>
           <%= ViewData["pageLinks"]%>
        <% } %>
            </td>
            
            

    </tr>



