<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<%= Html.Hidden(UIProductType.hidProductTypeID.ToString(), ViewData[UIProductType.hidProductTypeID.ToString()].ToString())%>
 <table border="0" style="width: 412px">
        <tr>
          <td class="style3" >Product Type</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td class="style3" colspan="3" ><%= Html.TextBox(UIProductType.txtProductType.ToString(), ViewData[UIProductType.txtProductType.ToString()].ToString(), new { maxlength = 25, Style = "width: 190px;color:Gray;", @ReadOnly = false })%>
        </tr>
        
        <tr>
          <td class="style3" >Enter date</td>
          <td >Status</td>
          <td >&nbsp;</td>
        </tr>
        <tr>
          <td class="style3" ><%= Html.TextBox(UIProductType.txtEnterDate.ToString(), ViewData[UIProductType.txtEnterDate.ToString()].ToString(), new { maxlength = 25, Style = "width: 120px;color:Gray", @ReadOnly = false })%></td>
          <td ></td>
          <td ></td>
        </tr>
        <tr>
          <td class="style3" >Description</td>
          <td >&nbsp;</td>
          <td >&nbsp;</td>
        </tr>
        <tr>
          <td class="style4" colspan="3" ><%= Html.TextArea(UIProductType.txtDescription.ToString(), ViewData[UIProductType.txtDescription.ToString()].ToString(), new { maxlength = 250, Style = "width: 190px;color:Gray;", @ReadOnly = false })%></td>
        </tr>
        <tr>
            <td valign="bottom" class="style3" colspan="3">&nbsp;</td>
        </tr>
        
        <tr>
            <td class="style3" colspan="3">
                <div id="attributePanel" >
                     <% ViewDataDictionary vdd = new ViewDataDictionary();
                        vdd[UIProductType.hidProductTypeID.ToString()] = ViewData[UIProductType.hidProductTypeID.ToString()].ToString();
                        vdd[UIProductType.listProductTypeAttribute.ToString()] = ViewData[UIProductType.listProductTypeAttribute.ToString()];
                        vdd[UIProductType.htmlProductTypeAttributeVariant.ToString()] = ViewData[UIProductType.htmlProductTypeAttributeVariant.ToString()];
                         Html.RenderPartial("ProductTypeAttributes", vdd);%>
                </div>
            </td>
        </tr>
      
       
     </table>