<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="System.Collections.Generic"  %>
<%@ Import Namespace="System.Data"  %>
<%@ Import Namespace="System"  %>
<%@ Import Namespace="DataSet"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Store Information
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%
        DSParameter ds = (DSParameter)System.Web.HttpContext.Current.Cache["data"];
    %>
    <% string querystring = ""; %>
    <p>
        <%= Html.ActionLink("Search", "Index", "Search")%>
    </p>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN"
  "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd"> 
<!--
 Copyright 2008 Google Inc. 
 Licensed under the Apache License, Version 2.0: 
 http://www.apache.org/licenses/LICENSE-2.0 
 --> 
<html xmlns="http://www.w3.org/1999/xhtml"> 
  <head> 
    <meta http-equiv="content-type" content="text/html; charset=utf-8"/> 
    <title>Google Maps Street View API Driving Directions Example</title> 
    
	<script src="http://maps.google.com/maps?file=api&amp;v=2&amp;sensor=false&amp;key=ABQIAAAAj2HTBsK69zEZ11d7jUHdihSiO7jbSln7tVRlrw_H_WOVZhGCZxTd3hHSIWqxt3OhG1vl4bR5hp25xA" type="text/javascript"></script>

   
    
  </head> 
  <% string imagelink = "http://maps.google.com/maps/api/staticmap?center=" + ViewData["endLatitude"].ToString() + "," + ViewData["endLongitude"].ToString() + "&zoom=15&size=1200x1200&maptype=roadmap&" +
         "markers=size:mid%7Ccolor:green%7Clabel:D%7C" + ViewData["endLatitude"].ToString() + "," + ViewData["endLongitude"].ToString() + "&sensor=false";  %>
   <body > 
    <div id="content"> 
    <p>
               <span class="text-main-products">
                <div>
                    <fieldset>
                    
                       <%-- <% if(  Session["QSCurDL"]  != null  )
                        {%>
                               <a href="<%=  Url.Content("~/SearchDealer/Index?" +  Session["QSCurDL"].ToString())%>">Back</a>
                        <%}%>--%>
                        <legend><b><%= ViewData["Distributor"].ToString()%></b></legend>
                        
                        <div class="editor-label">
                            <b>Address : </b><%= ViewData["Address"].ToString() %>
                        </div>
                        <%if (ViewData["Phone1"].ToString().Trim().Length > 0)
                          {%>
                        <div class="editor-label">
                            <b>Phone1 : </b><%= ViewData["Phone1"].ToString()%>
                        </div>
                        <%} %>
                         <%if (ViewData["Phone2"].ToString().Trim().Length > 0)
                          {%>
                        <div class="editor-label">
                            <b>Phone2 : </b><%= ViewData["Phone2"].ToString()%>
                        </div>
                        <%} %>
                        <%if (ViewData["Email"].ToString().Trim().Length > 0)
                          {%>
                        <div class="editor-field">
                            <b>Email : </b><a href="mailto:<%= ViewData["Email"].ToString() %>"><%= ViewData["Email"].ToString()%></a>  
                        </div>
                        <%} %>
                        <%if (ViewData["Website"].ToString().Trim().Length > 0)
                          {%>
                        <div class="editor-field">
                            <b>Website : </b><a href="<%= ViewData["Website"].ToString()%>" target="_blank"><%= ViewData["Website"].ToString()%></a>
                        </div>
                         <%} %>
                          <% querystring = HttpUtility.UrlEncode( querystring ); %>
                        <div class="editor-field">
                            <b>Available products : </b>
                            <% 
                                int distributor_ID = Convert.ToInt32(ViewData["Distributor_ID"].ToString());
                                DataRow[] rows = ds.Product_Distributor.Select(" Distributor_ID = " + distributor_ID.ToString() + " and Product_ID in ( " + ViewData["product_IDs"].ToString()  + ")" );
                                string _avl_Products = "";
                                foreach (DataRow row in rows)
                                {
                                    _avl_Products += ds.Product.FindByProduct_ID(Convert.ToInt32(row["Product_ID"] )).Product + ", ";
                                }
                            %>
                            <%= _avl_Products %>
                        </div>
                         
                        
                    </fieldset>
                </div>
                </span>
      </p>
    
    <p>
        <img border="0" src="<%= imagelink %>" alt="<%= ViewData["Distributor"].ToString()%>" />
    </p>
    
     </div> 
  </body> 
</html> 


</asp:Content>


