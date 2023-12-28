function addEvents(controllerName) {












    $(document).ready(function () {
        $('input[type="checkbox"]').change(function () {
            var row = $(this).closest('tr');
            var productName = row.find('#product-name').data('name');
            var productPrice = parseFloat(row.find('#product-price').data('price'));
            if (this.checked) {
                addProductToTable(productName, productPrice);
                updateTotalProductPrice(productPrice);
            }
        });

        $(document).on('click', '.delete-button', function () {
            var row = $(this).closest('tr');
            var productPrice = parseFloat(row.find('#product-price').data('price'));
            row.remove();
            updateTotalProductPrice(-productPrice);
        });

        $('#discount').change(function () {
            updateTotalAmount();
        });
    });


    // Attach event listener to the "Generate Invoice" button
    document.getElementById("generateInvoice").addEventListener("click", function () {
        // Add logic to generate the invoice based on the selected products and billing information
        // For example: alert("Invoice generated!");
    });


    var checkAllCheckbox = document.getElementById('checkAll');
    checkAllCheckbox.addEventListener('click', function () {
        var checkboxes = document.querySelectorAll('[id^="check-item-"]');
        checkboxes.forEach(function (checkbox) {
            checkbox.checked = checkAllCheckbox.checked;
        });
    });
    console.log("Hello");
    var deleteButton = document.getElementById('update-action-DeleteMultiple');
    var updateStatusButton = document.getElementById('update-action-UpdateStatusMultiple');
    //Get controllerName
    var controllerElement = document.querySelector('[id^="main-content-"]');
    if (controllerElement) {
        controllerName = controllerElement.id.replace('main-content-', '');
    }

    updateStatusButton.addEventListener('click', function () {
        sendUpdateRequest('UpdateStatusMultiple');
    });
    if (deleteButton != null) {
        console.log('Cúc cái');
        deleteButton.addEventListener('click', function () {
            sendUpdateRequest('DeleteMultiple');
        });
    }
    function sendUpdateRequest(actionName) {
        var checkboxes = document.querySelectorAll('[id^="check-item-"]:checked');
        var itemIds = [];

        checkboxes.forEach(function (checkbox) {
            // Extract the item ID from the checkbox ID
            var itemId = parseInt(checkbox.id.replace('check-item-', ''));
            itemIds.push(itemId);
        });
        console.log(itemIds);
        // Send the list of item IDs to the server for deletion
        if (itemIds.length > 0) {
            // Display a confirmation dialog with item information
            var confirmationMessage = "Are you sure you want to delete the following item?\n\n";
            itemIds.forEach(function (item) {
                confirmationMessage += "Item ID: " + item + "\n";
            });
            console.log('/Admin/Admin' + controllerName + '/' + actionName);
            if (confirm(confirmationMessage)) {
                fetch('/Admin/Admin' + controllerName + '/' + actionName, {
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
                        location.reload(true);
                    });
            }
        }
    }

}

function updateTotalProductPrice(change) {
    var totalProductPriceInput = document.getElementById("totalProductPrice");
    var currentTotal = parseFloat(totalProductPriceInput.value) || 0;
    totalProductPriceInput.value = (currentTotal + change).toFixed(2);
    updateTotalAmount();
}

function updateTotalAmount() {
    var totalProductPriceInput = document.getElementById("totalProductPrice");
    var discountInput = document.getElementById("discount");
    var totalAmountInput = document.getElementById("totalAmount");

    var totalProductPrice = parseFloat(totalProductPriceInput.value) || 0;
    var discount = parseFloat(discountInput.value) || 0;

    totalAmountInput.value = (totalProductPrice - discount).toFixed(2);
}

function addProductToTable(productName, productPrice) {
    var selectedProductsList = document.getElementById("selectedProductsList");
    var tr = document.createElement("tr");

    var tdName = document.createElement("td");
    tdName.textContent = productName;
    tr.appendChild(tdName);

    var tdPrice = document.createElement("td");
    tdPrice.textContent = productPrice;
    tr.appendChild(tdPrice);

    var tdButton = document.createElement("td");
    var button = document.createElement("button");
    button.style.backgroundColor = "red";
    button.textContent = "x";
    button.className = "delete-button";
    tdButton.appendChild(button);
    tr.appendChild(tdButton);

    selectedProductsList.appendChild(tr);
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
            keyword: strKeyword
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
        performAjaxRequest(0);
    });
    $("#keyword").keyup(function () {
        performAjaxRequest(0);
    });
    performAjaxRequest(0);
});
