/**
 * @author Henry
 *
 * 02/29/08 - Changes for $$ - Bob Follek
 */

try{document.domain = 'Jatai.net';}
catch(e){}

/* JQuery Initialization */
var $j = jQuery.noConflict();
$j(document).ready(function() {

	prepMenu();
	setRolloverHover();
	processStorePositionBlocks();
	processSpecPageBlocks();
	processLocationSelects();	
	processSearchCompare();
	processSearchCompareClear();
	processCompareTables();
	highlightSearchRows();
	processSearchBlocks();
    processTooltips();
    homepage.initialize();
    //setUpHeaderChangeStore();
});

var setUpHeaderChangeStore = function(){
   $j("#aChange").unbind("click").bind("click", ChangeClick);
   $j("#txtHeaderZip").unbind("keypress").bind("keypress", ChangePressEnter);
}

/*
 * Tooltips: attach cluetip tips
 */
 var processTooltips = function() {
	/* tooltips */
	$j('.tips').cluetip({
		width: 275,
		topOffset: 0,
        leftOffset: 10,
		arrows: true, 
		dropShadow: true,
		dropShadowSteps: 2,
		waitImage: false,
		showTitle: false,
		hoverIntent: {
			sensitivity:  50,
			interval:     250,
			timeout:      250 
		}
    });
}
    
/*
 * Search: Highlight results rows
 */
 var highlightSearchRows = function() {
	$j('.car-search-result').hover(
	    function() {
	        $j(this).addClass('highlighted')
    	},
	    function() {
	        $j(this).removeClass('highlighted');
    	}
    );
}

/*
 * Search: Process compare car checkboxes (click)
 */
var processSearchCompare = function() {

	var findCarResults = $j('.car-search-result td input:checkbox');
	
	findCarResults.each( function(e) {
	
        $j(this).click( function(e) {
            
            // get current compare list ('CL' hidden)
			var comparisonList = ($j('#CL').val() === "") ? [] : $j('#CL').val().split('+');
			
            var checkbox = $j(this);
            var checkid = checkbox.attr('id');

			if( checkbox.attr('checked') ) {
			
    			var max = $j('#MC').val();
   			
				if(comparisonList.length >= max) {
			    	e.stopPropagation();
			    	e.preventDefault();
			    	alert('Up to ' + max + ' cars may be compared.');
				} 
				else if ($j.inArray(checkid, comparisonList) == -1) {
					comparisonList.push(checkid);
					$j('#CL').val((comparisonList.length === 1) ? checkid : comparisonList.join('+'));
					regMod('CL');
				}
				
			} 
			else 
			{
				var r = $j.inArray(checkid, comparisonList);
				if (r > -1) {
        			comparisonList.splice(r, 1);
				}
				
				$j('#CL').val((comparisonList.length === 0) ? "" : comparisonList.join('+'));
				regMod('CL');
			}

			var cars = (comparisonList.length == 1) ? "car" : "cars";
			$j('#checktext').html(comparisonList.length + " " + cars + " checked");
            $j('#compare').attr('class', (comparisonList.length == 0 ) ? "replace compare-inactive" : "replace compare-checked");
			
		});
	});
	
}

/* 
 * Search:  Clear car compare checkboxes
 */
var processSearchCompareClear = function() {

    var clearAll = $j('li.clear-all a:first').click(function(e) {
    
        e.stopPropagation();
        e.preventDefault();
        
        $j('#CL').val("");
        regMod('CL');         

        /* clear checks */
        $j('.car-search-result input').each(function(e) {
            $j(this).attr('checked', false);
        });

        $j('#checktext').html('0 cars checked');
        $j('#compare').attr('class', 'replace compare-inactive');

        try
        {
            var clForm = $j('#CL'); 
            var comparisonList = clForm.val() == "" ? [] : clForm.val().split('+');
            
            $j('#checktext').html(comparisonList.length + " cars checked");
            $j('#compare').attr('class', (comparisonList.length == 0 ) ? "replace compare-inactive" : "replace compare-checked");
        }
        catch (e)
        {}
        
    });
     
}

function compareButton() {
     $j('#compare').attr('class', 'replace compare-inactive');
     $j('#compare').disabled = true;
     return $j('#CL').val().length>0;
}

/*
 * Process Location Selectors -- 
 * Research Section combo box navigation 
 *
 */
