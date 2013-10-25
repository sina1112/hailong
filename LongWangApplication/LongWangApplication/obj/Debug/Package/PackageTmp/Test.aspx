<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="LongWangApplication.Test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <%--<asp:button ID="btnInitRateType" Text="初始化捷旅价格类型" runat="server" 
            onclick="btnInitRateType_Click" />
    </div>--%>
    <div>
    入住时间：<asp:TextBox ID="fromDate" runat="server"></asp:TextBox>
    退房时间：<asp:TextBox ID="toDate" runat="server"></asp:TextBox>
    hotelid:<asp:TextBox ID="tbhotelId" runat="server"></asp:TextBox>
   <asp:button ID="btnGetPrice" Text="获取捷旅房价/房态" runat="server" 
        onclick="btnGetPrice_Click" />
    </div>
   <%--  <div>
     <asp:button ID="btnGetCity" Text="初始化Qunar城市数据" runat="server" 
            onclick="btnGetCity_Click" />
    </div>--%>
    </form>
</body>
</html>
