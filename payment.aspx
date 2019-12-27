<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="payment.aspx.vb" Inherits="razorvb.payment" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Razorpay .Net Sample App</title>
</head>
<body>
    <form  method="post" runat="server">
 <input type="hidden" value="Hidden Element" name="hidden">
 <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
</form>
</body>
</html>
