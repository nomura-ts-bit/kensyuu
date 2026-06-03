// ========================================================================
// <copyright file="MTProductEntity.cs" company="SUS">
//     (c) 株式会社エスユーエス All Rights Reserved. 
// </copyright>
// -----------------------------------------------------------------------
// システム名：研修Webサイト
// プログラム名：商品マスタエンティティクラス
// 作成日：2026/02/26
// 更新日：
// ========================================================================
namespace Songapp.Models.Entity // ネームスペースを定義(住所)
{

    using System.ComponentModel.DataAnnotations; // 画面からの入力ルール（[Required]で必須、[StringLength]で文字数制限など）を設定する道具を引用
    using System.ComponentModel.DataAnnotations.Schema; // データベースのテーブル構造（[Table]でテーブル名指定、[ForeignKey]で外部キー設定など）をカスタマイズする道具を引用

    /// <summary>
    /// 商品マスタエンティティクラス
    /// </summary>
    [Table("mt_login")]     // DBのテーブル指定
    [Serializable]          // データを保存できる形式に変換
    public class MTLoginEntity
    {
        /// <summary>
        /// 主キー
        /// </summary>
        [Key]       // 主キー設定
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   // 自動採番(番号振り)
        [Column("id")]      // DBのカラム(列)の指定
        public int Id { get; set; } = 0;

        /// <summary>
        /// ユーザー名
        /// </summary>
        [Display(Name = "ユーザー名")]       // 画面表示
        [Column("username")]    // DBのカラム(列)の指定
		[Required]      // 必須入力にする
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// パスワード
        /// </summary>
        [Display(Name = "パスワード")]       // 画面表示
		[Column("password")]    // DBのカラム(列)の指定
		[Required]      // 必須入力にする
		public string Password { get; set; } = string.Empty;

        /// <summary>
        /// 削除フラグ
        /// </summary>
        [Display(Name = "削除フラグ")]       // 画面表示
		[Column("is_deleted")]      // DBのカラム(列)の指定
		[Required]      // 必須入力にする
		public int IsDeleted { get; set; } = 0;

        /// <summary>
        /// 新規作成日時
        /// </summary>
        [Display(Name = "新規作成日時")]      // 画面表示
		[Column("create_date")]     // DBのカラム(列)の指定
		public DateTime? CreateDate { get; set; } = DateTime.Now;

        /// <summary>
        /// 更新日時
        /// </summary>
        [Display(Name = "更新日時")]        // 画面表示
		[Column("update_date")]     // DBのカラム(列)の指定
		public DateTime? UpdateDate { get; set; } = DateTime.Now;

    }
}
