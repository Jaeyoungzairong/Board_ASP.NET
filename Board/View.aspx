<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="View.aspx.cs" Inherits="Board.Board.View" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2 style="text-align:center">게시판 뷰</h2>

    <asp:Table runat="server" Width="600px" HorizontalAlign="Center">
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Right">
                <asp:HyperLink ID="lnkBack" runat="server" NavigateUrl="~/Board/List.aspx" 
                CssClass="btn btn-primary" Width="100px">뒤로가기
                </asp:HyperLink>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>

    <hr />

    <asp:Table runat="server" HorizontalAlign="Center">
        <asp:TableRow>
            <asp:TableCell Width="100px" HorizontalAlign="Center" BorderStyle="Double" BackColor="LightGray">
                작성자
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
                작성일
            </asp:TableCell>
            <asp:TableCell Width="500px" BorderStyle="Double" BorderColor="Gray">
                <asp:Label ID="lbPostDate" runat="server"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell Width="100px" HorizontalAlign="Center" BorderStyle="Double" BackColor="LightGray">
                조회수
            </asp:TableCell>
            <asp:TableCell Width="500px" BorderStyle="Double" BorderColor="Gray">
                <asp:Label ID="lbReadCount" runat="server"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell Width="100px" HorizontalAlign="Center" BorderStyle="Double" BackColor="LightGray">
                제목
            </asp:TableCell>
            <asp:TableCell Width="500px" BorderStyle="Double" BorderColor="Gray">
                <asp:Label ID="lbTitle" runat="server"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell Width="100px" HorizontalAlign="Center" BorderStyle="Double" BackColor="LightGray">
                내용
            </asp:TableCell>
            <asp:TableCell Width="500px" BorderStyle="Double" BorderColor="Gray">
                <asp:TextBox ID="tbContent" runat="server" Width="100%" Height="300px" TextMode="MultiLine" ReadOnly="true"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>

    <hr />

    <asp:Table runat="server" Width="600px" HorizontalAlign="Center">
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Right">
                <asp:LinkButton ID="btnModify" runat="server" Width="100px" CssClass="btn btn-primary" OnClick="btnModify_Click">수정</asp:LinkButton>
                &nbsp;
                <asp:LinkButton ID="btnDelete" runat="server" Width="100px" CssClass="btn btn-primary" 
                    OnClientClick="return confirm('글을 정말로 삭제하시겠습니까?');" OnClick="btnDelete_Click">
                    삭제
                </asp:LinkButton>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>

</asp:Content>
