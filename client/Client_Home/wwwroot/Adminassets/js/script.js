document.addEventListener('DOMContentLoaded', function () {
    var checkAllCheckbox = document.getElementById('checkAll');
    checkAllCheckbox.addEventListener('click', function () {
        var checkboxes = document.querySelectorAll('[id^="check-item-"]');
        checkboxes.forEach(function (checkbox) {
            checkbox.checked = checkAllCheckbox.checked;
        });
    });
});