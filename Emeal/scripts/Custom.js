
$(".btn-group-toggle input:radio").on('change', function ()
{
    $.post("/umbraco/surface/Cart/OrderType/", function (data)
    {
      $('#cart_content').load('@Url.Action("Basket","Cart")');
     });
});


function ChangeAddress(addType, addTd) {
    $.post("/umbraco/surface/Cart/ChangeAddress/", { addressType: addType, id: addTd });
}
