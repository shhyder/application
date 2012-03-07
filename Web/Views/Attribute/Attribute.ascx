<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<style type="text/css">
    .style1
    {
    }
    .style2
    {
        height: 22px;
    }
    .style3
    {
        height: 14px;
    }
    .style4
    {
        height: 15px;
    }
    .style5
    {
        height: 15px;
        width: 51px;
    }
    .style6
    {
        height: 15px;
        width: 352px;
    }
</style>

<script type="text/javascript">

    function NewAttributeVariant(id) {
        ae_popup('<%= UIAttributeVariant.dlgAttributeVariant.ToString() %>', 200, 300, '', true, 'center', true,
        { "Cancel": function() { $(this).dialog('close'); },"Save": function() { NewSaveAttributeVariant(); } },
        true);

        lpf<%= UIAttributeVariant.dlgAttributeVariant.ToString() %> = true;
        $.get("<%= Web.Model.Utility.Get_Path() %>/AttributeVariant/NewProductType/" + id,
            UpdateProductTypeAttribute
            );
    }
    
    
    
    
    
    function UpdateProductTypeAttributeVariant(data) {

        lpf<%= UIAttributeVariant.dlgAttributeVariant.ToString() %> = null;
        //style = "width: auto; min-height: 0px; height: 150px; "

        $("#<%= UIAttributeVariant.dlgAttributeVariant.ToString() %>").html(data);

        $("#<%= UIAttributeVariant.dlgAttributeVariant.ToString() %> form").ajaxForm({
            success: OnSuccesspfcreatedinner
        });

        $("#<%= UIAttributeVariant.dlgAttributeVariant.ToString() %>").dialog('open');
        $('#<%= UIAttributeVariant.dlgAttributeVariant.ToString() %>').attr("Style", "width: auto; min-height: 0px; height: 250px;");
        $("#<%= UIAttributeVariant.dlgAttributeVariant.ToString() %> form input:visible:first").focus();
    }
    
    
     function NewSaveAttributeVariant() {
        var queryString = $('#<%= UIAttributeVariant.dlgAttributeVariantForm.ToString() %>').formSerialize();
        alert( queryString );
        $.post("<%= Web.Model.Utility.Get_Path() %>/Attribute/Save/",queryString,newProductTypeAttribute
            );
    }


</script>

<form id="<%= UIAttribute.dlgAttributeForm.ToString() %>" action="Approved.ascx" method="post"> 

<table style="width: 99%; height: 299px;">
    <tr>
        <td class="style2" colspan="3">
            Attribute</td>
    </tr>
    <tr>
        <td class="style3" colspan="3">
        <%= Html.Hidden(UIAttribute.hidAttribute_ID.ToString(), ViewData[UIAttribute.hidAttribute_ID.ToString()].ToString())%>
        <%= Html.Hidden(UIAttribute.hidProduct_Type_ID.ToString(), ViewData[UIAttribute.hidProduct_Type_ID.ToString()].ToString())%>
        <%= Html.TextBox(UIAttribute.txtAttribute.ToString(), ViewData[UIAttribute.txtAttribute.ToString()].ToString())%>
        </td>
    </tr>
    <tr>
        <td class="style6">
            Variants</td>
        <td class="style5">
            &nbsp;</td>
        <td class="style4">
            <a href="javascript:NewAttributeVariant()" class="abtn ui-state-default ui-corner-all">Add variants</a></td>
    </tr>
    <tr>
        <td class="style1" colspan="3">
            &nbsp;</td>
    </tr>
</table>
</form>

<% ViewDataDictionary vdd = new ViewDataDictionary();
   vdd["name"] = UIAttributeVariant.dlgAttributeVariant.ToString();
    Html.RenderPartial("Dialog", vdd);%>