/*
 *  This method sets up the Bootstrap table. 
 */
$(document).ready(function () {
    var editableTable = new BSTable("example");
    editableTable.init();
});
/*
 * The find function is called to find all students who match the search query and turns them into cards to be displayed
 *
 */
function find() {
    var input = document.getElementById("search_text").value;
    var data = { 'input': input };
    $.post("/Applications/Search_Users", data)
        .done(function (result) {
            for (var i = 0; i < result.length; i++) {
                var new_card = $("#template").clone();
                $("#cards").prepend(new_card);
                $(new_card).find("card-title").html(result[i].UID);
                $(new_card).find("#email").html("Email: " +result[i].name);
                $(new_card).find("#gpa").html("GPA: " + result[i].gpa);
                $(new_card).find("#statement").html("Statement: " + result[i].statement);
                $(new_card).find("#skills").html("Skills: " + result[i].skills);
                $(new_card).show();
            }

        })
            .fail(function (result) {
                console.log("oops");
            })
            .always(function (result) {
                $("#sample").append(`<hr/>`);
            })
}
/*
 * This function changes the 'Active' state of the student
 */
function apply(email, switchid) {
    var isChecked = document.getElementById(switchid).checked;
    console.log('email: ' + email + ' is checked: ' + isChecked);
    var data = { 'email': email, 'toggle_state': isChecked };
    $.post("/Applications/Active", data)
}