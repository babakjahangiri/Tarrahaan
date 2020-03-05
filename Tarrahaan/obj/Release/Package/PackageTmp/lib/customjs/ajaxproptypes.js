function FillPropertyTypes() {

    var categoryId = $('#CategoryList').val();
    $.ajax({
        url: '/WebAdmin/en/Property/GetPropertyType',
        type: "GET",
        datatype: "JSON",
        data: { categoryId: categoryId },
        success: function (protypes) {
            $("#ProTypeList").html(""); // clear before appending new list 
            $.each(protypes, function (i, protype) {
                $("#ProTypeList").append(
                    $('<option></option>').val(protype.Value).html(protype.Text));
            });
        }
    });
}