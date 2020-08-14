using Board.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Board
{
    public partial class Signup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lbAlarm.Text = "회원정보를 입력해주세요.";
            lbAlarm.ForeColor = Color.Black;
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            if (tbPassword1.Text == tbPassword2.Text)
            {
                if (String.IsNullOrEmpty(tbId.Text) || String.IsNullOrEmpty(tbName.Text) || String.IsNullOrEmpty(tbPassword1.Text))
                {
                    lbAlarm.Text = "비어있는 회원정보를 입력해주세요.";
                    lbAlarm.ForeColor = Color.DarkRed;
                }
                else
                {
                    if (IsValidEmailAddress(tbEmail.Text))
                    {
                        var data = new AccountData();
                        data.Id = tbId.Text;
                        data.Email = tbEmail.Text;
                        data.Name = tbName.Text;
                        data.Password = tbPassword1.Text;

                        var repository = new AccountRepository();
                        bool check = repository.CheckAccount(data, false);
                        if (check)
                        {
                            lbAlarm.Text = "회원가입에 성공하였습니다.";
                            lbAlarm.ForeColor = Color.Black;

                            data = repository.Add(data);
                            FormsAuthentication.SetAuthCookie(data.Name, false);

                            Response.Cookies["Id"].Value = data.Id;
                            Response.Cookies["Password"].Value = data.Password;
                            Response.Cookies["Email"].Value = data.Email;

                            Response.Redirect("List.aspx");
                        }
                        else
                        {
                            lbAlarm.Text = "일치하는 회원정보가 있습니다.";
                            lbAlarm.ForeColor = Color.DarkRed;
                        }
                    }
                    else
                    {
                        lbAlarm.Text = "이메일 형식이 맞지 않습니다.";
                        lbAlarm.ForeColor = Color.DarkRed;
                    }
                }
            }
            else
            {
                lbAlarm.Text = "비밀정보가 일치하지 않습니다.";
                lbAlarm.ForeColor = Color.DarkRed;
                //string script = "<script type='text/javascript'>alert('비밀번호가 일치하지 않습니다.');</script>";
                //this.ClientScript.RegisterClientScriptBlock(this.GetType(), "login", script);
            }
        }

        protected void btnCanel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        public bool IsValidEmailAddress(string email)
        {
            if (!string.IsNullOrEmpty(email) && new EmailAddressAttribute().IsValid(email))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}