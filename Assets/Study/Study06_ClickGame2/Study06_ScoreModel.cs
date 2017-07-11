using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;

namespace UniRxWorkBook.Study
{
	/// <summary>
	/// クリックすると得点が増えるゲームです.
	/// </summary>
	public class Study06_ScoreModel : MonoBehaviour
    {
		/// <summary>
		/// 得点
		/// </summary>
		[SerializeField] int m_Score;

		/// <summary>
		/// スコア.
		/// </summary>
		public int score
		{
			get
			{
				return m_Score;
			}
			set
			{
				if(m_Score != value)
				{
					m_Score = value;
					if(onScoreChanged!= null)
					{
						onScoreChanged(m_Score);
					}
				}
			}
		}

		/// <summary>
		/// スコアが変更したときに呼ばれます.
		/// </summary>
		public event System.Action<int> onScoreChanged;
    }
}
