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
%>
<% using (Ajax.BeginForm("List", "SearchDealer", new AjaxOptions { UpdateTargetId = "mainSearchPanel", InsertionMode = InsertionMode.Replace }, new { autocomplete = "off" }))
     { %>
    <table width="100%" id = "toolTip_Container"  style="border-style: none;">
   <tr>
    <td >
    
    <table width="100%" >
    <% string style = "background:White";%>
    <% string querystring = ""; %>
    <% foreach (var consumer in (ViewData[UIDealerSearch.listDealer.ToString()] as List<DataRow>))
       { %>
            <tr>
               
               <td colspan="4" valign="middle">
               <p>
               <span class="text-main-products">
                <div>
                    <fieldset>
                        <legend><b><%= consumer["Distributor"].ToString() %></b></legend>
                        
                        <div class="editor-label">
                            <b>Address : </b><%= consumer["Address"].ToString() %>
                        </div>
                        <%if (consumer["Phone1"].ToString().Trim().Length > 0)
                          {%>
                        <div class="editor-label">
                            <b>Phone1 : </b><%= consumer["Phone1"].ToString()%>
                        </div>
                        <%} %>
                         <%if (consumer["Phone2"].ToString().Trim().Length > 0)
                          {%>
                        <div class="editor-label">
                            <b>Phone2 : </b><%= consumer["Phone2"].ToString() %>
                        </div>
                        <%} %>
                        <%if (consumer["Email"].ToString().Trim().Length > 0)
                          {%>
                        <div class="editor-field">
                            <b>Email : </b><a href="mailto:<%= consumer["Email"].ToString() %>"><%= consumer["Email"].ToString() %></a>  
                        </div>
                        <%} %>
                        <%if (consumer["Website"].ToString().Trim().Length > 0)
                          {%>
                        <div class="editor-field">
                            <b>Website : </b><a href="http://<%=  consumer["Website"].ToString()%>" target="_blank"><%= consumer["Website"].ToString()%></a>
                        </div>
                         <%} %>
                        <div class="editor-field">
                            <b>Distance : </b><%=  Convert.ToInt32( consumer["distance"] ).ToString()  + " miles"%>
                        </div>
                        
                        <% querystring = "strLat=" + ViewData["startLatitude"].ToString() + "&strLog=" + ViewData["startLongitude"].ToString() + "&endLat=" + consumer["Latitude"].ToString() + "&endLog=" + consumer["Longitude"].ToString() + "&id=" + consumer["Distributor_ID"].ToString() + "&dist=" + consumer["Distance"].ToString(); %>
                        <% querystring = HttpUtility.UrlEncode( querystring ); %>
                        <div style="font-family:Arial, Helvetica, sans-serif; font-size: x-small; color:#F60;" >
                            <b>Available products : </b>
                            <% 
                                int distributor_ID = Convert.ToInt32( consumer["Distributor_ID"].ToString() );
                                //DataRow[] rows = ds.Product_Distributor.Select(" Distributor_ID = " + distributor_ID.ToString() + " and Product_ID in ( " + ViewData["product_IDs"].ToString()  + ")" );
                                DataRow[] rows = ds.Product_Distributor.Select(" Distributor_ID = " + distributor_ID.ToString() );
                               
                                string _avl_Products = "";
                                foreach (DataRow row in rows)
                                {
                                    //_avl_Products += ds.Product.FindByProduct_ID(Convert.ToInt32(row["Product_ID"] )).Product + ", ";

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
                        
                        <a href="<%=  Url.Content("~/SearchDealer/Route?" +  querystring)%>">Show Map</a>
                        &nbsp;&nbsp;<a href="<%=  Url.Content("~/SearchDealer/Route?" +  querystring)%>">Show Dealers all products</a>
                    </fieldset>
                </div>
                </span></p></td>
               <td><img src="../images/spacer.gif" alt="" border="0" width="1" height="88"></td>

            </tr>
    <% } %>
    <tr>
            <td colspan="4" align="right">
                <br />
                <% if ((string)ViewData["pageLinks"] != "" && Convert.ToInt16(ViewData["Count"]) > 0)
                   { %>
           <%= ViewData["pageLinks"]%>
           <br /><br />
        <% } %>
            </td>
       </tr>
    </table>
    <%if (Convert.ToInt16(ViewData["Count"]) == 0)
    {%>
        No Product type is found in this search
    <%} %>
    </td>
    </tr>
    </table>  
    <%}%>