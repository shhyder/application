<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<%@ Import Namespace="System.Collections.Generic"  %>
<%@ Import Namespace="System.Data"  %>
<%@ Import Namespace="System"  %>
<%@ Import Namespace="DataSet"  %>
<script src='<%=  Url.Content("~/Scripts/MicrosoftAjax.js")%>' type="text/javascript"></script>
<script src='<%=  Url.Content("~/Scripts/MicrosoftMvcAjax.js")%>' type="text/javascript"></script>
<div id="divLoading"></div>
<%
    DSParameter ds = (DSParameter)System.Web.HttpContext.Current.Cache["data"];
    DSParameter.EventRow disRow = (DSParameter.EventRow)ds.Event.Select(" Event_ID = " + ViewData["Event_ID"].ToString())[0];
    
    
    string period = "";
    if( disRow.Event_Days == 1  )
    {
        period = " On " + disRow.Date.ToString("dd MMMM yyyy");
    }
    else
    {
        period = disRow.Date.ToString("dd MMMM yy") + " - " + disRow.Date.AddDays( 5 ).ToString("dd MMMM yyyy");
    }
    
%>


<%if (ViewData["Is_Direct_Link"] != null )
{%>
<script>

    jQuery(document).ready(function() {

        endLatitude = <%= disRow.Latitude%>;
        endLongitude = <%= disRow.Longitude%>;

        FB.XFBML.parse(document.getElementById('fb-root'));
        gapi.plusone.render("gPlus-root");
    });
</script>
<%} %>

    <table width="100%"  style="border-style: none;">
   <tr>
    <td >
    
    <table width="100%" >
    <% string style = "background:White";%>
    <% string querystring = ""; %>
   
            <tr>
               
               <td colspan="4" valign="middle">
               
               <span class="text-main-products">
                <div>
                    <fieldset>
                        <legend class="editor-field"><b class="editor-field"><%= disRow.Event %></b></legend>
                        
                        <div class="editor-field">
                            <b class="editor-field">Venue : </b><%= disRow.Address %>
                        </div>
                        <div class="editor-field">
                            <%= period %>
                        </div>
                        <%if (ViewData["Is_Direct_Link"] == null)
                        {%>
                       <div class="editor-field">
                            <b class="editor-field">Distance : </b><%= String.Format("{0:N0}", ViewData["distance"]) + " miles"%>
                        </div>
                      </fieldset>
                      <%} %>
                </div>
                </span>
                </td>
               <td></td>

            </tr>
    </table>
    </td>
    </tr>
    </table>  

