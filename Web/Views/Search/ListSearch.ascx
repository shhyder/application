<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.Collections.Generic"  %>
<%@ Import Namespace="System.Data"  %>
<%@ Import Namespace="System"  %>
<%@ Import Namespace="DataSet"  %>
<script src='<%=  Url.Content("~/Scripts/MicrosoftAjax.js")%>' type="text/javascript"></script>
<script src='<%=  Url.Content("~/Scripts/MicrosoftMvcAjax.js")%>' type="text/javascript"></script>
<%
    int grid_No = 1;
    string contentStyle = "";
    int id = 1;
%>
   
    
    <% if( ViewData[UIDealerSearch.listDealer.ToString()] != null  ) 
       {%>
    
    <table  width="350px" style="border-spacing:0px;" >
    <% string style = "background:White";%>
    <% string querystring = ""; %>
    <% int sr_No = Convert.ToInt32( ViewData["start"] ) ;  %>
   
  
    
    <% foreach (var consumer in (ViewData[UIDealerSearch.listDealer.ToString()] as List<DataRow>))
       { %>
            <% if (grid_No % 2 == 0)
               {
                   contentStyle = "gridContentStyle";
               }
               else
               {
                   contentStyle = "gridAlternateContentStyle";
               }
               grid_No++;
                ;

                querystring = "strLat=" + ViewData["startLatitude"].ToString() + "&strLog=" + ViewData["startLongitude"].ToString() + "&endLat=" + consumer["Latitude"].ToString() + "&endLog=" + consumer["Longitude"].ToString() + "&id=" + consumer["Distributor_ID"].ToString() + "&dist=12000" + ViewData["queryString"].ToString();
               querystring = HttpUtility.UrlEncode(querystring);

            
               System.Web.HttpUtility.HtmlEncode("");
           
            %>
            
            <tr  onmouseover="javascript:itemMouseOver(<%= id - 1 %>,this)" onmouseout="javascript:itemMouseOut(<%= id - 1 %>,this)" onclick="javascript:itemMouseClick(<%= id - 1 %>)">
                <td colspan="2" class="<%=contentStyle %>" style="padding: 0.5em  0.5em 0em  0.5em" >
                    <img style="border: 0; vertical-align: text-bottom;" src='<%=  Url.Content("~/MarkerIcon.ashx?label=" + sr_No.ToString() )%>' alt="" border="0" >
                    <span style="display: inline-block;vertical-align:top; padding: 0.5em 1em 0em 0em; "><b><%= consumer["Distributor"].ToString() %></b></span>  <br />
                    <%= consumer["Address"].ToString()%><br />
                    <%-- <%= consumer["City"].ToString().ToUpper()%><br />--%>
                    <% if( Convert.ToBoolean( consumer["Is_Phone1_Display"]    ) ){%>
                        <%if(  Web.Model.Utility.IsNumeric( consumer["Phone1"].ToString() )  )
                          {%>
                               <%=  Convert.ToDouble(consumer["Phone1"]).ToString("(###) ###-####") %><br />
                          <%}
                          else {%>
                                <%= consumer["Phone1"].ToString()%>
                          <%} %>
                    <%} %>
                </td>
            </tr>
            <tr style=" background:LightGray;" onmouseover="javascript:itemMouseOver(<%= id - 1 %>,jQuery(this).prev())" onmouseout="javascript:itemMouseOut(<%= id - 1 %>,jQuery(this).prev())" onclick="javascript:itemMouseClick(<%= id - 1 %>)">
                <td style="text-align: left;padding: 0.5em  0.5em 0em  0.5em" ><%= Convert.ToInt32( consumer["distance"] ).ToString()  + " miles"%></td>
                <td style="text-align: right;padding: 0.5em 0.5em 0em  0.5em"><a href="javascript:getDirection(<%= consumer["Latitude"].ToString() %>,<%= consumer["Longitude"].ToString() %>,<%= id - 1  %>)" >Directions and Jatai Products</a></td>
            </tr>
             <% 
                   id++;
                   sr_No++;
             %>
    <% } %>
   
   
    <tr>
         <td colspan="2" class="footer" style="text-align: right;padding:  0.5em  0.5em 0.5em  0.5em" >
         <% Html.RenderPartial("Navigator");%>
        </td>
    </tr>
    
    
    
    <%if (Convert.ToInt16(ViewData["Count"]) == 0)
    {%>
        <tr>
            <td colspan="2" class="gridAlternateContentStyle" >
            <p style="text-align:center; font-size:x-large">No store found</p>
            </td>
        </tr>
    <%} %>
    </table>
    
    <%} %>
    
 