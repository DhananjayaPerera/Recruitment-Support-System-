
$('#vacancyID').hide();

$('#firstname').on('input', function (e) {
    $('#firstname').css("border-color", "#D5D8D6");
});
$('#lastname').on('input', function (e) {
    $('#lastname').css("border-color", "#D5D8D6");
});
$('#email').on('input', function (e) {
    $('#email').css("border-color", "#D5D8D6");
});
$('#phoneNumber').on('input', function (e) {
    $('#phoneNumber').css("border-color", "#D5D8D6");
});
$('#Address').on('input', function (e) {
    $('#Address').css("border-color", "#D5D8D6");
});
$('#yyyy').on('input', function (e) {
    $('#yyyy').css("border-color", "#D5D8D6");
});


$('#zipcode').on('input', function (e) {
    $('#zipcode').css("border-color", "#D5D8D6");
});
$('#city').on('input', function (e) {
    $('#city').css("border-color", "#D5D8D6");
});

$('#dd').on('click', function (e) {
    $('#dd').css("border-color", "#D5D8D6");
});
$('#mm').on('click', function (e) {
    $('#mm').css("border-color", "#D5D8D6");
});
$('#FileUpload1').on('click', function (e) {
    $('#uploadCVLabel').css("color", "#858585");   
});


radiobtn = document.getElementById("gender_male");
radiobtn.checked = true;

$("#SubmitDetailsBtn").click(function () {
    var det = validateApplicationInputs();

    if (det == true) {
        SaveApplicationDetails();
    }

    return det;
});


radioVal = $('input[name=gender]:checked', '#genderForm').val();


$('#genderForm input').on('change', function () {
    radioVal = $('input[name=gender]:checked', '#genderForm').val();
    //alert($('input[name=gender]:checked', '#genderForm').val());
});

function validateApplicationInputs() {
    det = false;
    if (!$("#firstname").val()) {
        $('#firstname').css("border-color", "red");
    }
    if (!$("#lastname").val()) {
        $('#lastname').css("border-color", "red");
    }
    if (!$("#email").val()) {
        $('#email').css("border-color", "red");
    }
    if (!$("#phoneNumber").val()) {
        $('#phoneNumber').css("border-color", "red");
    }
    if (!$("#Address").val()) {
        $('#Address').css("border-color", "red");
    }


    if (!$("#zipcode").val()) {
        $('#zipcode').css("border-color", "red");
    }
    if (!$("#city").val()) {
        $('#city').css("border-color", "red");
    }
    if (!$("#dd").val()) {
        $('#dd').css("border-color", "red");
    }
    if (!$("#mm").val()) {
        $('#mm').css("border-color", "red");
    }
    if (!$("#yyyy").val()) {
        $('#yyyy').css("border-color", "red");
    }
    if (document.getElementById("FileUpload1").value == "") {
        $('#uploadCVLabel').css("color", "red");
    }

    //alert(setDOBFormat($("#dd").val(), $("#mm").val(), $("#yyyy").val()));
    if (ValidateDOBYear() && document.getElementById("FileUpload1").value != "" && $("#firstname").val() && $("#lastname").val() && $("#email").val() && $("#phoneNumber").val() && $("#Address").val() && validateDOB() && $("#zipcode").val() && $("#city").val()) {
        $('#firstname').css("border-color", "#D5D8D6");
        $('#lastname').css("border-color", "#D5D8D6");
        $('#email').css("border-color", "#D5D8D6");
        $('#phoneNumber').css("border-color", "#D5D8D6");
        $('#Address').css("border-color", "#D5D8D6");

        $('#dd').css("border-color", "#D5D8D6");
        $('#mm').css("border-color", "#D5D8D6");
        $('#yyy').css("border-color", "#D5D8D6");

        $('#zipcode').css("border-color", "#D5D8D6");
        $('#city').css("border-color", "#D5D8D6");

        $('#uploadCVLabel').css("color", "#858585");
        det = true;
    }
    return det;
}

//---------- this must validate properly ----------------------//
function validateDOB() {
    det = false;
    
    if ($("#yyyy").val() && $("#dd").val() && $("#mm").val()) {
        $('#yyyy').css("border-color", "#D5D8D6");
        $('#dd').css("border-color", "#D5D8D6");
        $('#mm').css("border-color", "#D5D8D6");
        det = true;
    }
    return det;
}
function setDOBFormat(day,month,year) {
    return (month + "/" + day + "/" + year);
}


//------------------------------ Save Application Details --------------------------------------------//
function SaveApplicationDetails() {
    var jsonObject = {
        "firstName": $("#firstname").val(),
        "lastName": $("#lastname").val(),
        "email": $("#email").val(),
        "phoneNumber": $("#phoneNumber").val(),
        "address": $("#Address").val(),
        "DOB": setDOBFormat($("#dd").val(), $("#mm").val(), $("#yyyy").val()),
        "Gender": radioVal,
        "ZipCode": $("#zipcode").val(),
        "City": $("#zipcode").val(),
        "marks": parseInt($("#vacancyID").val())
    };
    $.ajax({
        url: "/Home/SaveApplicationDetails",
        type: "POST",
        data: JSON.stringify(jsonObject),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (response) {
            alert("error");
        },
        success: function (response) {
            if (response != "Error") {
                uploadCVSoftCopy();                
            }
            else {
                bootbox.alert("There was somthing Wrong..Your CV Details Not Uploaded...Please Retry");
            }

            
        }
    });
}

function uploadCVSoftCopy() {
        // Checking whether FormData is available in browser  
        if (window.FormData !== undefined) {

            var fileUpload = $("#FileUpload1").get(0);
            var files = fileUpload.files;

            // Create FormData object  
            var fileData = new FormData();

            // Looping over all files and add it to FormData object  
            for (var i = 0; i < files.length; i++) {
                fileData.append(files[i].name, files[i]);
            }

            $.ajax({
                url: '/Home/UploadFiles',
                type: "POST",
                contentType: false, // Not to set any content header  
                processData: false, // Not to process data  
                data: fileData,
                success: function (result) {
                    var dialog = bootbox.dialog({
                        //'<p class="text-center">Your CV Details Successfully Uploaded...</p>'
                        message: result,
                        closeButton: false
                    });
                    setTimeout(function () {
                        window.location.replace("/Home/Index");
                    }, 1500);
                    //alert(result);
                },
                error: function (err) {
                    alert(err.statusText);
                }
            });
        } else {
            alert("FormData is not supported.");
        }  
}


function ValidateDOBYear() {
    det = false;
    if ($("#yyyy").val() > 1900 && $("#yyyy").val() < 2017) {
        det = true;
    }
    else {
        $('#yyyy').css("border-color", "red");
    }
    return det;
}