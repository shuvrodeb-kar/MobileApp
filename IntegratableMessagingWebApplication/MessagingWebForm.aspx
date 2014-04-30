<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MessagingWebForm.aspx.cs" Inherits="IntegratableMessagingWebApplication.MessagingWebForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="scripts/jquery-1.11.0.js"></script>
    <script src="scripts/AjaxUtility.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            setInterval(
                function () {
                    checkMessage();
                }, 5000);
        });

        function checkMessage() {
            AjaxHandler.CallSyncMethod("MessagingWebForm.aspx/GetMessage", {}, successCallbackHandler, callbackError);
        }
        var successCallbackHandler = function (data) {
            if (data.Name != null) {
                var notification = {
                    "Message": data.Name +' says ' +data.Message,
                    "Name": data.Name
                };

                $.ajax({
                    type: "POST",
                    url: "http://api.everlive.com/v1/ZC4pI8IMrNUAkvUX/Push/Notifications",
                    contentType: "application/json",
                    data: JSON.stringify(notification),
                    success: function (data) {
                        //$("#container").append(data.Name, "Message: " + data.Message);
                    },
                    error: function (error) {
                        //$("#container").append(data.Name, "Failed to Send");
                    }
                });
            }

        }
        var callbackError = function () {

        }
    </script>
</head>

<body>
    <form id="form1" runat="server">
        <div style="font-weight: bold">
            Messaging Server is running
        </div>
        <div id="container" style="font-style: italic; font-weight: bold"></div>
    </form>
</body>
</html>
