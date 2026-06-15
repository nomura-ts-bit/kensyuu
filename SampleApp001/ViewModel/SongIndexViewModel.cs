namespace Songapp.ViewModel
{
    using System.ComponentModel.DataAnnotations;// 文字数制限などを設定するための機能
    using Songapp.Models.Entity;

    public class SongIndexViewModel// 楽曲一覧画面の検索フォームの入力値と検索結果のデータ一覧をまとめて持ち運ぶための箱
    {
        public IEnumerable<MTSongEntity> Songs { get; set; }// データベースから検索・取得した楽曲データの一覧を格納するためのプロパティ

        [StringLength(200, ErrorMessage = "キーワードは200文字以内で入力してください")]
        public string? Keyword { get; set; }// 画面の検索窓に入力された検索キーワードを保持するプロパティ

    }
}