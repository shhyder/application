<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/App.Master" Inherits="System.Web.Mvc.ViewPage" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Customer
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
       
    <script src='<%=  Url.Content("~/Pro_files/jquery_002.js")%>' type="text/javascript"></script>
    <script src='<%=  Url.Content("~/Pro_files/Awesome.js")%>' type="text/javascript"></script>
    
    <script src='<%=  Url.Content("~/MapJS/Store.js")%>' type="text/javascript"></script>
    <link id="Link1" href='<%=  Url.Content("~/Pro_files/jquery-ui.css")%>' rel="stylesheet" type="text/css"/>
    <script src='<%=  Url.Content("~/Pro_files/jquery.js")%>' type="text/javascript"></script>
    <script src='<%=  Url.Content("~/Pro_files/jquery-ui.js")%>' type="text/javascript"></script>
    <script src='<%=  Url.Content("~/Pro_files/jquery_002.js")%>' type="text/javascript"></script>
    <script src='<%=  Url.Content("~/Pro_files/Awesome.js")%>' type="text/javascript"></script>
    <script src='<%=  Url.Content("~/Pro_files/style.js")%>' type="text/javascript"></script>
    
    <script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=false"></script>
<script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?libraries=places&sensor=false"></script>


<table width="100%">
        <tr>
        <td align="left">
            &nbsp;&nbsp;<a onclick="javascript:AddLoading(this);" href="<%=  Url.Content("~/Product/Index")%>">Product Search</a>
            &nbsp;&nbsp;<a onclick="javascript:AddLoading(this);" href="<%=  Url.Content("~/Analysis/Index")%>">Analysis</a>
            &nbsp;&nbsp;<a onclick="javascript:AddLoading(this);" href="<%=  Url.Content("~/Category/Index")%>">Category</a>
        </td>
        <td align="right">
            <% Html.RenderPartial("LogOnUserControl");%>
        </td>
        </tr>
</table>
    <div id="StoreList" >
        <% Html.RenderPartial("List");%>
    </div>
     <script type="text/javascript">
     var lastpage = 0;
     function GetList(page) {
         var queryString = jQuery('#formStoreSearch').formSerialize();
         jQuery('.gridContent').fadeTo('slow', 0.5, function() { });
         lastpage = page;
         jQuery.ajax({
             url: "<%= Web.Model.Utility.Get_Path() %>/Customer/GetList/" + page,
             dataType: 'json',
             type: "POST",
             data: queryString,
             success: function(json) {
                 jQuery('.gridContent').remove();
                 jQuery(json.StoreList).insertAfter('#StoreGrid');
                 json = null;
                 jQuery('.gridContent').fadeTo('slow', 1.0, function() { });
             }
         });
     }



     jQuery("input[type='text']").keyup(function() {
         GetList(1);
     });

     jQuery("select").change(function() {
         GetList(1);
     });

     jQuery("input, [checkbox]").click(function(event) {
         GetList(1);
     });
     
