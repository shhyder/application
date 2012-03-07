<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.Collections.Generic"  %>
<%@ Import Namespace="System.Data"  %>

<style type="text/css">
    .style1
    {
        width: 103px;
        height: 267px;
    }
    .style2
    {
        height: 267px;
    }
    .style3
    {
        height: 33px;
    }
    
	.attributeVariantTable{}
	
</style>
<script type="text/javascript">

    function NewProductTypeAttribute() {
        
        ae_popup('<%= UIProductTypeAttribute.dlgProductTypeAttribute.ToString() %>', 200, 300, '', true, 'center', true,
        { "Cancel": function() { $(this).dialog('close'); },"Save": function() { NewSaveAttribute(); } },
        true);

        lpf<%= UIProductTypeAttribute.dlgProductTypeAttribute.ToString() %> = true;
        $.get("<%= Web.Model.Utility.Get_Path() %>/Attribute/NewProductType/<%= ViewData[UIProductType.hidProductTypeID.ToString()].ToString()%>",
            UpdateProductTypeAttribute
            );
    }
    
    
    
    function UpdateProductTypeAttribute(data) {

        lpf<%= UIProductTypeAttribute.dlgProductTypeAttribute.ToString() %> = null;
        //style = "width: auto; min-height: 0px; height: 150px; "

        $("#<%= UIProductTypeAttribute.dlgProductTypeAttribute.ToString() %>").html(data);

        $("#<%= UIProductTypeAttribute.dlgProductTypeAttribute.ToString() %> form").ajaxForm({
            success: OnSuccesspfcreatedinner
        });

        $("#<%= UIProductTypeAttribute.dlgProductTypeAttribute.ToString() %>").dialog('open');
        $('#<%= UIProductTypeAttribute.dlgProductTypeAttribute.ToString() %>').attr("Style", "width: auto; min-height: 0px; height: 250px;");
        $("#<%= UIProductTypeAttribute.dlgProductTypeAttribute.ToString() %> form input:visible:first").focus();
    }


    function NewSaveAttribute() {
        var queryString = $('#<%= UIAttribute.dlgAttributeForm.ToString() %>').formSerialize();
        $.post("<%= Web.Model.Utility.Get_Path() %>/Attribute/Save/",queryString,NewProductTypeAttributeSave
            );
    }
    
    
    function NewProductTypeAttributeSave(data)
    {
       
        if( data.toString().length == 0 )
        {
            $("#<%= UIProductTypeAttribute.dlgProductTypeAttribute.ToString() %>").dialog('close');
            
             jQuery.ajax({
                type: "POST",
                url: "<%= Web.Model.Utility.Get_Path() %>/ProductType/AttributeList/<%= ViewData[UIProductType.hidProductTypeID.ToString()].ToString()%>",
                success: function(result) {
                    if (result.isOk == false) {
                        $("#attributePanel").html(result.message);
                    }
                    else {

                        $("#attributePanel").html(result);
                        
                    }
                },
                async: true
            });

        }
        else
        {
            alert( "out");
           lpf<%= UIProductTypeAttribute.dlgProductTypeAttribute.ToString() %> = null;
            //style = "width: auto; min-height: 0px; height: 150px; "

            $("#<%= UIProductTypeAttribute.dlgProductTypeAttribute.ToString() %>").html(data);
        
        }
    
    
    }
    
    
    
    
    
    
    
    
    
    
    function NewProductTypeAttributeVariant(attribute_ID) {
        alert( attribute_ID );
        ae_popup('<%= UIProductTypeAttributeVariant.dlgProductTypeAttributeVariant.ToString() %>', 200, 300, '', true, 'center', true,
        { "Cancel": function() { $(this).dialog('close'); },"Save": function() { NewSaveAttributeVariant(); } },
        true);

        lpf<%= UIProductTypeAttributeVariant.dlgProductTypeAttributeVariant.ToString() %> = true;
        $.get("<%= Web.Model.Utility.Get_Path() %>/AttributeVariant/NewProductType/<%= ViewData[UIProductType.hidProductTypeID.ToString()].ToString()%>/" + attribute_ID,
            UpdateProductTypeAttributeVariant
            );
    }
    
    
    
    function UpdateProductTypeAttributeVariant(data) {

        lpf<%= UIProductTypeAttributeVariant.dlgProductTypeAttributeVariant.ToString() %> = null;
        //style = "width: auto; min-height: 0px; height: 150px; "

        $("#<%= UIProductTypeAttributeVariant.dlgProductTypeAttributeVariant.ToString() %>").html(data);

        $("#<%= UIProductTypeAttributeVariant.dlgProductTypeAttributeVariant.ToString() %> form").ajaxForm({
            success: OnSuccesspfcreatedinner
        });

        $("#<%= UIProductTypeAttributeVariant.dlgProductTypeAttributeVariant.ToString() %>").dialog('open');
        $('#<%= UIProductTypeAttributeVariant.dlgProductTypeAttributeVariant.ToString() %>').attr("Style", "width: auto; min-height: 0px; height: 250px;");
        $("#<%= UIProductTypeAttributeVariant.dlgProductTypeAttributeVariant.ToString() %> form input:visible:first").focus();
    }


    function NewSaveAttributeVariant() {
        var queryString = $('#<%= UIAttributeVariant.dlgAttributeVariantForm.ToString() %>').formSerialize();
        $.post("<%= Web.Model.Utility.Get_Path() %>/AttributeVariant/Save/",queryString,NewProductTypeAttributeVariantSave
            );
    }
    
    
    function NewProductTypeAttributeVariantSave(data)
    {
       
        if( data.toString().length == 0 )
        {
            $("#<%= UIProductTypeAttributeVariant.dlgProductTypeAttributeVariant.ToString() %>").dialog('close');
            
             jQuery.ajax({
                type: "POST",
                url: "<%= Web.Model.Utility.Get_Path() %>/ProductType/AttributeList/<%= ViewData[UIProductType.hidProductTypeID.ToString()].ToString()%>",
                success: function(result) {
                    if (result.isOk == false) {
                        $("#attributePanel").html(result.message);
                    }
                    else {

                        $("#attributePanel").html(result);
                        
                    }
                },
                async: true
            });

        }
        else
        {
            alert( "out");
           lpf<%= UIProductTypeAttributeVariant.dlgProductTypeAttributeVariant.ToString() %> = null;
            //style = "width: auto; min-height: 0px; height: 150px; "

            $("#<%= UIProductTypeAttributeVariant.dlgProductTypeAttributeVariant.ToString() %>").html(data);
        
        }
    
    
    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    function AttributeClicked(attribute_ID,attribute)
    {
        var id = '#attr' + attribute_ID;
        $(".attributeVariantTable").attr("Style", "display: none;");
        $(id).attr("Style", "display: block;");
    }
    
    
    

</script>

<table style="width: 100%; height: 379px;">
    <tr>
        <td class="style3" colspan="3">
        <a href="javascript:NewProductTypeAttribute()" class="abtn ui-state-default ui-corner-all">Add Attribute</a>
        </td>
    </tr>
    <tr>
        <td class="style2">
        
        
          <table width="100%" >
     
        
            <% string style = "background:White";%>
            <% foreach (var consumer in (ViewData[UIProductType.listProductTypeAttribute.ToString()] as List<DataRow>))
               { %>
                   <tr style='<%= style %>' onclick="javascript:AttributeClicked('<%= consumer["Attribute_ID"].ToString()  %>','<%= consumer["Attribute"].ToString()  %>' );"  >
                    <td class="gridFirstAlternate">
                        <%= consumer["Attribute"].ToString()%> 
                    </td>
                  </tr>
            <% } %>
           
            </table>
        
        
        
        
        
        </td>
        <td class="style1">
        </td>
        <td class="style2">
            <%= ViewData[UIProductType.htmlProductTypeAttributeVariant.ToString()].ToString() %>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            &nbsp;</td>
    </tr>
</table>

<% ViewDataDictionary vdd = new ViewDataDictionary();
   vdd["name"] = UIProductTypeAttribute.dlgProductTypeAttribute.ToString();
    Html.RenderPartial("Dialog", vdd);%>
    
    
<% vdd = new ViewDataDictionary();
   vdd["name"] = UIProductTypeAttributeVariant.dlgProductTypeAttributeVariant.ToString();
    Html.RenderPartial("Dialog", vdd);%>