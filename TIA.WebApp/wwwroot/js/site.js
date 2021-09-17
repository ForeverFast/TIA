// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification


$.ajaxSetup({ cache: false });

function Index() {
    var $this = this;
    function initialize() {

        $(".popupCreateEdit").on('click', function (e) {
            modelPopupCreateEdit(this);
        });

        $(".popupDelete").on('click', function (e) {
            modelPopupDelete(this);
        });

        function showModal(data) {

            $('#modal-view').find(".modal-dialog").html(data);
            $('#modal-view > .modal', data).modal("show");
        }

        function modelPopupCreateEdit(reff) {
            var url = $(reff).data('url');
            debugger;
            $.get(url, { parentId: $(reff).data('parentId'), itemId: $(reff).data('itemId') }).done(function (data) {
                showModal(data);
            });
        }

        function modelPopupDelete(reff) {
            var url = $(reff).data('url');
            debugger;
            $.get(url, { id: $(reff).data('id') }).done(function (data) {
                showModal(data);
            });
        }
    }

    $this.init = function () {
        initialize();
    };
}

$(function () {
    var self = new Index();
    self.init();
});

function deleteEntity(button) {

}
