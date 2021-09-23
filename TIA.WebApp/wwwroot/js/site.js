// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification

/*const { Tab } = require("bootstrap");*/

$.ajaxSetup({ cache: false });

function addModalEvents() {
    $('#DataPage').on('click', '.popupCreateEdit', function (e) {
        modelPopupCreateEdit(this);
    });

    $('#DataPage').on('click', '.popupDelete', function (e) {
        modelPopupDelete(this);
    });
}

addModalEvents();

function showModal(data) {

    $('#modal-view').find(".modal-dialog").html(data);
    $('#modal-view > .modal', data).modal("show");

    $('#CreateEditProductForm').submit(function (e) { 
        e.preventDefault();

        if (!$('#CreateEditProductForm').valid())
            return;

        var formAction = $(this).attr("action");
        var actionType = this.dataset["actionType"];
        var product = $(this).serializeObject();
        var token = $('input[name="__RequestVerificationToken"]', this).val();

        $.post(formAction, { vm: product, __RequestVerificationToken: token }).done(function (data) {

            if (actionType == "Add") {
                if (productForm.dataset["isEmptyCatalog"] == "True") {
                    $.ajax({
                        url: "/Catalog/GetEmptyTable",
                        type: "GET",
                        async: false
                    }).done(function (table) {
                        $('#NoElementsData').hide();
                        $('#ProductsData').html(table);
                    });
                }

                insertTableItem(data);
            }
            else {
 
                var catalogId = $('#DataPage').get(0).dataset["catalogId"];
                if (catalogId == product["ParentCatalogId"]) {
                    $('#item-' + product["Id"]).get(0).outerHTML = data;
                }
                    
            }

            $('#modal-view').modal("hide");
        }).fail(function (data) {
            // todo?
        });

    });

    $('#YesNoForm').submit(function (e) {
        e.preventDefault();

        var formAction = $(this).attr("action");
        var objectDTO = $(this).serializeObject();
        var token = $('input[name="__RequestVerificationToken"]', this).val();

        $.post(formAction, { obj: objectDTO, __RequestVerificationToken: token }).done(function (data) {
            $(data).each(function (index) {
                try {
                    $('#item-' + this).get(0).remove();
                    $('#treeNode-' + this).get(0).remove();
                }
                catch (e) {

                }
            });

            $('#modal-view').modal("hide");
        });
    });

    $.validator.unobtrusive.parse('#modal-view');
}

function modelPopupCreateEdit(reff) {
    var url = $(reff).data('url');

    $.get(url, { parentId: $(reff).data('parentId'), itemId: $(reff).data('itemId') }).done(function (data) {
        showModal(data);
    });
}

function modelPopupDelete(reff) {
    var url = $(reff).data('url');

    $.get(url, { id: $(reff).data('id') }).done(function (data) {
        showModal(data);
    });
}

function insertTableItem(data) {
    if (data != null) {
        $('#ProductTable > tbody').append(data);
    } else {
        console.log('Не сохранилось...');
    }
}

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
    if ($("div").is("#DataPage")) {

        $.get(elem.dataset["url"], { partialViewFlag: true }).done(function (data) {
            $(".rightSide").html(data);
            addModalEvents();
        });

    }
    else {
        $(location).attr('href', elem.dataset["url"]);
    }
});
