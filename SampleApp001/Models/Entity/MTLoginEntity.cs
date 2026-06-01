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
namespace Songapp.Models.Entity
{
    #region -- using --
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    #endregion -- using --

    /// <summary>
    /// 商品マスタエンティティクラス
    /// </summary>
    [Table("mt_login")]
    [Serializable]
    public class MTLoginEntity
    {
        /// <summary>
        /// 主キー
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; } = 0;

        /// <summary>
        /// ユーザー名
        /// </summary>
        [Display(Name = "ユーザー名")]
        [Column("username")]
        [Required]
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// パスワード
        /// </summary>
        [Display(Name = "パスワード")]
        [Column("password")]
        [Required]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// 削除フラグ
        /// </summary>
        [Display(Name = "削除フラグ")]
        [Column("is_deleted")]
        [Required]
        public int IsDeleted { get; set; } = 0;

        /// <summary>
        /// 新規作成日時
        /// </summary>
        [Display(Name = "新規作成日時")]
        [Column("create_date")]
        public DateTime? CreateDate { get; set; } = DateTime.Now;

        /// <summary>
        /// 更新日時
        /// </summary>
        [Display(Name = "更新日時")]
        [Column("update_date")]
        public DateTime? UpdateDate { get; set; } = DateTime.Now;

    }
}
