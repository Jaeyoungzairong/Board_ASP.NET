using Board.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Board.Board
{
    public partial class Write : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string Name = this.Page.User.Identity.Name;
                lbName.Text = Name;

                var repository = new AccountRepository();
                var data = repository.GetAccount(Name);

                lbName.Text = data.Name;
                lbEmail.Text = data.Email;
            }            
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            var data = new BoardData();
            data.Name = lbName.Text;
            data.Email = lbEmail.Text;
            data.Title = tbTitle.Text;
            data.Content = tbContent.Text;
            data.ReadCount = 0;

            DateTime time = DateTime.Now;
            data.PostDate = time;
            data.ModifyDate = time;

            //data.Password = "Password";
            //data.PostIp = "PostIp";
            //data.ModifyIp = "ModifyIp";
            //data.Encoding = "Encoding";
            //data.Homepage = "Homepage";

            if (data.Title.Length > 0)
            {
                var repository = new BoardRepository();
                repository.Add(data);

                Response.Redirect("List.aspx");
            }
            else
            {
                string script = "<script type='text/javascript'>alert('제목을 입력해 주세요.');</script>";
                this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Write", script);
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("List.aspx");
        }

    }
}