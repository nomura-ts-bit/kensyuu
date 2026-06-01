// <copyright file="HomeController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// システム名：研修Webサイト
// プログラム名：ホーム画面コントローラークラス
// 作成日：2026/02/26
// 更新日：
// -----------------------------------------------------------------------

namespace Songapp.Controllers
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Songapp.Models;
    using Songapp.Models.Entity;
    using Songapp.Services;
    using Songapp.ViewModel;
    using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

    /// <summary>
    /// ホーム画面コントローラークラス.
    /// </summary>
    public class HomeController : BaseController<HomeController>
    {
        /// <summary>
        /// 楽曲管理ロジックを提供するサービスインスタンス.
        /// </summary>
        private SongModel service;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// コンストラクタ.
        /// </summary>
        /// <param name="context">DBコンテキスト.</param>
        /// <param name="logger">ロガー.</param>
        /// <param name="environment">環境.</param>
        public HomeController(SongDbContext context, ILogger<HomeController> logger, IWebHostEnvironment environment)
            : base(context, logger, environment)
        {
            this.service = new SongModel(context, logger);
        }

        /// <summary>
        /// Index画面を表示します.
        /// </summary>
        /// <param name="indexvm">検索条件を保持するViewModel.</param>
        /// <returns>ビュー.</returns>
        public async Task<IActionResult> Index(SongIndexViewModel indexvm)
        {
            var products = await this.service.IndexWhere(indexvm);
            return this.View(products);
        }

        /// <summary>
        /// 楽曲の編集処理を行います（POST）.
        /// </summary>
        /// <param name="editvm">編集された楽曲データを保持するViewModel.</param>
        /// <returns>ビュー.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SongEditViewModel editvm)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(editvm);
            }

            await this.service.EditPost(editvm);

            return this.RedirectToAction("Index");
        }

        /// <summary>
        /// 指定されたIDの楽曲編集画面を表示します（GET）.
        /// </summary>
        /// <param name="id">楽曲のID.</param>
        /// <returns>ビュー.</returns>
        public async Task<IActionResult> Edit(int id)
        {
            var products = await this.service.EditView(id);

            return this.View(products);
        }

        /// <summary>
        /// 楽曲の削除処理を行います.
        /// </summary>
        /// <param name="editvm">削除対象の楽曲データを保持するViewModel.</param>
        /// <returns>ビュー.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(SongEditViewModel editvm)
        {
            await this.service.Delete(editvm);

            return this.RedirectToAction("Index");
        }

        /// <summary>
        /// Privacy画面.
        /// </summary>
        /// <returns>ビュー.</returns>
        public IActionResult Privacy()
        {
            return this.View();
        }

        /// <summary>
        /// エラー画面.
        /// </summary>
        /// <returns>ビュー.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}