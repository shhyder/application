<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<%
    bool is_Adjust = false;

 %>

<ul style="z-index: <%= is_Adjust ?0:1 %> ; top: 0px; left: 0px; display: none;" aria-activedescendant="ui-active-menuitem" role="listbox" class="ui-autocomplete ui-menu ui-widget ui-widget-content ui-corner-all"></ul>
<div role="dialog" tabindex="-1" class="ui-dialog ui-widget ui-widget-content ui-corner-all  ui-draggable ae-fixed transparent" style="display: none; z-index: <%= is_Adjust ?0:1000 %>; width:200px; outline: 0px none;">



<div class="ui-dialog-titlebar ui-widget-header ui-corner-all ui-helper-clearfix">
<span id="ui-dialog-title-paboutdinner" class="ui-dialog-title"></span>
<a role="button" class="ui-dialog-titlebar-close ui-corner-all" href="#">
<span class="ui-icon ui-icon-closethick">close</span></a></div>

<div class="ui-dialog-content ui-widget-content" id="paboutdinner"></div></div>
<div aria-labelledby="ui-dialog-title-<%= ViewData["name"].ToString() %>" role="dialog" tabindex="-1" class="ui-dialog ui-widget ui-widget-content ui-corner-all  ui-draggable ae-fixed transparent" style="display: none; z-index:<%= is_Adjust ?0:1000 %> ; width:500px; outline: 0px none;">
<div class="ui-dialog-titlebar ui-widget-header ui-corner-all ui-helper-clearfix">
<span id="ui-dialog-title-<%= ViewData["name"].ToString() %>" class="ui-dialog-title">&nbsp;</span>
<a role="button" class="ui-dialog-titlebar-close ui-corner-all" href="#">
<span class="ui-icon ui-icon-closethick">close</span></a></div>
<div class="ui-dialog-content ui-widget-content" id="<%= ViewData["name"].ToString() %>"></div>
<div class="ui-dialog-buttonpane ui-widget-content ui-helper-clearfix">
<div class="ui-dialog-buttonset">
<button aria-disabled="false" role="button" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" type="button">
<span class="ui-button-text">OK</span></button><button aria-disabled="false" role="button" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" type="button">
<span class="ui-button-text">Cancel</span></button></div></div></div>
<div aria-labelledby="ui-dialog-title-lpsearchMeals" role="dialog" tabindex="-1" class="ui-dialog ui-widget ui-widget-content ui-corner-all  ui-draggable ae-fixed transparent" style="display: none; z-index: <%= is_Adjust ?0:1002 %>; outline: 0px none; height: auto; width: 550px; top: 21px; left: 13px;">
<div class="ui-dialog-titlebar ui-widget-header ui-corner-all ui-helper-clearfix"><span id="ui-dialog-title-lpsearchMeals" class="ui-dialog-title">&nbsp;</span>
<a role="button" class="ui-dialog-titlebar-close ui-corner-all" href="#"><span class="ui-icon ui-icon-closethick">close</span></a></div>
<div style="width: auto; min-height: 0px; height: 419.633px;" class="searchMealsie8 ui-dialog-content ui-widget-content" id="lpsearchMeals">
<div class="ui-dialog-buttonpane ui-widget-content ui-helper-clearfix"><div class="ui-dialog-buttonset">
<button aria-disabled="false" role="button" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" type="button"><span class="ui-button-text">OK</span></button>
<button aria-disabled="false" role="button" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" type="button"><span class="ui-button-text">Cancel</span></button></div></div></div>
