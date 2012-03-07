<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<style type="text/css">
    .style3
    {
    }
    .style4
    {
        height: 88px;
    }
</style>
<form id="<%= UIProductType.dlgProductTypeForm.ToString() %>" action="Approved.ascx" method="post"> 
<%= Html.Hidden(UIProductType.hidProductTypeID.ToString(), ViewData[UIProductType.hidProductTypeID.ToString()].ToString())%>
 <table border="0" style="width: 412px">
        <tr>
          <td class="style3" >Product Type</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td class="style3" colspan="3" ><%= Html.TextBox(UIProductType.txtProductType.ToString(), ViewData[UIProductType.txtProductType.ToString()].ToString(), new { maxlength = 25, Style = "width: 190px;color:Gray;", @ReadOnly = true })%>
        </tr>
        
        <tr>
          <td class="style3" >Enter date</td>
          <td >Status</td>
          <td >&nbsp;</td>
        </tr>
        <tr>
          <td class="style3" ><%= Html.TextBox(UIProductType.txtEnterDate.ToString(), ViewData[UIProductType.txtEnterDate.ToString()].ToString(), new { maxlength = 25, Style = "width: 120px;color:Gray", @ReadOnly = true })%></td>
          <td ></td>
          <td ></td>
        </tr>
        <tr>
          <td class="style3" >Description</td>
          <td >&nbsp;</td>
          <td >&nbsp;</td>
        </tr>
        <tr>
          <td class="style4" colspan="3" ><%= Html.TextArea(UIProductType.txtDescription.ToString(), ViewData[UIProductType.txtDescription.ToString()].ToString(), new { maxlength = 250, Style = "width: 90px;color:Gray;", @ReadOnly = false })%></td>
        </tr>
        <tr>
            <td valign="bottom" class="style3">&nbsp;</td>
            <td valign="bottom">&nbsp;</td>
            <td valign="bottom">
            </td>
        </tr>
        
        <tr>
            <td class="style3">&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
      
       
     </table>
</form>

