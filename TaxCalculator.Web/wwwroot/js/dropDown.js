var DropDown = DropDown || (function ($) {

    "use strict";

    //Constructor
    var dropDown = function () {
    };

    dropDown.prototype.Setup = function (el, placeholderMsg, listUrl, pageSize, defaultItem, textAsId) {
            var result = defaultItem;

            $(el).select2({
                allowClear: true,
                placeholder: placeholderMsg,
                ajax: {
                    url: listUrl,
                    dataType: "json",
                    width: 'style',
                    delay: 250,
                    data: function (params) {
                        return {
                            searchTerm: params.term,
                            pageSize: pageSize,
                            pageNumber: params.page
                        };
                    },
                    processResults: function (data, params) {
                        var more = false;
                        var pageSize = pageSize || 2;

                        if (data) {
                            result = data.map(function (item) {
                                return {
                                    id: textAsId ? item.text : item.id,
                                    text: item.text
                                };
                            });
                            more = data.length === pageSize;
                        }

                        params.page = params.page || 1;
                        return {
                            results: result,
                            pagination: {
                                more: more
                            }
                        };
                    },
                    cache: true
                }
            });
    };

    return new dropDown();
}($));