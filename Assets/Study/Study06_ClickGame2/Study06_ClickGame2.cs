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
	public class Study06_ClickGame2 : MonoBehaviour
    {
		/// <summary>
		/// 得点
		/// </summary>
		[SerializeField] Study06_ScoreModel scoreModel;
		[SerializeField] ParticleSystem particle;
		[SerializeField] Text log;

		private void OnEnable()
		{
			// スコア変更イベント登録
			scoreModel.onScoreChanged += Score_onScoreChanged;
		}

		private void OnDisable()
		{
			// スコア変更イベント削除
			scoreModel.onScoreChanged -= Score_onScoreChanged;
		}


		void Score_onScoreChanged (int score)
		{
			log.text = "Point : " + score;

			//10点とったらクリア！
			if (10 <= score)
			{
				//演出
				particle.Emit(50);

				//
				log.text = "Clear!";

				//ゲーム終了
				enabled = false;
			}
		}


		private void Update()
		{
			//マウスを押していたら
			if (Input.GetMouseButtonDown (0))
			{
				scoreModel.score++;
			}
		}
    }
}
