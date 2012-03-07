<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<style type="text/css">
    .style1
    {
        width: 239px;
    }
    .style2
    {
        width: 92px;
    }
    .style3
    {
        width: 2px;
    }
    .style4
    {
        width: 2px;
        height: 15px;
    }
    .style5
    {
        width: 92px;
        height: 15px;
    }
    .style6
    {
        width: 239px;
        height: 15px;
    }
    .style7
    {
        height: 15px;
    }
</style>

<%
    bool Is_Control_RO = false;
    string strClass = "";
    string imageUrl = Url.Content("~/images/no-product-image.jpg");
    
    if ( Convert.ToInt32( ViewData[UIProduct.hidState.ToString()] ) == 2 ||
        Convert.ToInt32(ViewData[UIProduct.hidState.ToString()]) == 3)
    {
        Is_Control_RO = false;
    }
    else
    {
        Is_Control_RO = true;
    }


    if (Convert.ToInt32(ViewData[UIStore.hidState.ToString()]) == 3)
    {
        strClass = "Enabled";
    }
    else
    {
        strClass = "Disabled";
        imageUrl = Url.Content("~/ProdImg/" + ViewData[UIProduct.hidFileName.ToString()].ToString());
    }


    
    
 %>

<div id="fileupload" >
<form id="formProduct" action="Upload" method="POST" enctype="multipart/form-data">
        
        
        
        <%= Html.Hidden(UIProduct.hidState.ToString(), ViewData[UIProduct.hidState.ToString()].ToString())%>
        <%= Html.Hidden(UIProduct.hidFolderName.ToString(), ViewData[UIProduct.hidFolderName.ToString()].ToString())%>
        <%= Html.Hidden(UIProduct.hidFileName.ToString(), ViewData[UIProduct.hidFileName.ToString()].ToString())%>
        <%= Html.Hidden(UIProduct.hidGUID.ToString(), ViewData[UIProduct.hidGUID.ToString()].ToString())%>
        <%= Html.Hidden(UIProduct.hidProduct_ID.ToString(), ViewData[UIProduct.hidProduct_ID.ToString()].ToString())%>
        
        <table border="0" style="width: 507px; border-top-style: none;" >
       
         <tr>
          <td class="style3"></td>
          <td class="style2">Code</td >
          <td class="style1">
             <input class="<%= strClass %>" type="text" name="txtProduct_Code" id="txtProduct_Code" maxlength="10"  size="10"  value="<%= ViewData[UIProduct.txtProduct_Code.ToString()].ToString()  %>"/>
          </td>
          <td rowspan="4" id="ProductImageHolder"><img border='0' width="120px" height="120px"  src='<%= imageUrl  %>' alt='image could not be found' />
          
          </td>
        </tr>
        <tr>
            <td class="style3" >&nbsp;</td>
            <td class="style2" >&nbsp;</td>
            <td colspan="2" ><span class="error"  id="product_CodeError"></span></td>
        </tr>
       
       
       
        <tr>
          <td class="style3"></td>
          <td class="style2" >Display Code</td>
          <td class="style1">
                <input class="<%= strClass %>" type="text" name="txtCodeToDisplay" style="width:80px" id="txtCodeToDisplay" maxlength="15"   value="<%= ViewData[UIProduct.txtCodeToDisplay.ToString()].ToString()  %>"/>
          </td>
        </tr>
        <tr>
            <td class="style3" >&nbsp;</td>
            <td class="style2" >&nbsp;</td>
            <td colspan="2" ><span class="error"  id="code_To_DisplayError"></span></td>
        </tr>
        
        
        
         <tr>
          <td class="style3"></td>
          <td class="style2" >Product</td>
          <td class="style1">
                <input class="<%= strClass %>" type="text" name="txtProduct" id="txtProduct" style="width:180px" maxlength="50" value="<%= ViewData[UIProduct.txtProduct.ToString()].ToString()  %>"/>
          </td>
          <td></td>
        </tr>
        <tr>
            <td class="style3" >&nbsp;</td>
            <td class="style2" >&nbsp;</td>
            <td colspan="2" ><span class="error"  id="productError"></span></td>
        </tr>
        
         <tr>
          <td class="style4"></td>
          <td class="style5" >Type</td>
          <td class="style6">
                <%=  Html.DropDownList( UIProduct.cobProductType.ToString(), (SelectList)ViewData[UIProductSearch.listProductType.ToString()], new { style = "width: 170px;" })%>
          </td>
          <td class="style7">
                
                <span id="btnSelectFile" class="btn success fileinput-button">
                    <span id="fileLoader">Add files...</span>
                    <input type="file" name="files[]" multiple="">
                </span><span ></span>
                
                
          </td>
        </tr>
        <tr>
            <td class="style3" >&nbsp;</td>
            <td class="style2" >&nbsp;</td>
            <td colspan="2" ><span class="error"  id="product_TypeError"></span></td>
        </tr>
        
        <tr>
          <td class="style3"></td>
          <td class="style2" >Description</td>
          <td class="style1" colspan="2">
                <textarea cols=10 rows=5  class="<%= strClass %>" type="text"  name="txtDescription" style="width:380px" id="txtDescription" maxlength="550" value="<%= ViewData[UIProduct.txtDescription.ToString()].ToString()  %>"><%= ViewData[UIProduct.txtDescription.ToString()].ToString()  %></textarea>
          </td>
        </tr>
        <tr>
            <td class="style3" >&nbsp;</td>
            <td class="style2" >&nbsp;</td>
            <td class="style1" ><span class="error"  id="descriptionError"></span></td>
            <td><span class="error"  id="file_NameError"></span></td>
        </tr>
        
        
        
        <tr>
          <td class="style3"></td>
          <td class="style2" >Link</td>
          <td class="style1">
                <input class="<%= strClass %>" type="text" name="txtLink" id="txtLink" maxlength="50" style="width:180px" value="<%= ViewData[UIProduct.txtLink.ToString()].ToString()  %>"/>
          </td>
          <td><input name="is_Active" <%= Convert.ToBoolean( ViewData[UIProduct.is_Active.ToString()] ) ? "checked='checked'":""  %>  id="is_Active" type="checkbox"/>Active</td>
        </tr>
        <tr>
            <td class="style3" >&nbsp;</td>
            <td class="style2" >&nbsp;</td>
            <td colspan="2" ><span class="error"  id="product_LinkError"></span></td>
        </tr>
        
     </table>
            
       
    </form>
    <div class="fileupload-content">
        <table class="files"></table>
        <div class="fileupload-progressbar"></div>
    </div>
</div>

<script src='<%=  Url.Content("~/PlgIn/jquery.fileupload.js")%>'></script>
<script src='<%=  Url.Content("~/PlgIn/jquery.fileupload-ui.js")%>'></script>
<script src='<%=  Url.Content("~/PlgIn/application.js")%>'></script>

<script>

    $("#formProduct").delegate("input", "focus", function() {
        $(".error").html("");
    });

</script>