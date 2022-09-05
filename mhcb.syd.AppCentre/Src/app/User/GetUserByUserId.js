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
            url: origin + '//api/User/getUserById',
            //url: dev + '/api/User/',
            //url: deploy + '/api/User/',
            dataType: 'json',
            data: { "userId": $('#userId').val() },

            //*****************************************************************************
            // Specify the authentication header btoa() method encodes a string to Base64
            //*****************************************************************************
            headers: {
                //'Authorization': 'Basic ' + btoa(username + ':' + password)
                'Authorization': 'Bearer ' + sessionStorage.getItem("accessToken")
            },
            success: function (value) {

                $('#bodyData').empty();
                $('#divData').removeClass('hidden');
                $('#DivSpanUsername').removeClass('hidden');

                //$.each(result, function (index, value) {

                var row = $('<tr><td>'
                    + value.USER_ID + '</td><td>'
                    + value.FIRST_NAME + '</td><td>'
                    + value.LAST_NAME + '</td><td>'
                    + value.PWD_CHANGE_IND + '</td><td>'
                    + value.RACF_STATUS_CODE + '</td><td>'
                    + value.BU_STATUS_CODE + '</td><td>'
                    + value.ACU_STATUS_CODE + '</td><td>'
                    + value.NO_OF_LOGIN_ATTEMPTS + '</td><td>'
                    + value.ENABLED_IND + '</td><td>'
                    + value.REC_STATUS + '</td><td>'
                    + value.GBASE_STATUS_CODE + '</td><td>'
                    + value.GBASE_PWD_BU_STATUS_CODE + '</td></tr>'
                );
                $('#tblData').append(row);
                hideSpinner();

                //$('#tblData').append("hello world");

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
                    $('#divModalHeader').text("GUIDE User");
                    $('#divModalBody').text("No record found");

                }
                else {
                    $('#divMessageText').text(jQXHR.responseText);
                    $('#divModalHeader').text("GUIDE User");
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

    //$('#spanUsername').text('Hello ' + localStorage.getItem('UserName'));
    $('#spanUsername').text(' Hello ' + sessionStorage.getItem('Username'));
});