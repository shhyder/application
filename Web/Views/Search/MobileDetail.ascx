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
    DSParameter.DistributorRow disRow = (DSParameter.DistributorRow)ds.Distributor.Select(" Distributor_ID = " + ViewData["Distributor_ID"].ToString())[0];
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
                        <legend class="editor-field"><b class="editor-field"><%= disRow.Distributor %></b></legend>
                        
                        <div class="editor-field">
                            <b class="editor-field">Address : </b><%= disRow.Address %>
                        </div>
                        <%if (!disRow.IsCityNull())
                          {%>
                            <div class="editor-field">
                                <b class="editor-field">City : </b><%= disRow.City.ToUpper()%>
                            </div>
                        <%} %>
                        <%if (!disRow.IsPhone1Null()  )
                          {
                              if (disRow.Phone1.Trim().Length != 0)
                              {%>
                                 <div class="editor-field"><b class="editor-field">Phone1 : </b>
                                      <%if (Web.Model.Utility.IsNumeric(disRow.Phone1))
                                      {%>
                                           <%=  Convert.ToDouble(disRow["Phone1"]).ToString("(###) ###-####")%>
                                      <%}
                                      else {%>
                                            <%= disRow.Phone1%>
                                      <%} %>
                                </div>
                              <%} %>
                        <%} %>
                         <%if (!disRow.IsPhone2Null())
                           {
                               if (disRow.Phone2.Trim().Length != 0)
                              {%>
                              
                                    <div class="editor-field"><b class="editor-field">Phone2 : </b>
                                      <%if (Web.Model.Utility.IsNumeric(disRow.Phone2))
                                      {%>
                                           <%=  Convert.ToDouble(disRow["Phone2"]).ToString("(###) ###-####")%>
                                      <%}
                                      else {%>
                                            <%= disRow.Phone1%>
                                      <%} %>
                                </div>
                         <%} %>
                        <%} %>
                        <%if ( !disRow.IsEmailNull())
                          {
                              if (disRow.Email.Trim().Length != 0)
                              {%>
                        <div class="editor-field">
                            <b class="editor-field">Email : </b><a href="mailto:<%=  disRow.Email %>"><%= disRow.Email%></a>  
                        </div>
                         <%} %>
                        <%} %>
                        <%if (!disRow.IsWebsiteNull())
                          {
                              if (disRow.Website.Trim().Length != 0)
                              {%>
                        <div class="editor-field">
                            <b class="editor-field">Website : </b><a href="http://<%=  disRow.Website %>" target="_blank"><%= disRow.Website%></a>
                        </div>
                         <%} %>
                        <%} %>
                       <div class="editor-field">
                            <b class="editor-field">Distance : </b><%= String.Format("{0:N0}", ViewData["distance"]) + " miles"%>
                        </div>
                        
                        <div style="font-family:Arial, Helvetica, sans-serif; font-size: x-small; color:#F60;" >
                            <b class="editor-field">Available products : </b>
                            <% 
                                int distributor_ID = disRow.Distributor_ID;
                                DataRow[] rows = ds.Product_Distributor.Select(" Distributor_ID = " + distributor_ID.ToString() );
                               
                                string _avl_Products = "";
                                foreach (DataRow row in rows)
                                {
                                 
                                    if (ds.Product_Distributor.Select(" Distributor_ID = " + distributor_ID.ToString() + " and Product_ID in ( " + ViewData["product_IDs"].ToString() + ") and Product_ID = " + row["Product_ID"].ToString()).Length == 0)
                                    {
                                        _avl_Products += ds.Product.FindByProduct_ID(Convert.ToInt32(row["Product_ID"])).Product + ", ";
                                    }
                                    else
                                    {
                                        _avl_Products += "<b>" + ds.Product.FindByProduct_ID(Convert.ToInt32(row["Product_ID"])).Product + "</b>, ";
                                    }
                                    
                                }
                            %>
                            <%= _avl_Products %>
                        </div>
                        
                      </fieldset>
                </div>
                </span>
                </td>
               <td></td>

            </tr>
    </table>
    </td>
    </tr>
    </table>  
