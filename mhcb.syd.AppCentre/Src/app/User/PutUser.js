$(document).ready(function () {

    var origin = location.origin;
    var dev = 'http://localhost:62001';
    var deploy = location.origin + '/webapi';

    if (sessionStorage.getItem('accessToken') == null) {
        window.location.href = origin + "/Register.html"
    }

    //$('#divBasicAuthUsername').load("../commonBasicAuthForm.html");

    var username = sessionStorage.getItem("Username");
    var password = sessionStorage.getItem("Userpassword")

    // test
    //alert(sessionStorage.getItem("Username"));
    //alert(sessionStorage.getItem("Userpassword"));
    //alert(username);
    //alert(password);

    //$('#txtUserId').val(sessionStorage.getItem("Username"));
    //$('#txtUserPassword').val(sessionStorage.getItem("Userpassword"));
    $('#txtUserId').val(username);
    $('#txtUserPassword').val(password);


    //if (sessionStorage.getItem('Username') == null) {
    //    $('#txtUserId').val("System");
    //} else {
    //    $('#txtUserId').val(sessionStorage.getItem("Username"));
    //}

    // Wired the button to click function
    $('#btnUpdate').click(function () {
        //*******************************
        // get user name and password
        //*******************************
        //var username = $('#txtUserId').val();
        //var password = $('#txtUserPassword').val();

        //$('#linkClose').click(function () {
        //    $('#divMessageBox').hide('fade');
        //});

        // test
        //alert($('#txtUserId').val());
        //alert($('#txtUserPassword').val());

        $.ajax({
            type: 'PUT',
            url: origin + '//api/User/',
            //url: dev + '/api/User/',
            //url: deploy + '/api/User/',
            dataType: 'json',
            data: {
                "USER_ID": username,
                "PASSWORD": password
            },
            //*****************************************************************************
            // Specify the authentication header btoa() method encodes a string to Base64
            //*****************************************************************************
            headers: {
                //'Authorization': 'Basic ' + btoa(username + ':' + password)
                'Authorization': 'Bearer ' + sessionStorage.getItem("accessToken")
            },

            success: function (result) {
                // or
                //alert("saved");
                $('#DivSpanUsername').removeClass('hidden');

                // or
                $('#divMessageBox').addClass('alert-success');
                $('#divMessageText').text("Updated Successfully");
               // $('#divMessageBox').show('fade');

                // and
                $('#divModalFooter').addClass("btn-primary");
                $('#divModalHeader').text("GUIDE User");
                $('#divModalBody').text("Updated Successfully");
                $('#successModal').modal('show');
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

                $('#divMessageBox').addClass('alert-warning');
               // $('#divMessageBox').show('fade');

                $('#divModalFooter').addClass("btn-warning");
                $('#successModal').modal('show');
            }

        });
    });

    // Wired the button to click function
    $('#btnBack').click(function () {
        //sessionStorage.removeItem('accessToken');
        window.location.href = origin + "/HOME";
    });

    // whenever error happen, it redirect to login page
    $('#linkClose').click(function () {
        $('#divMessageBox').hide('fade');
    });

    // whenever error happen, it redirect to login page
    $('#errorModal').on('hidden.bs.modal', function () {
        window.location.href = origin + "/HOME";
    });

    //$('#spanUsername').text('Hello ' + localStorage.getItem('UserName'));
    $('#spanUsername').text(' Hello ' + sessionStorage.getItem('Username'));

});