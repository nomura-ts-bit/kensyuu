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
using Microsoft.EntityFrameworkCore; // データベースを簡単に操作するための心臓部（Entity Framework Core）の基本機能を引用
using Songapp.Models.Entity; // 自分たちが作ったテーブルの具体的な形（SongやUserなどのEntityクラス）が入っているフォルダを引用

namespace Songapp.Models
{
	/// <summary>
	/// DBコンテキスト
	/// データベースとアプリケーションを仲介し、データの送受信を管理する中心クラス
	/// </summary>
	public class SongDbContext : DbContext      // DbContextを継承
    {
		/// <summary>
		/// ツールやテストで使用される空のコンストラクタ(初期設定)
		/// </summary>
		public SongDbContext()
        {
        }

		/// <summary>
		/// データベースの種類や接続先の情報を、DbContextに渡すコンストラクタ(初期設定)
		/// </summary>
		/// <param name="options">DBコンテキストオプション</param>
		/// 
		public SongDbContext(DbContextOptions<SongDbContext> options) : base(options)
        {
        }

		public DbSet<MTSongEntity> MTSong { get; set; } // 商品マスタ（MTSongテーブル）にアクセスするためのプロパティ(入出力を可能にするなどの詳細設定)

        public DbSet<MTLoginEntity> MTLogin { get; set; } // ログインマスタ（MTLoginテーブル）にアクセスするためのプロパティ
    }
}
