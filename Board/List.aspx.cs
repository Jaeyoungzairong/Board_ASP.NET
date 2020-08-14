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
    }
}