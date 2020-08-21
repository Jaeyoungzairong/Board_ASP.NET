using Board.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Board.Board.Documents
{
    public partial class List : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                cbSearch.Items.Clear();
                cbSearch.Items.Add("제목");
                cbSearch.Items.Add("작성자");
                cbSearch.Items.Add("제목+내용");
                DisplayData();

                if (this.Page.User.Identity.IsAuthenticated)
                {
                    lnkWrite.Visible = true;
                }
                else
                {
                    lnkWrite.Visible = false;
                }
            }
        }

        private void DisplayData()
        {
            var repository = new BoardRepository();
            ctlBoardList.DataSource = repository.GetAll();
            ctlBoardList.DataBind();
        }

        protected void ctlBoardList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ctlBoardList.PageIndex = e.NewPageIndex;
            DisplayData();
        }

        protected void ctlBoardList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DateTime PostDate = Convert.ToDateTime(e.Row.Cells[3].Text);
                TimeSpan gap = DateTime.Now - PostDate;
                e.Row.Cells[3].Text = PostDate.ToShortDateString();

                if (gap.TotalMinutes < 30)
                {
                    LiteralControl img = new LiteralControl("<img src='C:/Users/User/Desktop/Code/Board/Board/Resource/new.png' alt='새글' style='margin-left:5px;' />");
                    e.Row.Cells[1].Controls.Add(img);
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(tbSearch.Text))
            {
                string script = "<script type='text/javascript'>alert('검색어를 입력해 주세요.');</script>";
                this.ClientScript.RegisterClientScriptBlock(this.GetType(), "List", script);
            }
            else if (cbSearch.SelectedValue == "제목")
            {
                var repository = new BoardRepository();
                ctlBoardList.DataSource = repository.SearchTitle(tbSearch.Text);
                ctlBoardList.DataBind();
            }
            else if (cbSearch.SelectedValue == "작성자")
            {
                var repository = new BoardRepository();
                ctlBoardList.DataSource = repository.SearchName(tbSearch.Text);
                ctlBoardList.DataBind();
            }
            else if (cbSearch.SelectedValue == "제목+내용")
            {
                var repository = new BoardRepository();
                ctlBoardList.DataSource = repository.SearchTitleAndContent(tbSearch.Text);
                ctlBoardList.DataBind();
            }

        }
    }
}