var processLocationSelects = function() {
	/* Location-changing select elements */
	var locationSelects = $j('select.locationSelector');
	
	locationSelects.each(function(i){
        $j(this).change(function(e){
            selectRedirect(this);
        });
        
	});
}

/*
 * Processes open/closing of Research 
 * specifications blocks (open/close)
 *
 */
var processSpecPageBlocks = function() {

	var spec_blocks = $j('div.specHeader');
	spec_blocks.each(function(i) {
        
        var $this = $j(this);
		
		//bind to the link in the header 
		$this.find('div.title > a').click(function(e) {
			e.stopPropagation();
			$this.find('ul li').toggleClass('open');
			// toggle legend
			if ($this.find('.iconKeyStyle'))
				$this.find('.iconKeyStyle').toggle();
		});
	});
	
};

/*
 * Processes open/closing of store position blocks
 *
 */
var processStorePositionBlocks = function() {
	
	/* images for toggling hidden/displayed copy */
	var profiles = $j('div .profileHeader');
	
	profiles.each(function(i){
		var cell = $j(this).find('ul').get(0);
		var span = $j(this).next();
		
        $j(cell).bind("click", function(e) {
			
		    var image = $j(this).find('img').get(0);	
			
			//cancel bubble
			e.cancelBubble=true;
			e.preventDefault();
			
			if(span.hasClass("branch-open"))
				image.src = "/img/community/icon_open.gif";
			else
				image.src = "/img/community/icon_close.gif";

			span.toggleClass("branch-open");
			$j(cell).toggleClass("blue");
		});
	});
};

/* 
 * FUNCTION: prepMenu
 *
 * Event handler for global menu.
 *
 */
var prepMenu = function() {

	try {
		var subs = ['#submenu0','#submenu1','#submenu2','#submenu3'];
		$j("#submenu0,#submenu1,#submenu2,#submenu3").each( function(index) {
		
			var idIn = '#nav_in_'+index;
			var idOut = '#nav_out_'+index;
			
			$j(idIn).bind("mouseenter", function() {			
				$j(this).find('.submenuposition').removeClass('invisible');
				$j(this).find('.submenu').removeClass('invisible');
				$j(idOut).css('background', '#fff');
				//$j(idOut).zIndex = 100;
				var height = $j(subs[index]).height();
				$j(subs[index]).css('overflow', 'hidden').css('height', height).show(); 
			});
			
			$j(idIn).bind("mouseleave", function() {			
				setTimeout(function() {
					$j(this).addClass('invisible');
					$j(idIn).find('.submenuposition').addClass('invisible');
					$j(idOut).find('.submenu').addClass('invisible');
					$j(idOut).css('backgroundImage', 'url(/img/common/bg_mainnav_li.gif)');
					var height = $j(subs[index]).height();
					$j(subs[index]).css('overflow', 'hidden').css('height', height).hide(); 

				}, 100);
			});
		});
	} catch(e) {}
	
	/* Perform tweaks required for Safari only */
	if ($j.browser.safari) {
		$j('#globalNav').find('a').each( function(e) {
			$j(this).css('paddingBottom', '9px');
		});
	}
	
};

/*
 * FUNCTION: setRolloverHover
 *
 * Sets button SRC attribute to the hover state for
 * image buttons not using CSS sprites.
 *
 */
var setRolloverHover = function() {

	$j("input[type='image']").each(function(el)
	{
		var src = $j(this).attr('src');
		var src_rollover = src.replace(".gif","_hover.gif");

		$j(this).hover(
	        function() {
	            $j(this).attr('src', src_rollover);
    	    },
	        function() {
	            $j(this).attr('src', src);
    	    }
        );
    });

};

/*
 * FUNCTION: gc
 */
function gc(action) {
    gs($j('car_page').val(), action);
}

/*
 * FUNCTION: gs
 */
function gs(page, action, msf) {
    document.body.style.cursor='wait';
    var link='', anc='';
    if (page.length > 0) {
        if (page.indexOf("/") > -1)
            link = page;
        else link = $j("#"+page).val(); 
    }
    link += '?';

    if (action!=null && action!='') {
	var qs = action.split('#')
        if (qs.length>1) anc='#'+qs[1];
        link += qs[0];
    }

    if (link.charAt(link.length-1) != '?') link += '&';

    if (msf == undefined) msf = 'MS';
    link = ms(link,msf);

    if ($j('#search_state').val().length > 0) {
        if (link.charAt(link.length-1) != '&') link += '&';
        link += $j('#search_state').val();
    }
    location.href = link+anc;
}

