// <copyright file="MTSongEntity.cs" company="SUS">
// (c) 株式会社エスユーエス All Rights Reserved.
// </copyright>
// -----------------------------------------------------------------------
// システム名：研修Webサイト
// プログラム名：楽曲マスタエンティティクラス
// 作成日：2026/02/26
// 更新日：
// -----------------------------------------------------------------------

namespace Songapp.Models.Entity
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// 楽曲マスタエンティティクラス.
    /// </summary>
    [Table("mt_song")]      // DBのテーブル指定
    [Serializable]          // データを保存できる形式に変換
	public class MTSongEntity
    {
        /// <summary>
        /// 主キーを取得または設定します.
        /// </summary>
        [Key]       // 主キー設定
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]       // 自動採番(番号振り)
		[Column("id")]      // DBのカラム(列)の指定
		public int Id { get; set; } = 0;

        /// <summary>
        /// 曲名を取得または設定します.
        /// </summary>
        [Display(Name = "曲名")]      // 画面表示
		[Column("song")]        // DBのカラム(列)の指定
		[Required]      // 必須入力にする
		public string Song { get; set; } = string.Empty;

        /// <summary>
        /// グループ名を取得または設定します.
        /// </summary>
        [Display(Name = "グループ名")]       // 画面表示
		[Column("group")]       // DBのカラム(列)の指定
		[Required]      // 必須入力にする
		public string Group { get; set; } = string.Empty;

        /// <summary>
        /// 作曲者名を取得または設定します.
        /// </summary>
        [Display(Name = "作曲者名")]        // 画面表示
		[Column("artist")]      // DBのカラム(列)の指定
		public string? Artist { get; set; } = string.Empty;

        /// <summary>
        /// 年代を取得または設定します.
        /// </summary>
        [Display(Name = "年代")]      // 画面表示
		[Column("history")]     // DBのカラム(列)の指定
		public int? History { get; set; } = 0;

        /// <summary>
        /// 削除フラグを取得または設定します.
        /// </summary>
        [Display(Name = "削除フラグ")]       // 画面表示
		[Column("is_deleted")]      // DBのカラム(列)の指定
		[Required]      // 必須入力にする
		public int IsDeleted { get; set; } = 0;

        /// <summary>
        /// 歌詞を取得または設定します.
        /// </summary>
        [Display(Name = "歌詞")]      // 画面表示
		[Column("lyric")]       // DBのカラム(列)の指定
		public string? Lyric { get; set; } = string.Empty;

        /// <summary>
        /// 新規作成日時を取得または設定します.
        /// </summary>
        [Display(Name = "新規作成日時")]      // 画面表示
		[Column("create_date")]     // DBのカラム(列)の指定
		public DateTime? CreateDate { get; set; } = DateTime.Now;

        /// <summary>
        /// 新規作成ユーザコードを取得または設定します.
        /// </summary>
        [Display(Name = "新規作成ユーザコード")]      // 画面表示
		[Column("create_user_code")]        // DBのカラム(列)の指定
		public string? CreateUserCode { get; set; } = string.Empty;

        /// <summary>
        /// 新規作成ユーザ名を取得または設定します.
        /// </summary>
        [Display(Name = "新規作成ユーザ名")]        // 画面表示
		[Column("create_user_name")]        // DBのカラム(列)の指定
		public string? CreateUserName { get; set; } = string.Empty;

        /// <summary>
        /// 更新日時を取得または設定します.
        /// </summary>
        [Display(Name = "更新日時")]        // 画面表示
		[Column("update_date")]     // DBのカラム(列)の指定
		public DateTime? UpdateDate { get; set; } = DateTime.Now;

        /// <summary>
        /// 更新ユーザコードを取得または設定します.
        /// </summary>
        [Display(Name = "更新ユーザコード")]        // 画面表示
		[Column("update_user_code")]        // DBのカラム(列)の指定
		public string? UpdateUserCode { get; set; } = string.Empty;

        /// <summary>
        /// 更新ユーザ名を取得または設定します.
        /// </summary>
        [Display(Name = "更新ユーザ名")]      // 画面表示
		[Column("update_user_name")]        // DBのカラム(列)の指定
		public string? UpdateUserName { get; set; } = string.Empty;
    }
}