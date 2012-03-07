<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/App.Master" Inherits="System.Web.Mvc.ViewPage" %>



<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<link id="Link2" href='<%=  Url.Content("~/Pro_files/jquery-ui.css")%>' rel="stylesheet" type="text/css"/>
    <script src='<%=  Url.Content("~/Pro_files/jquery.js")%>' type="text/javascript"></script>
    <script src='<%=  Url.Content("~/Pro_files/jquery-ui.js")%>' type="text/javascript"></script>
    <script src='<%=  Url.Content("~/Pro_files/style.js")%>' type="text/javascript"></script>
   
     
    
<script src="//ajax.googleapis.com/ajax/libs/jquery/1.6.1/jquery.min.js"></script>
<script src="//ajax.googleapis.com/ajax/libs/jqueryui/1.8.13/jquery-ui.min.js"></script>
<script src="//ajax.aspnetcdn.com/ajax/jquery.templates/beta1/jquery.tmpl.min.js"></script>


<script src='<%=  Url.Content("~/Pro_files/Awesome.js")%>' type="text/javascript"></script>
<link id="Link1" href='<%=  Url.Content("~/Pro_files/jquery-ui.css")%>' rel="stylesheet" type="text/css"/>
<script src='<%=  Url.Content("~/Pro_files/jquery_002.js")%>' type="text/javascript"></script>


   


<table width="100%" >
        <tr>

        <td align="left" width="60%">
            &nbsp;<a onclick="javascript:AddLoading(this);" href="<%=  Url.Content("~/Store/Index")%>">Store Search</a> 
            &nbsp;&nbsp;<a onclick="javascript:AddLoading(this);" href="<%=  Url.Content("~/Analysis/Index")%>">Analysis</a>
            &nbsp;&nbsp;<a  href="##" onclick="javascript:UpdateProductStoreFromAccPac(this);" title="Upadate Store status from AccPac's database">Update Product Store</a>
            &nbsp;&nbsp;<a  href="##" onclick="javascript:ProductWebsiteViewStatus(this);" title="Update Product status from AccPac's database" >Update Product Status</a>
            &nbsp;&nbsp;<a onclick="javascript:AddLoading(this);" href="<%=  Url.Content("~/Category/Index")%>">Category</a>
        </td>
        <td align="right" width="35%">
            <% Html.RenderPartial("LogOnUserControl");%>
        </td>
        </tr>
