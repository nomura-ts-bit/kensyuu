// ========================================================================
// <copyright file="BaseController.cs" company="SUS">
//     (c) 株式会社エスユーエス All Rights Reserved. 
// </copyright>
// -----------------------------------------------------------------------
// システム名：研修Webサイト
// プログラム名：Baseコントローラークラス
// 作成日：2026/02/26
// 更新日：
// ========================================================================
namespace Songapp.Controllers // このプログラムがどこに属しているか（住所）を表す
{
    using Microsoft.AspNetCore.Mvc; // MVCの基本機能（Controllerクラスなど）を呼び出すための道具を引用
    using Songapp.Models; // プロジェクト内のデータモデル（SongDbContextなど）を引用

    /// <summary>
    /// Baseコントローラークラス
    /// </summary>
    /// <typeparam name="T">継承先コントローラー</typeparam>
    public class BaseController<T> : Controller // 全てのコントローラーに共通の道具を配るためのクラス（型を自由に変えられる<T>機能付き）
    {
        /// <summary>
        /// ロガー
        /// </summary>
        protected readonly ILogger<T> _logger; // 自分（親）と、これを継承した子供（HomeControllerなど）だけが使えるログ書き込み用の箱（上書き禁止）

        /// <summary>
        /// DBコンテキスト
        /// </summary>
        protected SongDbContext _context; // 自分と子供だけがアクセスできる、データベースへの通信の鍵（空箱宣言）

        /// <summary>
        /// 環境
        /// </summary>
        protected IWebHostEnvironment _environment; // 自分と子供だけが使える、このアプリが動いているサーバー環境の情報（フォルダのパスなど）の箱

        /// <summary>
        /// 初期セットアップ
        /// </summary>
        /// <param name="context">DBコンテキスト</param>
        /// <param name="logger">ロガー</param>
        /// <param name="environment">環境</param>
        public BaseController(SongDbContext context, ILogger<T> logger, IWebHostEnvironment environment) // 子供クラスが生まれた瞬間、そこからパスされてきた3つの必須の道具をここで受け取る(初期セットアップ)
        {
            this._context = context; // 受け取ったDBの鍵を、自分の身の回り（_context）にセット
            this._logger = logger; // 受け取ったログの道具を、自分の身の回り（_logger）にセット
            this._environment = environment; // 受け取った環境情報を、自分の身の回り（_environment）にセット
        }
    }
}