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
namespace Songapp.Controllers // このプログラムがどこに属しているかを表す
{
    // 外部の道具の引用
    using Microsoft.AspNetCore.Mvc; // MVC（Controller、IActionResult、HttpPostなど）の基本道具を引用
    using Songapp.Models; // プロジェクト内のModelsフォルダ（DbContextやエラー用Model）を引用
    using Songapp.Services; // ビジネスロジックを実行するseriveces（SongModel）を引用
    using Songapp.ViewModel; // 画面の表示・入力専用に用意したデータ構造（ViewModel）のフォルダを引用

    /// <summary>
    /// ホーム画面コントローラークラス.
    /// </summary>
    public class LoginController : BaseController<HomeController> // 共通機能を持つBaseControllerを親としたクラス
    {
        /// <summary>
        /// 初期セットアップ.
        /// </summary>
        /// <param name="context">DBコンテキスト(接続管理).</param>
        /// <param name="logger">ロガー.</param>
        /// <param name="environment">環境.</param>
        private SongModel service; // 外部からの盗み見を防ぐ(private)空箱宣言（service）

        public LoginController(SongDbContext context, ILogger<HomeController> logger, IWebHostEnvironment environment) // 起動時に自動で動く初期設定（引数で3つの道具を受け取る）
            : base(context, logger, environment) // 親クラス（BaseController）の初期設定へ受け取った道具を渡す
        {
            this.service = new SongModel(context, logger); // serviceのセッティング
        }

        /// <summary>
        /// Index画面.
        /// </summary>
        /// <returns>ビュー.</returns>
        public async Task<IActionResult> Login() // ログイン画面を最初に開いたとき（並行処理のため非同期）
        {
            return this.View(); // ログイン画面（Login.cshtml）を表示する
        }

        [HttpPost] // 画面のログインボタンから「データが送信されてきたときだけ」反応
        [ValidateAntiForgeryToken] // 偽物のデータ送信リクエストを弾く
        public async Task<IActionResult> Login(LoginViewModel loginvm) // 送信されてきた入力内容（loginvm）を受け取る
        {
            if (!this.ModelState.IsValid) // 画面から届いたデータが文字数制限などの入力ルールが正しいか
            {
                return this.View(loginvm); // ログイン処理へ進まずにエラーメッセージを付け元のログイン画面へ返す
            }

            var products = await this.service.Login(loginvm); // serviceのログイン認証処理の呼び出し
            if (products == false) // もしログイン認証が失敗（ユーザーが存在しない、またはパスワード間違い）したら
            {
                this.ModelState.AddModelError(string.Empty, "ユーザー名またはパスワードが違います"); // 画面に表示する用のエラーメッセージを追加
                return this.View(loginvm); // エラーメッセージを表示するために元のログイン画面へ返す
            }

            return this.RedirectToAction("Index", "Home"); // ログインが成功したらホーム画面（HomeControllerのIndex）へ移動
        }

        public async Task<IActionResult> Edit() // ユーザー情報編集画面を最初に開いたとき
        {
            return this.View(); // 編集画面（Edit.cshtml）を表示する
        }

        [HttpPost] // 画面の保存ボタンから「データが送信されてきたときだけ」反応
        [ValidateAntiForgeryToken] // 不正アクセスを防ぐセキュリティ
        public async Task<IActionResult> Edit(LoginViewModel loginvm) // 送信されてきた入力内容（loginvm）を受け取る
        {
            if (!this.ModelState.IsValid) // 画面から届いたデータが文字数制限などの入力ルールが正しいか
            {
                return this.View(loginvm); // 保存処理へ進まずにエラーメッセージを付け元の編集画面へ返す
            }

            var products = await this.service.EditUser(loginvm); // serviceのユーザー情報更新処理の呼び出し
            return this.RedirectToAction("Login"); // 更新が完璧に終わったらログイン画面へ移動
        }
    }
}