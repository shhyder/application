<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>


<style type="text/css">
    .style1
    {
        width: 14px;
    }
    .style2
    {
        width: 65px;
    }
    .style3
    {
        width: 118px;
    }
    .style4
    {
        width: 15px;
    }
</style>

<%
    bool Is_Control_RO = false;
    string strClass = "";

    if ( Convert.ToInt32( ViewData[UIProductType.hidState.ToString()] ) == 2 || 
        Convert.ToInt32( ViewData[UIProductType.hidState.ToString()] ) == 3 )
    {
        Is_Control_RO = false;
    }
    else
    {
        Is_Control_RO = true;
    }
    
    

    if (Convert.ToInt32(ViewData[UIProductType.hidState.ToString()]) == 3)
    {
        strClass = "Enabled";
    }
    else
    {
        strClass = "Disabled";
    }
    
    
 %>


<form id="formProductType" action="Approved.ascx" method="post"> 
 <%= Html.Hidden(UIProductType.hidState.ToString(), ViewData[UIProductType.hidState.ToString()].ToString())%>
 <%= Html.Hidden(UIProductType.hidProductTypeID.ToString(), ViewData[UIProductType.hidProductTypeID.ToString()].ToString())%>
 
 
 <table border="0" style="width: 400px; border-top-style: none;" >
        <tr>
            <td colspan="4">&nbsp;</td>
        </tr>
        
        <tr>
          <td>&nbsp;</td>
          <td >Category</td>
          <td >
            <input style="width:270px;" class="<%= strClass %>" type="text" name="txtProductType" id="txtProductType" maxlength="50"  value="<%= ViewData[UIProductType.txtProductType.ToString()].ToString()  %>"/>
          </td>
          <td ></td>
        </tr>
        <tr>
            <td >&nbsp;</td>
            <td colspan="3" ><span class="error" id="ProductTypeError"></span></td>
        </tr>
        
        
        
         <tr>
          <td></td>
          <td ></td>
          <td ><input name="is_Active" <%= Convert.ToBoolean( ViewData[UIStore.Is_Active.ToString()] ) ? "checked='checked'":""  %>  id="is_Active" type="checkbox"/>
            Active
          </td>
          <td class="style4"></td>
        </tr>
      
          
      
       
    
       
    
     </table>
</form>

<script>
    $("#formStore").delegate("input", "focus", function() {
        $(".error").html("");
    });
</script>

