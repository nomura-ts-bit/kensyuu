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
    [Table("mt_song")]
    [Serializable]
    public class MTSongEntity
    {
        /// <summary>
        /// 主キーを取得または設定します.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; } = 0;

        /// <summary>
        /// 曲名を取得または設定します.
        /// </summary>
        [Display(Name = "曲名")]
        [Column("song")]
        [Required]
        public string Song { get; set; } = string.Empty;

        /// <summary>
        /// グループ名を取得または設定します.
        /// </summary>
        [Display(Name = "グループ名")]
        [Column("group")]
        [Required]
        public string Group { get; set; } = string.Empty;

        /// <summary>
        /// 作曲者名を取得または設定します.
        /// </summary>
        [Display(Name = "作曲者名")]
        [Column("artist")]
        public string? Artist { get; set; } = string.Empty;

        /// <summary>
        /// 年代を取得または設定します.
        /// </summary>
        [Display(Name = "年代")]
        [Column("history")]
        public int? History { get; set; } = 0;

        /// <summary>
        /// 削除フラグを取得または設定します.
        /// </summary>
        [Display(Name = "削除フラグ")]
        [Column("is_deleted")]
        [Required]
        public int IsDeleted { get; set; } = 0;

        /// <summary>
        /// 歌詞を取得または設定します.
        /// </summary>
        [Display(Name = "歌詞")]
        [Column("lyric")]
        public string? Lyric { get; set; } = string.Empty;

        /// <summary>
        /// 新規作成日時を取得または設定します.
        /// </summary>
        [Display(Name = "新規作成日時")]
        [Column("create_date")]
        public DateTime? CreateDate { get; set; } = DateTime.Now;

        /// <summary>
        /// 新規作成ユーザコードを取得または設定します.
        /// </summary>
        [Display(Name = "新規作成ユーザコード")]
        [Column("create_user_code")]
        public string? CreateUserCode { get; set; } = string.Empty;

        /// <summary>
        /// 新規作成ユーザ名を取得または設定します.
        /// </summary>
        [Display(Name = "新規作成ユーザ名")]
        [Column("create_user_name")]
        public string? CreateUserName { get; set; } = string.Empty;

        /// <summary>
        /// 更新日時を取得または設定します.
        /// </summary>
        [Display(Name = "更新日時")]
        [Column("update_date")]
        public DateTime? UpdateDate { get; set; } = DateTime.Now;

        /// <summary>
        /// 更新ユーザコードを取得または設定します.
        /// </summary>
        [Display(Name = "更新ユーザコード")]
        [Column("update_user_code")]
        public string? UpdateUserCode { get; set; } = string.Empty;

        /// <summary>
        /// 更新ユーザ名を取得または設定します.
        /// </summary>
        [Display(Name = "更新ユーザ名")]
        [Column("update_user_name")]
        public string? UpdateUserName { get; set; } = string.Empty;
    }
}