// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// <copyright file="Program.cs" company="SUS">
// (c) 株式会社エスユーエス All Rights Reserved.
// </copyright>
// -----------------------------------------------------------------------
// システム名：研修Webサイト
// プログラム名：プログラムクラス
// 作成日：2026/02/26
// 更新日：
// -----------------------------------------------------------------------
namespace Songapp // このプログラムが「Songapp」というプロジェクトの仲間であることを示します
{
    using Microsoft.EntityFrameworkCore; // データベースを簡単に操作するための拡張パーツを引用
    using Songapp.Models; // プロジェクト内のデータ保存用の形（Models）を引用

    /// <summary>
    /// アプリケーションの起動と設定を行うメインクラス.
    /// </summary>
    public class Program // Webアプリを立ち上げるための大元のクラス
    {
        /// <summary>
        /// アプリケーションのメインエントリポイント.
        /// </summary>
        /// <param name="args">コマンドライン引数.</param>
        public static void Main(string[] args) // アプリが起動したときに自動で最初に1回だけ動く処理
        {
            var builder = WebApplication.CreateBuilder(args); // 1. Webアプリの部品を組み立てる準備（ビルダー）をスタート

            builder.Services.AddHttpContextAccessor(); // 2. コントローラー以外の場所でもアクセスした人の情報を覗ける道具を登録

            builder.Services.AddControllersWithViews(options => options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((value, field) => "正しい形式で入力してください")); // 3. 画面（View）と裏のプログラム（Controller）を動かすMVC基本セットを登録(形式チェックあり)

            builder.Services.AddSession(options => // 4. ユーザーごとにデータを一時的に記憶する「セッション機能」のルールを設定して登録
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20); // 5. 何も操作しない時間が20分続いたらセキュリティのため記憶を自動消去
                options.Cookie.HttpOnly = true; // 6. 悪意のある外部のプログラムからデータを盗まれないようにガード
                options.Cookie.IsEssential = true; // 7. このWebサイトが安全に動作するために、この記憶機能は「絶対に必須」と認識させる
            });

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty; // 8. 設定ファイルからDBの「住所」を読み込む
            builder.Services.AddDbContext<SongDbContext>(opt => opt.UseSqlite(connectionString)); // 9. 読み込んだ住所を使ってDBと通信する設定箱を登録

            var app = builder.Build(); // 10. ここまで登録したすべての設定部品を合体させてWebアプリ本体を完成

            app.UseExceptionHandler("/Home/Error"); // 11. 裏側でシステムエラーが発生したら自動でエラー専用画面へ移行
            app.UseHsts(); // 12. インターネット通信のセキュリティをより厳しく高めるための暗号化ルールを強制適用

            app.UseHttpsRedirection(); // 13. 危険な「http://」で入ってきた人を自動的に安全な「https://」へ切り替えて誘導
            app.UseRouting(); // 14. URLアドレスに応じてどの画面を呼び出すかを決める「交通整理」の仕組みを有効化

            app.UseSession(); // 15. 登録しておいた、ログイン情報などを一時記憶する「セッション機能」をここからスタート

            app.UseAuthorization(); // 16. ユーザーがその画面を見る権利（アクセス権限）があるかどうかを裏で厳しくチェック
            app.MapStaticAssets(); // 17. CSS（デザインシート）やJavaScriptなどの「見た目用ファイル」をブラウザに配る設定

            app.MapControllerRoute( // 18. URLの基本ルールを決定
                name: "default",
                pattern: "{controller=Login}/{action=Login}/{id?}") // 19. アドレスの後ろが空ならLogin画面を一番最初に開く設定
                .WithStaticAssets(); // 20. 最初の画面でもデザイン用のCSSファイルを一緒に読み込んで使えるように紐付け

            app.Run(); // 21. すべての準備が整ったのでWebサーバーを起動してユーザーからのアクセスを待ち受ける
        }
    }
}