<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="IntegratableMessagingWebApplication.Test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="scripts/jquery-1.11.0.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //login();
            trigger();
        });            
     
        var trigger = function () {
         

            $.ajax({
                beforeSend: function (xhr) {
                    xhr.setRequestHeader ("Authorization", "Basic YW5hbXNpbW9uOmdvMTI1Mg==");
                },
                crossDomain :true,
            username: "anamsimon",
            password: "go1252",
            type: "POST",
            url: "http://go.sashiimi.com:8153/go/api/pipelines/SashimiApp/schedule",                
            contentType: "application/json",            
            success: function (data) {
                alert(JSON.stringify(data));
            },
            error: function (error) {
                //$("#container").append(data.Name, "Failed to Send");
            }
        });


        }

    </script>
</head>

<body>
    <form id="form1" runat="server">
        <div style="font-weight: bold">
        </div>
        <div id="container" style="font-style: italic; font-weight: bold"></div>
    </form>
</body>
</html>
