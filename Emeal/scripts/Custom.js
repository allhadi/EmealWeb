
     $(".btn-group-toggle input:radio").on('change', function () {
        $.post("/umbraco/surface/Cart/OrderType/", function (data) {
      $('#cart_content').load('@Url.Action("Basket","Cart")');
         });
     });
