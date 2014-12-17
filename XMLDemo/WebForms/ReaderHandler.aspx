<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReaderHandler.aspx.cs" Inherits="XMLDemo.WebForms.ReaderHandler" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>读者操作</title>
    <link href="../Statics/css/Readers.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="item">
        <div class="content">
            <div class="textBox"><span class="title">读者ID</span><asp:TextBox ID="readerId" runat="server" CssClass="box"></asp:TextBox></div>
            <div class="textBox"><span class="title">读者姓名</span><asp:TextBox ID="name" runat="server" CssClass="box"></asp:TextBox></div>
            <div class="textBox"><span class="title">读者类型</span><asp:DropDownList ID="status" runat="server" CssClass="box"></asp:DropDownList></div>
            <div class="textBox"><span class="title">市</span><asp:TextBox ID="city" runat="server" CssClass="box"></asp:TextBox></div>
            <div class="textBox"><span class="title">区</span><asp:TextBox ID="district" runat="server" CssClass="box"></asp:TextBox></div>
            <div class="textBox"><span class="title">街道</span><asp:TextBox ID="street" runat="server" CssClass="box"></asp:TextBox></div>
            <div class="textBox"><span class="title">门牌号</span><asp:TextBox ID="no" runat="server" CssClass="box"></asp:TextBox></div>
            <div class="textBox"><span class="title">电话号码</span><asp:TextBox ID="phone" runat="server" CssClass="box"></asp:TextBox></div>
            <div class="textBox"><span class="title">电子邮件</span><asp:TextBox ID="email" runat="server" CssClass="box"></asp:TextBox></div>
            <div class="button">
                <asp:Button ID="modify" runat="server" Text="修改"  Height="20px" style="margin-top: 5px; margin-left: 5px" Width="70px" OnClick="modify_Click" />
                <asp:Button ID="delete" runat="server" Text="删除"  Height="20px" style="margin-top: 5px; margin-left: 5px" Width="70px" OnClick="delete_Click" />
                <asp:Button ID="add" runat="server" Text="添加"  Height="20px" style="margin-top: 5px; margin-left: 5px" Width="70px" OnClick="add_Click" />
            </div>   
            <div class="button">
                <asp:Button ID="pre" runat="server" Text="上一项"  Height="20px" style="margin-top: 5px; margin-left: 5px" Width="70px" OnClick="pre_Click" />
                <asp:Button ID="next" runat="server" Text="下一项" Height="20px" style="margin-top: 5px; margin-left: 5px" Width="70px" OnClick="next_Click" />
            </div>
        </div>
    </div>
    </form>
</body>
</html>
