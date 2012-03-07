<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
     
</head>

<script type="text/javascript" src='<%=  Url.Content("~/Advanced Search_files/jquery.js")%>'></script>
	<script type="text/javascript" src='<%=  Url.Content("~/Advanced Search_files/prototype.js")%>'></script>
	<script type="text/javascript" src='<%=  Url.Content("~/Advanced Search_files/slider.js")%>'></script>
	

    <link id="siteThemeLink" href='<%=  Url.Content("~/Pro_files/jquery-ui.css")%>' rel="stylesheet" type="text/css">
<script src='<%=  Url.Content("~/Pro_files/jquery_002.js")%>' type="text/javascript"></script>
<script src='<%=  Url.Content("~/Pro_files/Awesome.js")%>' type="text/javascript"></script>
<script src='<%=  Url.Content("~/Pro_files/style.js")%>' type="text/javascript"></script>
	<link rel="stylesheet" type="text/css" media="all" href='<%=  Url.Content("~/Advanced Search_files/multiSelect.css")%>'>
	<meta http-equiv="expires" content="-1">
	<meta http-equiv="pragma" content="no-cache">
	<meta http-equiv="cache-control" content="no-cache">
<script type="text/javascript">
   

    function press() {

        var queryString = jQuery('#searchForm').formSerialize(true);

        jQuery.post("<%= Web.Model.Utility.Get_Path() %>/Search/DealerCount/" , queryString, UpdateDealerLocatorCount
        );
}
    
   
    
    
    function UpdateDealerLocatorCount(data) {
        
        if (data.toString().length == 0) {
            jQuery("#ctl01_upError").html("Enter valid US Zipcode");
            return;
        }
        else {
            jQuery("#ctl01_upError").html("");
        }

        if (data.toString() == "0") {
            jQuery("#ctl01_upError").html("No store has been found");
            return;
        }

        
        var queryString = jQuery('#searchForm').formSerialize(true)


        window.location.href = "index.aspx";
        window.close();
        var str = "<%= Web.Model.Utility.Get_Path() %>/SearchDealer/Index/1" + "?" + queryString;
        window.opener.GetDealerList(str);    //.location.href = "<%= Web.Model.Utility.Get_Path() %>/SearchDealer/Index/1" + "?" + queryString;
        
    }


    function resetSearch() {
        if (confirm("Reset this form?")) {
            jQuery('input[type=checkbox]').removeAttr('checked');
        }
    }
</script>


<body>
     <form id="searchForm" name="searchForm" method="post" action="./Advanced Search_files/Advanced Search.htm" >
     <input name="<%= ViewData["productID"].ToString()  %>" type="hidden" id="<%= ViewData["productID"].ToString()  %>" value="<%= ViewData["productID"].ToString()  %>">
     <div >
							                    <div id="ctl01_upLocation">
                    		
                    									
                    <h2>Find dealers located</h2>
                    <select id="cobDistance" name="cobDistance" >
                        <option value="25">25 miles</option><option value="50">50 miles</option><option value="75">75 miles</option><option value="100">100 miles</option><option value="250" selected="">250 miles</option><option value="500">500 miles</option><option value="12000">any distance</option>
                    </select><br />
                    from ZIP&nbsp;<br />
                    <span id="ctl01_ucLocation_upZip">
	                    <input type="text" name="txtzipcode" id="txtzipcode" maxlength="5" onkeypress="javascript:isNumberKey(event);" size="8" onkeyup="if ((isNumberKey(event)) &amp;&amp; (this.value.length==5)) { this.blur(); }" onfocus="this.select();" onblur="SetZip();" value="">
                    </span>
                            <br />
                    			&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type=button value="Search" onclick="javascript:press();" />					
	                    </div>
							                    <div id="ctl01_upError" class="error">
                    		
								                    &nbsp;
                    							
	                    </div>
							                    <div id="ctl01_upResults">
                    		
                    									
                    
                    								
	                    </div>
						                    </div>
	 </form>
</body>
</html>
