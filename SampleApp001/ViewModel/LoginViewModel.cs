using Songapp.Models.Entity;
using System.ComponentModel.DataAnnotations;

namespace Songapp.ViewModel
{
    public class LoginViewModel
    {
        [StringLength(20, ErrorMessage = "ユーザー名は20文字以内で入力してください")]
        public string? Username { get; set; }
        [StringLength(20, ErrorMessage = "パスワードは20文字以内で入力してください")]
        public string? Password { get; set; }
    }
}