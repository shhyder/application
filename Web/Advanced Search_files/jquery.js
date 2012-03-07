/*
* jQuery JavaScript Library v1.3.1
* http://jquery.com/
*
* Copyright (c) 2009 John Resig
* Dual licensed under the MIT and GPL licenses.
* http://docs.jquery.com/License
*
* Date: 2009-01-21 20:42:16 -0500 (Wed, 21 Jan 2009)
* Revision: 6158
*/
(function() {
    var l = this, g, y = l.jQuery, p = l.$, o = l.jQuery = l.$ = function(E, F) { return new o.fn.init(E, F) }, D = /^[^<]*(<(.|\s)+>)[^>]*$|^#([\w-]+)$/, f = /^.[^:#\[\.,]*$/; o.fn = o.prototype = { init: function(E, H) { E = E || document; if (E.nodeType) { this[0] = E; this.length = 1; this.context = E; return this } if (typeof E === "string") { var G = D.exec(E); if (G && (G[1] || !H)) { if (G[1]) { E = o.clean([G[1]], H) } else { var I = document.getElementById(G[3]); if (I && I.id != G[3]) { return o().find(E) } var F = o(I || []); F.context = document; F.selector = E; return F } } else { return o(H).find(E) } } else { if (o.isFunction(E)) { return o(document).ready(E) } } if (E.selector && E.context) { this.selector = E.selector; this.context = E.context } return this.setArray(o.makeArray(E)) }, selector: "", jquery: "1.3.1", size: function() { return this.length }, get: function(E) { return E === g ? o.makeArray(this) : this[E] }, pushStack: function(F, H, E) { var G = o(F); G.prevObject = this; G.context = this.context; if (H === "find") { G.selector = this.selector + (this.selector ? " " : "") + E } else { if (H) { G.selector = this.selector + "." + H + "(" + E + ")" } } return G }, setArray: function(E) { this.length = 0; Array.prototype.push.apply(this, E); return this }, each: function(F, E) { return o.each(this, F, E) }, index: function(E) { return o.inArray(E && E.jquery ? E[0] : E, this) }, attr: function(F, H, G) { var E = F; if (typeof F === "string") { if (H === g) { return this[0] && o[G || "attr"](this[0], F) } else { E = {}; E[F] = H } } return this.each(function(I) { for (F in E) { o.attr(G ? this.style : this, F, o.prop(this, E[F], G, I, F)) } }) }, css: function(E, F) { if ((E == "width" || E == "height") && parseFloat(F) < 0) { F = g } return this.attr(E, F, "curCSS") }, text: function(F) { if (typeof F !== "object" && F != null) { return this.empty().append((this[0] && this[0].ownerDocument || document).createTextNode(F)) } var E = ""; o.each(F || this, function() { o.each(this.childNodes, function() { if (this.nodeType != 8) { E += this.nodeType != 1 ? this.nodeValue : o.fn.text([this]) } }) }); return E }, wrapAll: function(E) { if (this[0]) { var F = o(E, this[0].ownerDocument).clone(); if (this[0].parentNode) { F.insertBefore(this[0]) } F.map(function() { var G = this; while (G.firstChild) { G = G.firstChild } return G }).append(this) } return this }, wrapInner: function(E) { return this.each(function() { o(this).contents().wrapAll(E) }) }, wrap: function(E) { return this.each(function() { o(this).wrapAll(E) }) }, append: function() { return this.domManip(arguments, true, function(E) { if (this.nodeType == 1) { this.appendChild(E) } }) }, prepend: function() { return this.domManip(arguments, true, function(E) { if (this.nodeType == 1) { this.insertBefore(E, this.firstChild) } }) }, before: function() { return this.domManip(arguments, false, function(E) { this.parentNode.insertBefore(E, this) }) }, after: function() { return this.domManip(arguments, false, function(E) { this.parentNode.insertBefore(E, this.nextSibling) }) }, end: function() { return this.prevObject || o([]) }, push: [].push, find: function(E) { if (this.length === 1 && !/,/.test(E)) { var G = this.pushStack([], "find", E); G.length = 0; o.find(E, this[0], G); return G } else { var F = o.map(this, function(H) { return o.find(E, H) }); return this.pushStack(/[^+>] [^+>]/.test(E) ? o.unique(F) : F, "find", E) } }, clone: function(F) { var E = this.map(function() { if (!o.support.noCloneEvent && !o.isXMLDoc(this)) { var I = this.cloneNode(true), H = document.createElement("div"); H.appendChild(I); return o.clean([H.innerHTML])[0] } else { return this.cloneNode(true) } }); var G = E.find("*").andSelf().each(function() { if (this[h] !== g) { this[h] = null } }); if (F === true) { this.find("*").andSelf().each(function(I) { if (this.nodeType == 3) { return } var H = o.data(this, "events"); for (var K in H) { for (var J in H[K]) { o.event.add(G[I], K, H[K][J], H[K][J].data) } } }) } return E }, filter: function(E) { return this.pushStack(o.isFunction(E) && o.grep(this, function(G, F) { return E.call(G, F) }) || o.multiFilter(E, o.grep(this, function(F) { return F.nodeType === 1 })), "filter", E) }, closest: function(E) { var F = o.expr.match.POS.test(E) ? o(E) : null; return this.map(function() { var G = this; while (G && G.ownerDocument) { if (F ? F.index(G) > -1 : o(G).is(E)) { return G } G = G.parentNode } }) }, not: function(E) { if (typeof E === "string") { if (f.test(E)) { return this.pushStack(o.multiFilter(E, this, true), "not", E) } else { E = o.multiFilter(E, this) } } var F = E.length && E[E.length - 1] !== g && !E.nodeType; return this.filter(function() { return F ? o.inArray(this, E) < 0 : this != E }) }, add: function(E) { return this.pushStack(o.unique(o.merge(this.get(), typeof E === "string" ? o(E) : o.makeArray(E)))) }, is: function(E) { return !!E && o.multiFilter(E, this).length > 0 }, hasClass: function(E) { return !!E && this.is("." + E) }, val: function(K) { if (K === g) { var E = this[0]; if (E) { if (o.nodeName(E, "option")) { return (E.attributes.value || {}).specified ? E.value : E.text } if (o.nodeName(E, "select")) { var I = E.selectedIndex, L = [], M = E.options, H = E.type == "select-one"; if (I < 0) { return null } for (var F = H ? I : 0, J = H ? I + 1 : M.length; F < J; F++) { var G = M[F]; if (G.selected) { K = o(G).val(); if (H) { return K } L.push(K) } } return L } return (E.value || "").replace(/\r/g, "") } return g } if (typeof K === "number") { K += "" } return this.each(function() { if (this.nodeType != 1) { return } if (o.isArray(K) && /radio|checkbox/.test(this.type)) { this.checked = (o.inArray(this.value, K) >= 0 || o.inArray(this.name, K) >= 0) } else { if (o.nodeName(this, "select")) { var N = o.makeArray(K); o("option", this).each(function() { this.selected = (o.inArray(this.value, N) >= 0 || o.inArray(this.text, N) >= 0) }); if (!N.length) { this.selectedIndex = -1 } } else { this.value = K } } }) }, html: function(E) { return E === g ? (this[0] ? this[0].innerHTML : null) : this.empty().append(E) }, replaceWith: function(E) { return this.after(E).remove() }, eq: function(E) { return this.slice(E, +E + 1) }, slice: function() { return this.pushStack(Array.prototype.slice.apply(this, arguments), "slice", Array.prototype.slice.call(arguments).join(",")) }, map: function(E) { return this.pushStack(o.map(this, function(G, F) { return E.call(G, F, G) })) }, andSelf: function() { return this.add(this.prevObject) }, domManip: function(K, N, M) { if (this[0]) { var J = (this[0].ownerDocument || this[0]).createDocumentFragment(), G = o.clean(K, (this[0].ownerDocument || this[0]), J), I = J.firstChild, E = this.length > 1 ? J.cloneNode(true) : J; if (I) { for (var H = 0, F = this.length; H < F; H++) { M.call(L(this[H], I), H > 0 ? E.cloneNode(true) : J) } } if (G) { o.each(G, z) } } return this; function L(O, P) { return N && o.nodeName(O, "table") && o.nodeName(P, "tr") ? (O.getElementsByTagName("tbody")[0] || O.appendChild(O.ownerDocument.createElement("tbody"))) : O } } }; o.fn.init.prototype = o.fn; function z(E, F) { if (F.src) { o.ajax({ url: F.src, async: false, dataType: "script" }) } else { o.globalEval(F.text || F.textContent || F.innerHTML || "") } if (F.parentNode) { F.parentNode.removeChild(F) } } function e() { return +new Date } o.extend = o.fn.extend = function() { var J = arguments[0] || {}, H = 1, I = arguments.length, E = false, G; if (typeof J === "boolean") { E = J; J = arguments[1] || {}; H = 2 } if (typeof J !== "object" && !o.isFunction(J)) { J = {} } if (I == H) { J = this; --H } for (; H < I; H++) { if ((G = arguments[H]) != null) { for (var F in G) { var K = J[F], L = G[F]; if (J === L) { continue } if (E && L && typeof L === "object" && !L.nodeType) { J[F] = o.extend(E, K || (L.length != null ? [] : {}), L) } else { if (L !== g) { J[F] = L } } } } } return J }; var b = /z-?index|font-?weight|opacity|zoom|line-?height/i, q = document.defaultView || {}, s = Object.prototype.toString; o.extend({ noConflict: function(E) { l.$ = p; if (E) { l.jQuery = y } return o }, isFunction: function(E) { return s.call(E) === "[object Function]" }, isArray: function(E) { return s.call(E) === "[object Array]" }, isXMLDoc: function(E) { return E.nodeType === 9 && E.documentElement.nodeName !== "HTML" || !!E.ownerDocument && o.isXMLDoc(E.ownerDocument) }, globalEval: function(G) { G = o.trim(G); if (G) { var F = document.getElementsByTagName("head")[0] || document.documentElement, E = document.createElement("script"); E.type = "text/javascript"; if (o.support.scriptEval) { E.appendChild(document.createTextNode(G)) } else { E.text = G } F.insertBefore(E, F.firstChild); F.removeChild(E) } }, nodeName: function(F, E) { return F.nodeName && F.nodeName.toUpperCase() == E.toUpperCase() }, each: function(G, K, F) { var E, H = 0, I = G.length; if (F) { if (I === g) { for (E in G) { if (K.apply(G[E], F) === false) { break } } } else { for (; H < I; ) { if (K.apply(G[H++], F) === false) { break } } } } else { if (I === g) { for (E in G) { if (K.call(G[E], E, G[E]) === false) { break } } } else { for (var J = G[0]; H < I && K.call(J, H, J) !== false; J = G[++H]) { } } } return G }, prop: function(H, I, G, F, E) { if (o.isFunction(I)) { I = I.call(H, F) } return typeof I === "number" && G == "curCSS" && !b.test(E) ? I + "px" : I }, className: { add: function(E, F) { o.each((F || "").split(/\s+/), function(G, H) { if (E.nodeType == 1 && !o.className.has(E.className, H)) { E.className += (E.className ? " " : "") + H } }) }, remove: function(E, F) { if (E.nodeType == 1) { E.className = F !== g ? o.grep(E.className.split(/\s+/), function(G) { return !o.className.has(F, G) }).join(" ") : "" } }, has: function(F, E) { return F && o.inArray(E, (F.className || F).toString().split(/\s+/)) > -1 } }, swap: function(H, G, I) { var E = {}; for (var F in G) { E[F] = H.style[F]; H.style[F] = G[F] } I.call(H); for (var F in G) { H.style[F] = E[F] } }, css: function(G, E, I) { if (E == "width" || E == "height") { var K, F = { position: "absolute", visibility: "hidden", display: "block" }, J = E == "width" ? ["Left", "Right"] : ["Top", "Bottom"]; function H() { K = E == "width" ? G.offsetWidth : G.offsetHeight; var M = 0, L = 0; o.each(J, function() { M += parseFloat(o.curCSS(G, "padding" + this, true)) || 0; L += parseFloat(o.curCSS(G, "border" + this + "Width", true)) || 0 }); K -= Math.round(M + L) } if (o(G).is(":visible")) { H() } else { o.swap(G, F, H) } return Math.max(0, K) } return o.curCSS(G, E, I) }, curCSS: function(I, F, G) { var L, E = I.style; if (F == "opacity" && !o.support.opacity) { L = o.attr(E, "opacity"); return L == "" ? "1" : L } if (F.match(/float/i)) { F = w } if (!G && E && E[F]) { L = E[F] } else { if (q.getComputedStyle) { if (F.match(/float/i)) { F = "float" } F = F.replace(/([A-Z])/g, "-$1").toLowerCase(); var M = q.getComputedStyle(I, null); if (M) { L = M.getPropertyValue(F) } if (F == "opacity" && L == "") { L = "1" } } else { if (I.currentStyle) { var J = F.replace(/\-(\w)/g, function(N, O) { return O.toUpperCase() }); L = I.currentStyle[F] || I.currentStyle[J]; if (!/^\d+(px)?$/i.test(L) && /^\d/.test(L)) { var H = E.left, K = I.runtimeStyle.left; I.runtimeStyle.left = I.currentStyle.left; E.left = L || 0; L = E.pixelLeft + "px"; E.left = H; I.runtimeStyle.left = K } } } } return L }, clean: function(F, K, I) { K = K || document; if (typeof K.createElement === "undefined") { K = K.ownerDocument || K[0] && K[0].ownerDocument || document } if (!I && F.length === 1 && typeof F[0] === "string") { var H = /^<(\w+)\s*\/?>$/.exec(F[0]); if (H) { return [K.createElement(H[1])] } } var G = [], E = [], L = K.createElement("div"); o.each(F, function(P, R) { if (typeof R === "number") { R += "" } if (!R) { return } if (typeof R === "string") { R = R.replace(/(<(\w+)[^>]*?)\/>/g, function(T, U, S) { return S.match(/^(abbr|br|col|img|input|link|meta|param|hr|area|embed)$/i) ? T : U + "></" + S + ">" }); var O = o.trim(R).toLowerCase(); var Q = !O.indexOf("<opt") && [1, "<select multiple='multiple'>", "</select>"] || !O.indexOf("<leg") && [1, "<fieldset>", "</fieldset>"] || O.match(/^<(thead|tbody|tfoot|colg|cap)/) && [1, "<table>", "</table>"] || !O.indexOf("<tr") && [2, "<table><tbody>", "</tbody></table>"] || (!O.indexOf("<td") || !O.indexOf("<th")) && [3, "<table><tbody><tr>", "</tr></tbody></table>"] || !O.indexOf("<col") && [2, "<table><tbody></tbody><colgroup>", "</colgroup></table>"] || !o.support.htmlSerialize && [1, "div<div>", "</div>"] || [0, "", ""]; L.innerHTML = Q[1] + R + Q[2]; while (Q[0]--) { L = L.lastChild } if (!o.support.tbody) { var N = !O.indexOf("<table") && O.indexOf("<tbody") < 0 ? L.firstChild && L.firstChild.childNodes : Q[1] == "<table>" && O.indexOf("<tbody") < 0 ? L.childNodes : []; for (var M = N.length - 1; M >= 0; --M) { if (o.nodeName(N[M], "tbody") && !N[M].childNodes.length) { N[M].parentNode.removeChild(N[M]) } } } if (!o.support.leadingWhitespace && /^\s/.test(R)) { L.insertBefore(K.createTextNode(R.match(/^\s*/)[0]), L.firstChild) } R = o.makeArray(L.childNodes) } if (R.nodeType) { G.push(R) } else { G = o.merge(G, R) } }); if (I) { for (var J = 0; G[J]; J++) { if (o.nodeName(G[J], "script") && (!G[J].type || G[J].type.toLowerCase() === "text/javascript")) { E.push(G[J].parentNode ? G[J].parentNode.removeChild(G[J]) : G[J]) } else { if (G[J].nodeType === 1) { G.splice.apply(G, [J + 1, 0].concat(o.makeArray(G[J].getElementsByTagName("script")))) } I.appendChild(G[J]) } } return E } return G }, attr: function(J, G, K) { if (!J || J.nodeType == 3 || J.nodeType == 8) { return g } var H = !o.isXMLDoc(J), L = K !== g; G = H && o.props[G] || G; if (J.tagName) { var F = /href|src|style/.test(G); if (G == "selected" && J.parentNode) { J.parentNode.selectedIndex } if (G in J && H && !F) { if (L) { if (G == "type" && o.nodeName(J, "input") && J.parentNode) { throw "type property can't be changed" } J[G] = K } if (o.nodeName(J, "form") && J.getAttributeNode(G)) { return J.getAttributeNode(G).nodeValue } if (G == "tabIndex") { var I = J.getAttributeNode("tabIndex"); return I && I.specified ? I.value : J.nodeName.match(/(button|input|object|select|textarea)/i) ? 0 : J.nodeName.match(/^(a|area)$/i) && J.href ? 0 : g } return J[G] } if (!o.support.style && H && G == "style") { return o.attr(J.style, "cssText", K) } if (L) { J.setAttribute(G, "" + K) } var E = !o.support.hrefNormalized && H && F ? J.getAttribute(G, 2) : J.getAttribute(G); return E === null ? g : E } if (!o.support.opacity && G == "opacity") { if (L) { J.zoom = 1; J.filter = (J.filter || "").replace(/alpha\([^)]*\)/, "") + (parseInt(K) + "" == "NaN" ? "" : "alpha(opacity=" + K * 100 + ")") } return J.filter && J.filter.indexOf("opacity=") >= 0 ? (parseFloat(J.filter.match(/opacity=([^)]*)/)[1]) / 100) + "" : "" } G = G.replace(/-([a-z])/ig, function(M, N) { return N.toUpperCase() }); if (L) { J[G] = K } return J[G] }, trim: function(E) { return (E || "").replace(/^\s+|\s+$/g, "") }, makeArray: function(G) { var E = []; if (G != null) { var F = G.length; if (F == null || typeof G === "string" || o.isFunction(G) || G.setInterval) { E[0] = G } else { while (F) { E[--F] = G[F] } } } return E }, inArray: function(G, H) { for (var E = 0, F = H.length; E < F; E++) { if (H[E] === G) { return E } } return -1 }, merge: function(H, E) { var F = 0, G, I = H.length; if (!o.support.getAll) { while ((G = E[F++]) != null) { if (G.nodeType != 8) { H[I++] = G } } } else { while ((G = E[F++]) != null) { H[I++] = G } } return H }, unique: function(K) { var F = [], E = {}; try { for (var G = 0, H = K.length; G < H; G++) { var J = o.data(K[G]); if (!E[J]) { E[J] = true; F.push(K[G]) } } } catch (I) { F = K } return F }, grep: function(F, J, E) { var G = []; for (var H = 0, I = F.length; H < I; H++) { if (!E != !J(F[H], H)) { G.push(F[H]) } } return G }, map: function(E, J) { var F = []; for (var G = 0, H = E.length; G < H; G++) { var I = J(E[G], G); if (I != null) { F[F.length] = I } } return F.concat.apply([], F) } }); var C = navigator.userAgent.toLowerCase(); o.browser = { version: (C.match(/.+(?:rv|it|ra|ie)[\/: ]([\d.]+)/) || [0, "0"])[1], safari: /webkit/.test(C), opera: /opera/.test(C), msie: /msie/.test(C) && !/opera/.test(C), mozilla: /mozilla/.test(C) && !/(compatible|webkit)/.test(C) }; o.each({ parent: function(E) { return E.parentNode }, parents: function(E) { return o.dir(E, "parentNode") }, next: function(E) { return o.nth(E, 2, "nextSibling") }, prev: function(E) { return o.nth(E, 2, "previousSibling") }, nextAll: function(E) { return o.dir(E, "nextSibling") }, prevAll: function(E) { return o.dir(E, "previousSibling") }, siblings: function(E) { return o.sibling(E.parentNode.firstChild, E) }, children: function(E) { return o.sibling(E.firstChild) }, contents: function(E) { return o.nodeName(E, "iframe") ? E.contentDocument || E.contentWindow.document : o.makeArray(E.childNodes) } }, function(E, F) { o.fn[E] = function(G) { var H = o.map(this, F); if (G && typeof G == "string") { H = o.multiFilter(G, H) } return this.pushStack(o.unique(H), E, G) } }); o.each({ appendTo: "append", prependTo: "prepend", insertBefore: "before", insertAfter: "after", replaceAll: "replaceWith" }, function(E, F) { o.fn[E] = function() { var G = arguments; return this.each(function() { for (var H = 0, I = G.length; H < I; H++) { o(G[H])[F](this) } }) } }); o.each({ removeAttr: function(E) { o.attr(this, E, ""); if (this.nodeType == 1) { this.removeAttribute(E) } }, addClass: function(E) { o.className.add(this, E) }, removeClass: function(E) { o.className.remove(this, E) }, toggleClass: function(F, E) { if (typeof E !== "boolean") { E = !o.className.has(this, F) } o.className[E ? "add" : "remove"](this, F) }, remove: function(E) { if (!E || o.filter(E, [this]).length) { o("*", this).add([this]).each(function() { o.event.remove(this); o.removeData(this) }); if (this.parentNode) { this.parentNode.removeChild(this) } } }, empty: function() { o(">*", this).remove(); while (this.firstChild) { this.removeChild(this.firstChild) } } }, function(E, F) { o.fn[E] = function() { return this.each(F, arguments) } }); function j(E, F) { return E[0] && parseInt(o.curCSS(E[0], F, true), 10) || 0 } var h = "jQuery" + e(), v = 0, A = {}; o.extend({ cache: {}, data: function(F, E, G) { F = F == l ? A : F; var H = F[h]; if (!H) { H = F[h] = ++v } if (E && !o.cache[H]) { o.cache[H] = {} } if (G !== g) { o.cache[H][E] = G } return E ? o.cache[H][E] : H }, removeData: function(F, E) { F = F == l ? A : F; var H = F[h]; if (E) { if (o.cache[H]) { delete o.cache[H][E]; E = ""; for (E in o.cache[H]) { break } if (!E) { o.removeData(F) } } } else { try { delete F[h] } catch (G) { if (F.removeAttribute) { F.removeAttribute(h) } } delete o.cache[H] } }, queue: function(F, E, H) { if (F) { E = (E || "fx") + "queue"; var G = o.data(F, E); if (!G || o.isArray(H)) { G = o.data(F, E, o.makeArray(H)) } else { if (H) { G.push(H) } } } return G }, dequeue: function(H, G) { var E = o.queue(H, G), F = E.shift(); if (!G || G === "fx") { F = E[0] } if (F !== g) { F.call(H) } } }); o.fn.extend({ data: function(E, G) { var H = E.split("."); H[1] = H[1] ? "." + H[1] : ""; if (G === g) { var F = this.triggerHandler("getData" + H[1] + "!", [H[0]]); if (F === g && this.length) { F = o.data(this[0], E) } return F === g && H[1] ? this.data(H[0]) : F } else { return this.trigger("setData" + H[1] + "!", [H[0], G]).each(function() { o.data(this, E, G) }) } }, removeData: function(E) { return this.each(function() { o.removeData(this, E) }) }, queue: function(E, F) { if (typeof E !== "string") { F = E; E = "fx" } if (F === g) { return o.queue(this[0], E) } return this.each(function() { var G = o.queue(this, E, F); if (E == "fx" && G.length == 1) { G[0].call(this) } }) }, dequeue: function(E) { return this.each(function() { o.dequeue(this, E) }) } });
    /*
    * Sizzle CSS Selector Engine - v0.9.3
    *  Copyright 2009, The Dojo Foundation
    *  Released under the MIT, BSD, and GPL Licenses.
    *  More information: http://sizzlejs.com/
    */
    (function() { var Q = /((?:\((?:\([^()]+\)|[^()]+)+\)|\[(?:\[[^[\]]*\]|['"][^'"]+['"]|[^[\]'"]+)+\]|\\.|[^ >+~,(\[]+)+|[>+~])(\s*,\s*)?/g, K = 0, G = Object.prototype.toString; var F = function(X, T, aa, ab) { aa = aa || []; T = T || document; if (T.nodeType !== 1 && T.nodeType !== 9) { return [] } if (!X || typeof X !== "string") { return aa } var Y = [], V, ae, ah, S, ac, U, W = true; Q.lastIndex = 0; while ((V = Q.exec(X)) !== null) { Y.push(V[1]); if (V[2]) { U = RegExp.rightContext; break } } if (Y.length > 1 && L.exec(X)) { if (Y.length === 2 && H.relative[Y[0]]) { ae = I(Y[0] + Y[1], T) } else { ae = H.relative[Y[0]] ? [T] : F(Y.shift(), T); while (Y.length) { X = Y.shift(); if (H.relative[X]) { X += Y.shift() } ae = I(X, ae) } } } else { var ad = ab ? { expr: Y.pop(), set: E(ab)} : F.find(Y.pop(), Y.length === 1 && T.parentNode ? T.parentNode : T, P(T)); ae = F.filter(ad.expr, ad.set); if (Y.length > 0) { ah = E(ae) } else { W = false } while (Y.length) { var ag = Y.pop(), af = ag; if (!H.relative[ag]) { ag = "" } else { af = Y.pop() } if (af == null) { af = T } H.relative[ag](ah, af, P(T)) } } if (!ah) { ah = ae } if (!ah) { throw "Syntax error, unrecognized expression: " + (ag || X) } if (G.call(ah) === "[object Array]") { if (!W) { aa.push.apply(aa, ah) } else { if (T.nodeType === 1) { for (var Z = 0; ah[Z] != null; Z++) { if (ah[Z] && (ah[Z] === true || ah[Z].nodeType === 1 && J(T, ah[Z]))) { aa.push(ae[Z]) } } } else { for (var Z = 0; ah[Z] != null; Z++) { if (ah[Z] && ah[Z].nodeType === 1) { aa.push(ae[Z]) } } } } } else { E(ah, aa) } if (U) { F(U, T, aa, ab) } return aa }; F.matches = function(S, T) { return F(S, null, null, T) }; F.find = function(Z, S, aa) { var Y, W; if (!Z) { return [] } for (var V = 0, U = H.order.length; V < U; V++) { var X = H.order[V], W; if ((W = H.match[X].exec(Z))) { var T = RegExp.leftContext; if (T.substr(T.length - 1) !== "\\") { W[1] = (W[1] || "").replace(/\\/g, ""); Y = H.find[X](W, S, aa); if (Y != null) { Z = Z.replace(H.match[X], ""); break } } } } if (!Y) { Y = S.getElementsByTagName("*") } return { set: Y, expr: Z} }; F.filter = function(ab, aa, ae, V) { var U = ab, ag = [], Y = aa, X, S; while (ab && aa.length) { for (var Z in H.filter) { if ((X = H.match[Z].exec(ab)) != null) { var T = H.filter[Z], af, ad; S = false; if (Y == ag) { ag = [] } if (H.preFilter[Z]) { X = H.preFilter[Z](X, Y, ae, ag, V); if (!X) { S = af = true } else { if (X === true) { continue } } } if (X) { for (var W = 0; (ad = Y[W]) != null; W++) { if (ad) { af = T(ad, X, W, Y); var ac = V ^ !!af; if (ae && af != null) { if (ac) { S = true } else { Y[W] = false } } else { if (ac) { ag.push(ad); S = true } } } } } if (af !== g) { if (!ae) { Y = ag } ab = ab.replace(H.match[Z], ""); if (!S) { return [] } break } } } ab = ab.replace(/\s*,\s*/, ""); if (ab == U) { if (S == null) { throw "Syntax error, unrecognized expression: " + ab } else { break } } U = ab } return Y }; var H = F.selectors = { order: ["ID", "NAME", "TAG"], match: { ID: /#((?:[\w\u00c0-\uFFFF_-]|\\.)+)/, CLASS: /\.((?:[\w\u00c0-\uFFFF_-]|\\.)+)/, NAME: /\[name=['"]*((?:[\w\u00c0-\uFFFF_-]|\\.)+)['"]*\]/, ATTR: /\[\s*((?:[\w\u00c0-\uFFFF_-]|\\.)+)\s*(?:(\S?=)\s*(['"]*)(.*?)\3|)\s*\]/, TAG: /^((?:[\w\u00c0-\uFFFF\*_-]|\\.)+)/, CHILD: /:(only|nth|last|first)-child(?:\((even|odd|[\dn+-]*)\))?/, POS: /:(nth|eq|gt|lt|first|last|even|odd)(?:\((\d*)\))?(?=[^-]|$)/, PSEUDO: /:((?:[\w\u00c0-\uFFFF_-]|\\.)+)(?:\((['"]*)((?:\([^\)]+\)|[^\2\(\)]*)+)\2\))?/ }, attrMap: { "class": "className", "for": "htmlFor" }, attrHandle: { href: function(S) { return S.getAttribute("href") } }, relative: { "+": function(W, T) { for (var U = 0, S = W.length; U < S; U++) { var V = W[U]; if (V) { var X = V.previousSibling; while (X && X.nodeType !== 1) { X = X.previousSibling } W[U] = typeof T === "string" ? X || false : X === T } } if (typeof T === "string") { F.filter(T, W, true) } }, ">": function(X, T, Y) { if (typeof T === "string" && !/\W/.test(T)) { T = Y ? T : T.toUpperCase(); for (var U = 0, S = X.length; U < S; U++) { var W = X[U]; if (W) { var V = W.parentNode; X[U] = V.nodeName === T ? V : false } } } else { for (var U = 0, S = X.length; U < S; U++) { var W = X[U]; if (W) { X[U] = typeof T === "string" ? W.parentNode : W.parentNode === T } } if (typeof T === "string") { F.filter(T, X, true) } } }, "": function(V, T, X) { var U = "done" + (K++), S = R; if (!T.match(/\W/)) { var W = T = X ? T : T.toUpperCase(); S = O } S("parentNode", T, U, V, W, X) }, "~": function(V, T, X) { var U = "done" + (K++), S = R; if (typeof T === "string" && !T.match(/\W/)) { var W = T = X ? T : T.toUpperCase(); S = O } S("previousSibling", T, U, V, W, X) } }, find: { ID: function(T, U, V) { if (typeof U.getElementById !== "undefined" && !V) { var S = U.getElementById(T[1]); return S ? [S] : [] } }, NAME: function(S, T, U) { if (typeof T.getElementsByName !== "undefined" && !U) { return T.getElementsByName(S[1]) } }, TAG: function(S, T) { return T.getElementsByTagName(S[1]) } }, preFilter: { CLASS: function(V, T, U, S, Y) { V = " " + V[1].replace(/\\/g, "") + " "; var X; for (var W = 0; (X = T[W]) != null; W++) { if (X) { if (Y ^ (" " + X.className + " ").indexOf(V) >= 0) { if (!U) { S.push(X) } } else { if (U) { T[W] = false } } } } return false }, ID: function(S) { return S[1].replace(/\\/g, "") }, TAG: function(T, S) { for (var U = 0; S[U] === false; U++) { } return S[U] && P(S[U]) ? T[1] : T[1].toUpperCase() }, CHILD: function(S) { if (S[1] == "nth") { var T = /(-?)(\d*)n((?:\+|-)?\d*)/.exec(S[2] == "even" && "2n" || S[2] == "odd" && "2n+1" || !/\D/.test(S[2]) && "0n+" + S[2] || S[2]); S[2] = (T[1] + (T[2] || 1)) - 0; S[3] = T[3] - 0 } S[0] = "done" + (K++); return S }, ATTR: function(T) { var S = T[1].replace(/\\/g, ""); if (H.attrMap[S]) { T[1] = H.attrMap[S] } if (T[2] === "~=") { T[4] = " " + T[4] + " " } return T }, PSEUDO: function(W, T, U, S, X) { if (W[1] === "not") { if (W[3].match(Q).length > 1) { W[3] = F(W[3], null, null, T) } else { var V = F.filter(W[3], T, U, true ^ X); if (!U) { S.push.apply(S, V) } return false } } else { if (H.match.POS.test(W[0])) { return true } } return W }, POS: function(S) { S.unshift(true); return S } }, filters: { enabled: function(S) { return S.disabled === false && S.type !== "hidden" }, disabled: function(S) { return S.disabled === true }, checked: function(S) { return S.checked === true }, selected: function(S) { S.parentNode.selectedIndex; return S.selected === true }, parent: function(S) { return !!S.firstChild }, empty: function(S) { return !S.firstChild }, has: function(U, T, S) { return !!F(S[3], U).length }, header: function(S) { return /h\d/i.test(S.nodeName) }, text: function(S) { return "text" === S.type }, radio: function(S) { return "radio" === S.type }, checkbox: function(S) { return "checkbox" === S.type }, file: function(S) { return "file" === S.type }, password: function(S) { return "password" === S.type }, submit: function(S) { return "submit" === S.type }, image: function(S) { return "image" === S.type }, reset: function(S) { return "reset" === S.type }, button: function(S) { return "button" === S.type || S.nodeName.toUpperCase() === "BUTTON" }, input: function(S) { return /input|select|textarea|button/i.test(S.nodeName) } }, setFilters: { first: function(T, S) { return S === 0 }, last: function(U, T, S, V) { return T === V.length - 1 }, even: function(T, S) { return S % 2 === 0 }, odd: function(T, S) { return S % 2 === 1 }, lt: function(U, T, S) { return T < S[3] - 0 }, gt: function(U, T, S) { return T > S[3] - 0 }, nth: function(U, T, S) { return S[3] - 0 == T }, eq: function(U, T, S) { return S[3] - 0 == T } }, filter: { CHILD: function(S, V) { var Y = V[1], Z = S.parentNode; var X = V[0]; if (Z && (!Z[X] || !S.nodeIndex)) { var W = 1; for (var T = Z.firstChild; T; T = T.nextSibling) { if (T.nodeType == 1) { T.nodeIndex = W++ } } Z[X] = W - 1 } if (Y == "first") { return S.nodeIndex == 1 } else { if (Y == "last") { return S.nodeIndex == Z[X] } else { if (Y == "only") { return Z[X] == 1 } else { if (Y == "nth") { var ab = false, U = V[2], aa = V[3]; if (U == 1 && aa == 0) { return true } if (U == 0) { if (S.nodeIndex == aa) { ab = true } } else { if ((S.nodeIndex - aa) % U == 0 && (S.nodeIndex - aa) / U >= 0) { ab = true } } return ab } } } } }, PSEUDO: function(Y, U, V, Z) { var T = U[1], W = H.filters[T]; if (W) { return W(Y, V, U, Z) } else { if (T === "contains") { return (Y.textContent || Y.innerText || "").indexOf(U[3]) >= 0 } else { if (T === "not") { var X = U[3]; for (var V = 0, S = X.length; V < S; V++) { if (X[V] === Y) { return false } } return true } } } }, ID: function(T, S) { return T.nodeType === 1 && T.getAttribute("id") === S }, TAG: function(T, S) { return (S === "*" && T.nodeType === 1) || T.nodeName === S }, CLASS: function(T, S) { return S.test(T.className) }, ATTR: function(W, U) { var S = H.attrHandle[U[1]] ? H.attrHandle[U[1]](W) : W[U[1]] || W.getAttribute(U[1]), X = S + "", V = U[2], T = U[4]; return S == null ? V === "!=" : V === "=" ? X === T : V === "*=" ? X.indexOf(T) >= 0 : V === "~=" ? (" " + X + " ").indexOf(T) >= 0 : !U[4] ? S : V === "!=" ? X != T : V === "^=" ? X.indexOf(T) === 0 : V === "$=" ? X.substr(X.length - T.length) === T : V === "|=" ? X === T || X.substr(0, T.length + 1) === T + "-" : false }, POS: function(W, T, U, X) { var S = T[2], V = H.setFilters[S]; if (V) { return V(W, U, T, X) } } } }; var L = H.match.POS; for (var N in H.match) { H.match[N] = RegExp(H.match[N].source + /(?![^\[]*\])(?![^\(]*\))/.source) } var E = function(T, S) { T = Array.prototype.slice.call(T); if (S) { S.push.apply(S, T); return S } return T }; try { Array.prototype.slice.call(document.documentElement.childNodes) } catch (M) { E = function(W, V) { var T = V || []; if (G.call(W) === "[object Array]") { Array.prototype.push.apply(T, W) } else { if (typeof W.length === "number") { for (var U = 0, S = W.length; U < S; U++) { T.push(W[U]) } } else { for (var U = 0; W[U]; U++) { T.push(W[U]) } } } return T } } (function() { var T = document.createElement("form"), U = "script" + (new Date).getTime(); T.innerHTML = "<input name='" + U + "'/>"; var S = document.documentElement; S.insertBefore(T, S.firstChild); if (!!document.getElementById(U)) { H.find.ID = function(W, X, Y) { if (typeof X.getElementById !== "undefined" && !Y) { var V = X.getElementById(W[1]); return V ? V.id === W[1] || typeof V.getAttributeNode !== "undefined" && V.getAttributeNode("id").nodeValue === W[1] ? [V] : g : [] } }; H.filter.ID = function(X, V) { var W = typeof X.getAttributeNode !== "undefined" && X.getAttributeNode("id"); return X.nodeType === 1 && W && W.nodeValue === V } } S.removeChild(T) })(); (function() { var S = document.createElement("div"); S.appendChild(document.createComment("")); if (S.getElementsByTagName("*").length > 0) { H.find.TAG = function(T, X) { var W = X.getElementsByTagName(T[1]); if (T[1] === "*") { var V = []; for (var U = 0; W[U]; U++) { if (W[U].nodeType === 1) { V.push(W[U]) } } W = V } return W } } S.innerHTML = "<a href='#'></a>"; if (S.firstChild && S.firstChild.getAttribute("href") !== "#") { H.attrHandle.href = function(T) { return T.getAttribute("href", 2) } } })(); if (document.querySelectorAll) { (function() { var S = F, T = document.createElement("div"); T.innerHTML = "<p class='TEST'></p>"; if (T.querySelectorAll && T.querySelectorAll(".TEST").length === 0) { return } F = function(X, W, U, V) { W = W || document; if (!V && W.nodeType === 9 && !P(W)) { try { return E(W.querySelectorAll(X), U) } catch (Y) { } } return S(X, W, U, V) }; F.find = S.find; F.filter = S.filter; F.selectors = S.selectors; F.matches = S.matches })() } if (document.getElementsByClassName && document.documentElement.getElementsByClassName) { H.order.splice(1, 0, "CLASS"); H.find.CLASS = function(S, T) { return T.getElementsByClassName(S[1]) } } function O(T, Z, Y, ac, aa, ab) { for (var W = 0, U = ac.length; W < U; W++) { var S = ac[W]; if (S) { S = S[T]; var X = false; while (S && S.nodeType) { var V = S[Y]; if (V) { X = ac[V]; break } if (S.nodeType === 1 && !ab) { S[Y] = W } if (S.nodeName === Z) { X = S; break } S = S[T] } ac[W] = X } } } function R(T, Y, X, ab, Z, aa) { for (var V = 0, U = ab.length; V < U; V++) { var S = ab[V]; if (S) { S = S[T]; var W = false; while (S && S.nodeType) { if (S[X]) { W = ab[S[X]]; break } if (S.nodeType === 1) { if (!aa) { S[X] = V } if (typeof Y !== "string") { if (S === Y) { W = true; break } } else { if (F.filter(Y, [S]).length > 0) { W = S; break } } } S = S[T] } ab[V] = W } } } var J = document.compareDocumentPosition ? function(T, S) { return T.compareDocumentPosition(S) & 16 } : function(T, S) { return T !== S && (T.contains ? T.contains(S) : true) }; var P = function(S) { return S.nodeType === 9 && S.documentElement.nodeName !== "HTML" || !!S.ownerDocument && P(S.ownerDocument) }; var I = function(S, Z) { var V = [], W = "", X, U = Z.nodeType ? [Z] : Z; while ((X = H.match.PSEUDO.exec(S))) { W += X[0]; S = S.replace(H.match.PSEUDO, "") } S = H.relative[S] ? S + "*" : S; for (var Y = 0, T = U.length; Y < T; Y++) { F(S, U[Y], V) } return F.filter(W, V) }; o.find = F; o.filter = F.filter; o.expr = F.selectors; o.expr[":"] = o.expr.filters; F.selectors.filters.hidden = function(S) { return "hidden" === S.type || o.css(S, "display") === "none" || o.css(S, "visibility") === "hidden" }; F.selectors.filters.visible = function(S) { return "hidden" !== S.type && o.css(S, "display") !== "none" && o.css(S, "visibility") !== "hidden" }; F.selectors.filters.animated = function(S) { return o.grep(o.timers, function(T) { return S === T.elem }).length }; o.multiFilter = function(U, S, T) { if (T) { U = ":not(" + U + ")" } return F.matches(U, S) }; o.dir = function(U, T) { var S = [], V = U[T]; while (V && V != document) { if (V.nodeType == 1) { S.push(V) } V = V[T] } return S }; o.nth = function(W, S, U, V) { S = S || 1; var T = 0; for (; W; W = W[U]) { if (W.nodeType == 1 && ++T == S) { break } } return W }; o.sibling = function(U, T) { var S = []; for (; U; U = U.nextSibling) { if (U.nodeType == 1 && U != T) { S.push(U) } } return S }; return; l.Sizzle = F })(); o.event = { add: function(I, F, H, K) { if (I.nodeType == 3 || I.nodeType == 8) { return } if (I.setInterval && I != l) { I = l } if (!H.guid) { H.guid = this.guid++ } if (K !== g) { var G = H; H = this.proxy(G); H.data = K } var E = o.data(I, "events") || o.data(I, "events", {}), J = o.data(I, "handle") || o.data(I, "handle", function() { return typeof o !== "undefined" && !o.event.triggered ? o.event.handle.apply(arguments.callee.elem, arguments) : g }); J.elem = I; o.each(F.split(/\s+/), function(M, N) { var O = N.split("."); N = O.shift(); H.type = O.slice().sort().join("."); var L = E[N]; if (o.event.specialAll[N]) { o.event.specialAll[N].setup.call(I, K, O) } if (!L) { L = E[N] = {}; if (!o.event.special[N] || o.event.special[N].setup.call(I, K, O) === false) { if (I.addEventListener) { I.addEventListener(N, J, false) } else { if (I.attachEvent) { I.attachEvent("on" + N, J) } } } } L[H.guid] = H; o.event.global[N] = true }); I = null }, guid: 1, global: {}, remove: function(K, H, J) { if (K.nodeType == 3 || K.nodeType == 8) { return } var G = o.data(K, "events"), F, E; if (G) { if (H === g || (typeof H === "string" && H.charAt(0) == ".")) { for (var I in G) { this.remove(K, I + (H || "")) } } else { if (H.type) { J = H.handler; H = H.type } o.each(H.split(/\s+/), function(M, O) { var Q = O.split("."); O = Q.shift(); var N = RegExp("(^|\\.)" + Q.slice().sort().join(".*\\.") + "(\\.|$)"); if (G[O]) { if (J) { delete G[O][J.guid] } else { for (var P in G[O]) { if (N.test(G[O][P].type)) { delete G[O][P] } } } if (o.event.specialAll[O]) { o.event.specialAll[O].teardown.call(K, Q) } for (F in G[O]) { break } if (!F) { if (!o.event.special[O] || o.event.special[O].teardown.call(K, Q) === false) { if (K.removeEventListener) { K.removeEventListener(O, o.data(K, "handle"), false) } else { if (K.detachEvent) { K.detachEvent("on" + O, o.data(K, "handle")) } } } F = null; delete G[O] } } }) } for (F in G) { break } if (!F) { var L = o.data(K, "handle"); if (L) { L.elem = null } o.removeData(K, "events"); o.removeData(K, "handle") } } }, trigger: function(I, K, H, E) { var G = I.type || I; if (!E) { I = typeof I === "object" ? I[h] ? I : o.extend(o.Event(G), I) : o.Event(G); if (G.indexOf("!") >= 0) { I.type = G = G.slice(0, -1); I.exclusive = true } if (!H) { I.stopPropagation(); if (this.global[G]) { o.each(o.cache, function() { if (this.events && this.events[G]) { o.event.trigger(I, K, this.handle.elem) } }) } } if (!H || H.nodeType == 3 || H.nodeType == 8) { return g } I.result = g; I.target = H; K = o.makeArray(K); K.unshift(I) } I.currentTarget = H; var J = o.data(H, "handle"); if (J) { J.apply(H, K) } if ((!H[G] || (o.nodeName(H, "a") && G == "click")) && H["on" + G] && H["on" + G].apply(H, K) === false) { I.result = false } if (!E && H[G] && !I.isDefaultPrevented() && !(o.nodeName(H, "a") && G == "click")) { this.triggered = true; try { H[G]() } catch (L) { } } this.triggered = false; if (!I.isPropagationStopped()) { var F = H.parentNode || H.ownerDocument; if (F) { o.event.trigger(I, K, F, true) } } }, handle: function(K) { var J, E; K = arguments[0] = o.event.fix(K || l.event); var L = K.type.split("."); K.type = L.shift(); J = !L.length && !K.exclusive; var I = RegExp("(^|\\.)" + L.slice().sort().join(".*\\.") + "(\\.|$)"); E = (o.data(this, "events") || {})[K.type]; for (var G in E) { var H = E[G]; if (J || I.test(H.type)) { K.handler = H; K.data = H.data; var F = H.apply(this, arguments); if (F !== g) { K.result = F; if (F === false) { K.preventDefault(); K.stopPropagation() } } if (K.isImmediatePropagationStopped()) { break } } } }, props: "altKey attrChange attrName bubbles button cancelable charCode clientX clientY ctrlKey currentTarget data detail eventPhase fromElement handler keyCode metaKey newValue originalTarget pageX pageY prevValue relatedNode relatedTarget screenX screenY shiftKey srcElement target toElement view wheelDelta which".split(" "), fix: function(H) { if (H[h]) { return H } var F = H; H = o.Event(F); for (var G = this.props.length, J; G; ) { J = this.props[--G]; H[J] = F[J] } if (!H.target) { H.target = H.srcElement || document } if (H.target.nodeType == 3) { H.target = H.target.parentNode } if (!H.relatedTarget && H.fromElement) { H.relatedTarget = H.fromElement == H.target ? H.toElement : H.fromElement } if (H.pageX == null && H.clientX != null) { var I = document.documentElement, E = document.body; H.pageX = H.clientX + (I && I.scrollLeft || E && E.scrollLeft || 0) - (I.clientLeft || 0); H.pageY = H.clientY + (I && I.scrollTop || E && E.scrollTop || 0) - (I.clientTop || 0) } if (!H.which && ((H.charCode || H.charCode === 0) ? H.charCode : H.keyCode)) { H.which = H.charCode || H.keyCode } if (!H.metaKey && H.ctrlKey) { H.metaKey = H.ctrlKey } if (!H.which && H.button) { H.which = (H.button & 1 ? 1 : (H.button & 2 ? 3 : (H.button & 4 ? 2 : 0))) } return H }, proxy: function(F, E) { E = E || function() { return F.apply(this, arguments) }; E.guid = F.guid = F.guid || E.guid || this.guid++; return E }, special: { ready: { setup: B, teardown: function() { } } }, specialAll: { live: { setup: function(E, F) { o.event.add(this, F[0], c) }, teardown: function(G) { if (G.length) { var E = 0, F = RegExp("(^|\\.)" + G[0] + "(\\.|$)"); o.each((o.data(this, "events").live || {}), function() { if (F.test(this.type)) { E++ } }); if (E < 1) { o.event.remove(this, G[0], c) } } } }} }; o.Event = function(E) { if (!this.preventDefault) { return new o.Event(E) } if (E && E.type) { this.originalEvent = E; this.type = E.type } else { this.type = E } this.timeStamp = e(); this[h] = true }; function k() { return false } function u() { return true } o.Event.prototype = { preventDefault: function() { this.isDefaultPrevented = u; var E = this.originalEvent; if (!E) { return } if (E.preventDefault) { E.preventDefault() } E.returnValue = false }, stopPropagation: function() { this.isPropagationStopped = u; var E = this.originalEvent; if (!E) { return } if (E.stopPropagation) { E.stopPropagation() } E.cancelBubble = true }, stopImmediatePropagation: function() { this.isImmediatePropagationStopped = u; this.stopPropagation() }, isDefaultPrevented: k, isPropagationStopped: k, isImmediatePropagationStopped: k }; var a = function(F) { var E = F.relatedTarget; while (E && E != this) { try { E = E.parentNode } catch (G) { E = this } } if (E != this) { F.type = F.data; o.event.handle.apply(this, arguments) } }; o.each({ mouseover: "mouseenter", mouseout: "mouseleave" }, function(F, E) { o.event.special[E] = { setup: function() { o.event.add(this, F, a, E) }, teardown: function() { o.event.remove(this, F, a) } } }); o.fn.extend({ bind: function(F, G, E) { return F == "unload" ? this.one(F, G, E) : this.each(function() { o.event.add(this, F, E || G, E && G) }) }, one: function(G, H, F) { var E = o.event.proxy(F || H, function(I) { o(this).unbind(I, E); return (F || H).apply(this, arguments) }); return this.each(function() { o.event.add(this, G, E, F && H) }) }, unbind: function(F, E) { return this.each(function() { o.event.remove(this, F, E) }) }, trigger: function(E, F) { return this.each(function() { o.event.trigger(E, F, this) }) }, triggerHandler: function(E, G) { if (this[0]) { var F = o.Event(E); F.preventDefault(); F.stopPropagation(); o.event.trigger(F, G, this[0]); return F.result } }, toggle: function(G) { var E = arguments, F = 1; while (F < E.length) { o.event.proxy(G, E[F++]) } return this.click(o.event.proxy(G, function(H) { this.lastToggle = (this.lastToggle || 0) % F; H.preventDefault(); return E[this.lastToggle++].apply(this, arguments) || false })) }, hover: function(E, F) { return this.mouseenter(E).mouseleave(F) }, ready: function(E) { B(); if (o.isReady) { E.call(document, o) } else { o.readyList.push(E) } return this }, live: function(G, F) { var E = o.event.proxy(F); E.guid += this.selector + G; o(document).bind(i(G, this.selector), this.selector, E); return this }, die: function(F, E) { o(document).unbind(i(F, this.selector), E ? { guid: E.guid + this.selector + F} : null); return this } }); function c(H) { var E = RegExp("(^|\\.)" + H.type + "(\\.|$)"), G = true, F = []; o.each(o.data(this, "events").live || [], function(I, J) { if (E.test(J.type)) { var K = o(H.target).closest(J.data)[0]; if (K) { F.push({ elem: K, fn: J }) } } }); o.each(F, function() { if (this.fn.call(this.elem, H, this.fn.data) === false) { G = false } }); return G } function i(F, E) { return ["live", F, E.replace(/\./g, "`").replace(/ /g, "|")].join(".") } o.extend({ isReady: false, readyList: [], ready: function() { if (!o.isReady) { o.isReady = true; if (o.readyList) { o.each(o.readyList, function() { this.call(document, o) }); o.readyList = null } o(document).triggerHandler("ready") } } }); var x = false; function B() { if (x) { return } x = true; if (document.addEventListener) { document.addEventListener("DOMContentLoaded", function() { document.removeEventListener("DOMContentLoaded", arguments.callee, false); o.ready() }, false) } else { if (document.attachEvent) { document.attachEvent("onreadystatechange", function() { if (document.readyState === "complete") { document.detachEvent("onreadystatechange", arguments.callee); o.ready() } }); if (document.documentElement.doScroll && typeof l.frameElement === "undefined") { (function() { if (o.isReady) { return } try { document.documentElement.doScroll("left") } catch (E) { setTimeout(arguments.callee, 0); return } o.ready() })() } } } o.event.add(l, "load", o.ready) } o.each(("blur,focus,load,resize,scroll,unload,click,dblclick,mousedown,mouseup,mousemove,mouseover,mouseout,mouseenter,mouseleave,change,select,submit,keydown,keypress,keyup,error").split(","), function(F, E) { o.fn[E] = function(G) { return G ? this.bind(E, G) : this.trigger(E) } }); o(l).bind("unload", function() { for (var E in o.cache) { if (E != 1 && o.cache[E].handle) { o.event.remove(o.cache[E].handle.elem) } } }); (function() { o.support = {}; var F = document.documentElement, G = document.createElement("script"), K = document.createElement("div"), J = "script" + (new Date).getTime(); K.style.display = "none"; K.innerHTML = '   <link/><table></table><a href="/a" style="color:red;float:left;opacity:.5;">a</a><select><option>text</option></select><object><param/></object>'; var H = K.getElementsByTagName("*"), E = K.getElementsByTagName("a")[0]; if (!H || !H.length || !E) { return } o.support = { leadingWhitespace: K.firstChild.nodeType == 3, tbody: !K.getElementsByTagName("tbody").length, objectAll: !!K.getElementsByTagName("object")[0].getElementsByTagName("*").length, htmlSerialize: !!K.getElementsByTagName("link").length, style: /red/.test(E.getAttribute("style")), hrefNormalized: E.getAttribute("href") === "/a", opacity: E.style.opacity === "0.5", cssFloat: !!E.style.cssFloat, scriptEval: false, noCloneEvent: true, boxModel: null }; G.type = "text/javascript"; try { G.appendChild(document.createTextNode("window." + J + "=1;")) } catch (I) { } F.insertBefore(G, F.firstChild); if (l[J]) { o.support.scriptEval = true; delete l[J] } F.removeChild(G); if (K.attachEvent && K.fireEvent) { K.attachEvent("onclick", function() { o.support.noCloneEvent = false; K.detachEvent("onclick", arguments.callee) }); K.cloneNode(true).fireEvent("onclick") } o(function() { var L = document.createElement("div"); L.style.width = "1px"; L.style.paddingLeft = "1px"; document.body.appendChild(L); o.boxModel = o.support.boxModel = L.offsetWidth === 2; document.body.removeChild(L) }) })(); var w = o.support.cssFloat ? "cssFloat" : "styleFloat"; o.props = { "for": "htmlFor", "class": "className", "float": w, cssFloat: w, styleFloat: w, readonly: "readOnly", maxlength: "maxLength", cellspacing: "cellSpacing", rowspan: "rowSpan", tabindex: "tabIndex" }; o.fn.extend({ _load: o.fn.load, load: function(G, J, K) { if (typeof G !== "string") { return this._load(G) } var I = G.indexOf(" "); if (I >= 0) { var E = G.slice(I, G.length); G = G.slice(0, I) } var H = "GET"; if (J) { if (o.isFunction(J)) { K = J; J = null } else { if (typeof J === "object") { J = o.param(J); H = "POST" } } } var F = this; o.ajax({ url: G, type: H, dataType: "html", data: J, complete: function(M, L) { if (L == "success" || L == "notmodified") { F.html(E ? o("<div/>").append(M.responseText.replace(/<script(.|\s)*?\/script>/g, "")).find(E) : M.responseText) } if (K) { F.each(K, [M.responseText, L, M]) } } }); return this }, serialize: function() { return o.param(this.serializeArray()) }, serializeArray: function() { return this.map(function() { return this.elements ? o.makeArray(this.elements) : this }).filter(function() { return this.name && !this.disabled && (this.checked || /select|textarea/i.test(this.nodeName) || /text|hidden|password/i.test(this.type)) }).map(function(E, F) { var G = o(this).val(); return G == null ? null : o.isArray(G) ? o.map(G, function(I, H) { return { name: F.name, value: I} }) : { name: F.name, value: G} }).get() } }); o.each("ajaxStart,ajaxStop,ajaxComplete,ajaxError,ajaxSuccess,ajaxSend".split(","), function(E, F) { o.fn[F] = function(G) { return this.bind(F, G) } }); var r = e(); o.extend({ get: function(E, G, H, F) { if (o.isFunction(G)) { H = G; G = null } return o.ajax({ type: "GET", url: E, data: G, success: H, dataType: F }) }, getScript: function(E, F) { return o.get(E, null, F, "script") }, getJSON: function(E, F, G) { return o.get(E, F, G, "json") }, post: function(E, G, H, F) { if (o.isFunction(G)) { H = G; G = {} } return o.ajax({ type: "POST", url: E, data: G, success: H, dataType: F }) }, ajaxSetup: function(E) { o.extend(o.ajaxSettings, E) }, ajaxSettings: { url: location.href, global: true, type: "GET", contentType: "application/x-www-form-urlencoded", processData: true, async: true, xhr: function() { return l.ActiveXObject ? new ActiveXObject("Microsoft.XMLHTTP") : new XMLHttpRequest() }, accepts: { xml: "application/xml, text/xml", html: "text/html", script: "text/javascript, application/javascript", json: "application/json, text/javascript", text: "text/plain", _default: "*/*"} }, lastModified: {}, ajax: function(M) { M = o.extend(true, M, o.extend(true, {}, o.ajaxSettings, M)); var W, F = /=\?(&|$)/g, R, V, G = M.type.toUpperCase(); if (M.data && M.processData && typeof M.data !== "string") { M.data = o.param(M.data) } if (M.dataType == "jsonp") { if (G == "GET") { if (!M.url.match(F)) { M.url += (M.url.match(/\?/) ? "&" : "?") + (M.jsonp || "callback") + "=?" } } else { if (!M.data || !M.data.match(F)) { M.data = (M.data ? M.data + "&" : "") + (M.jsonp || "callback") + "=?" } } M.dataType = "json" } if (M.dataType == "json" && (M.data && M.data.match(F) || M.url.match(F))) { W = "jsonp" + r++; if (M.data) { M.data = (M.data + "").replace(F, "=" + W + "$1") } M.url = M.url.replace(F, "=" + W + "$1"); M.dataType = "script"; l[W] = function(X) { V = X; I(); L(); l[W] = g; try { delete l[W] } catch (Y) { } if (H) { H.removeChild(T) } } } if (M.dataType == "script" && M.cache == null) { M.cache = false } if (M.cache === false && G == "GET") { var E = e(); var U = M.url.replace(/(\?|&)_=.*?(&|$)/, "$1_=" + E + "$2"); M.url = U + ((U == M.url) ? (M.url.match(/\?/) ? "&" : "?") + "_=" + E : "") } if (M.data && G == "GET") { M.url += (M.url.match(/\?/) ? "&" : "?") + M.data; M.data = null } if (M.global && !o.active++) { o.event.trigger("ajaxStart") } var Q = /^(\w+:)?\/\/([^\/?#]+)/.exec(M.url); if (M.dataType == "script" && G == "GET" && Q && (Q[1] && Q[1] != location.protocol || Q[2] != location.host)) { var H = document.getElementsByTagName("head")[0]; var T = document.createElement("script"); T.src = M.url; if (M.scriptCharset) { T.charset = M.scriptCharset } if (!W) { var O = false; T.onload = T.onreadystatechange = function() { if (!O && (!this.readyState || this.readyState == "loaded" || this.readyState == "complete")) { O = true; I(); L(); H.removeChild(T) } } } H.appendChild(T); return g } var K = false; var J = M.xhr(); if (M.username) { J.open(G, M.url, M.async, M.username, M.password) } else { J.open(G, M.url, M.async) } try { if (M.data) { J.setRequestHeader("Content-Type", M.contentType) } if (M.ifModified) { J.setRequestHeader("If-Modified-Since", o.lastModified[M.url] || "Thu, 01 Jan 1970 00:00:00 GMT") } J.setRequestHeader("X-Requested-With", "XMLHttpRequest"); J.setRequestHeader("Accept", M.dataType && M.accepts[M.dataType] ? M.accepts[M.dataType] + ", */*" : M.accepts._default) } catch (S) { } if (M.beforeSend && M.beforeSend(J, M) === false) { if (M.global && ! --o.active) { o.event.trigger("ajaxStop") } J.abort(); return false } if (M.global) { o.event.trigger("ajaxSend", [J, M]) } var N = function(X) { if (J.readyState == 0) { if (P) { clearInterval(P); P = null; if (M.global && ! --o.active) { o.event.trigger("ajaxStop") } } } else { if (!K && J && (J.readyState == 4 || X == "timeout")) { K = true; if (P) { clearInterval(P); P = null } R = X == "timeout" ? "timeout" : !o.httpSuccess(J) ? "error" : M.ifModified && o.httpNotModified(J, M.url) ? "notmodified" : "success"; if (R == "success") { try { V = o.httpData(J, M.dataType, M) } catch (Z) { R = "parsererror" } } if (R == "success") { var Y; try { Y = J.getResponseHeader("Last-Modified") } catch (Z) { } if (M.ifModified && Y) { o.lastModified[M.url] = Y } if (!W) { I() } } else { o.handleError(M, J, R) } L(); if (X) { J.abort() } if (M.async) { J = null } } } }; if (M.async) { var P = setInterval(N, 13); if (M.timeout > 0) { setTimeout(function() { if (J && !K) { N("timeout") } }, M.timeout) } } try { J.send(M.data) } catch (S) { o.handleError(M, J, null, S) } if (!M.async) { N() } function I() { if (M.success) { M.success(V, R) } if (M.global) { o.event.trigger("ajaxSuccess", [J, M]) } } function L() { if (M.complete) { M.complete(J, R) } if (M.global) { o.event.trigger("ajaxComplete", [J, M]) } if (M.global && ! --o.active) { o.event.trigger("ajaxStop") } } return J }, handleError: function(F, H, E, G) { if (F.error) { F.error(H, E, G) } if (F.global) { o.event.trigger("ajaxError", [H, F, G]) } }, active: 0, httpSuccess: function(F) { try { return !F.status && location.protocol == "file:" || (F.status >= 200 && F.status < 300) || F.status == 304 || F.status == 1223 } catch (E) { } return false }, httpNotModified: function(G, E) { try { var H = G.getResponseHeader("Last-Modified"); return G.status == 304 || H == o.lastModified[E] } catch (F) { } return false }, httpData: function(J, H, G) { var F = J.getResponseHeader("content-type"), E = H == "xml" || !H && F && F.indexOf("xml") >= 0, I = E ? J.responseXML : J.responseText; if (E && I.documentElement.tagName == "parsererror") { throw "parsererror" } if (G && G.dataFilter) { I = G.dataFilter(I, H) } if (typeof I === "string") { if (H == "script") { o.globalEval(I) } if (H == "json") { I = l["eval"]("(" + I + ")") } } return I }, param: function(E) { var G = []; function H(I, J) { G[G.length] = encodeURIComponent(I) + "=" + encodeURIComponent(J) } if (o.isArray(E) || E.jquery) { o.each(E, function() { H(this.name, this.value) }) } else { for (var F in E) { if (o.isArray(E[F])) { o.each(E[F], function() { H(F, this) }) } else { H(F, o.isFunction(E[F]) ? E[F]() : E[F]) } } } return G.join("&").replace(/%20/g, "+") } }); var m = {}, n, d = [["height", "marginTop", "marginBottom", "paddingTop", "paddingBottom"], ["width", "marginLeft", "marginRight", "paddingLeft", "paddingRight"], ["opacity"]]; function t(F, E) { var G = {}; o.each(d.concat.apply([], d.slice(0, E)), function() { G[this] = F }); return G } o.fn.extend({ show: function(J, L) { if (J) { return this.animate(t("show", 3), J, L) } else { for (var H = 0, F = this.length; H < F; H++) { var E = o.data(this[H], "olddisplay"); this[H].style.display = E || ""; if (o.css(this[H], "display") === "none") { var G = this[H].tagName, K; if (m[G]) { K = m[G] } else { var I = o("<" + G + " />").appendTo("body"); K = I.css("display"); if (K === "none") { K = "block" } I.remove(); m[G] = K } this[H].style.display = o.data(this[H], "olddisplay", K) } } return this } }, hide: function(H, I) { if (H) { return this.animate(t("hide", 3), H, I) } else { for (var G = 0, F = this.length; G < F; G++) { var E = o.data(this[G], "olddisplay"); if (!E && E !== "none") { o.data(this[G], "olddisplay", o.css(this[G], "display")) } this[G].style.display = "none" } return this } }, _toggle: o.fn.toggle, toggle: function(G, F) { var E = typeof G === "boolean"; return o.isFunction(G) && o.isFunction(F) ? this._toggle.apply(this, arguments) : G == null || E ? this.each(function() { var H = E ? G : o(this).is(":hidden"); o(this)[H ? "show" : "hide"]() }) : this.animate(t("toggle", 3), G, F) }, fadeTo: function(E, G, F) { return this.animate({ opacity: G }, E, F) }, animate: function(I, F, H, G) { var E = o.speed(F, H, G); return this[E.queue === false ? "each" : "queue"](function() { var K = o.extend({}, E), M, L = this.nodeType == 1 && o(this).is(":hidden"), J = this; for (M in I) { if (I[M] == "hide" && L || I[M] == "show" && !L) { return K.complete.call(this) } if ((M == "height" || M == "width") && this.style) { K.display = o.css(this, "display"); K.overflow = this.style.overflow } } if (K.overflow != null) { this.style.overflow = "hidden" } K.curAnim = o.extend({}, I); o.each(I, function(O, S) { var R = new o.fx(J, K, O); if (/toggle|show|hide/.test(S)) { R[S == "toggle" ? L ? "show" : "hide" : S](I) } else { var Q = S.toString().match(/^([+-]=)?([\d+-.]+)(.*)$/), T = R.cur(true) || 0; if (Q) { var N = parseFloat(Q[2]), P = Q[3] || "px"; if (P != "px") { J.style[O] = (N || 1) + P; T = ((N || 1) / R.cur(true)) * T; J.style[O] = T + P } if (Q[1]) { N = ((Q[1] == "-=" ? -1 : 1) * N) + T } R.custom(T, N, P) } else { R.custom(T, S, "") } } }); return true }) }, stop: function(F, E) { var G = o.timers; if (F) { this.queue([]) } this.each(function() { for (var H = G.length - 1; H >= 0; H--) { if (G[H].elem == this) { if (E) { G[H](true) } G.splice(H, 1) } } }); if (!E) { this.dequeue() } return this } }); o.each({ slideDown: t("show", 1), slideUp: t("hide", 1), slideToggle: t("toggle", 1), fadeIn: { opacity: "show" }, fadeOut: { opacity: "hide"} }, function(E, F) { o.fn[E] = function(G, H) { return this.animate(F, G, H) } }); o.extend({ speed: function(G, H, F) { var E = typeof G === "object" ? G : { complete: F || !F && H || o.isFunction(G) && G, duration: G, easing: F && H || H && !o.isFunction(H) && H }; E.duration = o.fx.off ? 0 : typeof E.duration === "number" ? E.duration : o.fx.speeds[E.duration] || o.fx.speeds._default; E.old = E.complete; E.complete = function() { if (E.queue !== false) { o(this).dequeue() } if (o.isFunction(E.old)) { E.old.call(this) } }; return E }, easing: { linear: function(G, H, E, F) { return E + F * G }, swing: function(G, H, E, F) { return ((-Math.cos(G * Math.PI) / 2) + 0.5) * F + E } }, timers: [], fx: function(F, E, G) { this.options = E; this.elem = F; this.prop = G; if (!E.orig) { E.orig = {} } } }); o.fx.prototype = { update: function() { if (this.options.step) { this.options.step.call(this.elem, this.now, this) } (o.fx.step[this.prop] || o.fx.step._default)(this); if ((this.prop == "height" || this.prop == "width") && this.elem.style) { this.elem.style.display = "block" } }, cur: function(F) { if (this.elem[this.prop] != null && (!this.elem.style || this.elem.style[this.prop] == null)) { return this.elem[this.prop] } var E = parseFloat(o.css(this.elem, this.prop, F)); return E && E > -10000 ? E : parseFloat(o.curCSS(this.elem, this.prop)) || 0 }, custom: function(I, H, G) { this.startTime = e(); this.start = I; this.end = H; this.unit = G || this.unit || "px"; this.now = this.start; this.pos = this.state = 0; var E = this; function F(J) { return E.step(J) } F.elem = this.elem; if (F() && o.timers.push(F) == 1) { n = setInterval(function() { var K = o.timers; for (var J = 0; J < K.length; J++) { if (!K[J]()) { K.splice(J--, 1) } } if (!K.length) { clearInterval(n) } }, 13) } }, show: function() { this.options.orig[this.prop] = o.attr(this.elem.style, this.prop); this.options.show = true; this.custom(this.prop == "width" || this.prop == "height" ? 1 : 0, this.cur()); o(this.elem).show() }, hide: function() { this.options.orig[this.prop] = o.attr(this.elem.style, this.prop); this.options.hide = true; this.custom(this.cur(), 0) }, step: function(H) { var G = e(); if (H || G >= this.options.duration + this.startTime) { this.now = this.end; this.pos = this.state = 1; this.update(); this.options.curAnim[this.prop] = true; var E = true; for (var F in this.options.curAnim) { if (this.options.curAnim[F] !== true) { E = false } } if (E) { if (this.options.display != null) { this.elem.style.overflow = this.options.overflow; this.elem.style.display = this.options.display; if (o.css(this.elem, "display") == "none") { this.elem.style.display = "block" } } if (this.options.hide) { o(this.elem).hide() } if (this.options.hide || this.options.show) { for (var I in this.options.curAnim) { o.attr(this.elem.style, I, this.options.orig[I]) } } this.options.complete.call(this.elem) } return false } else { var J = G - this.startTime; this.state = J / this.options.duration; this.pos = o.easing[this.options.easing || (o.easing.swing ? "swing" : "linear")](this.state, J, 0, 1, this.options.duration); this.now = this.start + ((this.end - this.start) * this.pos); this.update() } return true } }; o.extend(o.fx, { speeds: { slow: 600, fast: 200, _default: 400 }, step: { opacity: function(E) { o.attr(E.elem.style, "opacity", E.now) }, _default: function(E) { if (E.elem.style && E.elem.style[E.prop] != null) { E.elem.style[E.prop] = E.now + E.unit } else { E.elem[E.prop] = E.now } } } }); if (document.documentElement.getBoundingClientRect) { o.fn.offset = function() { if (!this[0]) { return { top: 0, left: 0} } if (this[0] === this[0].ownerDocument.body) { return o.offset.bodyOffset(this[0]) } var G = this[0].getBoundingClientRect(), J = this[0].ownerDocument, F = J.body, E = J.documentElement, L = E.clientTop || F.clientTop || 0, K = E.clientLeft || F.clientLeft || 0, I = G.top + (self.pageYOffset || o.boxModel && E.scrollTop || F.scrollTop) - L, H = G.left + (self.pageXOffset || o.boxModel && E.scrollLeft || F.scrollLeft) - K; return { top: I, left: H} } } else { o.fn.offset = function() { if (!this[0]) { return { top: 0, left: 0} } if (this[0] === this[0].ownerDocument.body) { return o.offset.bodyOffset(this[0]) } o.offset.initialized || o.offset.initialize(); var J = this[0], G = J.offsetParent, F = J, O = J.ownerDocument, M, H = O.documentElement, K = O.body, L = O.defaultView, E = L.getComputedStyle(J, null), N = J.offsetTop, I = J.offsetLeft; while ((J = J.parentNode) && J !== K && J !== H) { M = L.getComputedStyle(J, null); N -= J.scrollTop, I -= J.scrollLeft; if (J === G) { N += J.offsetTop, I += J.offsetLeft; if (o.offset.doesNotAddBorder && !(o.offset.doesAddBorderForTableAndCells && /^t(able|d|h)$/i.test(J.tagName))) { N += parseInt(M.borderTopWidth, 10) || 0, I += parseInt(M.borderLeftWidth, 10) || 0 } F = G, G = J.offsetParent } if (o.offset.subtractsBorderForOverflowNotVisible && M.overflow !== "visible") { N += parseInt(M.borderTopWidth, 10) || 0, I += parseInt(M.borderLeftWidth, 10) || 0 } E = M } if (E.position === "relative" || E.position === "static") { N += K.offsetTop, I += K.offsetLeft } if (E.position === "fixed") { N += Math.max(H.scrollTop, K.scrollTop), I += Math.max(H.scrollLeft, K.scrollLeft) } return { top: N, left: I} } } o.offset = { initialize: function() { if (this.initialized) { return } var L = document.body, F = document.createElement("div"), H, G, N, I, M, E, J = L.style.marginTop, K = '<div style="position:absolute;top:0;left:0;margin:0;border:5px solid #000;padding:0;width:1px;height:1px;"><div></div></div><table style="position:absolute;top:0;left:0;margin:0;border:5px solid #000;padding:0;width:1px;height:1px;" cellpadding="0" cellspacing="0"><tr><td></td></tr></table>'; M = { position: "absolute", top: 0, left: 0, margin: 0, border: 0, width: "1px", height: "1px", visibility: "hidden" }; for (E in M) { F.style[E] = M[E] } F.innerHTML = K; L.insertBefore(F, L.firstChild); H = F.firstChild, G = H.firstChild, I = H.nextSibling.firstChild.firstChild; this.doesNotAddBorder = (G.offsetTop !== 5); this.doesAddBorderForTableAndCells = (I.offsetTop === 5); H.style.overflow = "hidden", H.style.position = "relative"; this.subtractsBorderForOverflowNotVisible = (G.offsetTop === -5); L.style.marginTop = "1px"; this.doesNotIncludeMarginInBodyOffset = (L.offsetTop === 0); L.style.marginTop = J; L.removeChild(F); this.initialized = true }, bodyOffset: function(E) { o.offset.initialized || o.offset.initialize(); var G = E.offsetTop, F = E.offsetLeft; if (o.offset.doesNotIncludeMarginInBodyOffset) { G += parseInt(o.curCSS(E, "marginTop", true), 10) || 0, F += parseInt(o.curCSS(E, "marginLeft", true), 10) || 0 } return { top: G, left: F} } }; o.fn.extend({ position: function() { var I = 0, H = 0, F; if (this[0]) { var G = this.offsetParent(), J = this.offset(), E = /^body|html$/i.test(G[0].tagName) ? { top: 0, left: 0} : G.offset(); J.top -= j(this, "marginTop"); J.left -= j(this, "marginLeft"); E.top += j(G, "borderTopWidth"); E.left += j(G, "borderLeftWidth"); F = { top: J.top - E.top, left: J.left - E.left} } return F }, offsetParent: function() { var E = this[0].offsetParent || document.body; while (E && (!/^body|html$/i.test(E.tagName) && o.css(E, "position") == "static")) { E = E.offsetParent } return o(E) } }); o.each(["Left", "Top"], function(F, E) { var G = "scroll" + E; o.fn[G] = function(H) { if (!this[0]) { return null } return H !== g ? this.each(function() { this == l || this == document ? l.scrollTo(!F ? H : o(l).scrollLeft(), F ? H : o(l).scrollTop()) : this[G] = H }) : this[0] == l || this[0] == document ? self[F ? "pageYOffset" : "pageXOffset"] || o.boxModel && document.documentElement[G] || document.body[G] : this[0][G] } }); o.each(["Height", "Width"], function(H, F) { var E = H ? "Left" : "Top", G = H ? "Right" : "Bottom"; o.fn["inner" + F] = function() { return this[F.toLowerCase()]() + j(this, "padding" + E) + j(this, "padding" + G) }; o.fn["outer" + F] = function(J) { return this["inner" + F]() + j(this, "border" + E + "Width") + j(this, "border" + G + "Width") + (J ? j(this, "margin" + E) + j(this, "margin" + G) : 0) }; var I = F.toLowerCase(); o.fn[I] = function(J) { return this[0] == l ? document.compatMode == "CSS1Compat" && document.documentElement["client" + F] || document.body["client" + F] : this[0] == document ? Math.max(document.documentElement["client" + F], document.body["scroll" + F], document.documentElement["scroll" + F], document.body["offset" + F], document.documentElement["offset" + F]) : J === g ? (this.length ? o.css(this[0], I) : null) : this.css(I, typeof J === "string" ? J : J + "px") } })
})();

/**
* hoverIntent is similar to jQuery's built-in "hover" function except that
* instead of firing the onMouseOver event immediately, hoverIntent checks
* to see if the user's mouse has slowed down (beneath the sensitivity
* threshold) before firing the onMouseOver event.
* 
* hoverIntent r5 // 2007.03.27 // jQuery 1.1.2
* <http://cherne.net/brian/resources/jquery.hoverIntent.html>
* 
* hoverIntent is currently available for use in all personal or commercial 
* projects under both MIT and GPL licenses. This means that you can choose 
* the license that best suits your project, and use it accordingly.
* 
* // basic usage (just like .hover) receives onMouseOver and onMouseOut functions
* $("ul li").hoverIntent( showNav , hideNav );
* 
* // advanced usage receives configuration object only
* $("ul li").hoverIntent({
*	sensitivity: 2, // number = sensitivity threshold (must be 1 or higher)
*	interval: 50,   // number = milliseconds of polling interval
*	over: showNav,  // function = onMouseOver callback (required)
*	timeout: 100,   // number = milliseconds delay before onMouseOut function call
*	out: hideNav    // function = onMouseOut callback (required)
* });
* 
* @param  f  onMouseOver function || An object with configuration options
* @param  g  onMouseOut function  || Nothing (use configuration options object)
* @return    The object (aka "this") that called hoverIntent, and the event object
* @author    Brian Cherne <brian@cherne.net>
*/
(function($) {
    $.fn.hoverIntent = function(f, g) {
        // default configuration options
        var cfg = {
            sensitivity: 7,
            interval: 100,
            timeout: 0
        };
        // override configuration options with user supplied object
        cfg = $.extend(cfg, g ? { over: f, out: g} : f);

        // instantiate variables
        // cX, cY = current X and Y position of mouse, updated by mousemove event
        // pX, pY = previous X and Y position of mouse, set by mouseover and polling interval
        var cX, cY, pX, pY;

        // A private function for getting mouse position
        var track = function(ev) {
            cX = ev.pageX;
            cY = ev.pageY;
        };

        // A private function for comparing current and previous mouse position
        var compare = function(ev, ob) {
            ob.hoverIntent_t = clearTimeout(ob.hoverIntent_t);
            // compare mouse positions to see if they've crossed the threshold
            if ((Math.abs(pX - cX) + Math.abs(pY - cY)) < cfg.sensitivity) {
                $(ob).unbind("mousemove", track);
                // set hoverIntent state to true (so mouseOut can be called)
                ob.hoverIntent_s = 1;
                return cfg.over.apply(ob, [ev]);
            } else {
                // set previous coordinates for next time
                pX = cX; pY = cY;
                // use self-calling timeout, guarantees intervals are spaced out properly (avoids JavaScript timer bugs)
                ob.hoverIntent_t = setTimeout(function() { compare(ev, ob); }, cfg.interval);
            }
        };

        // A private function for delaying the mouseOut function
        var delay = function(ev, ob) {
            ob.hoverIntent_t = clearTimeout(ob.hoverIntent_t);
            ob.hoverIntent_s = 0;
            return cfg.out.apply(ob, [ev]);
        };

        // A private function for handling mouse 'hovering'
        var handleHover = function(e) {
            // next three lines copied from jQuery.hover, ignore children onMouseOver/onMouseOut
            var p = (e.type == "mouseover" ? e.fromElement : e.toElement) || e.relatedTarget;
            while (p && p != this) { try { p = p.parentNode; } catch (e) { p = this; } }
            if (p == this) { return false; }

            // copy objects to be passed into t (required for event object to be passed in IE)
            var ev = jQuery.extend({}, e);
            var ob = this;

            // cancel hoverIntent timer if it exists
            if (ob.hoverIntent_t) { ob.hoverIntent_t = clearTimeout(ob.hoverIntent_t); }

            // else e.type == "onmouseover"
            if (e.type == "mouseover") {
                // set "previous" X and Y position based on initial entry point
                pX = ev.pageX; pY = ev.pageY;
                // update "current" X and Y position based on mousemove
                $(ob).bind("mousemove", track);
                // start polling interval (self-calling timeout) to compare mouse coordinates over time
                if (ob.hoverIntent_s != 1) { ob.hoverIntent_t = setTimeout(function() { compare(ev, ob); }, cfg.interval); }

                // else e.type == "onmouseout"
            } else {
                // unbind expensive mousemove event
                $(ob).unbind("mousemove", track);
                // if hoverIntent state is true, then call the mouseOut function after the specified delay
                if (ob.hoverIntent_s == 1) { ob.hoverIntent_t = setTimeout(function() { delay(ev, ob); }, cfg.timeout); }
            }
        };

        // bind the function to the two event listeners
        return this.mouseover(handleHover).mouseout(handleHover);
    };
})(jQuery);

/* Copyright (c) 2007 Paul Bakaus (paul.bakaus@googlemail.com) and Brandon Aaron (brandon.aaron@gmail.com || http://brandonaaron.net)
* Dual licensed under the MIT (http://www.opensource.org/licenses/mit-license.php)
* and GPL (http://www.opensource.org/licenses/gpl-license.php) licenses.
*
* $LastChangedDate: 2008-02-28 05:49:55 -0500 (Thu, 28 Feb 2008) $
* $Rev: 4841 $
*
* Version: @VERSION
*
* Requires: jQuery 1.2+
*/

(function($) {

    $.dimensions = {
        version: '@VERSION'
    };

    // Create innerHeight, innerWidth, outerHeight and outerWidth methods
    $.each(['Height', 'Width'], function(i, name) {

        // innerHeight and innerWidth
        $.fn['inner' + name] = function() {
            if (!this[0]) return;

            var torl = name == 'Height' ? 'Top' : 'Left',  // top or left
		    borr = name == 'Height' ? 'Bottom' : 'Right'; // bottom or right

            return this.css('display') != 'none' ? this[0]['client' + name] : num(this, name.toLowerCase()) + num(this, 'padding' + torl) + num(this, 'padding' + borr);
        };

        // outerHeight and outerWidth
        $.fn['outer' + name] = function(options) {
            if (!this[0]) return;

            var torl = name == 'Height' ? 'Top' : 'Left',  // top or left
		    borr = name == 'Height' ? 'Bottom' : 'Right'; // bottom or right

            options = $.extend({ margin: false }, options || {});
            var val = this.css('display') != 'none' ?
				this[0]['offset' + name] :
				num(this, name.toLowerCase())
					+ num(this, 'border' + torl + 'Width') + num(this, 'border' + borr + 'Width')
					+ num(this, 'padding' + torl) + num(this, 'padding' + borr);
            return val + (options.margin ? (num(this, 'margin' + torl) + num(this, 'margin' + borr)) : 0);
        };
    });

    // Create scrollLeft and scrollTop methods
    $.each(['Left', 'Top'], function(i, name) {
        $.fn['scroll' + name] = function(val) {
            if (!this[0]) return;

            return val != undefined ?

            // Set the scroll offset
			this.each(function() {
			    this == window || this == document ?
					window.scrollTo(
						name == 'Left' ? val : $(window)['scrollLeft'](),
						name == 'Top' ? val : $(window)['scrollTop']()
					) :
					this['scroll' + name] = val;
			}) :

            // Return the scroll offset
			this[0] == window || this[0] == document ?
				self[(name == 'Left' ? 'pageXOffset' : 'pageYOffset')] ||
					$.boxModel && document.documentElement['scroll' + name] ||
					document.body['scroll' + name] :
				this[0]['scroll' + name];
        };
    });

    $.fn.extend({
        position: function() {
            var left = 0, top = 0, elem = this[0], offset, parentOffset, offsetParent, results;

            if (elem) {
                // Get *real* offsetParent
                offsetParent = this.offsetParent();

                // Get correct offsets
                offset = this.offset();
                parentOffset = offsetParent.offset();

                // Subtract element margins
                offset.top -= num(elem, 'marginTop');
                offset.left -= num(elem, 'marginLeft');

                // Add offsetParent borders
                parentOffset.top += num(offsetParent, 'borderTopWidth');
                parentOffset.left += num(offsetParent, 'borderLeftWidth');

                // Subtract the two offsets
                results = {
                    top: offset.top - parentOffset.top,
                    left: offset.left - parentOffset.left
                };
            }

            return results;
        },

        offsetParent: function() {
            var offsetParent = this[0].offsetParent;
            while (offsetParent && (!/^body|html$/i.test(offsetParent.tagName) && $.css(offsetParent, 'position') == 'static'))
                offsetParent = offsetParent.offsetParent;
            return $(offsetParent);
        }
    });

    function num(el, prop) {
        return parseInt($.curCSS(el.jquery ? el[0] : el, prop, true)) || 0;
    };

})(jQuery);

/*
* jQuery clueTip plugin
* Version 0.9.8  (05/22/2008)
* @requires jQuery v1.1.4+
* @requires Dimensions plugin (for jQuery versions < 1.2.5)
*
* Dual licensed under the MIT and GPL licenses:
* http://www.opensource.org/licenses/mit-license.php
* http://www.gnu.org/licenses/gpl.html
*
*/
; (function($) {
    /*
    * @name clueTip
    * @type jQuery
    * @cat Plugins/tooltip
    * @return jQuery
    * @author Karl Swedberg
    *
    * @credit Inspired by Cody Lindley's jTip (http://www.codylindley.com)
    * @credit Thanks to the following people for their many and varied contributions:
    Shelane Enos, Glen Lipka, Hector Santos, Torben Schreiter, Dan G. Switzer, J�rn Zaefferer 
    * @credit Thanks to Jonathan Chaffer, as always, for help with the hard parts. :-)
    */

    /**
    * 
    * Displays a highly customizable tooltip when the user hovers (default) or clicks (optional) the matched element. 
    * By default, the clueTip plugin loads a page indicated by the "rel" attribute via ajax and displays its contents.
    * If a "title" attribute is specified, its value is used as the clueTip's heading.
    * The attribute to be used for both the body and the heading of the clueTip is user-configurable. 
    * Optionally, the clueTip's body can display content from an element on the same page.
    * * Just indicate the element's id (e.g. "#some-id") in the rel attribute.
    * Optionally, the clueTip's body can display content from the title attribute, when a delimiter is indicated. 
    * * The string before the first instance of the delimiter is set as the clueTip's heading.
    * * All subsequent strings are wrapped in separate DIVs and placed in the clueTip's body.
    * The clueTip plugin allows for many, many more options. Pleasee see the examples and the option descriptions below...
    * 
    * 
    * @example $('#tip).cluetip();
    * @desc This is the most basic clueTip. It displays a 275px-wide clueTip on mouseover of the element with an ID of "tip." On mouseout of the element, the clueTip is hidden.
    *
    *
    * @example $('a.clue').cluetip({
    *  hoverClass: 'highlight',
    *  sticky: true,
    *  closePosition: 'bottom',
    *  closeText: '<img src="cross.png" alt="close" />',
    *  truncate: 60,
    *  ajaxSettings: {
    *    type: 'POST'
    *  }
    * });
    * @desc Displays a clueTip on mouseover of all <a> elements with class="clue". The hovered element gets a class of "highlight" added to it (so that it can be styled appropriately. This is esp. useful for non-anchor elements.). The clueTip is "sticky," which means that it will not be hidden until the user either clicks on its "close" text/graphic or displays another clueTip. The "close" text/graphic is set to diplay at the bottom of the clueTip (default is top) and display an image rather than the default "Close" text. Moreover, the body of the clueTip is truncated to the first 60 characters, which are followed by an ellipsis (...). Finally, the clueTip retrieves the content using POST rather than the $.ajax method's default "GET."
    * 
    * More examples can be found at http://plugins.learningjquery.com/cluetip/demo/
    * 
    * Full list of options/settings can be found at the bottom of this file and at http://plugins.learningjquery.com/cluetip/
    */

    var $cluetip, $cluetipInner, $cluetipOuter, $cluetipTitle, $cluetipArrows, $dropShadow, imgCount;
    $.fn.cluetip = function(js, options) {
        if (typeof js == 'object') {
            options = js;
            js = null;
        }
        return this.each(function(index) {
            var $this = $(this);

            // support metadata plugin (v1.0 and 2.0)
            var opts = $.extend(false, {}, $.fn.cluetip.defaults, options || {}, $.metadata ? $this.metadata() : $.meta ? $this.data() : {});

            // start out with no contents (for ajax activation)
            var cluetipContents = false;
            var cluezIndex = parseInt(opts.cluezIndex, 10) - 1;
            var isActive = false, closeOnDelay = 0;

            // create the cluetip divs
            if (!$('#cluetip').length) {
                $cluetipInner = $('<div id="cluetip-inner"></div>');
                $cluetipTitle = $('<h3 id="cluetip-title"></h3>');
                $cluetipOuter = $('<div id="cluetip-outer"></div>').append($cluetipInner).prepend($cluetipTitle);
                $cluetip = $('<div id="cluetip"></div>').css({ zIndex: opts.cluezIndex })
        .append($cluetipOuter).append('<div id="cluetip-extra"></div>')[insertionType](insertionElement).hide();
                $('<div id="cluetip-waitimage"></div>').css({ position: 'absolute', zIndex: cluezIndex - 1 })
        .insertBefore('#cluetip').hide();
                $cluetip.css({ position: 'absolute', zIndex: cluezIndex });
                $cluetipOuter.css({ position: 'relative', zIndex: cluezIndex + 1 });
                $cluetipArrows = $('<div id="cluetip-arrows" class="cluetip-arrows"></div>').css({ zIndex: cluezIndex + 1 }).appendTo('#cluetip');
            }
            var dropShadowSteps = (opts.dropShadow) ? +opts.dropShadowSteps : 0;
            if (!$dropShadow) {
                $dropShadow = $([]);
                for (var i = 0; i < dropShadowSteps; i++) {
                    $dropShadow = $dropShadow.add($('<div></div>').css({ zIndex: cluezIndex - i - 1, opacity: .1, top: 1 + i, left: 1 + i }));
                };
                $dropShadow.css({ position: 'absolute', backgroundColor: '#000' })
        .prependTo($cluetip);
            }
            var tipAttribute = $this.attr(opts.attribute), ctClass = opts.cluetipClass;
            if (!tipAttribute && !opts.splitTitle && !js) return true;
            // if hideLocal is set to true, on DOM ready hide the local content that will be displayed in the clueTip      
            if (opts.local && opts.hideLocal) { $(tipAttribute + ':first').hide(); }
            var tOffset = parseInt(opts.topOffset, 10), lOffset = parseInt(opts.leftOffset, 10);
            // vertical measurement variables
            var tipHeight, wHeight;
            var defHeight = isNaN(parseInt(opts.height, 10)) ? 'auto' : (/\D/g).test(opts.height) ? opts.height : opts.height + 'px';
            var sTop, linkTop, posY, tipY, mouseY, baseline;
            // horizontal measurement variables
            var tipInnerWidth = isNaN(parseInt(opts.width, 10)) ? 275 : parseInt(opts.width, 10);
            var tipWidth = tipInnerWidth + (parseInt($cluetip.css('paddingLeft')) || 0) + (parseInt($cluetip.css('paddingRight')) || 0) + dropShadowSteps;
            var linkWidth = this.offsetWidth;
            var linkLeft, posX, tipX, mouseX, winWidth;

            // parse the title
            var tipParts;
            var tipTitle = (opts.attribute != 'title') ? $this.attr(opts.titleAttribute) : '';
            if (opts.splitTitle) {
                if (tipTitle == undefined) { tipTitle = ''; }
                tipParts = tipTitle.split(opts.splitTitle);
                tipTitle = tipParts.shift();
            }
            var localContent;

            /***************************************      
            * ACTIVATION
            ****************************************/

            //activate clueTip
            var activate = function(event) {
                if (!opts.onActivate($this)) {
                    return false;
                }
                isActive = true;
                $cluetip.removeClass().css({ width: tipInnerWidth });
                if (tipAttribute == $this.attr('href')) {
                    $this.css('cursor', opts.cursor);
                }
                $this.attr('title', '');
                if (opts.hoverClass) {
                    $this.addClass(opts.hoverClass);
                }
                linkTop = posY = $this.offset().top;
                linkLeft = $this.offset().left;
                mouseX = event.pageX;
                mouseY = event.pageY;
                if ($this[0].tagName.toLowerCase() != 'area') {
                    sTop = $(document).scrollTop();
                    winWidth = $(window).width();
                }
                // position clueTip horizontally
                if (opts.positionBy == 'fixed') {
                    posX = linkWidth + linkLeft + lOffset;
                    $cluetip.css({ left: posX });
                } else {
                    posX = (linkWidth > linkLeft && linkLeft > tipWidth)
          || linkLeft + linkWidth + tipWidth + lOffset > winWidth
          ? linkLeft - tipWidth - lOffset
          : linkWidth + linkLeft + lOffset;
                    if ($this[0].tagName.toLowerCase() == 'area' || opts.positionBy == 'mouse' || linkWidth + tipWidth > winWidth) { // position by mouse
                        if (mouseX + 20 + tipWidth > winWidth) {
                            $cluetip.addClass(' cluetip-' + ctClass);
                            posX = (mouseX - tipWidth - lOffset) >= 0 ? mouseX - tipWidth - lOffset - parseInt($cluetip.css('marginLeft'), 10) + parseInt($cluetipInner.css('marginRight'), 10) : mouseX - (tipWidth / 2);
                        } else {
                            posX = mouseX + lOffset;
                        }
                    }
                    var pY = posX < 0 ? event.pageY + tOffset : event.pageY;
                    $cluetip.css({ left: (posX > 0 && opts.positionBy != 'bottomTop') ? posX : (mouseX + (tipWidth / 2) > winWidth) ? winWidth / 2 - tipWidth / 2 : Math.max(mouseX - (tipWidth / 2), 0) });
                }
                wHeight = $(window).height();

                /***************************************
                * load a string from cluetip method's first argument
                ***************************************/
                if (js) {
                    $cluetipInner.html(js);
                    cluetipShow(pY);
                }
                /***************************************
                * load the title attribute only (or user-selected attribute). 
                * clueTip title is the string before the first delimiter
                * subsequent delimiters place clueTip body text on separate lines
                ***************************************/

                else if (tipParts) {
                    var tpl = tipParts.length;
                    for (var i = 0; i < tpl; i++) {
                        if (i == 0) {
                            $cluetipInner.html(tipParts[i]);
                        } else {
                            $cluetipInner.append('<div class="split-body">' + tipParts[i] + '</div>');
                        }
                    };
                    cluetipShow(pY);
                }
                /***************************************
                * load external file via ajax          
                ***************************************/

                else if (!opts.local && tipAttribute.indexOf('#') != 0) {
                    if (cluetipContents && opts.ajaxCache) {
                        $cluetipInner.html(cluetipContents);
                        cluetipShow(pY);
                    }
                    else {
                        var ajaxSettings = opts.ajaxSettings;
                        ajaxSettings.url = tipAttribute;
                        ajaxSettings.beforeSend = function() {
                            $cluetipOuter.children().empty();
                            if (opts.waitImage) {
                                $('#cluetip-waitimage')
              .css({ top: mouseY + 20, left: mouseX + 20 })
              .show();
                            }
                        };
                        ajaxSettings.error = function() {
                            if (isActive) {
                                $cluetipInner.html('<i>sorry, the contents could not be loaded</i>');
                            }
                        };
                        ajaxSettings.success = function(data) {
                            cluetipContents = opts.ajaxProcess(data);
                            if (isActive) {
                                $cluetipInner.html(cluetipContents);
                            }
                        };
                        ajaxSettings.complete = function() {
                            imgCount = $('#cluetip-inner img').length;
                            if (imgCount && !$.browser.opera) {
                                $('#cluetip-inner img').load(function() {
                                    imgCount--;
                                    if (imgCount < 1) {
                                        $('#cluetip-waitimage').hide();
                                        if (isActive) cluetipShow(pY);
                                    }
                                });
                            } else {
                                $('#cluetip-waitimage').hide();
                                if (isActive) cluetipShow(pY);
                            }
                        };
                        $.ajax(ajaxSettings);
                    }

                    /***************************************
                    * load an element from the same page
                    ***************************************/
                } else if (opts.local) {
                    var $localContent = $(tipAttribute + ':first');
                    var localCluetip = $.fn.wrapInner ? $localContent.wrapInner('<div></div>').children().clone(true) : $localContent.html();
                    $.fn.wrapInner ? $cluetipInner.empty().append(localCluetip) : $cluetipInner.html(localCluetip);
                    cluetipShow(pY);
                }
            };

            // get dimensions and options for cluetip and prepare it to be shown
            var cluetipShow = function(bpY) {
                $cluetip.addClass('cluetip-' + ctClass);

                if (opts.truncate) {
                    var $truncloaded = $cluetipInner.text().slice(0, opts.truncate) + '...';
                    $cluetipInner.html($truncloaded);
                }
                function doNothing() { }; //empty function
                tipTitle ? $cluetipTitle.show().html(tipTitle) : (opts.showTitle) ? $cluetipTitle.show().html('&nbsp;') : $cluetipTitle.hide();
                if (opts.sticky) {
                    var $closeLink = $('<div id="cluetip-close"><a href="#">' + opts.closeText + '</a></div>');
                    (opts.closePosition == 'bottom') ? $closeLink.appendTo($cluetipInner) : (opts.closePosition == 'title') ? $closeLink.prependTo($cluetipTitle) : $closeLink.prependTo($cluetipInner);
                    $closeLink.click(function() {
                        cluetipClose();
                        return false;
                    });
                    if (opts.mouseOutClose) {
                        if ($.fn.hoverIntent && opts.hoverIntent) {
                            $cluetip.hoverIntent({
                                over: doNothing,
                                timeout: opts.hoverIntent.timeout,
                                out: function() { $closeLink.trigger('click'); }
                            });
                        } else {
                            $cluetip.hover(doNothing,
            function() { $closeLink.trigger('click'); });
                        }
                    } else {
                        $cluetip.unbind('mouseout');
                    }
                }
                // now that content is loaded, finish the positioning 
                var direction = '';
                $cluetipOuter.css({ overflow: defHeight == 'auto' ? 'visible' : 'auto', height: defHeight });
                tipHeight = defHeight == 'auto' ? Math.max($cluetip.outerHeight(), $cluetip.height()) : parseInt(defHeight, 10);
                tipY = posY;
                baseline = sTop + wHeight;
                if (opts.positionBy == 'fixed') {
                    tipY = posY - opts.dropShadowSteps + tOffset;
                } else if ((posX < mouseX && Math.max(posX, 0) + tipWidth > mouseX) || opts.positionBy == 'bottomTop') {
                    if (posY + tipHeight + tOffset > baseline && mouseY - sTop > tipHeight + tOffset) {
                        tipY = mouseY - tipHeight - tOffset;
                        direction = 'top';
                    } else {
                        tipY = mouseY + tOffset;
                        direction = 'bottom';
                    }
                } else if (posY + tipHeight + tOffset > baseline) {
                    tipY = (tipHeight >= wHeight) ? sTop : baseline - tipHeight - tOffset;

                    /* CARMAX MOD */
                    //} else if ($this.css('display') == 'block' || $this[0].tagName.toLowerCase() == 'area' || opts.positionBy == "mouse") {
                } else if ($this[0].tagName.toLowerCase() == 'area' || opts.positionBy == "mouse") {
                    tipY = bpY - tOffset;
                } else {
                    tipY = posY - opts.dropShadowSteps;
                }
                if (direction == '') {
                    posX < linkLeft ? direction = 'left' : direction = 'right';
                }
                $cluetip.css({ top: tipY + 'px' }).removeClass().addClass('clue-' + direction + '-' + ctClass).addClass(' cluetip-' + ctClass);
                if (opts.arrows) { // set up arrow positioning to align with element
                    var bgY = (posY - tipY - opts.dropShadowSteps);
                    $cluetipArrows.css({ top: (/(left|right)/.test(direction) && posX >= 0 && bgY > 0) ? bgY + 'px' : /(left|right)/.test(direction) ? 0 : '' }).show();
                } else {
                    $cluetipArrows.hide();
                }

                // (first hide, then) ***SHOW THE CLUETIP***
                $dropShadow.hide();
                $cluetip.hide()[opts.fx.open](opts.fx.open != 'show' && opts.fx.openSpeed);
                if (opts.dropShadow) $dropShadow.css({ height: tipHeight, width: tipInnerWidth }).show();
                if ($.fn.bgiframe) { $cluetip.bgiframe(); }
                // trigger the optional onShow function
                if (opts.delayedClose > 0) {
                    closeOnDelay = setTimeout(cluetipClose, opts.delayedClose);
                }
                opts.onShow($cluetip, $cluetipInner);

            };

            /***************************************
            =INACTIVATION
            -------------------------------------- */
            var inactivate = function() {
                isActive = false;
                $('#cluetip-waitimage').hide();
                if (!opts.sticky || (/click|toggle/).test(opts.activation)) {
                    cluetipClose();
                    clearTimeout(closeOnDelay);
                };
                if (opts.hoverClass) {
                    $this.removeClass(opts.hoverClass);
                }
                $('.cluetip-clicked').removeClass('cluetip-clicked');
            };
            // close cluetip and reset some things
            var cluetipClose = function() {
                $cluetipOuter
      .parent().hide().removeClass().end()
      .children().empty();
                if (tipTitle) {
                    $this.attr(opts.titleAttribute, tipTitle);
                }
                $this.css('cursor', '');
                if (opts.arrows) $cluetipArrows.css({ top: '' });
            };

            /***************************************
            =BIND EVENTS
            -------------------------------------- */
            // activate by click
            if ((/click|toggle/).test(opts.activation)) {
                $this.click(function(event) {
                    if ($cluetip.is(':hidden') || !$this.is('.cluetip-clicked')) {
                        activate(event);
                        $('.cluetip-clicked').removeClass('cluetip-clicked');
                        $this.addClass('cluetip-clicked');

                    } else {
                        inactivate(event);

                    }
                    this.blur();
                    return false;
                });
                // activate by focus; inactivate by blur    
            } else if (opts.activation == 'focus') {
                $this.focus(function(event) {
                    activate(event);
                });
                $this.blur(function(event) {
                    inactivate(event);
                });
                // activate by hover
                // clicking is returned false if cluetip url is same as href url
            } else {
                $this.click(function() {
                    if ($this.attr('href') && $this.attr('href') == tipAttribute && !opts.clickThrough) {
                        return false;
                    }
                });
                //set up mouse tracking
                var mouseTracks = function(evt) {
                    if (opts.tracking == true) {
                        var trackX = posX - evt.pageX;
                        var trackY = tipY ? tipY - evt.pageY : posY - evt.pageY;
                        $this.mousemove(function(evt) {
                            $cluetip.css({ left: evt.pageX + trackX, top: evt.pageY + trackY });
                        });
                    }
                };
                if ($.fn.hoverIntent && opts.hoverIntent) {
                    $this.mouseover(function() { $this.attr('title', ''); })
          .hoverIntent({
              sensitivity: opts.hoverIntent.sensitivity,
              interval: opts.hoverIntent.interval,
              over: function(event) {
                  activate(event);
                  mouseTracks(event);
              },
              timeout: opts.hoverIntent.timeout,
              out: function(event) { inactivate(event); $this.unbind('mousemove'); }
          });
                } else {
                    $this.hover(function(event) {
                        activate(event);
                        mouseTracks(event);
                    }, function(event) {
                        inactivate(event);
                        $this.unbind('mousemove');
                    });
                }
            }
        });
    };

    /*
    * options for clueTip
    *
    * each one can be explicitly overridden by changing its value. 
    * for example: $.fn.cluetip.defaults.width = 200; 
    * would change the default width for all clueTips to 200. 
    *
    * each one can also be overridden by passing an options map to the cluetip method.
    * for example: $('a.example').cluetip({width: 200}); 
    * would change the default width to 200 for clueTips invoked by a link with class of "example"
    *
    */

    $.fn.cluetip.defaults = {  // set up default options
        width: 275,      // The width of the clueTip
        height: 'auto',   // The height of the clueTip
        cluezIndex: 97,       // Sets the z-index style property of the clueTip
        positionBy: 'auto',   // Sets the type of positioning: 'auto', 'mouse','bottomTop', 'fixed'
        topOffset: 15,       // Number of px to offset clueTip from top of invoking element
        leftOffset: 15,       // Number of px to offset clueTip from left of invoking element
        local: false,    // Whether to use content from the same page for the clueTip's body
        hideLocal: true,     // If local option is set to true, this determines whether local content
        // to be shown in clueTip should be hidden at its original location
        attribute: 'rel',    // the attribute to be used for fetching the clueTip's body content
        titleAttribute: 'title',  // the attribute to be used for fetching the clueTip's title
        splitTitle: '',       // A character used to split the title attribute into the clueTip title and divs
        // within the clueTip body. more info below [6]
        showTitle: true,     // show title bar of the clueTip, even if title attribute not set
        cluetipClass: 'default', // class added to outermost clueTip div in the form of 'cluetip-' + clueTipClass.
        hoverClass: '',       // class applied to the invoking element onmouseover and removed onmouseout
        waitImage: true,     // whether to show a "loading" img, which is set in jquery.cluetip.css
        cursor: 'help',
        arrows: false,    // if true, displays arrow on appropriate side of clueTip
        dropShadow: true,     // set to false if you don't want the drop-shadow effect on the clueTip
        dropShadowSteps: 6,        // adjusts the size of the drop shadow
        sticky: false,    // keep visible until manually closed
        mouseOutClose: false,    // close when clueTip is moused out
        activation: 'hover',  // set to 'click' to force user to click to show clueTip
        // set to 'focus' to show on focus of a form element and hide on blur
        clickThrough: false,    // if true, and activation is not 'click', then clicking on link will take user to the link's href,
        // even if href and tipAttribute are equal
        tracking: false,    // if true, clueTip will track mouse movement (experimental)
        delayedClose: 0,        // close clueTip on a timed delay (experimental)
        closePosition: 'top',    // location of close text for sticky cluetips; can be 'top' or 'bottom' or 'title'
        closeText: 'Close',  // text (or HTML) to to be clicked to close sticky clueTips
        truncate: 0,        // number of characters to truncate clueTip's contents. if 0, no truncation occurs

        // effect and speed for opening clueTips
        fx: {
            open: 'show', // can be 'show' or 'slideDown' or 'fadeIn'
            openSpeed: ''
        },

        // settings for when hoverIntent plugin is used             
        hoverIntent: {
            sensitivity: 3,
            interval: 50,
            timeout: 0
        },

        // function to run just before clueTip is shown.           
        onActivate: function(e) { return true; },

        // function to run just after clueTip is shown.
        onShow: function(ct, c) { },

        // whether to cache results of ajax request to avoid unnecessary hits to server    
        ajaxCache: true,

        // process data retrieved via xhr before it's displayed
        ajaxProcess: function(data) {
            data = data.replace(/<s(cript|tyle)(.|\s)*?\/s(cript|tyle)>/g, '').replace(/<(link|title)(.|\s)*?\/(link|title)>/g, '');
            return data;
        },

        // can pass in standard $.ajax() parameters, not including error, complete, success, and url
        ajaxSettings: {
            dataType: 'html'
        },
        debug: false
    };


    /*
    * Global defaults for clueTips. Apply to all calls to the clueTip plugin.
    *
    * @example $.cluetip.setup({
    *   insertionType: 'prependTo',
    *   insertionElement: '#container'
    * });
    * 
    * @property
    * @name $.cluetip.setup
    * @type Map
    * @cat Plugins/tooltip
    * @option String insertionType: Default is 'appendTo'. Determines the method to be used for inserting the clueTip into the DOM. Permitted values are 'appendTo', 'prependTo', 'insertBefore', and 'insertAfter'
    * @option String insertionElement: Default is 'body'. Determines which element in the DOM the plugin will reference when inserting the clueTip.
    *
    */

    var insertionType = 'appendTo', insertionElement = 'body';
    $.cluetip = {};
    $.cluetip.setup = function(options) {
        if (options && options.insertionType && (options.insertionType).match(/appendTo|prependTo|insertBefore|insertAfter/)) {
            insertionType = options.insertionType;
        }
        if (options && options.insertionElement) {
            insertionElement = options.insertionElement;
        }
    };

})(jQuery);


/**
* jQuery.ScrollTo - Easy element scrolling using jQuery.
* Copyright (c) 2007-2008 Ariel Flesler - aflesler(at)gmail(dot)com | http://flesler.blogspot.com
* Dual licensed under MIT and GPL.
* Date: 9/11/2008
* @author Ariel Flesler
* @version 1.4
*
* http://flesler.blogspot.com/2007/10/jqueryscrollto.html
*/
; (function(h) { var m = h.scrollTo = function(b, c, g) { h(window).scrollTo(b, c, g) }; m.defaults = { axis: 'y', duration: 1 }; m.window = function(b) { return h(window).scrollable() }; h.fn.scrollable = function() { return this.map(function() { var b = this.parentWindow || this.defaultView, c = this.nodeName == '#document' ? b.frameElement || b : this, g = c.contentDocument || (c.contentWindow || c).document, i = c.setInterval; return c.nodeName == 'IFRAME' || i && h.browser.safari ? g.body : i ? g.documentElement : this }) }; h.fn.scrollTo = function(r, j, a) { if (typeof j == 'object') { a = j; j = 0 } if (typeof a == 'function') a = { onAfter: a }; a = h.extend({}, m.defaults, a); j = j || a.speed || a.duration; a.queue = a.queue && a.axis.length > 1; if (a.queue) j /= 2; a.offset = n(a.offset); a.over = n(a.over); return this.scrollable().each(function() { var k = this, o = h(k), d = r, l, e = {}, p = o.is('html,body'); switch (typeof d) { case 'number': case 'string': if (/^([+-]=)?\d+(px)?$/.test(d)) { d = n(d); break } d = h(d, this); case 'object': if (d.is || d.style) l = (d = h(d)).offset() } h.each(a.axis.split(''), function(b, c) { var g = c == 'x' ? 'Left' : 'Top', i = g.toLowerCase(), f = 'scroll' + g, s = k[f], t = c == 'x' ? 'Width' : 'Height', v = t.toLowerCase(); if (l) { e[f] = l[i] + (p ? 0 : s - o.offset()[i]); if (a.margin) { e[f] -= parseInt(d.css('margin' + g)) || 0; e[f] -= parseInt(d.css('border' + g + 'Width')) || 0 } e[f] += a.offset[i] || 0; if (a.over[i]) e[f] += d[v]() * a.over[i] } else e[f] = d[i]; if (/^\d+$/.test(e[f])) e[f] = e[f] <= 0 ? 0 : Math.min(e[f], u(t)); if (!b && a.queue) { if (s != e[f]) q(a.onAfterFirst); delete e[f] } }); q(a.onAfter); function q(b) { o.animate(e, j, a.easing, b && function() { b.call(this, r, a) }) }; function u(b) { var c = 'scroll' + b, g = k.ownerDocument; return p ? Math.max(g.documentElement[c], g.body[c]) : k[c] } }).end() }; function n(b) { return typeof b == 'object' ? b : { top: b, left: b} } })(jQuery);