</table>





    <div id="ProductList" >
        <% Html.RenderPartial("List");%>
    </div>
     <script type="text/javascript">
         var lastpage = 0;
     function GetList(page) {



         var queryString = jQuery('#formProductSearch').formSerialize();

     
         jQuery('.gridContent').fadeTo('slow', 0.5, function() { });
         lastpage = page;
         jQuery.ajax({
             url: "<%= Web.Model.Utility.Get_Path() %>/Product/GetList/" + page,
             dataType: 'json',
             type: "POST",
             data: queryString,
             success: function(json) {
                 jQuery('.gridContent').remove();
                 jQuery(json.ProductList).insertAfter('#ProductGrid');
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
     ae_popup('<%=UIProduct.dlgProduct.ToString()%>', 580, 350, 'Product', true, 'center', true,
        { "Save": function() { OnSave(); }, "Cancel": function() { $(this).dialog('close'); } },
        false);
     lpf<%= UIProduct.dlgProduct.ToString() %> = true;

     $.get("<%= Web.Model.Utility.Get_Path() %>/Product/New/" + id,
            NewProduct
            );
        
    }
    
    
    function NewProduct(data) {

        lpf<%= UIProduct.dlgProduct.ToString() %> = null;
        //style = "width: auto; min-height: 0px; height: 150px; "

        $("#<%= UIProduct.dlgProduct.ToString() %>").html(data);

        jQuery("#<%= UIProduct.dlgProduct.ToString() %>").dialog('open');
        jQuery('#<%= UIProduct.dlgProduct.ToString() %>').attr("Style", "width: 580; min-height: 350px; height: 350px;");
        jQuery("#<%= UIProduct.dlgProduct.ToString() %> form input:visible:first").focus();

    }
    
    
    function OnSave()
    {
         var queryString = $('#<%= UIProduct.formProduct.ToString() %>').serialize();
         
         $.getJSON("<%= Web.Model.Utility.Get_Path() %>/Product/Save", queryString, function(data) {
                    if( data.Has_Error )
                    {
                        $('#code_To_DisplayError').html(data.code_To_DisplayError);
                        $('#file_NameError').html(data.file_NameError);
                        $('#productError').html(data.productError);
                        $('#product_CodeError').html(data.product_CodeError);
                        $('#product_TypeError').html(data.product_TypeError);
                        $('#descriptionError').html(data.descriptionError);
                        $('#product_LinkError').html(data.product_LinkError);
                        
                    }
                    else
                    {
                        alert("Producct Has been saved");
                        $('#<%= UIProduct.dlgProduct.ToString() %>').dialog('close'); 
                        GetList(lastpage );
                    }
                    
                });
    
    
    }
    
    
    
    function OnEdit(productID ) {
     var queryString = "";
     var id = 1;
     var str = productID; 
       
   
     ae_popup('<%= UIProduct.dlgProduct.ToString() %>', 580, 350, 'Product', true, 'center', true,
        { "Save": function() { OnSave(); }, "Cancel": function() { $(this).dialog('close'); } },
        false);
     lpf<%= UIProduct.dlgProduct.ToString() %> = true;

     $.get("<%= Web.Model.Utility.Get_Path() %>/Product/Edit/" + str ,
            EditProduct
            );
        
    }
    
    
    function EditProduct(data) {

        lpf<%= UIProduct.dlgProduct.ToString() %> = null;
        $("#<%= UIProduct.dlgProduct.ToString() %>").html(data);
        jQuery("#<%= UIProduct.dlgProduct.ToString() %>").dialog('open');
        jQuery('#<%= UIProduct.dlgProduct.ToString() %>').attr("Style", "width: 580; min-height: 350px; height: 350px;");
        jQuery("#<%= UIProduct.dlgProduct.ToString() %> form input:visible:first").focus();

    }
    
    
    
    function OnDelete(codeSLSP,SalesRepCode,Territory )
    {
         var str = codeSLSP + "/" + SalesRepCode + "/" + Territory; 
       
         $.getJSON("/Product/Delete/" + str , "", function(data) {
                    if( data.Has_Error )
                    {
                       
                    }
                    else
                    {
                        alert("Product Has been deleted");
                        GetSPList(1);
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
    
    

</script>  




<script>
    function UpdateProductStoreFromAccPac(ele) {
        str = '<img  src="' + pfxURL.staticVar + "/images/ajax-loader_Upper.gif" + '"  />' + ele.innerHTML;
        ele.innerHTML = str;
        jQuery.ajax({
            url: pfxURL.staticVar + "/Product/UpdateProductStoreFromAccPac",
            dataType: 'json',
            type: "POST",
            error: function(json) {
                ele.innerHTML = "Update Product Store";
                alert("Operation failed");
            },
            success: function(json) {
                ele.innerHTML = "Update Product Store"
                alert("Product Store has been included which have sale for last default months and Store status");
            }
        });
    }
    
    
    function ProductWebsiteViewStatus(ele) {
        str = '<img  src="' + pfxURL.staticVar + "/images/ajax-loader_Upper.gif" + '"  />' + ele.innerHTML;
        ele.innerHTML = str;
        jQuery.ajax({
            url: pfxURL.staticVar + "/Product/ProductWebsiteViewStatus",
            dataType: 'json',
            type: "POST",
            error: function(json) {
                ele.innerHTML = "Update Product Status";
                alert("Operation failed");
            },
            success: function(json) {
                ele.innerHTML = "Update Product Status"
                alert("Product Store from AccPacc to display product/n has been updatd");
            }
        });
    }
    
    
    
</script>

</asp:Content> 