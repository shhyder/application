<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ProductType
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<!DOCTYPE HTML>

<meta http-equiv="content-type" content="text/html; charset=UTF-8"><title>
</title><link rel="shortcut icon" href="http://mrgsp.md:8080/prodinner/f.ico" type="image/x-icon">
<link id="siteThemeLink" href='<%=  Url.Content("~/Pro_files/jquery-ui.css")%>' rel="stylesheet" type="text/css">
<script src='<%=  Url.Content("~/Pro_files/jquery.js")%>' type="text/javascript"></script>
<script src='<%=  Url.Content("~/Pro_files/jquery-ui.js")%>' type="text/javascript"></script>
<script src='<%=  Url.Content("~/Pro_files/jquery_002.js")%>' type="text/javascript"></script>
<script src='<%=  Url.Content("~/Pro_files/Awesome.js")%>' type="text/javascript"></script>
<script src='<%=  Url.Content("~/Pro_files/style.js")%>' type="text/javascript"></script>


<script type="text/javascript">
    $(function() {
    ae_popup('<%= UIProductType.dlgProductType.ToString() %>', 200, 300, '', true, 'center', true,
        { "Save": function() { OnSave(); }, "Cancel": function() { $(this).dialog('close'); } },
        true);
    });

    
    
     function NewProductType() {
        ae_popup('<%= UIProductType.dlgProductType.ToString() %>', 200, 300, '', true, 'center', true,
        { "Cancel": function() { $(this).dialog('close'); },"Save": function() { NewSave(); } },
        true);

        lpf<%= UIProductType.dlgProductType.ToString() %> = true;
        $.get("<%= Web.Model.Utility.Get_Path() %>/ProductType/New/",
            updateProductType
            );
    }
    
    function NewSave() {
        var queryString = $('#<%= UIProductType.dlgProductTypeForm.ToString() %>').formSerialize();
        alert( queryString );
        $.post("<%= Web.Model.Utility.Get_Path() %>/ProductType/Save/",queryString,newProductTypeAttribute
            );
    }
    
     function UpdateNewProductType(data) {

        lpf<%= UIProductType.dlgProductType.ToString() %> = null;
        //style = "width: auto; min-height: 0px; height: 150px; "

        $("#<%= UIProductType.dlgProductType.ToString() %>").html(data);

        $("#<%= UIProductType.dlgProductType.ToString() %> form").ajaxForm({
            success: OnSuccesspfcreatedinner
        });

        $("#<%= UIProductType.dlgProductType.ToString() %>").dialog('open');
        $('#<%= UIProductType.dlgProductType.ToString() %>').attr("Style", "width: auto; min-height: 0px; height: 250px;");
        $("#<%= UIProductType.dlgProductType.ToString() %> form input:visible:first").focus();
    }
    
    
    function EditProductType(productType_ID) {
        ae_popup('<%= UIProductType.dlgProductType.ToString() %>', 200, 300, '', true, 'center', true,
        { "Cancel": function() { $(this).dialog('close'); },"Save": function() { NewSave(); } },
        true);

        lpf<%= UIProductType.dlgProductType.ToString() %> = true;
        $.get("<%= Web.Model.Utility.Get_Path() %>/ProductType/New/",
            UpdateEditProductType
            );
    }
    
    function UpdateEditProductType(data) {

        lpf<%= UIProductType.dlgProductType.ToString() %> = null;
        //style = "width: auto; min-height: 0px; height: 150px; "

        $("#<%= UIProductType.dlgProductType.ToString() %>").html(data);

        $("#<%= UIProductType.dlgProductType.ToString() %> form").ajaxForm({
            success: OnSuccesspfcreatedinner
        });

        $("#<%= UIProductType.dlgProductType.ToString() %>").dialog('open');
        $('#<%= UIProductType.dlgProductType.ToString() %>').attr("Style", "width: auto; min-height: 0px; height: 250px;");
        $("#<%= UIProductType.dlgProductType.ToString() %> form input:visible:first").focus();
    }
    
    
    
    function ViewProductType(product_ID) {
        ae_popup('<%= UIProductType.dlgProductType.ToString() %>', 200, 500, '', true, 'center', true,
        { "Cancel": function() { $(this).dialog('close'); },"Edit": function() { Edit(); } },
        true);
        lpf<%= UIProductType.dlgProductType.ToString() %> = true;
        $.get("<%= Web.Model.Utility.Get_Path() %>/ProductType/View/" + product_ID,
            UpdateEditProductType
            );
    }
    
    
    
    
    
    function newProductTypeAttribute(data) {

        if( data.toString().length == 0 )
        {
            $('<%= UIProductType.dlgProductType.ToString() %>').dialog('close');
        }
        else
        {
            
            //if (lpfcreatedinner != null) return;
            lpf<%= UIProductType.dlgProductType.ToString() %> = true;
            $.get("<%= Web.Model.Utility.Get_Path() %>/ProductType/ProductType/",
                updateProductType
                );
        
        }

       
    }

    var lpf<%= UIProductType.dlgProductType.ToString() %> = null;


    function OnSuccesspfcreatedinner(result) {
        if (result == 'ok' || typeof (result) == 'object') {
            $("#<%= UIProductType.dlgProductType.ToString() %>").dialog('close');
            created(result);
        }
        else updateQuoteStatus(result);
    }



   
</script>
<%--<a href="javascript:newProductType()" class="abtn ui-state-default ui-corner-all">host a dinner</a>--%>
    <h2>ProductType</h2>
    
    
    <div id="mainSearchPanel" >
    <% Html.RenderPartial( "List");%>
    </div>

    
    
    
    <% ViewDataDictionary vdd = new ViewDataDictionary();
    vdd["name"] = UIProductType.dlgProductType.ToString();
    Html.RenderPartial("Dialog", vdd);%>
</asp:Content>
