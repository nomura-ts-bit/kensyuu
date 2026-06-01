// <copyright file="SongModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// システム名：研修Webサイト
// プログラム名：楽曲管理ロジッククラス
// 作成日：2026/02/26
// 更新日：
// -----------------------------------------------------------------------

namespace Songapp.Services
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Http.HttpResults;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Songapp.Common;
    using Songapp.Models;
    using SongApp.Models;
    using Songapp.Models.Entity;
    using Songapp.ViewModel;
    using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
    using static SongApp.Models.CommonModel;

    /// <summary>
    /// 楽曲管理に関するビジネスロジックを提供するクラスです.
    /// </summary>
    public class SongModel : CommonSongModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SongModel"/> class.
        /// 構造化されたコンストラクタ.
        /// </summary>
        /// <param name="context">DBコンテキスト.</param>
        /// <param name="logger">ロガーインスタンス.</param>
        public SongModel(SongDbContext context, ILogger logger)
            : base(context, logger)
        {
        }

        /// <summary>
        /// 条件に応じた楽曲の一覧を検索してViewModelに格納します.
        /// </summary>
        /// <param name="indexvm">検索条件および結果を保持するViewModel.</param>
        /// <returns>楽曲一覧が格納されたViewModel. 引数がNullの場合はNullを返します.</returns>
        public async Task<SongIndexViewModel?> IndexWhere(SongIndexViewModel? indexvm)
        {
            if (indexvm == null)
            {
                return null;
            }

            var products = this._context.MTSong.AsNoTracking();

            if (indexvm.Keyword != null)
            {
                // EntityやKeywordのNull許容警告を回避するため、安全な評価式に変更
                products = products.Where(x =>
                    (x.Song != null && x.Song.Contains(indexvm.Keyword)) ||
                    (x.Group != null && x.Group.Contains(indexvm.Keyword)) ||
                    (x.Artist != null && x.Artist.Contains(indexvm.Keyword)) ||
                    x.History.ToString().Contains(indexvm.Keyword));
            }

            products = products.Where(x => x.IsDeleted == CommonModel.SelectOnOff.Off);

            indexvm.Songs = await products.ToListAsync();

            return indexvm;
        }

        /// <summary>
        /// 楽曲情報の新規登録または更新処理を行います.
        /// </summary>
        /// <param name="editvm">画面から渡された編集用ViewModel.</param>
        /// <returns>処理が成功した場合はtrue.</returns>
        [HttpPost]
        public async Task<bool> EditPost(SongEditViewModel editvm)
        {
            MTSongEntity entity;

            if (editvm.Id == CommonModel.NewId)
            {
                entity = new MTSongEntity();
                this._context.MTSong.Add(entity);
            }
            else
            {
                // データが存在しない場合はFirstAsyncが安全に例外をスローします
                entity = await this._context.MTSong.FirstAsync(x => x.Id == editvm.Id);
            }

            // Nullの可能性がある値を安全に代入（Nullの場合は空文字をフォールバック）
            entity.Song = editvm.Edit_Song ?? string.Empty;
            entity.Artist = editvm.Edit_Artist ?? string.Empty;
            entity.Group = editvm.Edit_Group ?? string.Empty;
            entity.History = editvm.Edit_History;
            entity.Lyric = editvm.Edit_Lyric ?? string.Empty;
            entity.UpdateDate = DateTime.Now;

            await this._context.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// 編集画面に表示するための楽曲情報を取得します.
        /// </summary>
        /// <param name="id">対象楽曲のID.</param>
        /// <returns>画面表示用に最適化されたViewModel.</returns>
        public async Task<SongEditViewModel> EditView(int id)
        {
            if (id == CommonModel.NewId)
            {
                return new SongEditViewModel();
            }

            // 前回の設計思想に基づき、データ不在時はFirstAsyncで例外を発生させエラー画面へ一任
            var entity = await this._context.MTSong.AsNoTracking().FirstAsync(x => x.Id == id);

            var editvm = new SongEditViewModel
            {
                Id = entity.Id,
                Edit_Song = entity.Song,
                Edit_Artist = entity.Artist,
                Edit_Group = entity.Group,
                Edit_History = entity.History,
                Edit_Lyric = entity.Lyric, // 複数行初期化子の末尾カンマを追加
            };

            return editvm;
        }

        /// <summary>
        /// 楽曲の論理削除処理を行います.
        /// </summary>
        /// <param name="editvm">削除対象のIDを保持するViewModel.</param>
        /// <returns>処理が成功した場合はtrue.</returns>
        [HttpPost]
        public async Task<bool> Delete(SongEditViewModel editvm)
        {
            if (editvm.Id != CommonModel.NewId)
            {
                // 存在しないIDは例外にするため、ここもFirstAsyncへ統一し、Null逆参照警告を完全消滅
                MTSongEntity entity = await this._context.MTSong.FirstAsync(x => x.Id == editvm.Id);
                entity.IsDeleted = CommonModel.SelectOnOff.On;
                entity.UpdateDate = DateTime.Now;
                await this._context.SaveChangesAsync();
            }

            return true;
        }

        /// <summary>
        /// ログイン認証処理を行います.
        /// </summary>
        /// <param name="loginvm">ログイン情報を保持するViewModel.</param>
        /// <returns>認証に成功した場合はtrue、失敗した場合はfalse.</returns>
        [HttpPost]
        public async Task<bool> Login(LoginViewModel loginvm)
        {
            var entity = await this._context.MTLogin.FirstOrDefaultAsync(x => x.Username == loginvm.Username && x.Password == loginvm.Password);
            if (entity == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// ユーザー情報の新規登録を行います.
        /// </summary>
        /// <param name="loginvm">登録するユーザー情報を保持するViewModel.</param>
        /// <returns>処理が成功した場合はtrue.</returns>
        [HttpPost]
        public async Task<bool> EditUser(LoginViewModel loginvm)
        {
            MTLoginEntity entity = new MTLoginEntity();
            this._context.MTLogin.Add(entity);
            entity.CreateDate = DateTime.Now;
            entity.Username = loginvm.Username ?? string.Empty;
            entity.Password = loginvm.Password ?? string.Empty;

            await this._context.SaveChangesAsync();

            return true;
        }
    }
}