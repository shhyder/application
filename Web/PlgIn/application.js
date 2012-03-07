/*
 * jQuery File Upload Plugin JS Example 5.0.2
 * https://github.com/blueimp/jQuery-File-Upload
 *
 * Copyright 2010, Sebastian Tschan
 * https://blueimp.net
 *
 * Licensed under the MIT license:
 * http://creativecommons.org/licenses/MIT/
 */

/*jslint nomen: true */ 
/*global $ */

$(function() {
    'use strict';

    // Initialize the jQuery File Upload widget:
    $('#fileupload').fileupload({
        dataType: 'json',
        done: function(e, data) {
            document.getElementById('fileLoader').innerHTML = "Add file";
            document.getElementById('btnSelectFile').style.visibility = "";
            var path = "../ProdImg/" + data.result.Name;
            var imagelink = "<img border='0' src=" + path + "  width='120px' height='120px' alt='image could not be found' />";
            $("#ProductImageHolder").html(imagelink);
            $("#hidFileName").val(data.result.Name);
        },

        drop: function(e, data) {
        document.getElementById('fileLoader').innerHTML = '<div id="loadingUpper" ><img  src="' + pfxURL.staticVar + "/images/ajax-loaderWhite.gif" + '"  /><strong>Loading</strong></div>';
        //    document.getElementById('btnSelectFile').style.visibility = "hidden";
        },
        change: function(e, data) {
        document.getElementById('fileLoader').innerHTML = '<div id="loadingUpper" ><img  src="' + pfxURL.staticVar + "/images/ajax-loaderWhite.gif" + '"  /><strong>Loading</strong></div>';
        //    document.getElementById('btnSelectFile').style.visibility = "hidden";
            
        }





    });

    // Load existing files:
    //    $.getJSON($('#fileupload form').prop('action'), function(files) {
    //       
    //        var fu = $('#fileupload').data('fileupload');
    //        //fu._adjustMaxNumberOfFiles(-files.length);
    //        fu._renderDownload(files)
    //                .appendTo($('#fileupload .files'))
    //                .fadeIn(function() {
    //                    // Fix for IE7 and lower:
    //                    $(this).show();
    //                });
    //    });
    //    $('#fileupload').bind('fileuploaddone', function(e, data) {
    //        alert("upload done");
    //        if (data.jqXHR.responseText || data.result) {
    //            var fu = $('#fileupload').data('fileupload');
    //            var JSONjQueryObject = (data.jqXHR.responseText) ? jQuery.parseJSON(data.jqXHR.responseText) : data.result;
    //            fu._adjustMaxNumberOfFiles(JSONjQueryObject.files.length);
    //            //                debugger;
    //            fu._renderDownload(JSONjQueryObject.files)
    //                .appendTo($('#fileupload .files'))
    //                .fadeIn(function() {
    //                    // Fix for IE7 and lower:
    //                    $(this).show();
    //                });
    //        }
    //    });

    // Open download dialogs via iframes,
    // to prevent aborting current uploads:
    $('#fileupload .files a:not([target^=_blank])').live('click', function(e) {
        e.preventDefault();
        $('<iframe style="display:none;"></iframe>')
            .prop('src', this.href)
            .appendTo('body');
    });

});



