// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// setting up the data table
$(document).ready(function () {
    $('#UsersAndRoles').dataTable({
    });
});

// once a button is clicked it calls the handler in the Admin controller to update the database
function updateRole(switchid, uname, urole) {
    var isChecked = document.getElementById(switchid).checked;
    console.log('id: ' + switchid + ' is checked: ' + isChecked);
   // var data = JSON.stringify({ 'user': uname, 'role': urole });
    var data = { 'user': uname, 'role': urole, 'toggle_state': isChecked };
    $.post("/Admin/UpdateRole", data)
}

function markRead(notificationId) {
    var data = { 'notificationId': notificationId };
    $.post("/Notifications/MarkRead", data)
}

function acceptApplicant(oId, aId)
{
    var data = {
        'oId': oId,
        'aId': aId
    };
    $.post("/Applications/UpdateSlotsAndNotify", data)
}