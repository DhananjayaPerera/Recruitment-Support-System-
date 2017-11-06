function deleteJOB(id,title) {
    bootbox.confirm("Do You want to Delete this JOB  JOB Title : " + title, function (result) {
        if (result) {           
            RemoveJOB(id);
        } else {

        }
    });
}
function editJOB(a,x,y,z) {
    $("#createNewJob").hide(1000);
    $("#editJob").show(1000);

    $("#jobId").val(a);
    $("#jobTitleE").val(x);
    $("#jobDescriptionE").val(y);
    $("#requiredQulificationE").val(z);

    $('#jobTitleE').css("border-color", "#D5D8D6");
    document.getElementById("duplicateErrE").style.color = "white";
    $("#duplicateErrE").hide();
    //LoadJOBDetails(x);  
}
function createNewJOB() {
    $("#editJob").hide(1000);
    $("#createNewJob").show(1000);
    
}


$("#editJob").hide();
$("#duplicateJOBErr").hide();
$("#duplicateErrE").hide();


$('#jobTitle').on('input', function (e) {
    $('#jobTitle').css("border-color", "#D5D8D6");
    document.getElementById("duplicateJOBErr").style.color = "white";
    $("#duplicateJOBErr").hide();
});
$('#jobDescription').on('input', function (e) {
    $('#jobDescription').css("border-color", "#D5D8D6");
    $("#duplicateErr").hide();
});
$('#requiredQulification').on('input', function (e) {
    $('#requiredQulification').css("border-color", "#D5D8D6");
    $("#duplicateErr").hide();
});


$('#jobTitleE').on('input', function (e) {
    $('#jobTitleE').css("border-color", "#D5D8D6");
    document.getElementById("duplicateErrE").style.color = "white";
    $("#duplicateErrE").hide();
});
$('#jobDescriptionE').on('input', function (e) {
    $('#jobDescriptionE').css("border-color", "#D5D8D6");
    $("#duplicateErrE").hide();
});
$('#requiredQulificationE').on('input', function (e) {
    $('#requiredQulificationE').css("border-color", "#D5D8D6");
    $("#duplicateErrE").hide();
});

$('#searchJOB').on('input', function (e) {
    FilterJOBS();
});

$("#AddJOBbtn").click(function () {
    var det = validateInputs();

    if (det == true) {
        CreateJOB();
    }

    return det;
});

$("#editJOBbtn").click(function () {
    var det = validateInputsForEditJOBS();

    if (det == true) {
        SaveJOBDetails();
        //alert("ghj");
    }
    return det;
});





function validateInputs() {
    det = false;
    if (!$("#jobTitle").val()) {
        $('#jobTitle').css("border-color", "red");
    }
    if (!$("#jobDescription").val()) {
        $('#jobDescription').css("border-color", "red");
    }
    if (!$("#requiredQulification").val()) {
        $('#requiredQulification').css("border-color", "red");
    }
    
    if ($("#jobTitle").val() && $("#jobDescription").val() && $("#requiredQulification").val()) {
        $('#jobTitle').css("border-color", "#D5D8D6");
        $('#jobDescription').css("border-color", "#D5D8D6");
        $('#requiredQulification').css("border-color", "#D5D8D6");
        det = true;
    }
    return det;
}


function validateInputsForEditJOBS() {
    det = false;
    if (!$("#jobTitleE").val()) {
        $('#jobTitleE').css("border-color", "red");
    }
    if (!$("#jobDescriptionE").val()) {
        $('#jobDescriptionE').css("border-color", "red");
    }
    if (!$("#requiredQulificationE").val()) {
        $('#requiredQulificationE').css("border-color", "red");
    }

    if ($("#jobTitleE").val() && $("#jobDescriptionE").val() && $("#requiredQulificationE").val()) {
        $('#jobTitleE').css("border-color", "#D5D8D6");
        $('#jobDescriptionE').css("border-color", "#D5D8D6");
        $('#requiredQulificationE').css("border-color", "#D5D8D6");
        det = true;
    }
    return det;
}



