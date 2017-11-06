function deleteVacancy(id, title) {
    bootbox.confirm("Do You want to Delete this Vacancy  JOB Title : " + title, function (result) {
        if (result) {
            RemoveVacancy(id);
        } else {

        }
    });
}

function editVacancy(a, x, y, z) {
    $("#createNewVacancy").hide(1000);
    $("#editVacancy").show(1000);

    $("#VacancyID").val(a);
    $("#jobTitleE").val(x);
    var str = y;
    var date = str.split(" ");
     
    $("#clossingDateE").val(date[0]);
   
    if (z == 1) {       
        document.getElementById("isPublishedCheckBoxE").checked = true;
    }
    else {   
        document.getElementById("isPublishedCheckBoxE").checked = false;
    }    
}

function createNewVacancy() {
    $("#editVacancy").hide(1000);
    $("#createNewVacancy").show(1000);

}

$("#editVacancy").hide();
document.getElementById("isPublishedCheckBox").checked = true;
var ISpublishChecked = true;



$('#jobTitleSelect').on('input', function (e) {
    $('#jobTitleSelect').css("border-color", "#D5D8D6");
});
$('#clossingDate').on('input', function (e) {
    $('#clossingDate').css("border-color", "#D5D8D6");
});


$('#VacancyID').on('input', function (e) {
    $('#jobDescriptionE').css("border-color", "#D5D8D6");
});
$('#clossingDateE').on('input', function (e) {
    $('#clossingDateE').css("border-color", "#D5D8D6");
});



$('#searchVacancy').on('input', function (e) {
    //alert("k");
    FilterVacancyDetails();
});



$("#AddVacancybtn").click(function () {
    var det = validateVacancyInputs();

    if (det == true) {
        CreateVacancy();
    }

    return det;
});

$("#editVacancybtn").click(function () {
    var det = validateInputsForEditVcancies();

    if (det == true) {
        SaveVacancyDetails();
        //alert("ghj");
    }
    return det;
});





function validateVacancyInputs() {
    det = false;
    if (!$("#jobTitleSelect").val()) {
        $('#jobTitleSelect').css("border-color", "red");
    }
    if (!$("#clossingDate").val()) {
        $('#clossingDate').css("border-color", "red");
    }
   

    if ($("#jobTitleSelect").val() && $("#clossingDate").val()) {
        $('#jobTitleSelect').css("border-color", "#D5D8D6");
        $('#clossingDate').css("border-color", "#D5D8D6");     
        det = true;
    }
    return det;
}


function validateInputsForEditVcancies() {
    det = false;
    
    if (!$("#clossingDateE").val()) {
        $('#clossingDateE').css("border-color", "red");
    }
   
    if ($("#clossingDateE").val()) {
        $('#clossingDateE').css("border-color", "#D5D8D6");           
        det = true;
    }
    return det;
}


function check() {
    if (document.getElementById("isPublishedCheckBox").checked == true) {
        ISpublishChecked = true;        
    }
    else {
        ISpublishChecked = false;       
    }  
}

function checkE() {
    if (document.getElementById("isPublishedCheckBoxE").checked == true) {
        ISpublishCheckedE = true;
    }
    else {
        ISpublishCheckedE = false;
    }
}

//----------------- Create JOB vacancy-------------------------------------//
function CreateVacancy() {
    isactivatePublish = true;
   
    var jsonObject = {
        "publishedBy": $("#jobTitleSelect").val(),
        "closingDate": $("#clossingDate").val(),
        "isActivated": ISpublishChecked
    };
    $.ajax({
        url: "/Vacancy/CreateNewVacancy",
        type: "POST",
        data: JSON.stringify(jsonObject),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (response) {
            bootbox.alert("Error...");
        },      
        success: function (response) {
            if (response == "NewVacancyCreated_Successfully") {
                var dialog = bootbox.dialog({
                    message: '<p class="text-center"> New Vacancy Successfully Created...</p>',
                    closeButton: false
                });
                setTimeout(function () {
                    window.location.replace("/Vacancy/Index");
                }, 1500);              
            }

            else {
                if (response == "NewVacancyCeated") {
                    bootbox.alert("New Vacancy Details Saved...");
                }
                else if (response == "ErrorOccured") {
                    bootbox.alert("New Vacancy Details Not Saved... Exception Occured !!!");
                }
                else if (response == "JobTitle_Not_Exsist") {
                    bootbox.alert("New Vacancy Details Not Saved... !!!");
                }

            }


        }

    });
}