/*
 * FUNCTION: askIfLeavingTest
 * 
 * If initial store was oas capable, ask if users wants to leave the test from Advanced Search
 * pfratus, 4/8/2010
 */
function askIfLeavingTest(url, question) {
    if (document.getElementById('initial-isoas').value == 'true')
    {
        var leaveTestAnswer = confirm(question);
        if (leaveTestAnswer) 
        {
            goSearch(url);
        }
        else 
        {
            document.getElementById('zip').value = document.getElementById('initial-zip').value;
        }
    }
}

/*
 * FUNCTION: LookupOASCapableOnHomePage
 * 
 * Call LookupOASCapable.aspx to see if store is OAS capable.
 * pfratus, 4/12/2010
 * Asks if user wants to leave the test on Home Page
 * Returns a boolean to the web page
 * Uses LookupOASCapableOnHomePageSucceeded and LookupOASCapableOnHomePageFailed. 
 * async = false, timeout 3 seconds in case of no response to prevent browser lock up
 * ISAPI Rewrite requires RewriteCond %[REQUEST_URI] !(LookupOASCapable.aspx) ahead of
 * both RewriteRules for isapi.html
 */
function LookupOASCapableOnHomePage(oldZipcode, question, controlName) {
    if (oldZipcode == document.getElementById(controlName).value) 
    {
        return true;
    }
    else
    {
        var ret = true;
        var context = oldZipcode + "|" + question + "|" + controlName;
	    
        $j.ajax({
          async: false,
          type: "POST",
          url: "/data/LookupOASCapable.aspx/IsOASCapable",
          data: "{'zipCode': '" + document.getElementById(controlName).value + "'}",
          context: context,
          contentType: "application/json; charset=utf-8",
          dataType: "json",
          timeout: 3000,
          success: function(msg) {
                ret = LookupOASCapableOnHomePageSucceeded(msg, this.context); 
          },
          error: function(msg) {
                ret = LookupOASCapableOnHomePageFailed(msg, this.context); 
          } 
        });	
        return ret;
    }
}

/*
 * FUNCTION: LookupOASCapableOnHomePageSucceeded
 *
 * This is the AJAX callback function for success.
 * pfratus, 4/12/2010
 */
function LookupOASCapableOnHomePageSucceeded(result, context) {
    var oldZipcode='', question='', controlName='';
    if (context!=null && context!='') {
	    var qs = context.split('|')
        oldZipcode = qs[0];
        question = qs[1];
        controlName = qs[2];
    }

    if (result.d)
    {
        //let them go to new zip
        return true;
    }
    else
    {
        var leaveTestAnswer = confirm(question);
        if (leaveTestAnswer)
        {
            //let them go to new zip
            return true;
        }
        else
        {
            document.getElementById(controlName).value = oldZipcode;
            //prevent them from going
            return false;
        }
    }
}

/*
 * FUNCTION: LookupOASCapableOnHomePageFailed
 *
 * This is the AJAX callback function for fail.
 * pfratus, 4/12/2010
 */
function LookupOASCapableOnHomePageFailed(result, context) {
    var oldZipcode='', question='', controlName='';
    if (context!=null && context!='') {
	    var qs = context.split('|')
        oldZipcode = qs[0];
        controlName = qs[2];
    }
    document.getElementById(controlName).value = oldZipcode;

    alert('LookupOASCapableOnHomePage Failed: ' + result.status + ' ' + result.statusText);
    //there needs to be more than an alert
    return false;
}  

/*
 * FUNCTION: LookupOASCapable
 *
 * Call LookupOASCapable.aspx to see if store is OAS capable.
 * pfratus, 4/7/2010
 * Uses LookupOASCapableSucceeded and LookupOASCapableFailed. 
 * ISAPI Rewrite requires RewriteCond %[REQUEST_URI] !(LookupOASCapable.aspx) ahead of
 * both RewriteRules for isapi.html
 */
function LookupOASCapable(oldZipcode, newZipcode, action, msf, question) 
{
    if (oldZipcode == newZipcode) 
    {
	    gs('', action, msf);
    }
    else
    {
        var context =  action + "|" + msf + "|" + oldZipcode + "|" + question;
	    
        $j.ajax({
          type: "POST",
          url: "/data/LookupOASCapable.aspx/IsOASCapable",
          data: "{'zipCode': '" + newZipcode + "'}",
          context: context,
          contentType: "application/json; charset=utf-8",
          dataType: "json",
          success: function(msg) {
                LookupOASCapableSucceeded(msg, this.context); 
          },
          error: function(msg) {
                LookupOASCapableFailed(msg, this.context); 
          }
        });	    
     }
}

