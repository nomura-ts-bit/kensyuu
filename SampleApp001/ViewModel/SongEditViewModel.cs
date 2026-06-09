namespace Songapp.ViewModel
{
    using System.ComponentModel.DataAnnotations;//文字数制限などを設定するための機能

    public class SongEditViewModel//編集画面の入力内容を丸ごと保持して持ち運ぶための箱
    {
        /// <summary>
        /// 楽曲のIDを取得または設定します.
        /// </summary>
        public int Id { get; set; }//編集対象となる楽曲の識別番号（ID）を保持するプロパティ

        [StringLength(50, ErrorMessage = "曲名は50文字以内で入力してください")]
        [Required(ErrorMessage = "曲名は必須です")]
        public string? Edit_Song { get; set; }

        [StringLength(20, ErrorMessage = "グループ名は20文字以内で入力してください")]
        [Required(ErrorMessage = "グループ名は必須です")]
        public string? Edit_Group { get; set; }

        [StringLength(20, ErrorMessage = "作曲者名は20文字以内で入力してください")]
        public string? Edit_Artist { get; set; }

        [Range(0, 9999, ErrorMessage = "年代は4桁以内で入力してください")]
        public int? Edit_History { get; set; }

        [StringLength(1000, ErrorMessage = "曲名は1000文字以内で入力してください")]
        public string? Edit_Lyric { get; set; }
    }
}