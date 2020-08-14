<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="List.aspx.cs" 
    Inherits="Board.Board.Documents.List" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <h2 style="text-align:center">게시판 리스트</h2>

    <div style="text-align: right">
        <asp:HyperLink ID="lnkWrite" runat="server" NavigateUrl="~/Board/Write.aspx" 
        CssClass="btn btn-primary" Width="100px">글쓰기
        </asp:HyperLink>
    </div>

    <hr />

    <asp:GridView ID="ctlBoardList" runat="server"
        ItemType="Board.Models.BoardData"
        CssClass="table table-bordered table-hover table-striped"
        HeaderStyle-HorizontalAlign="Center"
        AllowPaging="true"
        PageSize="10"
        AutoGenerateColumns="false"
        OnPageIndexChanging="ctlBoardList_PageIndexChanging">
        <Columns>
            <asp:TemplateField HeaderText="번호"
                HeaderStyle-Width="50px"
                ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <%# Item.Id %>
                </ItemTemplate>
             </asp:TemplateField>
             <asp:TemplateField HeaderText="제목"
                HeaderStyle-Width="350px"
                ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:HyperLink ID="lnkTitle" runat="server" 
                        NavigateUrl=
                        '<%#  "View.aspx?Id=" + Item.Id%>'>
                        <%# Item.Title %>
                    </asp:HyperLink>
                </ItemTemplate>
             </asp:TemplateField>
             <asp:BoundField DataField="Name" HeaderText="작성자"
                HeaderStyle-Width="60px"
                ItemStyle-HorizontalAlign="Center">
             </asp:BoundField>
             <asp:TemplateField HeaderText="작성일"
                HeaderStyle-Width="50px"
                ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <%# Item.PostDate.ToShortDateString() %>
                </ItemTemplate>
             </asp:TemplateField>
             <asp:BoundField DataField="ReadCount" HeaderText="조회수"
                HeaderStyle-Width="60px"
                ItemStyle-HorizontalAlign="Right">
             </asp:BoundField>
        </Columns>   
    </asp:GridView>

</asp:Content>
