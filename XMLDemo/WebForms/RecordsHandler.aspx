<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RecordsHandler.aspx.cs" Inherits="XMLDemo.WebForms.RecordsHandler" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>借阅操作</title>
    <link href="../Statics/css/Records.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="item">
        <div class="content">
            <div class="textBox"><span class="title">借阅编号</span><asp:TextBox ID="recordId" runat="server" CssClass="box"></asp:TextBox></div>
            <div class="textBox"><span class="title">读者编号</span><asp:DropDownList ID="readerId" runat="server" CssClass="box"></asp:DropDownList></div>
            <div class="textBox"><span class="title">图书编号</span><asp:DropDownList ID="bookId" runat="server" CssClass="box"></asp:DropDownList></div>
            <div class="textBox"><span class="title">借书日期</span><asp:TextBox ID="borrowDate" runat="server" CssClass="box"></asp:TextBox></div>
            <div class="textBox"><span class="title">还书日期</span><asp:TextBox ID="returnDate" runat="server" CssClass="box"></asp:TextBox></div>
            <div class="textBox"><span class="title">备注信息</span><asp:TextBox ID="remark" runat="server" CssClass="box" TextMode="MultiLine"></asp:TextBox></div>
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