/*
 * FUNCTION: LookupOASCapableSucceeded
 *
 * This is the AJAX callback function for success.
 * Asks if user wants to leave the test
 * pfratus, 4/7/2010
 */
function LookupOASCapableSucceeded(result, context) {
    var action='', msf='', oldZipcode='', question='';
    if (context!=null && context!='') {
	    var qs = context.split('|')
        action = qs[0];
        msf = qs[1];
        oldZipcode = qs[2];
        question = qs[3];
    }

    if (result.d)
    {
        gs('', action, msf);
    }
    else
    {
        var leaveTestAnswer = confirm(question);
        if (leaveTestAnswer)
        {
            gs('', action, msf);
        }
        else
        {
            document.getElementById('zip_code').value = oldZipcode;
        }
    }
}

/*
 * FUNCTION: LookupOASCapableFailed
 *
 * This is the AJAX callback function for fail.
 * pfratus, 4/7/2010
 */
function LookupOASCapableFailed(result, context) {
    var action='', msf='', oldZipcode='', question='';
    if (context!=null && context!='') {
	    var qs = context.split('|')
        oldZipcode = qs[2];
    }
    document.getElementById('zip_code').value = oldZipcode;

    alert('LookupOASCapable Failed: ' + result.status + ' ' + result.statusText);
    //there needs to be more than an alert
}  

/*
 * FUNCTION: ShowSurveyPopup
 *
 * Shows the survey popup window with the selected height, width.
 */
function ShowSurveyPopup(url, height, width)
{
	newWin=null;
	var h = height;
	var w = width;
	myleft=(screen.width)?(screen.width-w)/2:100;
	mytop=(screen.height)?(screen.height-h)/2:100;
	settings='width=' + w + ',height=' + h + ',top=' + mytop + ',left=' + myleft + ',scrollbars=1,toolbar=0,location=0,directories=0,status=1,menubar=0,resizable=1';
	newWin=window.open(url,null,settings);
	if (newWin!=null)
        newWin.focus();
}

/*
 * FUNCTION: ShowAlert shows email alert or saved search popup
 *      a       anchor
 *      mode    savesearch or newalert
 *
 */
function ShowAlert()
{
    var a = document.createElement('a');
    a.href='/enUS/search-editor/default.html?mode=newalert&' + $j('#search_state').val();
    a.setAttribute('rel','popup 700 750');
    showpopup(a);
}

/* Function to redirect from a select box change. */
function ClosePopupRedirect(hrefLocation){

    if (hrefLocation == null)
    {
        if ((opener != null) && (!opener.closed)) 
            opener.location.href = opener.location.href;
    }
    else
    {
		if (opener != null)
	        opener.location.href = hrefLocation;
    }
    window.close();    
}

function selectRedirect(selectBox)
{
	destination = selectBox.options[selectBox.selectedIndex].value;
	if (destination) location.href = destination;
}

function fireDefault(evt,target) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode==13 && target != undefined) {
       evt.cancelBubble = true;
       if (evt.stopPropagation) {
          evt.stopPropagation();
          evt.preventDefault();
       }
       var btn = document.getElementById(target);
       btn.click();
       return false;
    }
    return true;
}

function isNumberKey(evt,target) {

    var charCode = (evt.which) ? evt.which : evt.keyCode;
    var isnum = true;
    
    if (!evt.ctrlKey && charCode > 31 && (charCode < 48 || charCode > 57)) {
		isnum = false;
		if (evt.which)
			evt.preventDefault();        
		else 
			evt.returnValue = false;
    }

    return fireDefault(evt,target) && isnum;
}
		
/*
 * Converted Search Functions
 *
 */ 
 
function returnState(url) {
    var r,
    fields = ['CL','CD'],
    actions = ['ACLc','ACD'],
    action;

    for( var i = 0; i < fields.length; i++ ) {
        if ($j('#MF').val().indexOf(','+fields[i] +',')>-1) {
            action = '&'+ actions[i]  + '=' + $j('#'+fields[i]).val();
            r = url.indexOf('#');
            if (r == -1)
                url += action;
            else 
                url = url.substr(0,r) + action + url.substr(r,5);
        }
    }
    return url;
}

