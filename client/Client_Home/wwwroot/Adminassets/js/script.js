function addEvents() {
    var checkAllCheckbox = document.getElementById('checkAll');
    if (checkAllCheckbox == null) {
        alert("con cac");
    }
    checkAllCheckbox.addEventListener('click', function () {
        var checkboxes = document.querySelectorAll('[id^="check-item-"]');
        checkboxes.forEach(function (checkbox) {
            checkbox.checked = checkAllCheckbox.checked;
        });
    });

    var deleteButton = document.getElementById('deleteSeletedItemsButton');
    deleteButton.addEventListener('click', function () {
        var checkboxes = document.querySelectorAll('[id^="check-item-"]:checked');
        var productIds = [];

        checkboxes.forEach(function (checkbox) {
            // Extract the product ID from the checkbox ID
            var productId = parseInt(checkbox.id.replace('check-item-', ''));
            productIds.push(productId);
        });

        // Send the list of product IDs to the server for deletion
        if (productIds.length > 0) {
            // Display a confirmation dialog with product information
            var confirmationMessage = "Are you sure you want to delete the following products?\n\n";
            productIds.forEach(function (productId) {
                confirmationMessage += "ID: " + productId + "\n";
            });
            if (confirm(confirmationMessage)) {
                // Continue with the deletion
                var deleteUrl = '/Admin/AdminProducts/DeleteMultiple';
                console.log(typeof productIds);
                console.log(typeof JSON.stringify({ productIds: productIds }));
                fetch(deleteUrl, {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                    },

                    body: JSON.stringify({ productIds: productIds }),

                })
                    .then(response => response.json())
                    .then(data => {
                        // Handle the response from the server if needed
                        console.log(data);
                    })
                    .catch(error => {
                        console.error('Error:', error);
                    });
            }
        }
    });
}


$(document).ready(function () {
    $('.pagination-container a').on('click', function (e) {
        var pageLinkElement = document.querySelector('.pagination');
        if (pageLinkElement) {
            linkElement.setAttribute('disabled', true);
        }
        performAjaxRequest(strkeyword);
    });
    $("#keyword").keyup(function () {
        var strkeyword = $('#keyword').val();
        console.log("Hello");
        performAjaxRequest(strkeyword);
    });
    performAjaxRequest('');
});
function performAjaxRequest(pageNumber) {

    // Extract the page number from the clicked link
    var strKeyword = $('#keyword').val();
    // Perform your custom action with the page number
    $.ajax({
        url: '/Admin/Search/FindProduct',
        datatype: "json",
        type: "POST",
        data: { page: pageNumber, keyword: strKeyword },
        async: true,
        success: function (result) {
            $("#records_table").html("");
            $("#records_table").html(result);
            addEvents();
        },
        error: function (xhr) {
            alert('error');
        }
    });
}