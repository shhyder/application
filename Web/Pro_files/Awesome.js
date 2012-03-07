function applyAwesomeStyles() {
    $(".ae-lookup-openbtn").empty().prepend('<span class="ui-icon ui-icon-newwin"></span>');
    $(".ae-lookup-clearbtn").empty().prepend('<span class="ui-icon ui-icon-gear"></span>');
    mybutton(".ae-lookup-openbtn");
    mybutton(".ae-lookup-clearbtn");
}

$(function() {
    $(".ae-pagination-current").addClass('ui-state-highlight');
    mybutton(".ae-pagination a");
    applyAwesomeStyles();
    $("body").ajaxComplete(applyAwesomeStyles);
});

function mybutton(sel) {
    $(sel).unbind('mousedown mouseup mouseleave')
        .hover(function() { $(this).addClass("ui-state-hover"); },
	            function() { $(this).removeClass("ui-state-hover"); })
        .bind({ 'mousedown mouseup': function() { $(this).toggleClass('ui-state-active'); } })
        .addClass("ui-state-default").addClass("ui-corner-all")
        .bind('mouseleave', function() { $(this).removeClass('ui-state-active') });
}

function ae_interactive(o) {
    $(o + ' input[type="text"]').keyup(function(e) {
        var w = e.which;
        if (w < 9 || w > 45 && w < 91 || w > 93 && w < 112 || w > 185 || w == 32)
            $(o).submit();
    });
    $(o + ' input[type="hidden"], ' + o + ' .ae-array').change(function() {
        $(o).submit();
    });
}

function ae_fullscreen(o) {
    $(window).bind("resize", function(e) { $(o).dialog("option", { height: $(window).height() - 50, width: $(window).width() - 50 }).trigger('dialogresize'); });
}

function ae_ajaxDropdown(o, p, url, keys, values) {
    ae_loadAjaxDropdown(o, p, url, false, keys, values);

    $("#" + o + "dropdown").keyup(function() { $(this).change(); })
    .change(function() {
        $('#' + o).val($('#' + o + 'dropdown').val()).trigger('change');
    });

    if (p) $('#' + p).change(function() { ae_loadAjaxDropdown(o, p, url, true, keys, values); });
    $.each(keys, function(i, k) {
        $('#' + k).change(function() { ae_loadAjaxDropdown(o, p, url, true, keys, values); });
    });
    //if keys foreach key change same 
}

function ae_loadAjaxDropdown(o, p, url, c, keys, values) {
    if (c) $('#' + o).val(null);

    var data = new Array();
    data.push({ name: "key", value: $('#' + o).val() });
    if (p) data.push({ name: "parent", value: $('#' + p).val() });

    $.each(keys, function(i, k) {
        if ($('#' + k).attr('name')) {
            data.push({ name: values[i], value: $('#' + k).val() });
        } else {
            $('#' + k).find('input').each(function(index) {
                data.push({ name: values[i], value: $(this).val() });
            });
        }
    });

    $.post(url, data,
        function(d) {
            $("#" + o + "dropdown").empty();
            if (typeof (d) == 'object')
                $.each(d, function(i, j) {
                    var sel = "";
                    if (j.Selected == true) sel = "selected = 'selected'";
                    $("#" + o + "dropdown").append("<option " + sel + " value=\"" + j.Value + "\">" + j.Text + "</option>");
                });
            if (c) $("#" + o + "dropdown").trigger('change');
        });
}

function ae_autocomplete(o, k, p, u, mr, delay, minLen, keys, values) {
    $('#' + o).autocomplete({
        delay: delay,
        minLength: minLen,
        source: function(request, response) {
            var data = new Array();
            data.push({ name: 'searchText', value: request.term });
            data.push({ name: 'maxResults', value: mr });

            if (p) data.push({ name: 'parent', value: $('#' + p).val() });

            $.each(keys, function(i, k) {
                if ($('#' + k).attr('name')) {
                    data.push({ name: values[i], value: $('#' + k).val() });
                } else {
                    $('#' + k).find('input').each(function(index) {
                        data.push({ name: values[i], value: $(this).val() });
                    });
                }
            });

            $.ajax({
                url: u, type: "POST", dataType: "json",
                data: data,
                success: function(d) { response($.map(d, function(o) { return { label: o.Text, value: o.Text, id: o.Id} })); }
            });
        }
    });

    $('#' + o).bind("autocompleteselect", function(e, ui) {
        $('#' + k).val(ui.item ? ui.item.id : null).trigger('change');
        $('#' + o).trigger('change');
    });

    $('#' + o).keyup(function(e) { if (e.which != '13') $("#" + k).val(null).trigger('change'); });
}

