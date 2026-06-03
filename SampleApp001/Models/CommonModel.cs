namespace SongApp.Models
{
    /// <summary>
    /// 共通で使用されるモデル型の基底クラス.
    /// </summary>
    public class CommonModel
    {
        /// <summary>
        /// 選択状態.
        /// </summary>
        public class SelectOnOff
        {
            /// <summary>
            /// ON.
            /// </summary>
            public const int On = 1;    // リテラル値の定数定義(0や1等をOn,Off等一目で分かる言葉に置き換える)

            /// <summary>
            /// OFF.
            /// </summary>
            public const int Off = 0;   // リテラル値の定数定義
        }

        public const int NewId = 0;     // リテラル値の定数定義
    }
}
