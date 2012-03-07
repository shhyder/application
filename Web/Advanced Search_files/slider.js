document.observe("dom:loaded", function() {
    slider.init();
});

var array = [];
function goSearch(url) {
    if (window.opener == null)
        location.href = url;
    else {
        window.opener.location.href = url;
        window.close();
    }
}

function gEId(s) { return document.getElementById(s); }
function si(s) { return s.selectedIndex; }
function sv(s) { return s.options[si(s)].value; }
function aDdl(s, i, t, v) { s.options[i == null ? s.options.length : i] = new Option(t, v); }
function iDdl(s, i, t, v) { s.options[0] = new Option(t, v); }

function ls_oc(l, r, ra, y) {
    var l = gEId(l); var r = gEId(r);
    var lSi; var cur; var j = 0;

    if (y || (y == undefined)) lSi = (si(l) - 1) * 2;
    else lSi = (si(l)) * 2;

    if (si(r) != null && si(r) != -1) cur = sv(r);
    r.options.length = 0;
    for (i = lSi; i < ra.length; i += 2) { aDdl(r, j, ra[i + 1], ra[i]); j++; }
    if (y || y == undefined) iDdl(r, j, 'No Max', '');
    if (cur) { for (i = 0; i < r.options.length; i++) { if (r.options[i].value == cur) r.selectedIndex = i; } };
    if (!cur || sv(l) > cur) r.selectedIndex = 0;
}

function wait() {
    document.getElementById('results').className = "resultsWait";
}

var slider = function() {
    var speed = 2; var timer = 2;
    return {
    init: function(t, c) {
            
            $$('.cbs input').each(function(item) {
                Event.observe($(item), "click", function(e) {
                    sMChk(item);
                })
            });
            
            $$('.content').each(function(item, index) {
                array.push(item);
                item.maxh = item.offsetHeight;
            });
        },
        process: function(selectedItem) {
            for (var i = 0; i < array.length; i++) {
                var contentItem = array[i];
                var headerItem = contentItem.previous().down();

                clearInterval(selectedItem.timer);

                if (contentItem.style.display == '')
                    contentItem.style.display = 'none';

                if (selectedItem == headerItem && contentItem.style.display == 'none') {
                    contentItem.style.display = 'block';

                } else if (contentItem.style.display == 'block') {
                    contentItem.style.display = 'none';
                }
            }
        },

        islide: function(i, d) {
            var c, m;
            c = $(i);
            m = c.maxh;
            c.direction = d;
            c.timer = setInterval("slider.slide('" + i.id + "')", timer)
        },
        slide: function(i) {
            var c, m, h, dist; c = $(i); m = c.maxh; h = c.offsetHeight;
            dist = (c.direction == 1) ? Math.round((m - h) / speed) : Math.round(h / speed);
            if (dist <= 1) { dist = 1 }
            c.style.height = h + (dist * c.direction) + 'px'; c.style.opacity = h / c.maxh; c.style.filter = 'alpha(opacity=' + (h * 100 / c.maxh) + ')';
            if (h < 2 && c.direction != 1) {
                c.style.display = 'none'; clearInterval(c.timer);
            } else if (h > (m - 2) && c.direction == 1) { clearInterval(c.timer) }
        }
    };
} ();

function bkgImgReset(sender, args) {
    for (var i = 0; i < array.length; i++) {
        var contentItem = array[i];
        var headerItem = contentItem.previous(0).down();
        
    }

}
function OnModelHeaderClick(pb, c, img) {
    if ($('models-content').style.display == 'block') {
        for (var i = 0; i < array.length; i++) {
            var contentItem = array[i];
            var headerItem = contentItem.previous(0).down();

            contentItem.style.display = 'none';
            headerItem.style.backgroundImage = "url('/img/findacar/search/adv/plus.gif')";
        }

        $('models-content').style.display = 'none';
    } else {
        $('__EVENTBLOCK').value = 'model';

        for (var i = 0; i < array.length; i++) {
            var contentItem = array[i];
            var headerItem = contentItem.previous(0).down();

            contentItem.style.display = 'none';
            
        }

        $('models-content').style.display = 'block';
        $('models-content').previous(0).down().style.backgroundImage = "url('/img/findacar/search/adv/minus.gif')";

        if ($(img)) $(img).style.display = 'block';
        if ($(c)) $(c).style.display = 'none';

        //__doPostBack(pb, 'model_select');
    }
}

