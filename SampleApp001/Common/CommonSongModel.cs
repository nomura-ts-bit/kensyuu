// ========================================================================
// <copyright file="CommonBusinessModel.cs" company="SUS">
//     (c) 株式会社エスユーエス All Rights Reserved. 
// </copyright>
// -----------------------------------------------------------------------
// システム名：研修Webサイト
// プログラム名：共通ビジネスクラス
// 作成日：2026/02/26
// 更新日：
// ========================================================================
namespace Songapp.Common
{
    using Songapp.Models;

    /// <summary>
    /// 共通ビジネスクラス
    /// </summary>
    public class CommonSongModel
    {
        #region 内部変数

        /// <summary>
        /// DBコンテキスト
        /// </summary>
        protected SongDbContext _context;

        /// <summary>
        /// ロガー
        /// </summary>
        protected ILogger _logger;

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dbContext">DBコンテキスト</param>
        /// <param name="logger">ロガー</param>
        public CommonSongModel(SongDbContext context, ILogger logger)
        {
            this._context = context;
            this._logger = logger;
        }

        #endregion
    }
}
