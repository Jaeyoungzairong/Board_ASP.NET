using Board.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Board
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lbAlarm.Text = "회원정보를 입력해주세요.";
            lbAlarm.ForeColor = Color.Black;
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            var data = new AccountData();
            data.Id = tbId.Text;
            data.Password = tbPassword.Text;

            var repository = new AccountRepository();
            data = repository.Login(data);

            if (data == null)
            {
                lbAlarm.Text = "로그인 실패하였습니다.";
                lbAlarm.ForeColor = Color.DarkRed;
                //string script = "<script type='text/javascript'>alert('로그인 실패하였습니다.');</script>";
                //this.ClientScript.RegisterClientScriptBlock(this.GetType(), "login", script);
            }
            else
            {
                lbAlarm.Text = "로그인 성공하였습니다.";
                lbAlarm.ForeColor = Color.Black;

                FormsAuthentication.SetAuthCookie(data.Name, false);

                Response.Cookies["Id"].Value = data.Id;
                Response.Cookies["Password"].Value = data.Password;
                Response.Cookies["Email"].Value = data.Email;

                Response.Redirect("List.aspx");
            }
        }

        protected void btnSignup_Click(object sender, EventArgs e)
        {
            Response.Redirect("Signup.aspx");
        }
    }
}