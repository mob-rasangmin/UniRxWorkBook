using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;

namespace UniRxWorkBook.Study
{
	/// <summary>
	/// クリックすると得点が増えるゲームです.
	/// </summary>
	public class Study06_ScoreModel_UniRx : MonoBehaviour
    {
		/// <summary>
		/// 得点
		/// </summary>
		public IntReactiveProperty score = new IntReactiveProperty();
    }
}
