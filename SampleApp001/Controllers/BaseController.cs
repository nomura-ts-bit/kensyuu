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
namespace Songapp.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Songapp.Models;
    /// <summary>
    /// Baseコントローラークラス
    /// </summary>
    /// <typeparam name="T">継承先コントローラー</typeparam>
    public class BaseController<T> : Controller
    {
        #region メンバ変数

        /// <summary>
        /// ロガー
        /// </summary>
        protected readonly ILogger<T> _logger;

        /// <summary>
        /// DBコンテキスト
        /// </summary>
        protected SongDbContext _context;

        /// <summary>
        /// 環境
        /// </summary>
        protected IWebHostEnvironment _environment;

        #endregion メンバ変数

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="context">DBコンテキスト</param>
        /// <param name="logger">ロガー</param>
        /// <param name="environment">環境</param>
        public BaseController(SongDbContext context, ILogger<T> logger, IWebHostEnvironment environment)
        {
            this._context = context;
            this._logger = logger;
            this._environment = environment;
        }

        #endregion コンストラクタ
    }
}
