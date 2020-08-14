using Board.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
                tbName.Enabled = false;
                tbEmail.Enabled = false;


                tbPassword1.Enabled = false;
                tbPassword2.Enabled = false;
                tblAccount.Rows[4].Visible = false;
                tblAccount.Rows[5].Visible = false;

                string Name = this.Page.User.Identity.Name;

                var repository = new AccountRepository();
                var data = repository.GetAccount(Name);

                tbId.Text = data.Id;
                tbName.Text = data.Name;
                tbEmail.Text = data.Email;
            }
        }

        protected void btnName_Click(object sender, EventArgs e)
        {
            tbName.Enabled = true;
        }

        protected void btnEmail_Click(object sender, EventArgs e)
        {
            tbEmail.Enabled = true;
        }

        protected void btnPassword_Click(object sender, EventArgs e)
        {
            tbPassword1.Enabled = true;
            tbPassword2.Enabled = true;
            tblAccount.Rows[4].Visible = true;
            tblAccount.Rows[5].Visible = true;
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            bool OK = true;
            string alarm = string.Empty;
            string Name = this.Page.User.Identity.Name;

            var repository = new AccountRepository();
            var data = repository.GetAccount(Name);

            if (tbPassword.Text != data.Password)
            {
                OK = false;
                alarm = "비밀번호가 틀렸습니다.";
            }

            if (tbPassword1.Enabled || tbPassword2.Enabled)
            {
                bool check = true;
                check &= tbPassword.Text == data.Password ? true : false;
                check &= tbPassword1.Text == tbPassword2.Text ? true : false;
                check &= !String.IsNullOrEmpty(tbPassword1.Text);
                check &= !String.IsNullOrEmpty(tbPassword2.Text);

                if (check)
                {
                    data.Password = tbPassword1.Text;
                }
                else
                {
                    OK = false;
                    alarm = "새 비밀번호가 틀렸습니다.";
                }
                
            }

            if (data.Name != tbName.Text)
            {
                if (!repository.CheckAccount(data, true))
                {
                    OK = false;
                    alarm = "이미 있는 닉네임입니다.";
                }
            }

            if (!IsValidEmailAddress(tbEmail.Text))
            {
                OK = false;
                alarm = "이메일 형식이 잘못되었습니다.";
            }

            if (OK)
            {
                data.Name = tbName.Text;
                data.Email = tbEmail.Text;

                repository.Modify(data);
            }
            else
            {
                lbAlarm.Text = alarm;
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
    }
}