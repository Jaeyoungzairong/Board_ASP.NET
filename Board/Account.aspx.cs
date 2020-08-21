using Board.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Board
{
    public partial class Account : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(Request["Id"]))
            {
                Response.Write("잘못된 요청입니다.");
                Response.End(); // 현재 페이지 멈추기
            }

            if (!this.Page.User.Identity.IsAuthenticated)
            {
                Response.Write("잘못된 요청입니다.");
                Response.End(); // 현재 페이지 멈추기
            }

            if (!Page.IsPostBack)
            {
                tbId.Enabled = false;
                tbName.Enabled = true;
                tbEmail.Enabled = true;

                tbPassword1.Enabled = false;
                tbPassword2.Enabled = false;
                tblAccount.Rows[4].Visible = false;
                tblAccount.Rows[5].Visible = false;

                btnConfirm.Visible = true;
                btnPassword.Visible = true;
                btnPwCancel.Visible = false;

                string Name = this.Page.User.Identity.Name;

                var repository = new AccountRepository();
                var data = repository.GetAccount(Name);

                tbId.Text = data.Id;
                tbName.Text = data.Name;
                tbEmail.Text = data.Email;

                lbAlarm.Text = "수정할 정보를 입력해 주세요.";
                lbAlarm.ForeColor = Color.Black;
            }
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

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            string Name = this.Page.User.Identity.Name;
            var repository = new AccountRepository();
            var data = repository.GetAccount(Name);

            if (tbPassword1.Enabled && tbPassword2.Enabled)
            {
                bool check = true;
                check &= tbPassword.Text == data.Password ? true : false;
                check &= tbPassword1.Text == tbPassword2.Text ? true : false;
                check &= !String.IsNullOrEmpty(tbPassword1.Text);
                check &= !String.IsNullOrEmpty(tbPassword2.Text);

                if (check)
                {
                    data.Password = tbPassword1.Text;

                    repository.Modify(data);

                    Response.Write(this.Page.User.Identity.Name + " 로그아웃");

                    System.Web.Security.FormsAuthentication.SignOut();

                    //쿠키 해제
                    Response.Cookies["Id"].Expires = DateTime.MinValue;
                    Response.Cookies["Password"].Expires = DateTime.MinValue;
                    Response.Cookies["Email"].Expires = DateTime.MinValue;

                    Response.Redirect("List.aspx");
                }
                else
                {
                    lbAlarm.Text = "비밀번호를 다시 확인해 주세요.";
                    lbAlarm.ForeColor = Color.DarkRed;
                }
            }
            else
            {
                bool OK = true;
                string alarm = string.Empty;

                bool NameChange = false;
                string OldName = string.Empty;

                if (tbPassword.Text != data.Password)
                {
                    OK = false;
                    alarm = "비밀번호가 틀렸습니다.";
                }

                if (data.Name != tbName.Text)
                {
                    bool check = true;
                    check &= repository.CheckName(tbName.Text);
                    check &= !String.IsNullOrEmpty(tbName.Text);

                    if (check)
                    {
                        NameChange = true;
                        OldName = data.Name;
                        data.Name = tbName.Text;
                    }
                    else
                    {
                        OK = false;
                        alarm = "이미 사용 중인 닉네임입니다.";
                    }
                }

                if (!IsValidEmailAddress(tbEmail.Text))
                {
                    OK = false;
                    alarm = "이메일 형식이 잘못되었습니다.";
                }

                if (OK)
                {
                    data.Email = tbEmail.Text;
                    repository.Modify(data);

                    if (NameChange)
                    {
                        var repos = new BoardRepository();
                        repos.ModifyName(OldName, data.Name);
                    }

                    Response.Write(this.Page.User.Identity.Name + " 로그아웃");

                    System.Web.Security.FormsAuthentication.SignOut();

                    //쿠키 해제
                    Response.Cookies["Id"].Expires = DateTime.MinValue;
                    Response.Cookies["Password"].Expires = DateTime.MinValue;
                    Response.Cookies["Email"].Expires = DateTime.MinValue;

                    Response.Redirect("List.aspx");
                }
                else
                {
                    lbAlarm.Text = alarm;
                    lbAlarm.ForeColor = Color.DarkRed;
                }
            }
        }

        protected void btnPassword_Click(object sender, EventArgs e)
        {
            tbId.Enabled = false;
            tbName.Enabled = false;
            tbEmail.Enabled = false;
            tblAccount.Rows[0].Visible = false;
            tblAccount.Rows[1].Visible = false;
            tblAccount.Rows[2].Visible = false;

            tbPassword1.Enabled = true;
            tbPassword2.Enabled = true;
            tblAccount.Rows[4].Visible = true;
            tblAccount.Rows[5].Visible = true;

            btnPassword.Visible = false;
            btnPwCancel.Visible = true;

            lbAlarm.Text = "수정할 비밀번호를 입력해 주세요.";
            lbAlarm.ForeColor = Color.Black;
        }

        protected void btnPwCancel_Click(object sender, EventArgs e)
        {
            tbId.Enabled = false;
            tbName.Enabled = true;
            tbEmail.Enabled = true;
            tblAccount.Rows[0].Visible = true;
            tblAccount.Rows[1].Visible = true;
            tblAccount.Rows[2].Visible = true;

            tbPassword1.Enabled = false;
            tbPassword2.Enabled = false;
            tblAccount.Rows[4].Visible = false;
            tblAccount.Rows[5].Visible = false;

            btnPassword.Visible = true;
            btnPwCancel.Visible = false;

            lbAlarm.Text = "수정할 정보를 입력해 주세요.";
            lbAlarm.ForeColor = Color.Black;
        }

        protected void btnCanel_Click(object sender, EventArgs e)
        {
            Response.Redirect("List.aspx");
        }
    }
}