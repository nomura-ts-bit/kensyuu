// <copyright file="HomeController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// システム名：研修Webサイト
// プログラム名：ホーム画面コントローラークラス
// 作成日：2026/02/26
// 更新日：
// -----------------------------------------------------------------------
namespace Songapp.Controllers // このプログラムがどこに属しているか（住所）を表す
{ 
    // 外部の道具の引用
    using System.Diagnostics; // システム診断やエラー追跡（Activity）の道具を引用
    using Microsoft.AspNetCore.Mvc; // MVC（Controller、IActionResult、HttpPostなど）の基本道具を引用
    using Songapp.Models; // プロジェクト内のModelsフォルダ（DbContextやエラー用Model）を引用
    using Songapp.Services; // ビジネスロジックを身代わりに実行してくれるサービス層（SongModel）を引用
    using Songapp.ViewModel; // 画面の表示・入力専用に用意したデータ構造（ViewModel）のフォルダを引用
    
    /// <summary>
    /// ホーム画面コントローラークラス.
    /// </summary>
    public class HomeController : BaseController<HomeController> // 共通機能を持つBaseControllerを親としたクラス
    { // HomeControllerクラスの中身がここから始まる
        /// <summary>
        /// 楽曲管理ロジックを提供するサービスインスタンス.
        /// </summary>
        private SongModel service; // 外部からの盗み見を防ぐ(privete)空箱宣言（service）

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// 初期セットアップ.
        /// </summary>
        /// <param name="context">DBコンテキスト(接続管理).</param>
        /// <param name="logger">ロガー.</param>
        /// <param name="environment">環境.</param>
        public HomeController(SongDbContext context, ILogger<HomeController> logger, IWebHostEnvironment environment) // 起動時に自動で動く初期設定（引数で3つの道具を受け取る）
            : base(context, logger, environment) // 親クラス（BaseController）の初期設定へ受け取った道具を渡す
        { // コンストラクタの実際の初期化処理がここから始まる
            this.service = new SongModel(context, logger); // sreviceのセッティング
        } // コンストラクタの処理が終了

        /// <summary>
        /// Index画面を表示します.
        /// </summary>
        /// <param name="indexvm">検索条件を保持するViewModel.</param>
        /// <returns>ビュー.</returns>
        public async Task<IActionResult> Index(SongIndexViewModel indexvm) // 一覧画面を開いたとき、または検索ボタンが押されたとき（並行処理のため非同期）
        {
            var products = await this.service.IndexWhere(indexvm); // serviceの検索条件ソート機能の呼び出し
            return this.View(products); // ゲットした楽曲一覧データ（products）を、一覧画面（Index.cshtml）に返す
        }

        /// <summary>
        /// 楽曲の編集処理（POST）.
        /// </summary>
        /// <param name="editvm">編集された楽曲データを保持するViewModel.</param>
        /// <returns>ビュー.</returns>
        [HttpPost] // 画面の保存ボタンから「データが送信されてきたときだけ」反応
        [ValidateAntiForgeryToken] // 偽物のデータ送信リクエストを弾く
        public async Task<IActionResult> Edit(SongEditViewModel editvm) // 送信されてきた入力内容（editvm）を受け取る
        {
            if (!this.ModelState.IsValid) // 画面から届いたデータが文字数制限などの入力ルールが正しいか
            {
                return this.View(editvm); // 保存処理へ進まずにエラーメッセージを付け元の編集画面へ返す
            }

            await this.service.EditPost(editvm); // sreviceのDB追加更新処理の呼び出し

            return this.RedirectToAction("Index"); // DB保存が完璧に終わったら(await)画面を一覧画面（Index）へ移動
        } 

        /// <summary>
        /// 指定されたIDの楽曲編集画面を表示します（GET）.
        /// </summary>
        /// <param name="id">楽曲のID.</param>
        /// <returns>ビュー.</returns>
        public async Task<IActionResult> Edit(int id) // 一覧画面で「変更する」ボタンが押されたとき（idを受け取る）
        {
            var products = await this.service.EditView(id); // serviceのid番の曲データをDBから受け取る処理の呼び出し

            return this.View(products); // 受け取ったデータ（products）を入力欄にセットして、編集画面（Edit.cshtml）を開く
        }

        /// <summary>
        /// 楽曲の削除処理を行います.
        /// </summary>
        /// <param name="editvm">削除対象の楽曲データを保持するViewModel.</param>
        /// <returns>ビュー.</returns>
        [HttpPost] // 削除ボタンが押されて「データが送信されてきたときだけ」反応
        [ValidateAntiForgeryToken] // 不正アクセスを防ぐセキュリティ
        public async Task<IActionResult> Delete(SongEditViewModel editvm) // 削除したい曲の番号が入ったデータを受け取る
        {
            await this.service.Delete(editvm); // serviceの曲の論理削除フラグをONにする処理の呼び出し

            return this.RedirectToAction("Index"); // 一覧画面（Index）に戻す
        }

        /// <summary>
        /// Privacy画面.
        /// </summary>
        /// <returns>ビュー.</returns>
        public IActionResult Privacy() // テンプレート用のプライバシーポリシー画面の呼び出し
        {
            return this.View(); // Privacy.cshtml表示
        }

        /// <summary>
        /// エラー画面.
        /// </summary>
        /// <returns>ビュー.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)] // エラー画面の内容をブラウザに記憶させない命令
        public IActionResult Error() // システムで予期せぬエラー（存在しないURL直打ちやバグ）が起きたときに呼び出される
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier }); // エラー特定用コードを発行して、バグ画面（Error.cshtml）に埋め込んで表示
        }
    }
}