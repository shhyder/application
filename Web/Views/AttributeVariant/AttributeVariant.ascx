<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<style type="text/css">
    .style1
    {
        height: 23px;
    }
    .style2
    {
        height: 16px;
    }
</style>


<form id="<%= UIAttributeVariant.dlgAttributeVariantForm.ToString() %>" action="Approved.ascx" method="post"> 

    <%= Html.Hidden(UIAttributeVariant.hidAttribute_ID.ToString(), ViewData[UIAttributeVariant.hidAttribute_ID.ToString()].ToString())%>
    <%= Html.Hidden(UIAttributeVariant.hidProduct_Type_ID.ToString(), ViewData[UIAttributeVariant.hidProduct_Type_ID.ToString()].ToString())%>
  
<table style="width: 99%; height: 51px;">
    <tr>
        <td class="style1" colspan="3">
            Attribute variant</td>
    </tr>
    <tr>
        <td class="style2" colspan="3">
        <%= Html.TextBox(UIAttributeVariant.txtAttributeVariant.ToString(), ViewData[UIAttributeVariant.txtAttributeVariant.ToString()].ToString())%>
        </td>
    </tr>
    <tr>
        <td class="style6">
            Variants</td>
        <td class="style5">
            &nbsp;</td>
        <td class="style4">
            </td>
    </tr>
    <tr>
        <td class="style1" colspan="3 
            &nbsp;</td>
    </tr>
</table>
</form>