//------------------------------ Save Vacancy Details --------------------------------------------//
function SaveVacancyDetails() {
    var jsonObject = {
        "VacancyId": $("#VacancyID").val(),
        "closingDate": $("#clossingDateE").val(),
        "publishedBy": $("#jobTitleE").val(),
        "isActivated": ISpublishCheckedE
    };
    $.ajax({
        url: "/Vacancy/SaveVacancyDetails",
        type: "POST",
        data: JSON.stringify(jsonObject),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (response) {
            bootbox.alert("Error...");
        },
        success: function (response) {
            if (response == "VacancyDetails_Successfullly_Saved") {
                var msg = '<p class="text-center">Vacancy Details Successfully Saved...</p>';
                var dialog = bootbox.dialog({
                    message: msg,
                    closeButton: false
                });
                setTimeout(function () {
                    window.location.replace("/Vacancy/Index");
                }, 1500);
            }
            else {
                if (response == "VacancyDetails_Not_Saved") {
                    bootbox.alert("Vacancy Details Not Saved...");
                }
                if (response == "ErrorOccured") {
                    bootbox.alert("Vacancy Details Not Saved... Exception Occured !!!");
                }
            }

        }
    });
}

//----------- Remove Vacancy -------------------------------------//
function RemoveVacancy(id) {
    var jsonObject = {
        "VacancyId": id
    };
    $.ajax({
        url: "/Vacancy/DeleteVacancy",
        type: "POST",
        data: JSON.stringify(jsonObject),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (response) {
            bootbox.alert("Error...");
        },
        success: function (response) {

            if (response == "VacancyDetails_Successfullly_Deleted") {
                var dialog = bootbox.dialog({
                    message: '<p class="text-center">Vacancy Details Successfully Deleted...</p>',
                    closeButton: false
                });
                setTimeout(function () {
                    window.location.replace("/Vacancy/Index");
                }, 2500);
            }
            else {
                if (response == "VacancyDetails_Deleted") {
                    bootbox.alert("Vacancy Details Removed...");
                }
                if (response == "ErrorOccured") {
                    bootbox.alert("Vacancy Details Not Removed... Exception Occured !!!");
                }
            }

        }
    });
}


//-------------------- Filter Vacancy Details -----------------------------------//
function FilterVacancyDetails() {
    var jsonObject = {
        "publishedBy": $("#searchVacancy").val()
    };
    $.ajax({
        url: "/Vacancy/FilterVacancy",
        type: "POST",
        data: JSON.stringify(jsonObject),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (response) {
            bootbox.alert("Error...");
        },
        success: function (response) {
            $("#example112").empty();
            $('#example112').append($('<tr/>').append($('<th/>').append("JOB Title"),$('<th/>').append("Closing Date"), $('<th/>').append("")));
            for (i in response) {
                var MyDate_String_Value = response[i].closingDate;
                var value = new Date
                (
                parseInt(MyDate_String_Value.replace(/(^.*\()|([+-].*$)/g, ''))
                );
                var h = value.getHours();
                var suffix = (h < 12) ? 'AM' : 'PM';
                if (h > 12) {
                    h = h - 12;
                }

                var dat = value.getMonth() +
                1 +
                "/" +
                value.getDate() +
                "/" +
                value.getFullYear();


                if (response[i].isActivated) {
                    $('#example112').append($('<tr/>').append($('<td/>').append(response[i].job.JobTitle), $('<td/>').append(dat), $('<td/>').append('<a href="#" onclick=editVacancy("' + response[i].job.JOBId + '","' + response[i].job.JobTitle + '","' + response[i].job.JobDescription + '","' + response[i].job.JobRequiredQualification + '")>Manage</a>' + ' | ' + '<a href="#" onclick=editVacancy("' + response[i].job.JOBId + '","' + response[i].job.JobTitle + '")>Delete</a>')).css('background-color', "#A4F988"));
                   // $('#example112').append($('<tr/>').append($('<td/>').append(response[i].job.JobTitle), ('<td/>').append(response[i].closingDate), ('<td/>').append('<a href="#" onclick=editVacancy("' + response[i].VacancyId + '","' + response[i].job.JobTitle + '","' + response[i].closingDate + '","' + response[i].job.JobTitle + '")>Manage</a>' + ' | ' + '<a href="#" onclick=deleteVacancy("' + response[i].VacancyId + '","' + response[i].job.JobTitle + '")>Delete</a>')));
                }
                else {
                    $('#example112').append($('<tr/>').append($('<td/>').append(response[i].job.JobTitle), $('<td/>').append(dat), $('<td/>').append('<a href="#" onclick=editVacancy("' + response[i].job.JOBId + '","' + response[i].job.JobTitle + '","' + response[i].job.JobDescription + '","' + response[i].job.JobRequiredQualification + '")>Manage</a>' + ' | ' + '<a href="#" onclick=deleteJOB("' + response[i].job.JOBId + '","' + response[i].job.JobTitle + '")>Delete</a>')).css('background-color', "#FFD290"));
                }

            }
        }
    });
}