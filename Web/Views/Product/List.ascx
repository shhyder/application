<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<%@ Import Namespace="System.Collections.Generic"  %>




<form id="formProductSearch" action="Approved.ascx" method="post"> 

    <div id="map-pane" style="position: relative;" >
  

    
    <%if (Convert.ToInt16(ViewData["Count"]) > 0)
      {%>
      
      
            <table width="100%" class="Grid">
        <tr class="Grid" id="ProductGrid" >
            <th class="Grid">
                Sr. 
            </th>
            <th class="Grid" style="width:70px"></th>
            <th class="Grid" style="width:70px">
                Code
                <%= Html.TextBox("srhCode", "", new { maxlength = 25, Style = "width: 100%;color:Gray;"})%>
            </th>
            <th class="Grid" style="width:190px">
                Display Code
                <%= Html.TextBox("srhDisplayCode", "", new { maxlength = 25, Style = "width: 100%;color:Gray;" })%>
            </th>
            <th class="Grid" style="width:170px">
                Product
                <%= Html.TextBox("srhProduct", "", new { maxlength = 25, Style = "width: 100%;color:Gray;" })%>
            </th>
            
            <th class="Grid" style="width:370px">
                Description
                <%= Html.TextBox("srhDescription", "", new { Style = "width: 100%;color:Gray;" })%>
            </th>
            <th class="Grid" style="width:80px">
                Product Type
                <%=  Html.DropDownList( UIProduct.srhProductType.ToString(), (SelectList)ViewData[UIProductSearch.listProductType.ToString()], new { style = "width: 120px;" })%>
            </th>
             <th class="Grid" style="width:80px">
                Status
                <input name="srhStatus" class="cbs" checked="checked"  id="srhStatus" type="checkbox"/>
            </th>
            <th class="Grid" style="width:140px">
                Action <a href="javascript:OnNew()" class="checkButton">New</a> <br />
                rows <%=  Html.DropDownList(UIAll.gridSize.ToString(), (SelectList)ViewData[UIAll.gridSizeList.ToString()], new { style = "width: 80px;" })%>
            </th>
        </tr>
        
        
        
        
            <% Html.RenderPartial("Grid");%>
        
    </table>
       
     <%}
      else
      {%>
        <p>No consumer has been found</p>
    <%} %>
    
    </div>
   
</form>



<% ViewDataDictionary vdd = new ViewDataDictionary();
   vdd["name"] = UIProduct.dlgProduct.ToString();
    Html.RenderPartial("Dialog", vdd);%>