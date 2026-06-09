// <copyright file="SongModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// システム名：研修Webサイト
// プログラム名：楽曲管理ロジッククラス
// 作成日：2026/02/26
// 更新日：
// -----------------------------------------------------------------------

namespace Songapp.Services//ネームスペースを定義(住所)
{
    using Microsoft.AspNetCore.Mvc;//SP.NET Core MVCの機能を利用可能にする
    using Microsoft.EntityFrameworkCore;//SP.NET Core MVCの機能を利用可能にする
    using Songapp.Common;//ViewModelクラス群を読み込み↓
    using Songapp.Models;
    using SongApp.Models;
    using Songapp.Models.Entity;
    using Songapp.ViewModel;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// 楽曲管理に関するビジネスロジックを提供するクラス.
    /// </summary>
    public class SongModel : CommonSongModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SongModel"/> class.
        /// 構造化されたコンストラクタ.
        /// </summary>
        /// <param name="context">DBコンテキスト.</param>
        /// <param name="logger">ロガーインスタンス.</param>
        private readonly IHttpContextAccessor _httpContextAccessor;　// セッションを直接覗き込むための専用の通り道
        public SongModel(SongDbContext context, ILogger logger, IHttpContextAccessor httpContextAccessor)
            : base(context, logger)//クラスのインスタンスが生成されるときに最初に動くコンストラクタ
                                   //データベース接続を管理する context と、ログを出力する logger を受け取り、そのまま親クラス（base）に引き渡して初期化
        {
            // 受け取った道具を保存する
            this._httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// クラス内のどこからでも「this.CurrentUsername」と書くだけで、
        /// セッションからログイン中のユーザー名を一発で取得できる魔法の引き出し
        /// </summary>
        private string CurrentUsername => this._httpContextAccessor.HttpContext?.Session.GetString("LoginUser") ?? string.Empty;

        /// <summary>
        /// 条件に応じた楽曲の一覧を検索してViewModelに格納
        /// </summary>
        /// <param name="indexvm">検索条件および結果を保持するViewModel.</param>
        /// <returns>楽曲一覧が格納されたViewModel. 引数がNullの場合はNullを返す.</returns>
        public async Task<SongIndexViewModel?> IndexWhere(SongIndexViewModel? indexvm)
        //検索条件を受け取り、合致する楽曲一覧を返す非同期メソッドの開始
        {
            if (indexvm == null)
            {
                return null;
            }
            //もし画面から渡された検索条件（ViewModel）自体が空（null）だった場合は、何も処理せず null を返して終了

            var products = this._context.MTSong.AsNoTracking();
            //データベースの楽曲マスターテーブル（MTSong）からデータを取得する準備
            products = products.Where(x => x.CreateUserName == this.CurrentUsername);

            if (indexvm.Keyword != null)//画面から検索キーワードが入力されているかどうかを判定
            {
                // EntityやKeywordのNull許容警告を回避するため、安全な評価式に変更
                products = products.Where(x =>
                    (x.Song != null && x.Song.Contains(indexvm.Keyword)) ||
                    (x.Group != null && x.Group.Contains(indexvm.Keyword)) ||
                    (x.Artist != null && x.Artist.Contains(indexvm.Keyword)) ||
                    x.History.ToString().Contains(indexvm.Keyword));
                //
            }

            products = products.Where(x => x.IsDeleted == CommonModel.SelectOnOff.Off); // 削除フラグ（IsDeleted）がオフ（未削除）のデータのみに絞り込み

            indexvm.Songs = await products.ToListAsync();
            // ここまでに組み立てた条件でデータベースに実際の検索命令を投げ（ToListAsync）、結果のリストをViewModelの Songs プロパティに格納します。
            // await はデータベースからの返事を入るまで非同期で待つ指示(処理が完了してから実行)

            return indexvm; // 検索結果を詰め込んだViewModelを呼び出し元に返して、メソッドを終了
        }

        /// <summary>
        /// 楽曲情報の新規登録または更新処理を行います.
        /// </summary>
        /// <param name="editvm">画面から渡された編集用ViewModel.</param>
        /// <returns>処理が成功した場合はtrue.</returns>
        [HttpPost] // データ送信を伴うHTTPリクエストで呼び出されることを示す。
        public async Task<bool> EditPost(SongEditViewModel editvm)
        {
            MTSongEntity entity;// データベースに保存するための箱を宣言。

            if (editvm.Id == CommonModel.NewId)// もし画面から送られてきたIDが新規登録用IDが0だった場合、新しく真っ新なデータを作成し、データベースの追加対象（Add）として登録
            {
                entity = new MTSongEntity();
                entity.CreateUserName = this.CurrentUsername;
                this._context.MTSong.Add(entity);
            }
            else
            {
                // データが存在しない場合はFirstAsyncが安全に例外を返す
                entity = await this._context.MTSong.FirstAsync(x => x.Id == editvm.Id);
            }

            // Nullの可能性がある値を安全に代入（Nullの場合は空文字を返す）↓
            entity.Song = editvm.Edit_Song ?? string.Empty;
            entity.Artist = editvm.Edit_Artist ?? string.Empty;
            entity.Group = editvm.Edit_Group ?? string.Empty;
            entity.History = editvm.Edit_History;
            entity.Lyric = editvm.Edit_Lyric ?? string.Empty; // ここまで
            entity.UpdateDate = DateTime.Now; // 更新日時（UpdateDate）に、現在の時刻を代入

            await this._context.SaveChangesAsync(); // ここまでで行った追加や変更の指示を、データベースに実際に反映

            return true; // 処理が正常に完了した証として、true を返してメソッドを終了
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
                return new SongEditViewModel(); // もし指定されたIDが新規登録用のIDであれば、中身が空の新しいViewModelを作成してそのまま返します
            }

            var entity = await this._context.MTSong.AsNoTracking().FirstAsync(x => x.Id == id); // 前回の設計思想に基づき、データ不在時はFirstAsyncで例外を発生させエラー画面へ一任

            var editvm = new SongEditViewModel
            {
                Id = entity.Id, // データベースから見つけてきたデータを、画面表示用の SongEditViewModel の中に詰め替え↓
                Edit_Song = entity.Song,
                Edit_Artist = entity.Artist,
                Edit_Group = entity.Group,
                Edit_History = entity.History,
                Edit_Lyric = entity.Lyric, // 複数行初期化子の末尾カンマを追加
            };//ここまで

            return editvm;
        }

