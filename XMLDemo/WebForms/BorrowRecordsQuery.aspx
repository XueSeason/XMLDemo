﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BorrowRecordsQuery.aspx.cs" Inherits="XMLDemo.WebForms.BorrowRecordsQuery" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>相关查询</title>
    <link href="../Statics/css/Query.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="searchPanel">
        <div class="search">
            <div class="readerId">
                <span>读者借阅查询</span>
                <asp:TextBox ID="readerText" runat="server"></asp:TextBox>
                <asp:Button ID="readerQuery" runat="server" Text="查询" OnClick="readerQuery_Click" />
                <span>（请输入相应的编号）</span>
            </div>
            <div class="bookId">
                <span>图书借阅查询</span>
                <asp:TextBox ID="bookText" runat="server"></asp:TextBox>
                <asp:Button ID="bookQuery" runat="server" Text="查询" OnClick="bookQuery_Click" style="height: 21px" />
                <span>（请输入相应的编号）</span>
            </div>
        </div>
    </div>       
    </form>
</body>
</html>
