var Utils = Utils || (function ($) {

    "use strict";

    //Constructor
    var utils = function () {
    };
    
    utils.prototype.DisplayAlert = function (selector, text, error) {

        var msgSpan;
        msgSpan = $(selector).find(".alert-text")[0];
        if (!msgSpan)
            return;

        // ensure alert is safe for re-use
        $(selector).find('.close').click(function () {
            $(this).parent().removeClass("in");
        });

        if (typeof error === "boolean" && error === true) {
            $(selector).addClass("alert-danger").removeClass("alert-success");
            text = text ? text : "There was an error processing the request";
        }
        else {
            $(selector).addClass("alert-success").removeClass("alert-danger");
            text = text ? text : "Request completed successfully";
        }

        $(msgSpan).html(text);
        $(selector).addClass("el-in");
    };

    utils.prototype.ClearAlert = function (selector) {
        var msgSpan;
        msgSpan = $(selector).removeClass("el-in").find(".alert-text")[0];
        if (!msgSpan)
            return;

        $(msgSpan).text("");
    };

    utils.prototype.LoadPartialView = function (url, params, callBackSource, alertEl, callback) {
        $.ajax({
            data: params,
            url: url,
            success: function (data) {
                if (typeof callback === 'function') {
                    callback.apply(callBackSource, [data]);
                }
            }
        }).fail(function (xhr, status, error) {
            if (!error || error.length === 0) {
                error = "Network Error";
            }
            Utils.DisplayAlert(alertEl, error, true);
        });
    };

    return new utils();
}($));