function deleteUser(x) {
    bootbox.confirm("Do You want to Delete this user   UserName : "+x, function (result) {
        if (result) {
            RemoveUser(x);
        } else {

        }
    });
}
function editUser(userName,fullName,email,jobRole) {
    $("#addUser").hide(1000);
    $("#editUser").show(1000);

    $("#userNameE").val(userName);
    $("#fullNameE").val(fullName);
    $("#emailAddressE").val(email);
    $("#userRoleE").val(jobRole);
    
}


$("#passwordErrE").hide();
$("#editUser").hide();
$("#duplicateErr").hide();
$("#passwordErr").hide();


$('#userName').on('input', function (e) {
    $('#userName').css("border-color", "#D5D8D6");
    $("#duplicateErr").hide();
    $("#passwordErr").hide();
});
$('#password').on('input', function (e) {
    $('#password').css("border-color", "#D5D8D6");
    $('#compassword').css("border-color", "#D5D8D6");
    $('#compassword').val("");
    $("#duplicateErr").hide();
    $("#passwordErr").hide();
});
$('#compassword').on('input', function (e) {
    $('#compassword').css("border-color", "#D5D8D6");
    $("#duplicateErr").hide();
    $("#passwordErr").hide();
});


$('#passwordE').on('input', function (e) {
    $('#passwordE').css("border-color", "#D5D8D6");
    $('#compasswordE').css("border-color", "#D5D8D6");
    $('#compasswordE').val("");
    $("#passwordErrE").hide();
});
$('#compasswordE').on('input', function (e) {
    $('#compasswordE').css("border-color", "#D5D8D6");
    $("#passwordErrE").hide();
});


$('#searchUser').on('input', function (e) {
    //alert("k");
    FilterUsers();
});


$("#AddUser").click(function () {   
    var det = validateSystemUserInputs();

    if (det == true) {
        CreateUser();
    }

    return det;
});

$("#changePassword").click(function () {
    var det = validateInputsForChangePassword();

    if (det == true) {
        changePassword();
        //alert("ghj");
    }
    return det;
});


function validateSystemUserInputs() {
    det = false;
    if (!$("#userName").val()) {
        $('#userName').css("border-color", "red");
    }
    if (!$("#fullName").val()) {
        $('#fullName').css("border-color", "red");
    }
    if (!$("#emailAddress").val()) {
        $('#emailAddress').css("border-color", "red");
    }
    


    if (!$("#password").val()) {
        $('#password').css("border-color", "red");
    }
    if (!$("#compassword").val()) {
        $('#compassword').css("border-color", "red");
    }
    if ($("#compassword").val() && $("#password").val() && ($("#password").val() != $("#compassword").val())) {
        $('#password').css("border-color", "red");
        $('#compassword').css("border-color", "red");
    }

    if ($("#compassword").val() && $("#password").val() && ($("#password").val() == $("#compassword").val()) && ($("#password").val()).length < 6) {
        $('#password').css("border-color", "red");
        $("#passwordErr").show();
        $('#passwordErr').css("color", "red");
    }

    if ($("#userName").val() && $("#fullName").val() && $("#emailAddress").val() && $("#password").val() && $("#compassword").val() && ($("#password").val() == $("#compassword").val()) && ($("#password").val()).length >= 6) {
        $('#password').css("border-color", "#D5D8D6");
        $('#compassword').css("border-color", "#D5D8D6");
        det = true;
    }
    return det;
}


function validateInputsForChangePassword() {
    det = false;

    if (!$("#passwordE").val()) {
        $('#passwordE').css("border-color", "red");
    }
    if (!$("#compasswordE").val()) {
        $('#compasswordE').css("border-color", "red");
    }
    if ($("#compasswordE").val() && $("#passwordE").val() && ($("#passwordE").val() != $("#compasswordE").val())) {
        $('#passwordE').css("border-color", "red");
        $('#compasswordE').css("border-color", "red");
    }

    if ($("#compasswordE").val() && $("#passwordE").val() && ($("#passwordE").val() == $("#compasswordE").val()) && ($("#passwordE").val()).length < 6) {
        $('#passwordE').css("border-color", "red");
        $("#passwordErrE").show();
        $('#passwordErrE').css("color", "red");
    }

    if ($("#userNameE").val() && $("#passwordE").val() && $("#compasswordE").val() && ($("#passwordE").val() == $("#compasswordE").val()) && ($("#passwordE").val()).length >= 6) {
        $('#passwordE').css("border-color", "#D5D8D6");
        $('#compasswordE').css("border-color", "#D5D8D6");
        det = true;
    }
    return det;
}



//----------------- Create User -------------------------------------//
function CreateUser() {
    var jsonObject = {
        "UserName": $("#userName").val(),
        "fullName": $("#fullName").val(),
        "emailAddress": $("#emailAddress").val(),
        "JobRole": $("#selectUserRole").val(),
        "Password": $("#password").val(),
        "ConfirmPassword": $("#compassword").val()
    };
    $.ajax({
        url: "/Account/RegisterNewUser",
        type: "POST",
        data: JSON.stringify(jsonObject),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (response) {
            alert("error");
        },
        success: function (response) {

            if (response.length != 0) {
                var dialog = bootbox.dialog({
                    message: '<p class="text-center">New System User Sucessfully added...</p>',
                    closeButton: false
                });
                setTimeout(function () {
                    window.location.replace("/Account/ManageUsers");
                }, 2500);
                
            }
            else {
                $('#userName').css("border-color", "red");
                $('#password').css("border-color", "red");
                $('#compassword').css("border-color", "red");
                $("#duplicateErr").show();
                $('#duplicateErr').css("color", "red");

            }
        }
    });
}
//------------------------------------------------------------------------------------------------------------//

