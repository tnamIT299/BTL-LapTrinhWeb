function addEvents(controllerName) {
    var checkAllCheckbox = document.getElementById('checkAll');
    checkAllCheckbox.addEventListener('click', function () {
        var checkboxes = document.querySelectorAll('[id^="check-item-"]');
        checkboxes.forEach(function (checkbox) {
            checkbox.checked = checkAllCheckbox.checked;
        });
    });

    var deleteButton = document.getElementById('deleteSeletedItemsButton');
    //Get controllerName
    var controllerElement = document.querySelector('[id^="main-content-"]');
    if (controllerElement) {
        controllerName = controllerElement.id.replace('main-content-', '');
    }
    deleteButton.addEventListener('click', function () {
        var checkboxes = document.querySelectorAll('[id^="check-item-"]:checked');
        var itemIds = [];

        checkboxes.forEach(function (checkbox) {
            // Extract the item ID from the checkbox ID
            var itemId = parseInt(checkbox.id.replace('check-item-', ''));
            itemIds.push(itemId);
        });

        // Send the list of item IDs to the server for deletion
        if (itemIds.length > 0) {
            // Display a confirmation dialog with item information
            var confirmationMessage = "Are you sure you want to delete the following item?\n\n";
            itemIds.forEach(function (item) {
                confirmationMessage += "Item ID: " + item + "\n";
            });

            if (confirm(confirmationMessage)) {
                fetch('/Admin/Admin' + controllerName + '/DeleteMultiple', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                    body: JSON.stringify({
                        itemIds: itemIds
                    }),
                })
                    .then(response => response.json())
                    .then(data => {
                        if (data.success) {
                            alert(data.message);
                        } else {
                            var errorMessage = data.message + "\n";
                            data.errors.forEach(function (error) {
                                errorMessage += "Invoice ID: " + error.invoiceId + " - " + error.message + "\n";
                            });
                            alert(errorMessage);
                        }
                        performAjaxRequest('');
                    });
                    
            }
        }
    });

}
function performAjaxRequest(pageNumber) {

    // Extract the page number from the clicked link
    var strKeyword = $('#keyword').val();
    var controllerElement = document.querySelector('[id^="main-content-"]');
    var controllerName = null;
    if (controllerElement) {
        controllerName = controllerElement.id.replace('main-content-', '');
    }
    // Perform your custom action with the page number
    $.ajax({
        url: '/Admin/Search/Find' + controllerName ,
        datatype: "json",
        type: "POST",
        data: {
            page: pageNumber,
            keyword: strKeyword,
            typeName: 'Client_Home.Models.'+controllerName
        },
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
        performAjaxRequest(strkeyword);
    });
    performAjaxRequest('');
});
