<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
    string url = Web.Model.Utility.Get_Path() + ViewData["Item_Link"].ToString();
 %>
 
<fb:like href="<%= url  %>" send="true" height="35" width="300" show_faces="true"></fb:like>
<script src="//platform.twitter.com/widgets.js" type="text/javascript"></script>



<!-- Add the following three tags to your body -->
<%--<span itemprop="name">Title of your content</span>
<span itemprop="description"></span>--%>



<link rel="canonical" href="<%= url  %>" />





<a url="<%= url  %>"  href="https://twitter.com/share" class="twitter-share-button">Tweet</a>
<div id="gPlus-root">
    <%--<g:plusone annotation="inline" href="<%= url  %>"></g:plusone>--%>
</div>

<fb:comments href="<%= url  %>" num_posts="6" width="600"></fb:comments>
