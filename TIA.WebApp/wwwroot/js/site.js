// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification

(function ($) {
    function Index() {
        var $this = this;
        function initialize() {

            $(".popup").on('click', function (e) {
                modelPopup(this);
            });

            function modelPopup(reff) {
                var url = $(reff).data('url');
                $.get(url, { parentId: $(reff).data('parentId'), itemId: $(reff).data('itemId') } ).done(function (data) {
                    debugger;
                    $('#modal-create-edit-catalog').find(".modal-dialog").html(data);
                    $('#modal-create-edit-catalog > .modal', data).modal("show");
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
}(jQuery));