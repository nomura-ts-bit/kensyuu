namespace Songapp.Models
{
	// エラー画面のための宣言
	public class ErrorViewModel
    {
		// エラーが発生したリクエストを特定するための、一意の識別ID（ログ調査用）
		public string? RequestId { get; set; }

		// 画面にリクエストIDを表示するかどうかの判定フラグ（値が空でない場合はtrue）
		public bool ShowRequestId => !string.IsNullOrEmpty(this.RequestId);
    }
}
