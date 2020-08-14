using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Models
{
    public class AccountData
    {
        public static bool LoginStatus = false;

        [Display(Name = "번호")]
        public int No { get; set; }
        [Required(ErrorMessage = "아이디을 입력하세요.")]
        public string Id { get; set; }
        [Required(ErrorMessage = "비밀번호를 입력하세요.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "이름을 입력하세요.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "이메일을 입력하세요.")]
        public string Email { get; set; }
    }
}