</script>
    
  
  
 <script type="text/javascript">



 function OnNew() {
     var queryString = "";
     var id = 1;
   
     ae_popup('<%= UIStore.dlgStore.ToString() %>', 650, 500, 'Store', true, 'center', true,
        { "Save": function() { OnSave(); }, "Cancel": function() { $(this).dialog('close'); } },
        false);
     lpf<%= UIStore.dlgStore.ToString() %> = true;

     $.get("<%= Web.Model.Utility.Get_Path() %>/Customer/New/" + id,
            NewStore
            );
        
    }
    
    
    function NewStore(data) {

        lpf<%= UIStore.dlgStore.ToString() %> = null;
        //style = "width: auto; min-height: 0px; height: 150px; "

        $("#<%= UIStore.dlgStore.ToString() %>").html(data);

        jQuery('#<%= UIStore.dlgStore.ToString() %>').attr("Style", "width: 600px; min-height: 300px; height: 500px;");
        jQuery("#<%= UIStore.dlgStore.ToString() %>").dialog('open');
        jQuery("#<%= UIStore.dlgStore.ToString() %> form input:visible:first").focus();
        SetAddress();  
        
       
    }
    
    
    function OnSave()
    {
         var queryString = $('#<%= UIStore.formStore.ToString() %>').serialize();
         
         $.getJSON("<%= Web.Model.Utility.Get_Path() %>/Customer/Save", queryString, function(data) {
                    
                    if( data.Has_Error )
                    {
                        $('#CodeError').html(data.CodeError );
                        $('#StoreError').html(data.StoreError);
                        $('#BuildingError').html(data.BuildingError);
                        $('#AddressError').html(data.AddressError);
                        $('#FullAddressError').html(data.FullAddressError);
                        $('#CityError').html(data.CityError);
                        $('#Email1Error').html(data.Email1Error);
                        $('#Email2Error').html(data.Email2Error);
                        $('#Phone1Error').html(data.Phone1Error);
                        $('#Phone2Error').html(data.Phone2Error);
                        $('#WebsiteError').html(data.WebsiteError);
                    }
                    else
                    {
                        alert("Store has been saved");
                        $('#<%= UIStore.dlgStore.ToString() %>').dialog('close'); 
                        GetList(lastpage );
                    }
                    
                });
    
    
    }
    
    
    
    function OnEdit(id) {
    
     ae_popup('<%= UIStore.dlgStore.ToString() %>', 650, 500, 'Store', true, 'center', true,
        { "Save": function() { OnSave(); }, "Cancel": function() { $(this).dialog('close'); } },
        false);
     lpf<%= UIStore.dlgStore.ToString() %> = true;

     $.get("<%= Web.Model.Utility.Get_Path() %>/Customer/Edit/" + id ,
            EditStore
            );
        
    }
    
    
    function EditStore(data) {

        lpf<%= UIStore.dlgStore.ToString() %> = null;
        $("#<%= UIStore.dlgStore.ToString() %>").html(data);
        jQuery('#<%= UIStore.dlgStore.ToString() %>').attr("Style", "width: 600px; min-height: 300px; height: 500px;");
        jQuery("#<%= UIStore.dlgStore.ToString() %>").dialog('open');
        jQuery("#<%= UIStore.dlgStore.ToString() %> form input:visible:first").focus();
        SetAddress();  
    }
    
    
    
    function OnDelete(codeSLSP,SalesRepCode,Territory )
    {
         var str = codeSLSP + "/" + SalesRepCode + "/" + Territory; 
       
         $.getJSON( pfxURL.staticVar + "/Store/Delete/" + str , "", function(data) {
                    if( data.Has_Error )
                    {
                       
                    }
                    else
                    {
                        alert("Store Has been deleted");
                        GetList(lastpage );
                    }
                    
                });
    
    
    }
    
    
    
    
     jQuery("#map-pane").ajaxStart(function() {
         var width = jQuery(this).width();
         var height = jQuery(this).height();
         jQuery("#map-loading").css({
             top: ((height / 2) - 25),
             left: ((width / 2) - 50)
         }).fadeIn(200);    // fast fade in of 200 mili-seconds
     }).ajaxStop(function() {
         jQuery("#map-loading", this).fadeOut(1000);    // slow fade out of 1 second
     });
    
    
    
    function SetCustomerStatus(ele,id) {
        str = '<img  src="' + pfxURL.staticVar + "/images/ajax-loader_Upper.gif" + '"  />' + ele.innerHTML;
        ele.innerHTML = str;
        status = "";
        
        if( ele.innerHTML.indexOf("De-Activate") != -1 )
        {
              status = false; 
        }
        else
        {
              status = true;
        }
        
        jQuery.ajax({
            url: pfxURL.staticVar + "/Customer/SetCustomerStatus/"+ id + "/" + status,
            dataType: 'json',
            type: "POST",
            error: function(json) {
                if( ele.innerHTML.indexOf("De-Activate") != -1 )
                {
                    ele.innerHTML = "De-Activate";
                }
                else
                {
                     ele.innerHTML = "Activate";
                }
                
                alert("Operation failed");
            },
            success: function(json) {
                if( ele.innerHTML.indexOf("De-Activate") != -1 )
                {
                    ele.innerHTML = "Activate";
                }
                else
                {
                     ele.innerHTML = "De-Activate";
                }
            
                //alert("Product Store from AccPacc to display product/n has been updatd");
            }
        });
    }
    

</script>











  
  </asp:Content> 