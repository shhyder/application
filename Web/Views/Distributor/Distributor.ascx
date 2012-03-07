<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>


<script runat="server">

    protected void Page_Load(object sender, EventArgs e)
    {

    }
</script>

<script src='<%=  Url.Content("~/Scripts/MicrosoftAjax.js")%>' type="text/javascript"></script>
<script src='<%=  Url.Content("~/Scripts/MicrosoftMvcAjax.js")%>' type="text/javascript"></script>
<script type="text/javascript">

    function showResults() {
        

        alert("resultMsg");
    }
    
    function loadmap()
    {
        
        //initialize();

    }
    //loadmap();
</script>

<% using (Ajax.BeginForm("Distributor", "Distributor", new AjaxOptions { UpdateTargetId = "pnlPersonal", InsertionMode = InsertionMode.Replace, OnSuccess = "Done" }, new { autocomplete = "off" }))
     { %>
     
     <%= Html.Hidden(UIDistributor.hidLatitude.ToString(), ViewData[UIDistributor.hidLatitude.ToString()])%>
     <%= Html.Hidden(UIDistributor.hidLongitude.ToString(), ViewData[UIDistributor.hidLongitude.ToString()])%>
     <%= Html.ValidationSummary(true, "Login was unsuccessful. Please correct the errors and try again.") %>
    
     <style type="text/css">
         .style1
         {
             width: 22px;
         }
         .style2
         {
         }
     </style>
     <table border="0" style="width: 776px">
        <tr>
        <td rowspan="22" valign="top" >
            <p style="width: 282px">Just drag over your icon to business location</p>
            <div id="map_canvas" 
                style="width:450px; height:295px" ></div></td>
         <td align="right" >&nbsp;</td>
          <td valign="bottom" class="style2"  >Distributor</td>
          <td valign="bottom"  >Email</td>
        </tr>
        <tr>
         <td class="style1" align="right" >&nbsp;</td>
          <td ><%= Html.TextBox(UIDistributor.txtDistributor.ToString(), ViewData[UIDistributor.txtDistributor.ToString()].ToString(), new { maxlength = 50, Style = "width: 190px;" })%>
          </td>
          <td><%= Html.TextBox(UIDistributor.txtEmail.ToString(), ViewData[UIDistributor.txtEmail.ToString()].ToString(), new { maxlength = 50, Style = "width: 170px;" })%></td>
        </tr>
        <tr>
            <td ></td>
            <td valign="bottom" class="style2">Phone</td>
            <td valign="bottom">Fax</td>
        </tr>
        <tr>
         <td align="right" >&nbsp;</td>
          <td class="style2" ><%= Html.TextBox(UIDistributor.txtPhone.ToString(), ViewData[UIDistributor.txtPhone.ToString()].ToString(), new { maxlength = 25, Style = "width: 120px;" })%></td>
          <td ><%= Html.TextBox(UIDistributor.txtFax.ToString(), ViewData[UIDistributor.txtFax.ToString()].ToString(), new { maxlength = 25, Style = "width: 120px;" })%></td>
        </tr>
        <tr>
         <td align="right" >&nbsp;</td>
          <td class="style2" colspan="2" >Building / Apartment</td>
        </tr>
        <tr>
         <td align="right" >&nbsp;</td>
          <td class="style2" colspan="2" ><%= Html.TextBox(UIDistributor.txtBuildingAppartment.ToString(), ViewData[UIDistributor.txtBuildingAppartment.ToString()].ToString(), new { maxlength = 25, Style = "width: 220px;" })%></td>
        </tr>
        <tr>
         <td align="right" >&nbsp;</td>
          <td class="style2" >Street No.</td>
          <td >Street Name</td>
        </tr>
        <tr>
         <td align="right" >&nbsp;</td>
          <td class="style2" ><%= Html.TextBox(UIDistributor.txtStreet_No.ToString(), ViewData[UIDistributor.txtStreet_No.ToString()].ToString(), new { maxlength = 25, Style = "width: 120px;color:Gray", @ReadOnly = true })%></td>
          <td ><%= Html.TextBox(UIDistributor.txtStreet_Name.ToString(), ViewData[UIDistributor.txtStreet_Name.ToString()].ToString(), new { maxlength = 25, Style = "width: 120px;color:Gray", @ReadOnly = true })%></td>
        </tr>
        <tr>
         <td align="right" >&nbsp;</td>
          <td class="style2" >Zip Code</td>
          <td >City</td>
        </tr>
        <tr>
         <td align="right" >&nbsp;</td>
          <td class="style2" ><%= Html.TextBox(UIDistributor.txtZipCode.ToString(), ViewData[UIDistributor.txtZipCode.ToString()].ToString(), new { maxlength = 25, Style = "width: 90px;color:Gray;", @ReadOnly = true })%></td>
          <td ><%= Html.TextBox(UIDistributor.txtCity.ToString(), ViewData[UIDistributor.txtCity.ToString()].ToString(), new { maxlength = 25, Style = "width: 120px;color:Gray;", @ReadOnly = true })%></td>
        </tr>
        <tr>
         <td align="right" >&nbsp;</td>
          <td class="style2" >State</td>
          <td >Country</td>
        </tr>
        <tr>
         <td align="right" >&nbsp;</td>
          <td class="style2" ><%= Html.TextBox(UIDistributor.txtState.ToString(), ViewData[UIDistributor.txtState.ToString()].ToString(), new { maxlength = 25, Style = "width: 50px;color:Gray", @ReadOnly = true })%></td>
          <td ><%= Html.TextBox(UIDistributor.txtCountry.ToString(), ViewData[UIDistributor.txtCountry.ToString()].ToString(), new { maxlength = 25, Style = "width: 120px;color:Gray", @ReadOnly = true })%></td>
        </tr>
        <tr>
            <td >
            <input id="btnSubmit" name="btnSubmit" type="hidden" value="" /></td>
            <td align="right" class="style2" colspan="2">
            <input name="submitValue" type="submit" value="Back" onclick="$('#btnSubmit').attr('value','Back');"   style="width: 5em" />
            <input name="submitValue" type="submit" value="Save" onclick="$('#btnSubmit').attr('value','Save');"   style="width: 5em" /></td>
        </tr>
        <tr>
         <td align="right" ></td>
          <td class="style2"><span id="errorMessage" style="color:Red;"><%= ViewData["ErrorMessage"].ToString()%></span></td>
          <td>&nbsp;</td>
        </tr>
     </table>
    <%}%>