function sMChk(element, model) {
    //Synchronize for models with duplicate IDs (i.e. Neon)
    var field = document.getElementsByName(element.name);
    for (var i = 0; i < field.length; i++)
        field[i].checked = element.checked;

    var m = model;
    //if (model == null || !(model)) {
    //    m = "";
    //    document.getElementsByName('__EVENTBLOCK')[0].value = element.up('div.content').previous().down().down().id;
    //}
    //else
    //    document.getElementsByName('__EVENTBLOCK')[0].value = "Multiselect Model";

    /*
    var text = $(element.id).up('.text');
    var count = 0;

	text.select('input').each(
    function(item, index) {
    if (item.checked) {
    count++;
    throw $break;
    }
    });

    var blk = $(element.id).up('div.content').previous().down().down().innerHTML.replace(' ','');
    var models = $(element.id).up('div.content').next('div.content').down().down().next();
    if (blk == 'Makes' && count == 0 && models != null){models.remove();}
    //$(element.id).up('div.content').previous().down().style.backgroundImage = "url('/img/findacar/search/adv/minus.gif')";
    
    if (count > 0)
    $(element.id).up('div.content').previous().down().className = 'facetheadSelected';
    else
    $(element.id).up('div.content').previous().down().className = 'facethead';

    */

    //setTimeout('__doPostBack(\'' + element.id + '\', \'' + m + '\')', 0);
}

function SelectRangeDropDown(element, eventtarget, eventblock) {
    document.getElementsByName('__EVENTBLOCK')[0].value = eventblock;
    /* the following does not appear to be needed
    alert('set text');
    var text = element.up('.text');
    var count = 0;
    alert('iterate select');
    text.select('select').each(
    function (item, index) {
    if (item.value != 'NA')
    count++;
    });
    alert('set heading css');
    if (count > 0)
    element.up('.content').previous().down().className = 'facetheadSelected';
    else
    element.up('.content').previous().down().className = 'facethead';
    alert('ajax pb');
    */
    //setTimeout('__doPostBack(\'' + eventtarget + '\', \'\')', 0);
}

function checkAll(elementid, eventblock) {
    element = $(elementid);
    element.select('input').each(
		function(item, index) {
		    item.checked = true;
		});
    element.up('.content').previous().down().className = 'facetheadSelected';
    //if (eventblock == 'Models') {
    //    __doPostBack(element.select('input').first().id, 'model');
    //}
    //else
    //    __doPostBack(element.select('input').first().id, '');
    press();
}

function uncheckAll(elementid, eventblock) {

    element = $(elementid);
    element.select('input').each(
		function(item, index) {
		    item.checked = false;
		});
		element.up('.content').previous().down().className = 'facethead';
		press();
    //Disable models tab header immediately
//    element = $(elementid);
//    if (eventblock == 'Makes') {
//        var hdr = element.up('.content').next().down();
//        if (hdr) {
//            hdr.className = 'facethead off';
//            hdr.onclick = '';
//        }
//        var c = $(elementid).up('div.content').next('div.content').down().down().next();
//        if (c) c.remove();
//    }

//    $('__EVENTBLOCK').value = eventblock;

//    $('__EVENTBLOCK').value = eventblock;

//    element.select('input').each(
//		function(item, index) {
//		    item.checked = false;
//		});
//    element.up('.content').previous().down().className = 'facethead';


//    if (eventblock == 'Models') {
//        __doPostBack(element.select('input').first().id, 'model');
//    }
//    else
//        __doPostBack(element.select('input').first().id, '');

}

function resetForm() {
    if (confirm("Reset this form?"))
        location.replace('default.html?Ep=AdvancedReset');
        //location.replace('default.html?' + $F('reset_qs'));
}

function number_format(n) {
    var arr = new Array('0'), i = 0;
    while (n > 0)
    { arr[i] = '' + n % 1000; n = Math.floor(n / 1000); i++; }
    arr = arr.reverse();
    for (var i in arr) if (i > 0) //padding zeros
        while (arr[i].length < 3) arr[i] = '0' + arr[i];
    return arr.join();
}
