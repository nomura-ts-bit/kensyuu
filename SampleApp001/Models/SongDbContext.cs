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
	/// データベースとアプリケーションを仲介し、データの送受信を管理する中心クラス
	/// </summary>
	public class SongDbContext : DbContext      // DbContextを継承
    {
		/// <summary>
		/// ツールやテストで使用される空のコンストラクタ
		/// </summary>
		public SongDbContext()
        {
        }

		/// <summary>
		/// データベースの種類や接続先の情報を、DbContextに渡すコンストラクタ
		/// </summary>
		/// <param name="options">DBコンテキストオプション</param>
		/// 
		public SongDbContext(DbContextOptions<SongDbContext> options) : base(options)
        {
        }

		// 商品マスタ（MTSongテーブル）にアクセスするためのプロパティ
		public DbSet<MTSongEntity> MTSong { get; set; }

		// ログインマスタ（MTLoginテーブル）にアクセスするためのプロパティ
		public DbSet<MTLoginEntity> MTLogin { get; set; }
    }
}
