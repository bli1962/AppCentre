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
            url: origin + '//api/MoneyMarketTicket/getMMGID',
            //url: dev + '/api/MoneyMarketTicket/Pending',
            //url: deploy + '/api/MoneyMarketTicket/Pending',
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
                        + value.TicketId + '</td><td>'
                        + value.REF_NO + '</td><td>'
                        + value.Counterparty + '</td><td>'
                        + value.TradeId + '</td><td>'
                        + value.Event + '</td><td>'
                        + value.Typology + '</td><td>'
                        + value.PortfolioGroup + '</td><td>'
                        + value.PortfolioDesk + '</td><td>'
                        + value.PortfolioBook + '</td><td>'
                        + value.Currency + '</td><td>'
                        + value.PrincipalAmount + '</td><td>'
                        + value.InputBy + '</td><td>'
                        + value.AuthorisedBy + '</td><td>'
                        + value.TradeDate + '</td><td>'
                        + value.PrintDateTime + '</td><td>'
                        + value.PrintStatus + '</td><td>'
                        + value.GIDUpload + '</td>'
                        + '</tr>'
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
                    $('#divModalHeader').text("MoneyMarket Transaction");
                    $('#divModalBody').text("No record found");
                }
                else {
                    $('#divMessageText').text(jQXHR.responseText);
                    $('#divModalHeader').text("MoneyMarket Transaction");
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
        if ($('#txtMMTicketID').val() == '') {
            $('#divMessageBox').addClass('alert-warning');
            $('#divMessageText').text("Please enter MM Ticket ID");
           // $('#divMessageBox').show('fade');
            return false;
        }

        $.ajax({
            type: 'PUT',
            url: origin + '//api/MoneyMarketTicket/',
            dataType: 'json',
            data: {
                TicketId: $('#txtMMTicketID').val(),
                optDate: $('#OperationDate').val(),
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
                $('#divMessageText').text("Updated Successfully");
               // $('#divMessageBox').show('fade');

                // and
                $('#divModalFooter').addClass("btn-primary");
                $('#divModalHeader').text("MoneyMarket Transaction GID Upload");
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
                    $('#divModalHeader').text("MoneyMarket Transaction GID Upload");
                    $('#divModalBody').text("No record found");

                }
                else {
                    $('#divMessageText').text(jQXHR.responseText);
                    $('#divModalHeader').text("MoneyMarket Transaction GID Upload");
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