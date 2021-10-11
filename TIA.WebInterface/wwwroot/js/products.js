

$('#FilterProductDataForm').submit(function (e) {
    e.preventDefault();

    

    if (!$('#FilterProductDataForm').valid())
        return;

    var form = this;
    var formAction = $(form).attr("action");
    var filterData = $(form).serializeObject();
    var token = $('input[name="__RequestVerificationToken"]', this).val();

    $.get(formAction,
        {
            minPrice: filterData["minPrice"],
            maxPrice: filterData["maxPrice"],
            minDate: filterData["minDate"],
            maxDate: filterData["maxDate"],
           /* Authorization: ,*/
            __RequestVerificationToken: token
        }).done(function (table) {

            $("#ProductFullDataTablePlace").html(table);

        }).fail(function (data) {

        });

});

