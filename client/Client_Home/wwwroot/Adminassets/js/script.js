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
                confirmationMessage += "ID: " + item + "\n";
            });
            if (confirm(confirmationMessage)) {
                // Continue with the deletion
                var deleteUrl = '/Admin/Admin' + controllerName + '/DeleteMultiple';
                console.log(deleteUrl);
                console.log(itemIds);
                fetch(deleteUrl, {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                    },

                    body: JSON.stringify({ itemIds: itemIds }),

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
        performAjaxRequest(strkeyword);
    });
    performAjaxRequest('');
});