function ae_popup(o, w, h, title, modal, pos, res, btns, fulls) {
    if (fulls) { res = false; modal = true; }
    $("#" + o).dialog({
        show: "fade",
        width: w,//fulls ? $(window).width() - 50 : w,
        height: fulls ? $(window).height() - 50 : h,
        title: title,
        modal: modal,
        position: pos,
        resizable: res,
        buttons: btns,
        autoOpen: false,
        close: function(e, ui) { $("#" + o).find('*').remove(); }
    });
    if (modal || fulls) $("#" + o).dialog("option", { dialogClass: 'ae-fixed' });
    if (fulls) ae_fullscreen("#" + o);
}

function ae_loadLookupDisplay(o, url) {
    $('#ld' + o).val('');
    var id = $('#' + o).val();
    if (id) $.get(url, { id: id }, function(d) { $("#ld" + o).val(d); });
}

function ae_loadMultiLookupDisplay(o, url) {
    var ids = $("#" + o + " input").map(function() { return $(this).attr("value"); }).get();
    $("#ld" + o).html('');
    if (ids.length != 0) $.post(url, $.param({ selected: ids }, true),
        function(d) {
            $.each(d, function() { $("#ld" + o).append('<li>' + this.Text + '</li>') });
        });
}

function ae_lookupChoose(o, url, sel) {
    $('#' + o).val('');
    $('#' + o).val($('#' + o + 'ls .' + sel).attr("data-value")).change();
    ae_loadLookupDisplay(o, url);
    $("#lp" + o).dialog('close');
}

function ae_multiLookupChoose(o, loadUrl, prop) {
    $("#" + o).empty();
    $.each($("#" + o + "se li").map(function() { return $(this).attr("data-value"); }).get(), function() {
        $("#" + o).append($("<input type='hidden' name='" + prop + "' \>").attr("value", this));
    });
    $("#" + o).change();
    ae_loadMultiLookupDisplay(o, loadUrl);
    $("#lp" + o).dialog('close');
}

function ae_lookupClear(o) {
    $("#lc" + o).click(function() {
        $("#" + o).val("").change();
        $("#ld" + o).val("");
    });
}

function ae_multiLookupClear(o) {
    $("#lc" + o).click(function() {
        $("#" + o + ",#ld" + o).empty();
        $("#" + o).change();
    });
}

function ae_confirm(o, f, h, w, yes, no) {
    $("#dialog-confirm-" + o).dialog({
        show: "fade",
        hide: "fade",
        resizable: false,
        height: h,
        width: w,
        modal: true,
        autoOpen: false,
        dialogClass: 'ae-fixed'
    })
    .dialog("option", "buttons", [
    {
        text: yes,
        click: function() { $(this).dialog("close"); f.submit(); }
    },
    {
        text: no,
        click: function() { $(this).dialog("close"); }
    },
    ]);

    $("." + o).live('click', function() {
        f = $(this).closest('form');
        $("#dialog-confirm-" + o).dialog('open');
        return false;
    });
}

function ae_lookupPopupOpenClick(o, lck, url, paging, multi, keys, values) {
    $("#lpo" + o).click(function(e) {
        e.preventDefault();
        if (lck != null) return;
        lck = true;
        var data = [{ name: 'prop', value: o}];
        data.push({ name: 'paging', value: paging });
        data.push({ name: 'multi', value: multi });
        for (var i in keys) data.push({ name: 'keys', value: keys[i] });
        for (var i in values) data.push({ name: 'values', value: values[i] });

        $.get(url, $.param(data), function(d) {
            $("#lp" + o).html(d).dialog('open'); lck = null;
        });
    });
}

function ae_takevals(a, b, w) {
    $.each(a, function(i, v) {
        var e = $('#' + v);
        var t = e.attr('name') ? e : e.find('input');
        t.clone().removeAttr('id').attr('name', b[i]).appendTo('#' + w);
    });
}
