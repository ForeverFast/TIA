// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification

/*const { Tab } = require("bootstrap");*/

$.ajaxSetup({ cache: false });

$(".popupCreateEdit").on('click', function (e) {
    modelPopupCreateEdit(this);
});

$(".popupDelete").on('click', function (e) {
    modelPopupDelete(this);
});

function showModal(data) {

    $('#modal-view').find(".modal-dialog").html(data);
    $('#modal-view > .modal', data).modal("show");

    $('#CreateEditProductForm').submit(function (e) {
        e.preventDefault()
       
        var formAction = $(this).attr("action");
        var productForm = document.getElementById('CreateEditProductForm');
        var actionType = productForm.dataset["actionType"];
        var product = $(productForm).serializeObject();

        $.post(formAction, { product: product }).done(function (data) {
            debugger;
            if (actionType == "Add") {
                insertProductHTML(data);
            }
            else {
                var table = document.getElementById('ProductTable');
                var prElement = document.getElementById('product-' + product["id"]);
                table.removeChild(prElement);

                insertProductHTML(data);
            }
            
        });
    });
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
    
function insertProductHTML(data) {
    if (data != null) {
        $('#ProductTable').append(data);
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






//tr.id = 'product-' + data["id"];
//tr.innerHTML =
//    '<td>' + data["title"] + '</td>' +
//    '<td>' + data["description"] + '</td>' +
//    '<td>' + data["price"] + '</td>' +
//    '<td><a class="btn btn-primary popupCreateEdit" data-url="/Product/CreateEdit" data-item-id="' + data["id"] + '" data-parent-id="' + data["parentCatalogId"] +
//    '"data-toggle="modal" data-target="#modal-view" id="Product-E">/</a></td>' +
//    '<td><a class="btn btn-primary popupDelete" data-url="/Product/Delete" data-id="' + data["id"] +
//    '"data-toggle="modal" data-target="#modal-view" id="Product-D">-</a></td>';