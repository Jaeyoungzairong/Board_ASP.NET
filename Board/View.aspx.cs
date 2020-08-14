using Board.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Board.Board
{
    public partial class View : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (String.IsNullOrEmpty(Request["Id"]))
            {
                Response.Write("잘못된 요청입니다.");
                Response.End(); // 현재 페이지 멈추기
            }

            if (!Page.IsPostBack)
            {
                int Id = Convert.ToInt32(Request.QueryString["Id"]);

                var repository = new BoardRepository();
                repository.UpdateReadCount(Id);

                var data = repository.View(Id);

                lbName.Text = data.Name;
                lbTitle.Text = data.Title;
                lbEmail.Text = data.Email;
                tbContent.Text = data.Content;
                lbReadCount.Text = data.ReadCount.ToString();
                lbPostDate.Text = data.PostDate.ToString();

                if (data.ModifyDate > data.PostDate)
                {
                    tbContent.Text += Environment.NewLine + Environment.NewLine;
                    tbContent.Text += data.ModifyDate.ToString() + "에 수정 됨";
                }

                if (data.Name == this.Page.User.Identity.Name)
                {
                    btnModify.Visible = true;
                    btnDelete.Visible = true;
                }
                else
                {
                    btnModify.Visible = false;
                    btnDelete.Visible = false;
                }
            }
        }

        protected void btnModify_Click(object sender, EventArgs e)
        {
            int Id = Convert.ToInt32(Request.QueryString["Id"]);
            Response.Redirect("Modify.aspx?Id="+ Id.ToString());
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int Id = Convert.ToInt32(Request.QueryString["Id"]);

            var repository = new BoardRepository();
            repository.Delete(Id);
            Response.Redirect("LIst.aspx");
        }
    }
}