//--------------------------- Change Email Address -----------------------------------------------------------//

$("#changeEmailDiv").hide();

function changeEmailAddress() {
    $("#changeEmailDiv").show(1000);
    $("#emailDiv").hide(1000);
}

$("#changeEmailAddressBtn").click(function () {
    if (!$("#emailAddressEE").val()) {
        $('#emailAddressEE').css("border-color", "red");
    }
    else{
        ChangeEmailAddress();
    }
   
});

function ChangeEmailAddress() {
    var jsonObject = {
        "UserName": $("#userNameE").val(),
        "emailAddress": $("#emailAddressEE").val(),
       
    };
    $.ajax({
        url: "/Account/ChangeEmailAddress",
        type: "POST",
        data: JSON.stringify(jsonObject),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (response) {
            alert("error");
        },
        success: function (response) {
            if (response == "Error") {
                var dialog = bootbox.dialog({
                    message: '<p class="text-center">Some Error Ocuured During Saving new Email Address...</p>',
                    closeButton: false
                });
                setTimeout(function () {
                    $('#emailAddressEE').css("border-color", "red");
                }, 2500);
            }
            else {
                var dialog = bootbox.dialog({
                    message: '<p class="text-center">Email Address Sucessfully Changed...</p>',
                    closeButton: false
                });
                setTimeout(function () {
                    window.location.replace("/Account/ManageUsers");
                }, 2500);
            }
                     
        }
    });
}


$('#emailAddressEE').on('input', function (e) {
    $('#emailAddressEE').css("border-color", "#D5D8D6");   
});

//----------------------------------------------------------------------------------------------------//



//------------- Change Password ---------------------------//
function changePassword() {
    var jsonObject = {
        "UserName": $("#userNameE").val(),
        "Password": $("#passwordE").val()
    };
    $.ajax({
        url: "/Account/changePassword",
        type: "POST",
        data: JSON.stringify(jsonObject),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (response) {
            alert("error");
        },
        success: function (response) {
            var dialog = bootbox.dialog({
                message: '<p class="text-center">Password Successfully changed...</p>',
                closeButton: false
            });
            setTimeout(function () {
                dialog.modal('hide');
                $("#compasswordE").val("");
                $("#passwordE").val("");
                $("#addUser").show(1000);
                $("#editUser").hide(1000);
            }, 1500);

        }
    });
}

//----------- Remove User -------------------------------------//
function RemoveUser(userName) {
    var jsonObject = {
        "UserName": userName
    };
    $.ajax({
        url: "/Account/RemoveUser",
        type: "POST",
        data: JSON.stringify(jsonObject),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (response) {
            alert(response.toString());
        },
        success: function (response) {
            var dialog = bootbox.dialog({
                message: '<p class="text-center"> System User Sucessfully Deleted...</p>',
                closeButton: false
            });
            setTimeout(function () {
                window.location.replace("/Account/ManageUsers");
            }, 2500);          
        }
    });
}


//-------------------- Filter Users -----------------------------------//
function FilterUsers() {
    var jsonObject = {
        "UserName": $("#searchUser").val()
    };
    $.ajax({
        url: "/Account/FilterUser",
        type: "POST",
        data: JSON.stringify(jsonObject),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (response) {
            alert("error");
        },
        success: function (response) {
            $("#example1").empty();
            $('#example1').append($('<tr/>').append($('<th/>').append(" "), $('<th/>').append("User Name"), $('<th/>').append("Full Name"), $('<th/>').append("")));
            
            for (i in response) {
                var fulName= response[i].fullName;
                var res = fulName.split(" ");
                fulName = res[0] + "_" + res[1];
                if (response[i].JobRole == "Managing Assisstance") {
                    var x = "Managing_Assisstance";
                    $('#example1').append($('<tr/>').append($('<td/>').append("MA"), $('<td/>').append(response[i].UserName), $('<td/>').append(response[i].fullName), $('<td/>').append('<a href="#" onclick=editUser("' + response[i].UserName + '","' + fulName + '","' + response[i].emailAddress + '","' + x + '")>Manage</a>' + ' | ' + '<a href="#" onclick=deleteUser("' + response[i].UserName + '")>Delete</a>')));
                }
                if (response[i].JobRole == "Interview Board Member") {
                    var x = "Interview_Board_Member";
                    $('#example1').append($('<tr/>').append($('<td/>').append("IB"), $('<td/>').append(response[i].UserName), $('<td/>').append(response[i].fullName), $('<td/>').append('<a href="#" onclick=editUser("' + response[i].UserName + '","' + fulName + '","' + response[i].emailAddress + '","' + x + '")>Manage</a>' + ' | ' + '<a href="#" onclick=deleteUser("' + response[i].UserName + '")>Delete</a>')));
                }
               
            }
        }
    });
}