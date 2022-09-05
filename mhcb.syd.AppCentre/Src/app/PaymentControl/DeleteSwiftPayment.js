$(document).ready(function () {

    var origin = location.origin;
    var dev = 'http://localhost:62001';
    var deploy = location.origin + '/webapi';

    if (sessionStorage.getItem('accessToken') == null) {
        window.location.href = origin + "/Register.html"
    }

    //**************************************************************
    // it is for basic authorization purpose, don't remove please
    //**************************************************************
    //$('#divBasicAuthUsername').load("../commonBasicAuthForm.html");

    if (sessionStorage.getItem('Username') == null) {
        $('#authoriser').val("System");
    } else {
        $('#authoriser').val(sessionStorage.getItem("Username"));
    }

    $('#txtGUsername').val(sessionStorage.getItem("Username"));
    $('#txtGPassword').val(sessionStorage.getItem("Password"));

    $('#btnRefresh').click(function () {

        showSpinner();

        $.ajax({
            type: 'GET',
            url: origin + '//api/SwiftPayment/getSwiftPTransByDate',
            //url: dev + '/api/SwiftPayment/',
            //url: deploy + '/api/SwiftPayment/',
            dataType: 'json',
            data: {
                "optDate": $('#OperationDate').val(),
                "dateType": $('#dateTypes').val()
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

                $.each(result, function (index, value) {
                    var row = $('<tr><td>'
                        + value.PaymentId + '</td><td>'
                        + value.SwiftPaymentId + '</td><td>'
                        + value.GBaseRefNo + '</td><td>'
                        + value.CustomerAbbreviation + '</td><td>'
                        + value.StatusDescription + '</td><td>'
                        + value.Processed + '</td><td>'
                        + value.ValueDate + '</td><td>'

                        + value.PaymentDate + '</td><td>'
                        + value.Currency + '</td><td>'
                        + value.Amount + '</td><td>'
                        + value.SenderBank + '</td><td>'
                        + value.ReceiverBank + '</td><td>'
                        + value.CreatedWhen + '</td></tr>'
                    );
                    $('#tblData').append(row);

                });
                hideSpinner();
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
                    $('#divModalHeader').text("SwiftPayment");
                    $('#divModalBody').text("No record found");
                }
                else {
                    $('#divMessageText').text(jQXHR.responseText);
                    $('#divModalHeader').text("SwiftPayment");
                    $('#divModalBody').text("Internal server error");
                }
                $('#bodyData').empty();
                hideSpinner();

                $('#divMessageBox').addClass('alert-warning');
                // $('#divMessageBox').show('fade');

                $('#divModalFooter').addClass("btn-warning");
                $('#successModal').modal('show');
            }

        });
    });


    // Wired the button to click function
    $('#btnUpdate').click(function () {
        //*******************************
        // get user name and password
        //*******************************
        var username = $('#txtGUsername').val();
        var password = $('#txtGPassword').val();

        $('#linkClose').click(function () {
            $('#divMessageBox').hide('fade');
        });

        $.ajax({
            type: 'DELETE',
            url: origin + '//api/SwiftPayment/',
            dataType: 'json',
            data: {
                PaymentId: "",
                SwiftPaymentId: $('#swiftPaymentId').val(),
                StatusId: "",
                AUTHORIZE_BY: $('#authoriser').val(),
            },
            //data: guideStatus,
            //data: { "id": id, "GUIDEStatus" : guideStatus },
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
                $('#divMessageText').text("Delete Payment Control Record Successfully");
                // $('#divMessageBox').show('fade');

                // and
                $('#divModalFooter').addClass("btn-primary");
                $('#divModalHeader').text("SwiftPayment");
                $('#divModalBody').text("Delete Payment Control Record Successfully");
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
                    $('#divModalHeader').text("SwiftPayment");
                    $('#divModalBody').text("No record found");

                }
                else {
                    $('#divMessageText').text(jQXHR.responseText);
                    $('#divModalHeader').text("SwiftPayment");
                    $('#divModalBody').text("Internal server error");
                }

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