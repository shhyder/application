<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<%@ Import Namespace="System.Collections.Generic"  %>

<form id="formCategorySearch" action="Approved.ascx" method="post"> 

    <div id="map-pane" style="position: relative;" >
  

    <div id="divLoading" ></div>
    <%if (Convert.ToInt16(ViewData["Count"]) > 0)
      {%>
        <table width="100%" class="Grid">
        <tr class="Grid" id="CategoryGrid" >
            <th class="Grid" style="width:40px">
                Sr. 
            </th>
            <th class="Grid" style="width:270px">
                Category
                <%= Html.TextBox("srhProductType", "", new { maxlength = 25, Style = "width: 100%;color:Gray;" })%>
            </th>
            <th class="Grid" style="width:80px">
                Status
            </th>
            <th class="Grid" style="width:120px">
                Action
                rows <%=  Html.DropDownList(UIAll.gridSize.ToString(), (SelectList)ViewData[UIAll.gridSizeList.ToString()], new { style = "width: 40px;" })%>
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
   vdd["name"] = UIProductType.dlgProductType.ToString();
    Html.RenderPartial("Dialog", vdd);%>