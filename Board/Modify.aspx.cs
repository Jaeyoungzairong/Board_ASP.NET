using Board.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Board.Board
{
    public partial class Modify : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int Id = Convert.ToInt32(Request.QueryString["Id"]);

                var repository = new BoardRepository();
                var data = repository.View(Id);

                lbName.Text = data.Name;
                tbTitle.Text = data.Title;
                lbEmail.Text = data.Email;
                tbContent.Text = data.Content;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int Id = Convert.ToInt32(Request.QueryString["Id"]);
            

            var repository = new BoardRepository();
            var data = new BoardData();
            data.Id = Id;
            data.Title = tbTitle.Text;
            data.Content = tbContent.Text;
            data.ModifyDate = DateTime.Now;

            repository.Modify(data);

            Response.Redirect("List.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            int Id = Convert.ToInt32(Request.QueryString["Id"]);
            Response.Redirect("View.aspx?Id=" + Id.ToString());
        }
    }
}