function addState(anc) {
    anc.href = returnState(anc.href);
}

function regMod(field, rMsf) {
    if ($j('#MF').val().indexOf(','+field+',') == -1 ) {
       if ($j('#MF').val().indexOf(',') == -1)
          $j('#MF').val(',');
        $j('#MF').val($j('#MF').val() + (field+','));
    }
    if (rMsf || rMsf != undefined){
        var msf = $j('#MSW');
        if (msf) 
            msf.val(msf.val().replace(findFullStr(msf.val(),rMsf),field));
    }
}

function findFullStr(str,stem) {
        var zcI = str.indexOf(stem,0);
        var sIdx = str.lastIndexOf("+",zcI);
        var lIdx = str.indexOf("~",zcI);
        if (lIdx == -1)lIdx = str.indexOf("+",zcI);
        return str.substring(sIdx+1,lIdx);
}

function gv(field) {
    return $j('#'+field).val();
}

function ms(link,msf) {
    var modifiedState = $j('#'+msf);

    if (modifiedState) {
        var modified = modifiedState.val();
        var namesValues = modified.split('~');
        var names  = namesValues[0].split('+'),
            values = namesValues[1].split('+'),
            blanks = namesValues[2];
        var fields = $j('#MF').val();
	    var value;


        for( var i = 0; i < names.length; i++ ) {
           if (fields.indexOf(','+values[i]+',') > -1) {
               var valueFields = values[i].split('-');
               value = gv(valueFields[0]);
                //if it is zip, we need the entered text to be displayed in the error msg
               if (value == 0 && names[i] !='AZ' && names[i] != 'APc') value = 'NA';

               if (valueFields.length > 1) {
                   var max = gv(valueFields[1]);

                   if (max == 0) max = 'NA';

                   value += '-' + max;
               }

               if (i>0 && link.charAt(link.length-1) != '&') link += '&';

               link += names[i] + '=' + value;
               var field = valueFields[0], num = parseInt(gv(field));

               if (num != NaN && num > 1000000 && blanks.indexOf(field) > -1)
                   $j('#'+field).val('');
           }
        }
    }

    return link;
}

/*
 * Processes the search refinement blocks (open/close)
 *
 */
var processSearchBlocks = function () {
	
	$j('#refine-blocks div h4 a').click( function(e) {
        var closed = $j('#CD');
        var parentDiv = $j(this).parents('div .refine-block');
        var x = $j(this).attr('rel');
        parentDiv.toggleClass('open');
        var closedList = (closed.val() === "") ? [] : closed.val().split('+');
		if (parentDiv.hasClass('open')) {
		    if($j.inArray(x, closedList) != -1) {
		        closedList.splice($j.inArray(x, closedList), 1);
		    }
		    closed.val((closedList.length === 0) ? "" : closedList.join('+'));
		}
		else {
		    if($j.inArray(x, closedList) == -1)
		        closedList.push(x);			        
		    closed.val((closedList.length === 1) ? x : closedList.join('+'));
		}
		regMod('CD');
		e.preventDefault();
	});

}

/***** END SEARCH ******/  

/*
 * Function: SetFocusOnEmail
 *
 * Sets focus to the email address of the login widget.
 *
 */
function SetFocusOnEmail(){
    var emailbox = $j('#mykmx_login .mykmx_body input:first');
    if (emailbox)
        emailbox.focus();
}

/*
 * Function: HideAuth
 *
 * Hides the login widget
 *
 */
function HideAuth(){
	
	if ($j("#mykmx_login"))
		$j("#mykmx_login").toggleClass("hide");
	
	if ($j("#myCarMax_login"))
		$j("#myCarMax_login").css("display", "block");
	
	if ($j("#myCarMax_close"))
		$j("#myCarMax_close").css("display", "none");
	
	if ($j("#myCarMax_reg"))
		$j("#myCarMax_reg").attr("class", "last");
}

