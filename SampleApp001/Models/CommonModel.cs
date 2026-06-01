namespace SongApp.Models
{
    /// <summary>
    /// 共通で使用されるモデル型の基底クラス
    /// </summary>
    public class CommonModel
    {
        /// <summary>
        /// 選択状態
        /// </summary>
        public class SelectOnOff
        {
            /// <summary>
            /// ON
            /// </summary>
            public const int On = 1;

            /// <summary>
            /// OFF
            /// </summary>
            public const int Off = 0;
        }

        public const int NewId = 0;
    }
}
