// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification

/*const { Tab } = require("bootstrap");*/

const emptyGuid = "00000000-0000-0000-0000-000000000000";

$.ajaxSetup({ cache: false });

function showModal(data) {
    $('#modal-view').find(".modal-dialog").html(data);
    $('#modal-view > .modal', data).modal("show");
}

function doFilter(form, id) {
    if (!$('#FilterForm').valid())
        return;

    var formAction = $(form).attr("action");
    var filterData = $(form).serializeObject();
    var token = $('input[name="__RequestVerificationToken"]', this).val();

    $.get(formAction,
        {
            id: id,
            title: filterData["title"],
            minPrice: filterData["minPrice"],
            maxPrice: filterData["maxPrice"],
            minDate: filterData["minDate"],
            maxDate: filterData["maxDate"],
            __RequestVerificationToken: token
        }).done(function (table) {

            $("#DataPlace").html(table);
        }).fail(function (data) {

        });
}

function addModalEvents() {

    $('#DataPage').on('click', '.popupCreateEdit', function (e) {
        var url = $(this).data('url');

        $.get(url, { parentId: $(this).data('parentId'), itemId: $(this).data('itemId') }).done(function (data) {

            showModal(data);

            $('#CreateEditProductForm').submit(function (e) {
                e.preventDefault();

                if (!$('#CreateEditProductForm').valid())
                    return;

                var form = this;
                var formAction = $(form).attr("action");
                var product = $(form).serializeObject();
                var token = $('input[name="__RequestVerificationToken"]', form).val();

                $.post(formAction, { vm: product, __RequestVerificationToken: token }).done(function (data) {

                    if (emptyGuid == product["Id"]) {
                        if (form.dataset["isEmptyCatalog"] == "True") {
                            $.get('/Catalog/TableData', { id: product["ParentCatalogId"] }).done(function (table) {
                                $("#DataPlace").html(table);
                            });
                        }
                        else {
                            if (data != null) {
                                $('#ProductTable > tbody').append(data);
                            } else {
                                console.log('Не сохранилось...');
                            }
                        }
                    }
                    else {

                        var catalogId = $('#TableData').data("catalogId");
                        if (catalogId == product["ParentCatalogId"]) {
                            $('#item-' + product["Id"]).get(0).outerHTML = data;
                        }

                    }

                    $('#modal-view').modal("hide");
                }).fail(function (data) {
                    // todo?
                });

            });
        });
    });

    $('#DataPage').on('click', '.popupDelete', function (e) {

        var formDiv = $('#YesNoFormDiv');
        formDiv.show();
        showModal(formDiv);

        var elem = this;
        var itemId = elem.dataset["id"];

        $('#YesNoForm').submit(function (e) {
            e.preventDefault();

            var formAction = $(this).attr("action");
            var token = $('input[name="__RequestVerificationToken"]', this).val();

            $.post(formAction, { id: itemId, __RequestVerificationToken: token }).done(function (data) {

                if (data != null) {
                    try {
                        $('#item-' + data).remove();
                        $('#treeNode-' + data).remove();
                    }
                    catch (e) {

                    }
                }


            }).fail(function (data) {
                console.log('Не удалилось...');
            });

            $('#modal-view').modal("hide");
        });

        $.validator.unobtrusive.parse('#modal-view');
    });

    $('#FilterForm').submit(function (e) {
        e.preventDefault();

        doFilter(this, $('#TableData').data("catalogId"));
        
    });
}

$('#YesNoFormDiv').hide();

addModalEvents();

$.fn.serializeObject = function () {

    var self = this,
        json = {},
        push_counters = {},
        patterns = {
            "validate": /^[a-zA-Z][a-zA-Z0-9_]*(?:\[(?:\d*|[a-zA-Z0-9_]+)\])*$/,
            "key": /[a-zA-Z0-9_]+|(?=\[\])/g,
            "push": /^$/,
            "fixed": /^\d+$/,
            "named": /^[a-zA-Z0-9_]+$/
        };


    this.build = function (base, key, value) {
        base[key] = value;
        return base;
    };

    this.push_counter = function (key) {
        if (push_counters[key] === undefined) {
            push_counters[key] = 0;
        }
        return push_counters[key]++;
    };

    $.each($(this).serializeArray(), function () {

        // skip invalid keys
        if (!patterns.validate.test(this.name)) {
            return;
        }

        var k,
            keys = this.name.match(patterns.key),
            merge = this.value,
            reverse_key = this.name;

        while ((k = keys.pop()) !== undefined) {

            // adjust reverse_key
            reverse_key = reverse_key.replace(new RegExp("\\[" + k + "\\]$"), '');

            // push
            if (k.match(patterns.push)) {
                merge = self.build([], self.push_counter(reverse_key), merge);
            }

            // fixed
            else if (k.match(patterns.fixed)) {
                merge = self.build([], k, merge);
            }

            // named
            else if (k.match(patterns.named)) {
                merge = self.build({}, k, merge);
            }
        }

        json = $.extend(true, json, merge);
    });

    return json;
};

$('a[name="ctlgBtn"]').click(function () {
    var elem = this;
    if ($("#DataPlace").length != 0) {
        
        doFilter($('#FilterForm'), elem.dataset["id"]);
        addModalEvents();
    }
    else {
        $(location).attr('href', elem.dataset["url"]);
    }
});
