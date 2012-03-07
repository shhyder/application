<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>


<%@ Import Namespace="System.Collections.Generic"  %>
<%@ Import Namespace="System.Data"  %>
<%@ Import Namespace="System"  %>
<%@ Import Namespace="DataSet"  %>
<%@ Import Namespace="System.Text"  %>
<script src='<%=  Url.Content("~/Scripts/MicrosoftAjax.js")%>' type="text/javascript"></script>
<script src='<%=  Url.Content("~/Scripts/MicrosoftMvcAjax.js")%>' type="text/javascript"></script>
<%
    DSParameter ds = (DSParameter)System.Web.HttpContext.Current.Cache["data"];
    bool is_Category_Search = Convert.ToBoolean( ViewData[ UIProductSearch.is_Category_Search.ToString()] );
    string search_Text = ViewData[ UIProductSearch.search.ToString()].ToString();
 %>

<div id="divLoading"></div>


<% using (Ajax.BeginForm("List", "SearchDealer", new AjaxOptions { UpdateTargetId = "mainSearchPanel", InsertionMode = InsertionMode.Replace }, new { autocomplete = "off" }))
     { %>

    <table width="100%" id = "toolTip_Container"  style="border-style: none;">
   <tr>
    <td >
    
    <table width="100%" >
      
        
    <% string style = "background:White";%>
    <% foreach (var consumer in (ViewData[ UIProductSearch.listProduct.ToString()] as List<DataRow>))
       { %>
            <tr>
               <td colspan="2"><a href="<%= consumer["Heading_Link"].ToString()  %>"><img src="<%= consumer["Product_Image_Link"].ToString()  %>" alt="" name="product_r9_c2" width="300" height="88" border="0" class="catfixed" id="product_r9_c2"  style="opacity: 1; "></a></td>
               <!--<td colspan="2"><img name="product_r9_c6" src="<%= consumer["Product_Brand_Image_Link"].ToString()  %>" width="100" height="88" border="0" id="product_r9_c6" alt=""></td>!-->
               <td colspan="8" valign="middle"><p><strong class="product-detail-subheading"><a href="<%= consumer["Heading_Link"].ToString()  %>" class="product-detail-subheading"><%= Web.Model.Utility.HightLigthText(consumer["Product_Heading"].ToString(), is_Category_Search, search_Text)%></a></strong> <br>
                   <span class="text-main-products"><%= Web.Model.Utility.HightLigthText(consumer["Description"].ToString(), is_Category_Search, search_Text)%></span>
                   <br />
                   <!-- <a  onclick="javascript:PopUp('<%= consumer["Product_ID"].ToString() %>');" >Search Dealers</a> !-->
                   </p></td>
               <td><img src="./Jatai International, Feather Razor, Feather Shears, Feather Replacement Blades, Fuji Paper, Seki Edge and more..._files/spacer.gif" width="1" height="88" border="0" alt=""></td>
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