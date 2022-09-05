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

        $.ajax({
            type: 'GET',
            url: origin + '//api/GAS/GetGASTransByDate',
            //url: dev + '/api/EUCBalanceEvent/Pending',
            //url: deploy + '/api/EUCBalanceEvent/Pending',
            dataType: 'json',
            data: {
                "optDate": $('#ValueDate').val()
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
                        + value.MIN_NO + '</td><td>'
                        + value.VALUEDATE_DT + '</td><td>'
                        + value.STATUS_INT + '</td><td>'
                        + value.DREAMS_REF_CODE + '</td><td>'
                        + value.REMARKS_TX + '</td><td>'
                        + value.CCY_TX + '</td><td>'
                        + value.AMOUNT_AMT + '</td><td>'
                        + value.CUSTID_TX + '</td><td>'
                        + value.ACC_CD_TX + '</td><td>'
                        + value.ACCNUM_TX + '</td><td>'
                        + value.DESCRIPTION_TX + '</td><td>'
                        + value.CREATED_BY + '</td><td>'
                        + value.CREATED_DATE + '</td><td>'
                        + value.UPLOADED_BY + '</td><td>'
                        + value.UPLOADED_DATE + '</td><td>'
                        + value.TRANSNO_TX                     
                        + '</td></tr>'
                    );

                    $('#tblData').append(row);
                });
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
                    $('#divModalHeader').text("GAS Transation");
                    $('#divModalBody').text("No GAS Transation found");

                }
                else {
                    $('#divMessageText').text(jQXHR.responseText);
                    $('#divModalHeader').text("GAS Transation");
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
    $('#btnUpdate').click(function () {
        //*******************************
        // get user name and password
        //*******************************
        var username = $('#txtGUsername').val();
        var password = $('#txtGPassword').val();

        //$('#linkClose').click(function () {
        //	$('#divMessageBox').hide('fade');
        //});

        if ($('#refNo').val() == '') {
            $('#divMessageBox').addClass('alert-warning');
            $('#divMessageText').text("Please enter Gbase Reference No");
           // $('#divMessageBox').show('fade');
            return false;
        }

        $.ajax({
            type: 'PUT',
            url: origin + '//api/GAS/',
            dataType: 'json',
            data: {
                Min_No: $('#txtMinNo').val(),             
                Authorize_By: $('#authoriser').val(),
                valueDate: $('#ValueDate').val(),
                HasDCSPayment: $('#txtDCSPayment').val()
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
                $('#divMessageText').text("Updated Successfully");
               // $('#divMessageBox').show('fade');

                // and
                $('#divModalFooter').addClass("btn-primary");
                $('#divModalHeader').text("GAS Transation");
                $('#divModalBody').text("GAS Transation Updated Successfully");
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
                    $('#divModalHeader').text("GAS Transation");
                    $('#divModalBody').text("No GAS Transation Updated");

                }
                else {
                    $('#divMessageText').text(jQXHR.responseText);
                    $('#divModalHeader').text("GAS Transation");
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