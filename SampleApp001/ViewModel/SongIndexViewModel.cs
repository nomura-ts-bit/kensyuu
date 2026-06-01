namespace Songapp.ViewModel
{
    using System.ComponentModel.DataAnnotations;
    using Songapp.Models.Entity;

    public class SongIndexViewModel
    {
        public IEnumerable<MTSongEntity> Songs { get; set; }

        [StringLength(200, ErrorMessage = "キーワードは200文字以内で入力してください")]
        public string? Keyword { get; set; }

    }
}