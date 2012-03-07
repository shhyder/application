<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<script type="text/javascript">
</script>
<% using (Ajax.BeginForm("Home", "Home", new AjaxOptions { UpdateTargetId = "pnlPersonal", InsertionMode = InsertionMode.Replace, OnSuccess = "Done" }, new { autocomplete = "off", ID = "myForm", Name = "myForm" }))
     { %>
     <br />
     <br />
     <p>
     <h2>Product search</h2>
     </p>
     
     <p>
     <b>To have a product search, you need to provide your zipcode, which enable us to </b>
     </p>
     <p>provide with your Jatai International Stores.</p>
     <p>
      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<label>Zip Code: </label>
    <%= Html.TextBox("txtZipcode","", new {  Style = "width: 270px;"  })%>
    <span id="errorMessage" style="color:Red;"><%= ViewData["txtErrorMessage"].ToString()%></span>
   <br /><br />
        &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp;  
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <input name="submitValue" type="submit" value="Search"  class="wpcf7-submit"    style="width: 7em" />
    </p>
    <p>
    ;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp
    ;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp
    ;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp<a href="<%=  Url.Content("~/SearchProduct/Index" )%>">View all Products</a>
    </p>
    
    
    <br />
    <br />
    <br />
    <br />
    <br />
<%}%>