function showpopup(a,evt)
{
	var arr = a.getAttribute("rel").split(" ");
	var href = a.href;
	if (arr && href) {
		var pH = popupH;
		var pW = popupW;
		var sW = screen.width;
		var sH = screen.height;
		if (arr.length > 1) {
			pH = arr[1];
			pW = arr[2];
			//if (arr.length == 4)href = arr[3];
		}
		var top = parseInt((sH - pH)/2);
		var left = parseInt((sW - pH)/2);
		var features = "status=no,scrollbars=yes,"+"height="+pH+",width="+pW+",top="+top+",left="+left;
		
		if (href.length >= 40)
		{
    		winRef = window.open(href,href.substring(0,40).replace(/[/.:?&=-]/g,"x"),features);
		}
		else
		{
    		winRef = window.open(href,"popup",features);
		}
		
		winRef.focus();
		var e=(evt)?evt:window.event;
		if (evt!=undefined)
		{
			if (window.event)
				e.cancelBubble=true;
			else
				e.stopPropagation();		
		}
		return false;
	}
}

<!--
// The Central Randomizer 1.3 (C) 1997 by Paul Houle (houle@msc.cornell.edu)
// See:  http://www.msc.cornell.edu/~houle/javascript/randomizer.html

rnd.today=new Date();
rnd.seed=rnd.today.getTime();

function rnd() {
        rnd.seed = (rnd.seed*9301+49297) % 233280;
        return rnd.seed/(233280.0);
};

function rand(number) {
        return Math.ceil(rnd()*number);
};

// end central randomizer. -->

/* Vehicle Image Fixer */
function vif(img,w) {
   var width = 132;
   if (",64,88,400,700,".indexOf("," + String(w) + ",") > -1 )
      width = w;
	  
   img.src = '/img/carpage/vehicles/comingSoon' + width + '.jpg';
}

var isSafari = $j.browser.safari;


popupH = 540;
popupW = 730;
winRef = null;
filtersChanged = false;

if (typeof kmx === "undefined") {var kmx={};} // Establish kmx namespace

// ------------------------------------------------------------------

var processCompareTables = function() {

	/* locate required HTML elements */
	var container = $j("table.compare:first");
	var tLeft  = $j('table.compare-leftnav:first');
	var tRight = $j('table.compare-rightnav:first');
	var scrollContainer = $j('div.scroll-container:first');
	var simulators = $j('div.scroll-simulator');

	/* If not found, script fails, exit */
	if (container.length == 0 || 
	    tLeft.length == 0 || 
	    tRight.length == 0 || 
	    scrollContainer.length == 0 || 
	    simulators.length == 0) return;

	/* Tweak Safari rendering issues */
	if (isSafari) {
		container.css("marginTop", "-20px");
		$j(simulators[0]).css("height", "21px");
		$j(simulators[1]).css("height", "auto");
	}

    /* opera issues */
	if ($j.browser.opera) {
		container.css("marginTop", "-20px");
		$j(simulators[0]).css("height", "37px");
		$j(simulators[1]).css("height", "auto");
	}

	/* resize hidden image to display upper scroll bar identically with lower scroll bar */
	var img = $j(simulators[1]).find('img:first');
	
	img.css("width", parseInt(tRight[0].scrollWidth, 10) + "px");

	/* process rows of both tables */
	lRows = tLeft.find('tr');
	rRows = tRight.find('tr');

    lRows.each( function (i) {	
		/* All other browsers respect setting row height; Safari requires setting cell height */
		var lRow = $j(this);
		var rRow = $j(rRows[i]);
		var lCell = lRow.find('td');
		var rCell = rRow.find('td');
		
		var lHeight = lCell[0].offsetHeight;
		var rHeight = rCell[0].offsetHeight;
	
		/* Based on general content, assume right row is taller */
		var changeValue = rHeight;
		
		/* Insert offset for IE; doesn't include padding in calculation? */
		var offset = ($j.browser.msie) ? 20 : 0;

		if (lHeight !== rHeight) {
		
	
			if (lHeight >= rHeight) {
				changeValue = lHeight
			}
			
			lCell.css('height', changeValue - offset + "px");
			rCell.css('height', changeValue - offset + "px");
			lCell.height(changeValue - offset);
			rCell.height(changeValue - offset);
		}

	});

    $j(simulators[1]).bind('scroll', function(e) {

        var el = e.target;
		//var el = Event.element(e);
		if (scrollContainer[0].scrollLeft != el.scrollLeft) {
			scrollContainer[0].scrollLeft = el.scrollLeft;
		}
        
    });
    
    scrollContainer.bind('scroll', function(e) {
    
		var el = e.target;
		if (simulators[1].scrollLeft != el.scrollLeft) {
			simulators[1].scrollLeft = el.scrollLeft;
		}
    
    });

}

