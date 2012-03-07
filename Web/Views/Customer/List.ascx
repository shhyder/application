<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<%@ Import Namespace="System.Collections.Generic"  %>

<form id="formStoreSearch" action="Approved.ascx" method="post"> 

    <div id="map-pane" style="position: relative;" >
  

    <div id="divLoading" ></div>
    <%if (Convert.ToInt16(ViewData["Count"]) > 0)
      {%>
        <table width="100%" class="Grid">
        <tr class="Grid" id="StoreGrid" >
            <th class="Grid">
                Sr. 
            </th>
            <th class="Grid" style="width:70px">
                Code
                <%= Html.TextBox("srhCode", "", new { maxlength = 25, Style = "width: 100%;color:Gray;", onkeypress = "javascript:isNumberKey(event);" })%>
            </th>
            <th class="Grid" style="width:190px">
                Store
                <%= Html.TextBox("srhStore", "", new { maxlength = 25, Style = "width: 100%;color:Gray;" })%>
            </th>
            
            <th class="Grid" style="width:270px">
                Address
                <%= Html.TextBox("srhAddress", "", new { maxlength = 25, Style = "width: 100%;color:Gray;" })%>
            </th>
            <th class="Grid" style="width:80px">
                City
                <%= Html.TextBox("srhCity", "", new { maxlength = 25, Style = "width: 100%;color:Gray;" })%>
            </th>
             <th class="Grid" style="width:80px">
                State
                <%= Html.TextBox("srhState", "", new { maxlength = 25, Style = "width: 100%;color:Gray;" })%>
            </th>
            <th class="Grid" style="width:90px">
                Email
                <%= Html.TextBox("srhEmail", "", new { maxlength = 25, Style = "width: 100%;color:Gray;" })%>
            </th>
            <th class="Grid" style="width:80px">
                Phone
                <%= Html.TextBox("srhPhone1", "", new { maxlength = 25, Style = "width: 100%;color:Gray;" })%>
            </th>
            <th class="Grid" style="width:80px">
                Status
                <input name="srhStatus" class="cbs" checked="checked"  id="srhStatus" type="checkbox"/>
            </th>
            <th class="Grid">
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
   vdd["name"] = UIStore.dlgStore.ToString();
    Html.RenderPartial("Dialog", vdd);%>