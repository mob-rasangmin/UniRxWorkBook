using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;

namespace UniRxWorkBook.Study
{
	/// <summary>
	/// クリックすると得点が増えるゲームです.
	/// </summary>
	public class Study04_ClickGame_UniRx : MonoBehaviour
    {
		/// <summary>
		/// 得点
		/// </summary>
		[SerializeField] int score;

        private void Start()
        {
			//			this.UpdateAsObservable ()						// Updateのタイミングで通知が飛ぶ (Trigger) ココからスタート！！！
        }

    }
}
