﻿$(document).ready(function () {

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

    var auth = sessionStorage.getItem("Authorisers");
    if (!auth.includes("BIF - Authorise")) {
        $('#btnUpdate').prop('disabled', true);
        alert("Sorry, you don't have BIF authorisd permission");
    }

    $('#btnRefresh').click(function () {

        $.ajax({
            type: 'GET',
            url: origin + '//api/BankInf/getPendingBIF',
            //url: dev + '/api/BankInf/Pending',
            //url: deploy + '/api/BankInf/Pending',
            dataType: 'json',
            data: {},
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
                        + value.SWIFT_ID + '</td><td>'
                        + value.BRANCH_NO + '</td><td>'
                        + value.ACRONYM + '</td><td>'
                        + value.NAME_ADDR_1 + '</td><td>'
                        + value.NAME_ADDR_2 + '</td><td>'
                        + value.COR_BANK_CD + '</td><td>'
                        + value.CCY + '</td><td>'
                        + value.MM_SETTL_BANK_ID + '</td><td>'
                        + value.STATUS + '</td><td>'
                        + value.DELETION_STATUS + '</td><td>'
                        + value.GIP_STATUS + '</td><td>'
                        + value.GIP_DESCRIPTION + '</td><td>'
                        + value.AUTHORIZE_BY + '</td><td>'
                        + value.TRAN_NO + '</td></tr>'
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
                    $('#divModalHeader').text("Bank Information");
                    $('#divModalBody').text("No record found");
                }
                else {
                    $('#divMessageText').text(jQXHR.responseText);
                    $('#divModalHeader').text("Bank Information");
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

        if ($('#swiftId').val() == '') {
            $('#divMessageBox').addClass('alert-warning');
            $('#divMessageText').text("Please enter Swift ID");
           // $('#divMessageBox').show('fade');
            return false;
        }
        if ($('#tranNo').val() == '') {
            $('#divMessageBox').addClass('alert-warning');
            $('#divMessageText').text("Please enter Transaction No.");
           // $('#divMessageBox').show('fade');
            return false;
        }

        //$('#linkClose').click(function () {
        //	$('#divMessageBox').hide('fade');
        //});

        $.ajax({
            type: 'PUT',
            url: origin + '//api/BankInf/',
            dataType: 'json',
            data: {
                SWIFT_ID: $('#swiftId').val(),
                AUTHORIZE_BY: $('#authoriser').val(),
                STATUS: $('#status').val(),
                DELETION_STATUS: $('#deletionStatus').val(),
                GIP_STATUS: $('#gipStatus').val(),
                GIP_DESCRIPTION: $('#gipDescription').val(),
                TRAN_NO: $('#tranNo').val()
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
                $('#divModalHeader').text("Bank Information");
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
                    $('#divModalHeader').text("Bank Information");
                    $('#divModalBody').text("It's not your Bank Information!");
                }
                else {
                    $('#divMessageText').text(jQXHR.responseText);
                    $('#divModalHeader').text("Bank Information");
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

    $("#deletionStatus").click(function () {
        if ($("#deletionStatus").val() == "D") {
            $('#gipStatus').val("8");
        } else {
            $('#gipStatus').val("4");
        }
    });

});