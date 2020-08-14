<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Signup.aspx.cs" Inherits="Board.Signup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2 style="text-align:center">회원가입</h2>

    <hr />

    <asp:Table runat="server" Width="360px" HorizontalAlign="Center">
        <asp:TableRow HorizontalAlign="Center" BackColor="LightGray">
            <asp:TableCell>
                <asp:Label ID="lbAlarm" runat="server" Text="회원정보를 입력해 주세요"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>

    <asp:Table runat="server" Width="360px" HorizontalAlign="Center" CssClass="table table-bordered table-hover table-stripe">
        <asp:TableRow BackColor="LightGray" Height="20%">
            <asp:TableCell>아이디</asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="tbId" runat="server" Width="200px" placeholder="사용자 ID 입력"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow BackColor="LightGray" Height="20%">
            <asp:TableCell>닉네임</asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="tbName" runat="server" Width="200px" placeholder="사용자 닉네임 입력"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow BackColor="LightGray" Height="20%">
            <asp:TableCell>이메일</asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="tbEmail" runat="server" Width="200px" TextMode="Email" placeholder="사용자 이메일 입력"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow BackColor="LightGray" Height="20%">
            <asp:TableCell>비밀번호</asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="tbPassword1" runat="server" Width="200px" TextMode="Password" placeholder="사용자 Password 입력"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow BackColor="LightGray" Height="20%">
            <asp:TableCell>비밀번호 확인</asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="tbPassword2" runat="server" Width="200px" TextMode="Password" placeholder="사용자 Password 입력"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>

    </asp:Table>

    <hr />

    <asp:Table runat="server" Width="360px" HorizontalAlign="Center">
        <asp:TableRow HorizontalAlign="Right">
            <asp:TableCell>
                <asp:LinkButton ID="btnConfirm" runat="server" Width="100px" CssClass="btn btn-primary" OnClick="btnConfirm_Click" >가입</asp:LinkButton>
                &nbsp;
                <asp:LinkButton ID="btnCanel" runat="server" Width="100px" CssClass="btn btn-primary" OnClick="btnCanel_Click">뒤로가기</asp:LinkButton>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
