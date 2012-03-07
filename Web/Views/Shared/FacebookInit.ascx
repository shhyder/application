<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<script src="//connect.facebook.net/en_US/all.js"></script>
<script>
	    FB.init({
	        appId: '171952196223634',
	        status: false, // check login status
	        cookie: false, // enable cookies to allow the server to access the session
	        oauth: true, // enable OAuth 2.0
	        xfbml: false  // parse XFBML
	    });
</script>