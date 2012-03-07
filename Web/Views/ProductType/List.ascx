<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<%@ Import Namespace="System.Collections.Generic"  %>
<%@ Import Namespace="System.Data"  %>
<script src='<%=  Url.Content("~/Scripts/MicrosoftAjax.js")%>' type="text/javascript"></script>
<script src='<%=  Url.Content("~/Scripts/MicrosoftMvcAjax.js")%>' type="text/javascript"></script>
<div id="divLoading"></div>
<%= Html.Hidden(UIProductType.hidCurrentPage.ToString(), ViewData[UIProductType.hidCurrentPage.ToString()].ToString())%>
<% using (Ajax.BeginForm("List", "ProductType", new AjaxOptions { UpdateTargetId = "mainSearchPanel", InsertionMode = InsertionMode.Replace }, new { autocomplete = "off" }))
     { %>

    <table width="100%" id = "toolTip_Container"  style="border-style:double;">
   <tr>
    <td >
    
    <table width="100%" >
        <tr bgcolor="#d9e8f7"  align="left">
            <th class="contentStyle" style="background-color:#d9e8f7;">
                Sr. No.
            </th>
            <th class="contentStyle" style="background-color:#d9e8f7;">
                Product Type
            </th>
            <th class="contentStyle" style="background-color:#d9e8f7;">
                --
            </th>
            
            <th class="contentStyle" style="background-color:#d9e8f7;">
                --
            </th>
             <th class="contentStyle" style="background-color:#d9e8f7;">
                --
            </th>
            <th class="contentStyle" style="background-color:#d9e8f7;">
                --
            </th>
            <th class="contentStyle" style="background-color:#d9e8f7;">
                --
            </th>
             <th class="contentStyle" style="background-color:#d9e8f7;">
                --
            </th>
            <th class="contentStyle" style="background-color:#d9e8f7;">
                Action
            </th>
        </tr>
        <tr bgcolor="#000066"  align="left">
            <th class="contentStyle" style="background-color:#d9e8f7;">
            </th>
            <th class="contentStyle" style="background-color:#d9e8f7;">
                <%= Html.TextBox(UIProductType.txtSerProductType.ToString(), ViewData[UIProductType.txtSerProductType.ToString()].ToString(), new { maxlength = 10, Style = "width: 70px;" })%>
            </th>
            <th class="contentStyle" style="background-color:#d9e8f7;">
                
            </th>
            
            <th class="contentStyle" style="background-color:#d9e8f7;">
                
            </th>
             <th class="contentStyle" style="background-color:#d9e8f7;">
                
            </th>
            <th class="contentStyle" style="background-color:#d9e8f7;">
                
            </th>
            <th class="contentStyle" style="background-color:#d9e8f7;">
                
            </th>
             <th class="contentStyle" style="background-color:#d9e8f7;">
                
            </th>
            <th class="contentStyle" style="background-color:#d9e8f7;">
               <input name="submitValue" type="submit" value="Search"   style="width: 5em" />
            </th>
        </tr>
        
    <% string style = "background:White";%>
    <% foreach (var consumer in (ViewData[UIProductType.listProductType.ToString()] as List<DataRow>))
       { %>
           
           
           <tr style='<%= style %>' onclick="ViewProductType(<%= consumer["Product_Type_ID"].ToString()  %> );"  >
            <td class="gridFirstAlternate">
                <%= consumer["ID"].ToString()%> 
            </td>
            <td class="gridFirstAlternate">
                <%= consumer["Product_Type"].ToString()%> 
            </td>
            <td class="gridFirstAlternate" >
                "---"
            </td>
            <td class="gridFirstAlternate">
                "---"
            </td>
            <td class="gridFirstAlternate">
                "---"
            </td>
            <td class="gridFirstAlternate">
                "---"
            </td>
            <td class="gridFirstAlternate">
                "---"
            </td>
            <td class="gridFirstAlternate">
                "---"
            </td>
            <td class="gridFirstAlternate">
            
            </td>
          </tr>
    <% } %>
    <tr>
            <td colspan="4" align="right">
                <br />
                <% if ((string)ViewData["pageLinks"] != "" && Convert.ToInt16(ViewData["Count"]) > 0)
                   { %>
           <%= ViewData["pageLinks"]%>
           <br /><br />
        <% } %>
            </td>
       </tr>
    </table>
    <%if (Convert.ToInt16(ViewData["Count"]) == 0)
    {%>
        No Product type is found in this search
    <%} %>
    </td>
    </tr>
    </table>  
    <%}%>