/// Jquery Toggle has wrote by babak jahangiri
/// <reference path="~/Scripts/jquery-1.10.2.intellisense.js" />
/// <reference path="~/lib/jquery/jquery-1.10.2.js" />
// ITANC.COM 

$("#a-toggle-arrow").click(function () {
    $("#form-filter").toggle(500);
    $("#btn-toggle-chevron").toggleClass("fa-chevron-down fa-chevron-up");
});

var width = $(window).width();
var id;
$(window).resize(function () {
    clearTimeout(id);
    id = setTimeout(decide_width, 100);
});

function decide_width() {
    if ($(this).width() > 767) {
        $("#form-filter").css("display", "block");
    }
    else {
         $("#form-filter").css("display", "none");
     }
}

// usage:
//$(window).smartresize(function () {
//   // decide_width();
//});

 
//(function ($, sr) {

//    // debouncing function from John Hann
//    // http://unscriptable.com/index.php/2009/03/20/debouncing-javascript-methods/
//    var debounce = function (func, threshold, execAsap) {
//        var timeout;

//        return function debounced() {
//            var obj = this, args = arguments;
//            function delayed() {
//                if (!execAsap)
//                    func.apply(obj, args);
//                timeout = null;
//            };

//            if (timeout)
//                clearTimeout(timeout);
//            else if (execAsap)
//                func.apply(obj, args);

//            timeout = setTimeout(delayed, threshold || 100);
//        };
//    }
//    // smartresize 
//    jQuery.fn[sr] = function (fn) { return fn ? this.bind("resize", debounce(fn)) : this.trigger(sr); };

//})(jQuery, "smartresize");



