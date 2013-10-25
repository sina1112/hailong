<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="LongWangApplication.Manager.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Resource/Css/style.css" rel="stylesheet" type="text/css" />
    <script src="../Resource/Js/jquery-1.7.1.min.js" type="text/javascript"></script>
    <script src="../Resource/Js/function.js" type="text/javascript"></script>
    <script src="../Resource/Js/operamasks-ui.min.js" type="text/javascript"></script>
    <link href="../Resource/Css/operamasks-ui/elegant/om-elegant.css" rel="stylesheet"
        type="text/css" />
    <style type="text/css">
        table
        {
            empty-cells: show;
            border-collapse: collapse;
        }
    </style>
    <script type="text/javascript">
     var handlerUrl = "../Ashx/ManagerHandler.ashx";
     $(function () {
         $(document).keydown(function (e) {
             var ev = document.all ? window.event : e;
             if (ev.keyCode == 13) {
                 $("#loginsubmit").click();
             }
         });
         $("#loginsubmit").click(function loginFn() {
             var userName = $("#txtUserName").val();
             var passWord = $("#txtUserPwd").val();
             if (userName == "") {
                 $(".tipbox").html("提示：请输入用户名！");
                 return false;
             }
             if (passWord == "") {
                 $(".tipbox").html("提示：请输入密码！");
                 return false;
             }
             $.ajax({ url: handlerUrl, data: { action: "userLogin", userName: userName, passWord: passWord }, cache: false, type: "POST",
                 success: function (data) {
                     data = $.parseJSON(data);
                     if (!data.IsError) {
                         if (data.Result == true)
                             window.location.href = "Index.aspx";
                     } else {
                         $(".tipbox").html("提示：" + data.Result);
                         $("#txtValid").val("");
                     }
                 }
             });


         });
         $("#txtUserName").focus();

         $(':button').omButton({});
     });
    </script>
</head>
<body>
    
    <div style="width:800px;margin:auto; padding-top:150px">
        <table style="width: 70%; ">
            <tr>
                <td style="height:30px; text-align:right;">
                    用户名：
                </td>
                <td style="width:200px">
                    <input type="text" id="txtUserName" class="login_input" />
                </td>
            </tr>
            <tr>
                <td style="height:30px;text-align:right;">
                    密  码：
                </td>
                <td>
                    <input type="password" id="txtUserPwd" class="login_input" />
                </td>
            </tr>
            <tr>
            <td>&nbsp;</td>
                <td colspan="2" class="tipbox" >
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                  <td  style="text-align:center" >
                        <button id="loginsubmit" type="button"   class="btn-2letter">
                登 录</button>
                  </td>
            </tr>
        </table>
    </div>
</body>
</html>