//----------------- Create JOB -------------------------------------//
function  CreateJOB() {
    var jsonObject = {
        "JobTitle": $("#jobTitle").val(),
        "JobDescription": $("#jobDescription").val(),
        "JobRequiredQualification": $("#requiredQulification").val()
    };
    $.ajax({
        url: "/JOB/CreateNewJOB",
        type: "POST",
        data: JSON.stringify(jsonObject),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (response) {
            bootbox.alert("Error...");
        },
        success: function (response) {
            if (response == "JOBCreated_And_JobFolderCreated") {
                var dialog = bootbox.dialog({
                    message: '<p class="text-center"> New JOB Sucessfully Created...</p>',
                    closeButton: false
                });
                setTimeout(function () {
                    window.location.replace("/JOB/Index");
                }, 2500);              
            }

            if (response == "JobTitleAlready_Exsist") {
                $('#jobTitle').css("border-color", "red");
                document.getElementById("duplicateJOBErr").style.color = "red";
                $("#duplicateJOBErr").show();

            }
        }
    });
}


//------------------------------ Save JOB Details --------------------------------------------//
function SaveJOBDetails() {
    var jsonObject = {
        "JOBId": $("#jobId").val(),
        "JobTitle": $("#jobTitleE").val(),
        "JobDescription": $("#jobDescriptionE").val(),
        "JobRequiredQualification": $("#requiredQulificationE").val()
    };
    $.ajax({
        url: "/JOB/SaveJOBDetails",
        type: "POST",
        data: JSON.stringify(jsonObject),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (response) {
            bootbox.alert("Error...");
        },
        success: function (response) {
            
            if (response == "JOBDetails_Successfullly_Saved") {
                var msg = '<p class="text-center">JOB Details Successfully Saved...</p>';
                var dialog = bootbox.dialog({
                    message: msg,
                    closeButton: false
                });
                setTimeout(function () {
                    window.location.replace("/JOB/Index");
                }, 1500);
            }
            else {
                if (response == "JOBDetails_Not_Saved") {
                    bootbox.alert("JOB Details Not Saved...");
                }                      
                if (response == "ErrorOccured") {
                    bootbox.alert("JOB Details Not Saved... Exception Occured !!!");
                }
                            
            }
            
        }
    });
}

//----------- Remove JOB -------------------------------------//
function RemoveJOB(id) {
    var jsonObject = {
        "JOBId": id
    };
    $.ajax({
        url: "/JOB/DeleteJOB",
        type: "POST",
        data: JSON.stringify(jsonObject),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (response) {
            bootbox.alert("Error...");
        },
        success: function (response) {
            if (response == "JOBDetails_Successfullly_Deleted") {
                var dialog = bootbox.dialog({
                    message: '<p class="text-center">JOB Details Sucessfully Removed...</p>',
                    closeButton: false
                });
                setTimeout(function () {
                    window.location.replace("/JOB/Index");
                }, 2500);
            }
            else {
                if (response == "JOBDetails_Deleted") {
                    bootbox.alert("JOB Details Removed...");
                }
                if (response == "ErrorOccured") {
                    bootbox.alert("JOB Details Not Removed... Exception Occured !!!");
                }
            }           
        }
    });
}


//-------------------- Filter Users -----------------------------------//
function FilterJOBS() {
    var jsonObject = {
        "JobTitle": $("#searchJOB").val()
    };
    $.ajax({
        url: "/JOB/FilterJOB",
        type: "POST",
        data: JSON.stringify(jsonObject),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (response) {
            bootbox.alert("Error...");
        },
        success: function (response) {
            
            $("#example1").empty();
            $('#example1').append($('<tr/>').append($('<th/>').append("JOB Title"), $('<th/>').append("")));
            for (i in response) {
                $('#example1').append($('<tr/>').append($('<td/>').append(response[i].JobTitle), $('<td/>').append('<a href="#" onclick=editJOB("' + response[i].JOBId + '","' + response[i].JobTitle + '","' + response[i].JobDescription + '","' + response[i].JobRequiredQualification + '")>Manage</a>' + ' | ' + '<a href="#" onclick=deleteJOB("' + response[i].JOBId + '","' + response[i].JobTitle + '")>Delete</a>')));
            }
        }
    });
}

