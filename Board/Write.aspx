<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Write.aspx.cs" Inherits="Board.Board.Write" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2 style="text-align:center">게시판 글쓰기</h2>

    <hr />

    <asp:Table runat="server" Width="600px" HorizontalAlign="Center">
        <asp:TableRow>
            <asp:TableCell Width="100px" HorizontalAlign="Center" BorderStyle="Double" BackColor="LightGray">
                이름
            </asp:TableCell>
            <asp:TableCell Width="500px" BorderStyle="Double" BorderColor="Gray">
                <asp:Label ID="lbName" runat="server"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell Width="100px" HorizontalAlign="Center" BorderStyle="Double" BackColor="LightGray">
                이메일
            </asp:TableCell>
            <asp:TableCell Width="500px" BorderStyle="Double" BorderColor="Gray">
                <asp:Label ID="lbEmail" runat="server"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell Width="100px" HorizontalAlign="Center" BorderStyle="Double" BackColor="LightGray">
                제목
            </asp:TableCell>
            <asp:TableCell Width="500px" BorderStyle="Double" BorderColor="Gray">
                <asp:TextBox ID="tbTitle" runat="server" Width="500px"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell Width="100px" Height="300px" HorizontalAlign="Center" BorderStyle="Double" BackColor="LightGray">
                내용
            </asp:TableCell>
            <asp:TableCell Width="500px" Height="300px" BorderStyle="Double" BorderColor="Gray">
                <asp:TextBox ID="tbContent" runat="server" Width="500px" Height="300px" TextMode="MultiLine"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>

    <hr />

    <asp:Table runat="server" Width="600px" HorizontalAlign="Center">
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Right">
                <asp:LinkButton ID="btnSave" runat="server" Width="100px" CssClass="btn btn-primary" OnClick="btnSave_Click">올리기</asp:LinkButton>
                &nbsp;
                <asp:LinkButton ID="btnCancel" runat="server" Width="100px" CssClass="btn btn-primary" OnClick="btnCancel_Click">취소</asp:LinkButton>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>

</asp:Content>
