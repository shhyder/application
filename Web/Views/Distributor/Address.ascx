<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<script src='<%=  Url.Content("~/Scripts/MicrosoftAjax.js")%>' type="text/javascript"></script>
<script src='<%=  Url.Content("~/Scripts/MicrosoftMvcAjax.js")%>' type="text/javascript"></script>

<script type="text/javascript">
    function textChanged() {
        $("#myForm").each(function() {

            var $that = $(this);
            $that.find("input[type='image'],input[type='submit'],input[type='text']").attr('disabled', 'disabled');
            $that.find("input[type='submit'],input[type='text']").css('color', 'InactiveCaption');
            $that.find("input[type='submit'],input[type='text']").css('font-weight:', 'bold');
           
        });



    }

    //loadmap();
</script>
<% using (Ajax.BeginForm("Distributor", "Distributor", new AjaxOptions { UpdateTargetId = "pnlPersonal", InsertionMode = InsertionMode.Replace, OnSuccess = "Done" }, new { autocomplete = "off", ID = "myForm", Name = "myForm" }))
     { %>
     <br />
     <br />
     <p>
     <h2>Store registeration</h2>
     </p>
     
     <p>
     <b>It's free and you'll receive a firm proposal within 24 hours!</b>
     </p>
     <p>Type your business zip code in given box <b>example 90066</b>, and select from suggested list</p>
     <p>
      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<label>Address: </label>
    <%= Html.TextBox("address22","", new {  Style = "width: 270px;"  })%>
    <span id="errorMessage" style="color:Red;"><%= ViewData[UIDistributor.txtErrorMessage.ToString()].ToString()%></span>
    <%= Html.Hidden(UIDistributor.hidLatitude.ToString())%>
    <%= Html.Hidden(UIDistributor.hidLongitude.ToString())%> <br /><br />
        &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <input name="submitValue" type="submit" value="Register"  class="wpcf7-submit"    style="width: 7em" />
    </p>
    
    
    
    <br />
    <br />
    <br />
    <br />
    <br />
<%}%>