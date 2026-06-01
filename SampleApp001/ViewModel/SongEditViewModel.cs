namespace Songapp.ViewModel
{
    using System.ComponentModel.DataAnnotations;

    public class SongEditViewModel
    {
        /// <summary>
        /// 楽曲のIDを取得または設定します.
        /// </summary>
        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "曲名は50文字以内で入力してください")]
        public string? Edit_Song { get; set; }

        [StringLength(20, ErrorMessage = "グループ名は20文字以内で入力してください")]
        public string? Edit_Group { get; set; }

        [StringLength(20, ErrorMessage = "作曲者名は20文字以内で入力してください")]
        public string? Edit_Artist { get; set; }

        [Range(0, 9999, ErrorMessage = "年代は4桁以内で入力してください")]
        public int? Edit_History { get; set; }

        [StringLength(1000, ErrorMessage = "曲名は1000文字以内で入力してください")]
        public string? Edit_Lyric { get; set; }
    }
}