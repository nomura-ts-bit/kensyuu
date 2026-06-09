using Songapp.Models.Entity;//データベースのエンティティが格納されている名前空間を読み込
using System.ComponentModel.DataAnnotations;//文字数制限などを設定するための機能

namespace Songapp.ViewModel
{
    public class LoginViewModel//ログイン画面やユーザー登録画面のフォームに入力された値を一時的に保持するための箱
    {
        [StringLength(20, ErrorMessage = "ユーザー名は20文字以内で入力してください")]//文字列の長さを最大20文字までに制限
        [Required(ErrorMessage = "ユーザー名は必須です")]
        public string? Username { get; set; }
        [StringLength(20, ErrorMessage = "パスワードは20文字以内で入力してください")]
        [Required(ErrorMessage = "パスワードは必須です")]
        public string? Password { get; set; }//文字列の長さを最大20文字までに制限
    }
}