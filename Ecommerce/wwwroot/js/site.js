// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
//const getCartItemCount = (cartSessionkey) => {
//}

//getCartItemCount(cartSessionkey);

$(document).ready(function () {
    updateCartItemCount(); // Cập nhật số lượng sản phẩm khi trang được tải

    function updateCartItemCount() {
        $.ajax({
            url: '/Cart/GetCartItemsCount',
            type: 'GET',
            success: function (data) {
                $('#cartItemCount').text(data); // Cập nhật số lượng sản phẩm trên navbar
            },
            error: function (error) {
                console.log(error);
            }
        });
    }
});