// ========================================================================
// <copyrightfile="HomeController.cs" company="SUS">
//     (c) 株式会社エスユーエス All Rights Reserved.
// </copyright>
// -----------------------------------------------------------------------
// システム名：研修Webサイト
// プログラム名：ホーム画面コントローラークラス
// 作成日：2026/02/26
// 更新日：
// ========================================================================
namespace Songapp.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Songapp.Models;
    using Songapp.Services;
    using Songapp.ViewModel;

    /// <summary>
    /// ホーム画面コントローラークラス.
    /// </summary>
    public class LoginController : BaseController<HomeController>
    {
        /// <summary>
        /// コンストラクタ.
        /// </summary>
        /// <param name="context">DBコンテテキスト.</param>
        /// <param name="logger">ロガー.</param>
        /// <param name="environment">環境.</param>
        private SongModel service;

        public LoginController(SongDbContext context, ILogger<HomeController> logger, IWebHostEnvironment environment)
            : base(context, logger, environment)
        {
            this.service = new SongModel(context, logger);
        }

        /// <summary>
        /// Index画面.
        /// </summary>
        /// <returns>ビュー.</returns>
        public async Task<IActionResult> Login()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginvm)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(loginvm);
            }

            var products = await this.service.Login(loginvm);
            if (products == false)
            {
                this.ModelState.AddModelError(string.Empty, "ユーザー名またはパスワードが違います");
                return this.View(loginvm);
            }
            return this.RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Edit()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(LoginViewModel loginvm)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(loginvm);
            }

            var products = await this.service.EditUser(loginvm);
            return this.RedirectToAction("Login");
        }
    }
}