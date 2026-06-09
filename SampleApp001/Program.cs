// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
/// <copyright file="Program.cs" company="SUS">
/// (c) 株式会社エスユーエス All Rights Reserved.
/// </copyright>
/// -----------------------------------------------------------------------
/// システム名：研修Webサイト
/// プログラム名：プログラムクラス
/// 作成日：2026/02/26
/// 更新日：
/// -----------------------------------------------------------------------
namespace Songapp
{
    using Microsoft.EntityFrameworkCore;
    using Songapp.Models;

    /// <summary>
    /// アプリケーションの起動と設定を行うメインクラス.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// アプリケーションのメインエントリポイント.
        /// </summary>
        /// <param name="args">コマンドライン引数.</param>
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // システム全体でセッションの直接取得（IHttpContextAccessor）を使えるように登録
            builder.Services.AddHttpContextAccessor();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // -----------------------------------------------------------------------
            // ★【追加】セッション機能のサービス登録（一時記憶の容量を確保する設定）
            // -----------------------------------------------------------------------
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20); // 20分間操作がないと自動リセット
                options.Cookie.HttpOnly = true; // セキュリティ対策
                options.Cookie.IsEssential = true; // セッションに必須の設定
            });

            // DB接続文字列の取得とDBコンテキストの登録(SQLite)
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
            builder.Services.AddDbContext<SongDbContext>(opt => opt.UseSqlite(connectionString));

            var app = builder.Build();

            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();

            app.UseHttpsRedirection();
            app.UseRouting();

            // -----------------------------------------------------------------------
            // ★【追加】セッション機能を有効化（※必ずUseRoutingとUseAuthorizationの間に配置）
            // -----------------------------------------------------------------------
            app.UseSession();

            app.UseAuthorization();
            app.MapStaticAssets();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Login}/{action=Login}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}