<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	BrowserCompatibility
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="fullscreen">
<table width="100%" height="100%">
	<tr>
    	<td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
    
    <tr>
    	<td>&nbsp;</td>
        <td align="center">
        
        
        <h2>Browser compatibility issue:</h2>
    <p>
        Your browser <%= Request.Browser.Browser%> version <%=  Request.Browser.Version.Split('.')[0].ToString()  %> does not <br />
        supports features, mandatory for this application
    </p>
    
    <h3>Compatible Browers</h3>
    <ul>
        <li>Firefox 4+</li>
        <li>Safari 5+</li>
        <li>Google Chrome</li>
        <li>Opera 10+</li>
    </ul>
    
    
    <p> <span style="color:Red;"> <b>Note: </b> Application does not supports IE browser.</span> </p>

    
        
        
        
    &nbsp;</td>
        <td>&nbsp;</td>
    </tr>
    
    <tr>
    	<td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
    </tr>


</table>
</div>
</asp:Content>
