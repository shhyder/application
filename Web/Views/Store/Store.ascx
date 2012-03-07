<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<style type="text/css">
    .style2
    {
        width: 65px;
    }
    .style3
    {
        width: 138px;
    }
    .style4
    {
        width: 15px;
    }
    .style5
    {
        width: 75px;
    }
    .style6
    {
        width: 94px;
    }
</style>

<%
    bool Is_Control_RO = false;
    string strClass = "";

    if ( Convert.ToInt32( ViewData[UIStore.hidState.ToString()] ) == 2 || 
        Convert.ToInt32( ViewData[UIStore.hidState.ToString()] ) == 3 )
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
    }
    
    
 %>


<form id="formStore" action="Approved.ascx" method="post"> 
 <%= Html.Hidden(UIStore.hidState.ToString(), ViewData[UIStore.hidState.ToString()].ToString())%>
 <%= Html.Hidden(UIStore.hidID.ToString(), ViewData[UIStore.hidID.ToString()].ToString())%>
 <%= Html.Hidden(UIStore.hidLatitude.ToString(), ViewData[UIStore.hidLatitude.ToString()].ToString())%>
 <%= Html.Hidden(UIStore.hidLongitude.ToString(), ViewData[UIStore.hidLongitude.ToString()].ToString())%>
 
 
 <table border="0" style="width: 620px; border-top-style: none;" >
        <tr>
          <td></td>
          <td class="style2" >Code</td>
          <td class="style3" colspan="4">
            <input class="<%= strClass %>" type="text" name="txtCode" id="txtCode" maxlength="9" onkeypress="javascript:isNumberKey(event);" size="8"  value="<%= ViewData[UIStore.txtCode.ToString()].ToString()  %>"/>
          </td>
          
          <td colspan="2" rowspan="8" id="MapImage" align="right" valign="top"><img border='0' style="width:250px;height:180px;" src='<%=  Url.Content("~/images/Google-Maps-icon.png")%>' alt='image could not be found' /> </td>
        </tr>
        <tr>
            <td >&nbsp;</td>
            <td class="style2" ></td>
            <td colspan="4" ><span class="error" id="CodeError"></span></td>
        </tr>
        
        
        
        <tr>
          <td></td>
          <td class="style2" >Store</td>
          <td class="style3" colspan="4" > 
            <input class="<%= strClass %>" type="text" style=" width:100%;" name="txtStore" id="txtStore" maxlength="50" value="<%= ViewData[UIStore.txtStore.ToString()].ToString()  %>"/>
          </td>
          
         
        </tr>
        <tr>
            <td >&nbsp;</td>
            <td class="style2" ></td>
            <td class="style3" colspan="4"  ><span class="error" id="StoreError"></span></td>
            
        </tr>
        
       
       
          
        <tr>
          <td>&nbsp;</td>
          <td class="style2" >Address</td>
          <td class="style3" colspan="4">   
            <input class="<%= strClass %>" type="text" name="txtAddress" id="txtAddress" style=" width:100%;" value="<%= ViewData[UIStore.txtAddress.ToString()].ToString()  %>"/>
          </td>
          
        </tr>
        <tr>
            <td >&nbsp;</td>
            <td class="style2" ></td>
            <td class="style3" colspan="4"><span class="error" id="AddressError" ></span></td>
            
        </tr>
       
       
       <tr>
          <td></td>
          <td class="style2" >Street No</td>
          <td class="style3" colspan="4"> 
                <input class="<%= strClass %>" type="text" name="txtStreetNo" id="txtStreetNo" maxlength="5" Style ="width:50px;" value="<%= ViewData[UIStore.txtStreetNo.ToString()].ToString()  %>"/>
                &nbsp;&nbsp;&nbsp;&nbsp;ZipCode&nbsp;<input class="<%= strClass %>" type="text" name="txtZipCode" id="txtZipCode" maxlength="5" Style ="width:50px;" value="<%= ViewData[UIStore.txtZipCode.ToString()].ToString()  %>"/> 
          </td>
         
        </tr>
        <tr>
            <td >&nbsp;</td>
            <td class="style2" >City</td>
            <td class="style3" colspan="4">
                <input class="<%= strClass %>" type="text" name="txtCity" id="txtCity" maxlength="5" Style ="width:100px;" value="<%= ViewData[UIStore.txtCity.ToString()].ToString()  %>"/>
                &nbsp;&nbsp;&nbsp;County&nbsp;&nbsp;<input class="<%= strClass %>" type="text" name="txtCounty" id="txtCounty" maxlength="5" Style ="width:80px;" value="<%= ViewData[UIStore.txtCounty.ToString()].ToString()  %>"/>
            </td>
        </tr>
       
      <tr>
          <td></td>
          <td class="style2">State</td>
          <td class="style3" colspan="5">
                <input class="<%= strClass %>" type="text" name="txtState" id="txtState" maxlength="5" Style ="width:50px;" value="<%= ViewData[UIStore.txtState.ToString()].ToString()  %>"/>
                <input class="<%= strClass %>" type="text" name="txtCountry" id="txtCountry" maxlength="5" Style ="width:50px;" value="<%= ViewData[UIStore.txtCountry.ToString()].ToString()  %>"/>
          </td>
        </tr>
     
        
        
         <tr>
          <td></td>
          <td class="style2"></td>
          <td class="style3" colspan="5">
               
          </td>
        </tr>
        <tr>
            <td >&nbsp;</td>
            <td class="style2" >&nbsp;</td>
            <td colspan="2" ><span class="error" id="CityError" ></span></td>
            <td class="style6" >&nbsp;</td>
            <td class="style5" >&nbsp;</td>
            <td colspan="2" ><span class="error" id="Span11" ></span></td>
        </tr>
        
        
        <tr>
          <td></td>
          <td class="style2">Address</td>
          <td class="style3" colspan="5">
          <%if (!Is_Control_RO)
            {%>
                <%= Html.TextBox(UIStore.txtFullAddress.ToString(), ViewData[UIStore.txtFullAddress.ToString()].ToString(), new { maxlength = 50, Style = "width: 450px;color:Gray;" })%>
           <%}
          else
          {%>
                <%= Html.TextBox(UIStore.txtFullAddress.ToString(), ViewData[UIStore.txtFullAddress.ToString()].ToString(), new { maxlength = 50, Style = "width: 450px;color:White;background-color:Gray;", @ReadOnly = true })%>
          <%} %>
          
          </td>
         
        </tr>
        <tr>
            <td >&nbsp;</td>
            <td class="style2" >&nbsp;</td>
            <td colspan="2" ><span class="error" id="FullAddressError" ></span></td>
            <td class="style6" >&nbsp;</td>
            <td class="style5" >&nbsp;</td>
            <td colspan="2" ><span class="error" id="Span2" ></span></td>
        </tr>
       
       
        
        
        
        
        
      
       
         <tr>
          <td></td>
          <td class="style2">Email 1</td>
          <td class="style3" colspan="3">
          <%if (!Is_Control_RO)
            {%>
                <%= Html.TextBox(UIStore.txtEmail.ToString(), ViewData[UIStore.txtEmail.ToString()].ToString(), new { maxlength = 25, Style = "width: 120px;color:Gray;" })%>
           <%}
          else
          {%>
                <%= Html.TextBox(UIStore.txtEmail.ToString(), ViewData[UIStore.txtEmail.ToString()].ToString(), new { maxlength = 25, Style = "width: 120px;color:White;background-color:Gray;", @ReadOnly = true })%>
          <%} %>
          <input name="is_Active" <%= Convert.ToBoolean( ViewData[UIStore.Is_Email1_Display.ToString()] ) ? "checked='checked'":""  %>  id="Is_Email1_Display" type="checkbox"/>Display
          </td>
          
         
          <td class="style5" align="right">Email 2</td>
          <td colspan="2">
          <%if (!Is_Control_RO)
            {%>
                <%= Html.TextBox(UIStore.txtEmail2.ToString(), ViewData[UIStore.txtEmail2.ToString()].ToString(), new { maxlength = 25, Style = "width: 120px;color:Gray;" })%>
           <%}
          else
          {%>
                <%= Html.TextBox(UIStore.txtEmail2.ToString(), ViewData[UIStore.txtEmail2.ToString()].ToString(), new { maxlength = 25, Style = "width: 120px;color:White;background-color:Gray;", @ReadOnly = true })%>
          <%} %>
          <input name="is_Active" <%= Convert.ToBoolean( ViewData[UIStore.Is_Email2_Display.ToString()] ) ? "checked='checked'":""  %>  id="Is_Email2_Display" type="checkbox"/>Display
          </td>
        </tr>
        <tr>
            <td >&nbsp;</td>
            <td class="style2" >&nbsp;</td>
            <td colspan="2" ><span class="error" id="Email1Error" ></span></td>
            <td class="style6" >&nbsp;</td>
            <td class="style5" >&nbsp;</td>
            <td colspan="2" ><span class="error" id="Email2Error" ></span></td>
        </tr>
        
        
        <tr>
          <td></td>
          <td class="style2">Phone 1</td>
          <td class="style3" colspan="3">
          <%if (!Is_Control_RO)
            {%>
                <%= Html.TextBox(UIStore.txtPhone1.ToString(), ViewData[UIStore.txtPhone1.ToString()].ToString(), new { maxlength = 25, Style = "width: 120px;color:Gray;" })%>
           <%}
          else
          {%>
                <%= Html.TextBox(UIStore.txtPhone1.ToString(), ViewData[UIStore.txtPhone1.ToString()].ToString(), new { maxlength = 25, Style = "width: 120px;color:White;background-color:Gray;", @ReadOnly = true })%>
          <%} %>
          <input name="Is_Phone1_Display" <%= Convert.ToBoolean( ViewData[UIStore.Is_Phone1_Display.ToString()] ) ? "checked='checked'":""  %>  id="Is_Phone1_Display" type="checkbox"/>Display
          </td>
          
         
          <td class="style5" align="right">Phone 2</td>
          <td colspan="2">
          <%if (!Is_Control_RO)
            {%>
                <%= Html.TextBox(UIStore.txtPhone2.ToString(), ViewData[UIStore.txtPhone2.ToString()].ToString(), new { maxlength = 25, Style = "width: 120px;color:Gray;" })%>
           <%}
          else
          {%>
                <%= Html.TextBox(UIStore.txtPhone2.ToString(), ViewData[UIStore.txtPhone2.ToString()].ToString(), new { maxlength = 25, Style = "width: 120px;color:White;background-color:Gray;", @ReadOnly = true })%>
          <%} %>
          <input name="Is_Phone2_Display" <%= Convert.ToBoolean( ViewData[UIStore.Is_Phone2_Display.ToString()] ) ? "checked='checked'":""  %>  id="Is_Phone2_Display" type="checkbox"/>Display
          </td>
          
        </tr>
        <tr>
            <td >&nbsp;</td>
            <td class="style2" >&nbsp;</td>
            <td colspan="2" ><span class="error" id="Phone1Error" ></span></td>
            <td class="style6" >&nbsp;</td>
            <td class="style5" >&nbsp;</td>
            <td colspan="2" ><span class="error" id="Phone2Error" ></span></td>
        </tr>
        
        <tr>
          <td></td>
          <td class="style2">Website</td>
          <td class="style3" colspan="2">
          <%if (!Is_Control_RO)
            {%>
                <%= Html.TextBox(UIStore.txtWebsite.ToString(), ViewData[UIStore.txtWebsite.ToString()].ToString(), new { maxlength = 25, Style = "width: 120px;color:Gray;" })%>
           <%}
          else
          {%>
                <%= Html.TextBox(UIStore.txtWebsite.ToString(), ViewData[UIStore.txtWebsite.ToString()].ToString(), new { maxlength = 25, Style = "width: 120px;color:White;background-color:Gray;", @ReadOnly = true })%>
          <%} %>
          
          </td>
          
           <td class="style6" ><input name="Is_Website_Display" <%= Convert.ToBoolean( ViewData[UIStore.Is_Website_Display.ToString()] ) ? "checked='checked'":""  %>  id="Is_Website_Display" type="checkbox"/>Display</td>
            <td class="style5" ></td>
            <td ><input name="is_Active" <%= Convert.ToBoolean( ViewData[UIStore.Is_Active.ToString()] ) ? "checked='checked'":""  %>  id="is_Active" type="checkbox"/>Active</td>
            
        </tr>
        <tr>
            <td></td>
            <td></td>
             <td colspan="4" ><span class="error" id="WebsiteError" ></span></td>
            
        </tr>
    
     </table>
</form>

<script>
    $("#txtAddress").live("focus", function() {
        $('.pac-container').attr('Style', 'position: absolute; z-index: 1005; left: 215px; top: 155px; width: 420px; display: none');
    });

    $("#formStore").delegate("input", "focus", function() {
        $(".error").html("");
    });
    
  
 

    
</script>

   