        /// <summary>
        /// 楽曲の論理削除処理.
        /// </summary>
        /// <param name="editvm">削除対象のIDを保持するViewModel.</param>
        /// <returns>処理が成功した場合はtrue.</returns>
        [HttpPost]
        public async Task<bool> Delete(SongEditViewModel editvm)
        {
            if (editvm.Id != CommonModel.NewId) // 削除対象のIDが、新規IDではないことを確認。
            {
                // 存在しないIDは例外にするため、ここもFirstAsyncへ統一し、Null逆参照警告を完全消滅
                MTSongEntity entity = await this._context.MTSong.FirstAsync(x => x.Id == editvm.Id); // データベースから、削除したいIDに一致するデータを検索して取得
                entity.IsDeleted = CommonModel.SelectOnOff.On; // 画面上は見えなくなるがデータベースにはデータが残る(論理削除)
                entity.UpdateDate = DateTime.Now; // 削除処理を行った時間として、更新日時に現在時刻を設定
                await this._context.SaveChangesAsync(); //  変更内容をデータベースに保存
            }

            return true; // 成功の証としてtrueを返す
        }

        /// <summary>
        /// ログイン認証処理を行います.
        /// </summary>
        /// <param name="loginvm">ログイン情報を保持するViewModel.</param>
        /// <returns>認証に成功した場合はtrue、失敗した場合はfalse.</returns>
        [HttpPost]
        public async Task<bool> Login(LoginViewModel loginvm) // ユーザーが入力したユーザー名とパスワードが正しいかチェックするメソッド
        {
            var entity = await this._context.MTLogin.FirstOrDefaultAsync(x => x.Username == loginvm.Username && x.Password == loginvm.Password);
            // 完全に一致するデータを検索
            if (entity == null)// 一致するデータが見つからなかった場合は、認証失敗としてfalseを返す
            {
                return false;
            }

            return true; // 見つかった場合はtureを返す
        }

        /// <summary>
        /// ユーザー情報の新規登録を行います.
        /// </summary>
        /// <param name="loginvm">登録するユーザー情報を保持するViewModel.</param>
        /// <returns>処理が成功した場合はtrue.</returns>
        [HttpPost]
        public async Task<bool> EditUser(LoginViewModel loginvm)
        // ログインユーザーを追加するメソッド
        {
            MTLoginEntity entity = new MTLoginEntity(); // データベースの追加対象として登録
            this._context.MTLogin.Add(entity);
            entity.CreateDate = DateTime.Now; // 現在の時刻を設定
            entity.Username = loginvm.Username ?? string.Empty; // ユーザー名とパスワードをデータベースの箱に格納
            entity.Password = loginvm.Password ?? string.Empty;

            await this._context.SaveChangesAsync(); // 新しいユーザー情報をデータベースに実際に保存

            return true; // 成功の証を返す
        }
    }
}