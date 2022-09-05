$(document).ready(function () {

    var origin = location.origin;
    var dev = 'http://localhost:62001';
    var deploy = location.origin + '/webapi';

    if (sessionStorage.getItem('accessToken') == null) {
        window.location.href = origin + "/Register.html"
    }

    $('#txtGUsername').val(sessionStorage.getItem("Username"));
    $('#txtGPassword').val(sessionStorage.getItem("Password"));

    //**************************************************************
    // it is for basic authorization purpose, don't remove please
    //**************************************************************
    //$('#divBasicAuthUsername').load("../commonBasicAuthForm.html");


    // Wired the button to click function
    $('#btnGet').click(function () {
        //*******************************
        // get user name and password
        //*******************************
        var username = $('#txtGUsername').val();
        var password = $('#txtGPassword').val();

        $('#linkClose').click(function () {
            $('#divMessageBox').hide('fade');
        });

        //alert("hello");
        showSpinner();

        $.ajax({
            type: 'GET',
            url: origin + '//api/CovenantMaster/getCovenantRatio',
            //url: dev + '/api/CovenantMaster',
            //url: deploy + '/api/CovenantMaster',

            dataType: 'json',
            data: {
                "covenantType": $('#covenantType').val(),
            },
            //*****************************************************************************
            // Specify the authentication header btoa() method encodes a string to Base64
            //*****************************************************************************
            headers: {
                //'Authorization': 'Basic ' + btoa(username + ':' + password)
                'Authorization': 'Bearer ' + sessionStorage.getItem("accessToken")
            },

            success: function (result) {

                $('#bodyData').empty();
                $('#divData').removeClass('hidden');
                $('#DivSpanUsername').removeClass('hidden');


                $.each(result, function (index, value) {
                    var row = $('<tr><td>'
                        + value.COVENANT_NO + '</td><td>'
                        + value.RECORD_NO + '</td><td>'
                        + value.CUST_ABBR + '</td><td>'
                        + value.CCIF_NAME + '</td><td>'
                        + value.COVENANT_DECS + '</td><td>'
                        + value.CONDITION_GROUP_DESC + '</td><td>'
                        + value.CONDITION_DESC + '</td><td>'
                        + value.RATE + '</td><td>'
                        + value.RATE_ADJ + '</td><td>'
                        + value.DATE_RECIEVED_ADJ + '</td><td>'
                        + value.REPORT_DATE + '</td><td>'
                        + value.DUE_DATE + '</td><td>'
                        + value.DATE_RECIEVED + '</td></tr>'
                    );

                    $('#tblData').append(row);                   
                });
                hideSpinner();

                $('#divModalTitleText').text("Covenant Master");

                $('#divMessageBox').addClass('alert-success');
                $('#divMessageText').text("Fetched Successfully");
               // $('#divMessageBox').show('fade');
                
            },

            error: function (jQXHR) {

                // If status code is 401, access token expired, so redirect the user to the login page
                if (jQXHR.status == "401") {
                    $('#divMessageText').text(jQXHR.responseText);
                    $('#divModalHeader').text("Access Deny");
                    $('#divModalBody').text("Your token has expired, please login again");

                }
                else if (jQXHR.status == "404") {
                    $('#divMessageText').text(jQXHR.responseText);
                    $('#divModalHeader').text("Covenant Master");
                    $('#divModalBody').text("No record found");

                }
                else {
                    $('#divMessageText').text(jQXHR.responseText);
                    $('#divModalHeader').text("Covenant Master");
                    $('#divModalBody').text("Internal server error");

                }
                $('#bodyData').empty();
                hideSpinner();

                $('#divMessageBox').addClass('alert-warning');
               // $('#divMessageBox').show('fade');

                $('#divModalFooter').addClass("btn-warning");
                $('#successModal').modal('show');

                //400	Bad Request
                //401	Unauthorized
                //403	Forbidden
                //404	Not Found
                //500	Internal Server Error
               
            }

        });

       
    });

    // remove AccessToken from session after logout
    $('#btnBack').click(function () {
        //sessionStorage.removeItem('accessToken');
        window.location.href = origin + "/HOME";
    });

    $('#linkClose').click(function () {
        $('#divMessageBox').hide('fade');
    });

    // whenever error happen, it redirect to login page
    $('#errorModal').on('hidden.bs.modal', function () {
        window.location.href = "/HOME";
    });

    $('#spanUsername').text('  Hello ' + sessionStorage.getItem('Username'));
    //$('#spanUsername').text('Hello ' + localStorage.getItem('UserName'));

    includeHTML();
});