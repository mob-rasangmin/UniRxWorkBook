using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UniRx;
using UniRx.Triggers;

namespace UniRxWorkBook.Study
{
	/// <summary>
	/// クリックすると得点が増えるゲームです.
	/// </summary>
	public class Study06_ClickGame2_UniRx : MonoBehaviour
    {
		/// <summary>
		/// 得点
		/// </summary>
		[SerializeField] Study06_ScoreModel_UniRx scoreModel;
		[SerializeField] ParticleSystem particle;
		[SerializeField] Text log;

		private void OnEnable()
		{

			//クリックされたらスコアを加算


			//スコアが増えたらテキスト表示を更新
			scoreModel.score
				.Select(x => "Point : " + x)
				.SubscribeToText(log)
				.AddTo(this);



			//スコアが10点になったらクリア
		}

    }
}