/* 
* Homepage -- handles showing 
* and hiding tab flyouts on the homepage 
*
*/
var homepage = function() {

    // 
    // Close all open tabs 
    //
    var CloseTabs = function(evt) {
	
		if (evt) {
	        evt.preventDefault();
	        evt.stopPropagation();
		}

        $j("#tombstone .tabs li").removeClass("open");
        $j("#tombstone .tabs li").css("backgroundImage", "");
        $j("#tombstone .blocks").removeClass("active");
        $j("#tombstone .blocks li").removeClass("open");
    }
    
    // 
    // On Tab Click 
    //
    var TabClick = function(evt) {

        evt.preventDefault();
        evt.stopPropagation();

        var link = $j(evt.target);
        var tab = link.parents('li:first');

        if (tab.hasClass("open")) {
            CloseTabs();
        } else {
            CloseTabs();
            var className = tab.attr("class");

            //get class name of tab
            var url="url(/img/home/bg_tab_"+className+"_open.png)"

            //open tab
            tab.addClass("open");
            tab.css("backgroundImage", url);
            tab.css("backgroundPosition", "0 0");
            $j("#tombstone .blocks").addClass("active");
            $j("#tombstone .blocks li." + className).addClass("open");
        }
    }

    /* public object return */
    return {
        /* initialize the tabs */
        initialize: function() {

            $j("#tombstone .tabs li").each(function(e) {

                var $this = $j(this);

                if (!$this.hasClass("heading")) {
                    var link = $this.find("a");
                    link.click(TabClick);
                }
            });

            /* hook up close button for each tab */
            $j("#tombstone ul.blocks li.block a.close").click(CloseTabs);
        }
    };
} ();

/*
 * navigatePaymentCalculator
 * 
 * Helper function for calculator pages
 */
function navigatePaymentCalculator(url)
{
    var amt;
    var params;
    
    try
    {
        amt = $j('#calc-price').val();
        if (amt.length > 0)
            params = 'calc-price=' + amt;
    }
    catch (ex) { }
    
    try
    {
        amt = $j('#calc-ttt').val();
        if (amt.length > 0)
        {
            if (params != null)
                params = params + '&calc-ttt=' + amt;
            else
                params = 'calc-ttt=' + amt;
        }
    }
    catch (ex) { }
    
    if (params != null)
        window.location = url + '?' + params;
    else
        window.location = url;
}
function DoSearch(ep){
    var search = escape(document.getElementById('txtSearch').value);
    window.location = "/enUS/search-results/default.html?search="+search+"&Ep="+ep;
    return false;
}
function SearchEntry(event, target, ep){
    var run = DefaultButton(event, target);
    if(!run){
        DoSearch(ep);
        return false;
    }
    return true;
}
function SynchCars(){
    document.getElementById('mkCC').innerHTML++;
}

//Default button function
function DefaultButton(event, target){
    if (event.keyCode == 13 && !(event.srcElement && (event.srcElement.tagName.toLowerCase() == 'textarea')))
        return false;
    return true;
}

$j(document).ready(function() {
 // hides the slickbox as soon as the DOM is ready (a little sooner that page load)
  $j('#slickbox').hide();
  
 // shows and hides and toggles the slickbox on click  
  $j('#slick-show').click(function() {
    $j('#slickbox').show('slow');
    return false;
  });
  $j('#slick-hide').click(function() {
    $j('#slickbox').hide('fast');
    return false;
  });
});

function SaveCarsDisplayToggle(target){
    var item = $j("#" + target);
        
    $j("span[sf=container]").each( function(index){
            var element = $j(this);
            if(element.attr("id") != item.attr("id")){
                element.removeClass("on");
                element.addClass("off");
            }
        }
    );    
        
    if(item.hasClass("off")){
        item.removeClass("off");
        item.addClass("on");
    }
    else{
        item.removeClass("on");
        item.addClass("off");    
    }
}

function UpdateSaveFavoriteUrl(urlYes, urlNo, auth, update){
    var a = $j("a[sf=" + auth + "]");
    var c = $j("span[sf=" + update + "]").find(":checkbox");

    if (c.is(":checked"))
        a.attr("href",urlYes);
    else
        a.attr("href",urlNo);
}

/* 
 * --- END GLOBAL ---
 * 
 * DO NOT USE PROTOTYPE FOR ANY NEW DEVELOPMENT.
 * 
 */
