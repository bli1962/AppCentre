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

        showSpinner();

        $.ajax({
            type: 'GET',
            url: origin + '//api/FXFCust/getMMCustByAbbr',
            //url: dev + '/api/FXFCust/',
            //url: deploy + '/api/FXFCust/',
            dataType: 'json',
            data: {
                "abbreviation": $('#custAbbr').val(),
                "branchNo": $('#branchNo').val()
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
                        + value.SHORTNAME + '</td><td>'
                        + value.Abbreviation + '</td><td>'
                        + value.DeptCode + '</td><td>'
                        + value.TaxCode + '</td><td>'
                        + value.IBOSNO + '</td><td>'
                        + value.Location + '</td><td>'
                        + value.CCIF + '</td><td>'
                        + value.Category + '</td><td>'
                        + value.Email + '</td><td>'
                        + value.Country + '</td><td>'
                        + value.Active + '</td><td>'
                        + value.STATUS + '</td><td>'
                        + value.DELETION_STATUS + '</td><tt>'
                    );

                    $('#tblData').append(row);                  
                });
                hideSpinner();

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
                    $('#divModalHeader').text("FX Customer");
                    $('#divModalBody').text("No record found");

                }
                else {
                    $('#divMessageText').text(jQXHR.responseText);
                    $('#divModalHeader').text("FX Customer");
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


});