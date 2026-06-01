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

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // DB接続文字列の取得とDBコンテキストの登録(SQLite)
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
            builder.Services.AddDbContext<SongDbContext>(opt => opt.UseSqlite(connectionString));

            var app = builder.Build();

            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();

            app.UseHttpsRedirection();
            app.UseRouting();
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