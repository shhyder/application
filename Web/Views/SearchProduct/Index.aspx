<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript" src='<%=  Url.Content("~/js/jquery-1.4.2.min.js")%>' ></script> 
<script type="text/javascript" src='<%=  Url.Content("~/js/jquery-ui-1.8.1.custom.min.js")%>' ></script>

    <script type="text/javascript">
        function GoSearch() {
         var str = "<%= Web.Model.Utility.Get_Path() %>/SearchProduct/List/1?cat=" + jQuery("#cobProduct_Type").val() + "&search=" + jQuery("#search").val();
         jQuery.ajax({
             type: "POST",
             url:str ,
             success: function(result) {
                 
                 if (result.isOk == false) {

                     $("#mainSearchPanel").html(result.message);
                 }
                 else {

                     $("#mainSearchPanel").html(result);
                 }
                 
             },
             async: true
         });
     }


     function GetDealerList(str) {
         window.location.href = str;
     }


     function PopUp(id) {

         w = 250;
         h = 200;
            
            LeftPosition = (screen.width) ? (screen.width-w)/2 : 0;
            TopPosition = (screen.height) ? (screen.height-h)/2 : 0;
            settings =
            'height='+h+',width='+w+',top='+TopPosition+',left='+LeftPosition+',scrollbars='+scroll+',resizable'
     
     
         window.open(
                 "<%= Web.Model.Utility.Get_Path() %>/Search/PopUp/" + id
        ,
        "Popup",
        settings
        );
        
     }
    

    function IsCompletePageRefresh() {
        return $get("txtIsPage").value;
    }

    function GetURI() {
        return $get("URI").value;
        
    }


    function textChanged() {
        onCleanPage();
    }
 </script>


    <p>
        <%= Html.ActionLink("Search", "Index", "Search")%>
    </p>
    <table>
        <tr>
            <td></td>        
            <td>Search</td>
            <td>
                <%=  Html.DropDownList(UIProductSearch.cobProduct_Type.ToString(), (SelectList)ViewData[UIProductSearch.listProductType.ToString()], new { style = "width: 220px;" })%>
            </td> 
            <td>
                <%= Html.TextBox( UIProductSearch.search.ToString(),"",new { style = "width: 120px;"}) %>
            </td> 
            <td>
                <input name="searchValue" type="button" value="Search"   style="width: 5em" onclick= "GoSearch()" />
            </td>              
        </tr>
    </table>
    
    
      <div id="mainSearchPanel" >
    <% Html.RenderPartial( "List");%>
    </div>


</asp:Content>
