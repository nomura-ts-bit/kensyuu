// ========================================================================
// <copyright file="SongDbContext.cs" company="SUS">
//     (c) 株式会社エスユーエス All Rights Reserved. 
// </copyright>
// -----------------------------------------------------------------------
// システム名：研修Webサイト
// プログラム名：DBコンテキスト
// 作成日：2026/02/26
// 更新日：2026/05/07
// ========================================================================
using Microsoft.EntityFrameworkCore;
using Songapp.Models.Entity;

namespace Songapp.Models
{
    /// <summary>
    /// DBコンテキスト
    /// </summary>
    public class SongDbContext : DbContext
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SongDbContext()
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="options">DBコンテキストオプション</param>
        public SongDbContext(DbContextOptions<SongDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// 商品マスタ
        /// </summary>
        public DbSet<MTSongEntity> MTSong { get; set; }
        public DbSet<MTLoginEntity> MTLogin { get; set; }
    }
}
