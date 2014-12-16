<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BooksHandler.aspx.cs" Inherits="XMLDemo.网页窗口.BooksHandler" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>图书操作</title>
    <link href="../Statics/css/Books.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="item">
        <div class="image"><img src="../Statics/images/追风筝的人.jpg" id="image" runat="server"/></div> 
        <div class="content">
            <div class="textBox"><span class="title">ISBN</span><asp:TextBox ID="isbn" runat="server" CssClass="box"></asp:TextBox></div>
            <div class="textBox"><span class="title">标题</span><asp:TextBox ID="title" runat="server" CssClass="box"></asp:TextBox></div>
            <div class="textBox"><span class="title">作者</span><asp:TextBox ID="author" runat="server" CssClass="box"></asp:TextBox></div>
            <div class="textBox"><span class="title">分类</span><asp:DropDownList ID="series" runat="server" CssClass="box"></asp:DropDownList></div>
            <div class="textBox"><span class="title">出版社</span><asp:TextBox ID="publisher" runat="server" CssClass="box"></asp:TextBox></div>
            <div class="textBox"><span class="title">出版年</span><asp:TextBox ID="publishYear" runat="server" CssClass="box"></asp:TextBox></div>
            <div class="textBox"><span class="title">价格</span><asp:TextBox ID="price" runat="server" CssClass="box"></asp:TextBox></div>
            <div class="button">
                <asp:Button ID="modify" runat="server" Text="修改"  Height="20px" style="margin-top: 5px" Width="70px" OnClick="modify_Click" />
                <asp:Button ID="delete" runat="server" Text="删除"  Height="20px" style="margin-top: 5px" Width="70px" OnClick="delete_Click" />
                <asp:Button ID="add" runat="server" Text="添加"  Height="20px" style="margin-top: 5px" Width="70px" OnClick="add_Click" />
            </div>   
            <div class="button">
                <asp:Button ID="pre" runat="server" Text="上一项"  Height="20px" style="margin-top: 5px" Width="70px" OnClick="pre_Click" />
                <asp:Button ID="next" runat="server" Text="下一项" Height="20px" style="margin-top: 5px" Width="70px" OnClick="next_Click" />
                <asp:FileUpload ID="uploadImg" Height="20px" style="margin-top: 5px" Width="70px" runat="server" />
            </div>
        </div>
    </div>
    </form>
</body>
</html>
