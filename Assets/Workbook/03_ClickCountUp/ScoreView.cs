/*
【数値が変更された時にコールバックを実行する】
ボタンを押したらスコアが加算される機能を実装しなさい。

・UIにはゲームスタートボタンと得点テキストが表示されている
・ボタンを押すと、得点が+1される
・得点が更新されると、得点テキストが更新される

なお、この機能は「ScoreModel」「ScoreView」「ClickCountUp」の3つのクラスを利用する。
・ScoreModel: スコアを格納するクラス
・ScoreView: スコアを表示するクラス
・ClickCountUp: スコアを更新するクラス
*/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UniRx;

namespace Workbook03
{
	/// <summary>
	/// 「ScoreModel.score」が変更されると、テキストの内容を書き換えるクラス.
	/// </summary>
	public class ScoreView : MonoBehaviour
	{
		/// <summary>
		/// スコアモデル.
		/// ※本来ならシングルトンにしたほうが良いです。
		/// </summary>
		public ScoreModel scoreModel;

		/// <summary>
		/// スコアテキスト.
		/// </summary>
		public Text textScore;

		// Use this for initialization
		void Start()
		{
			scoreModel.score
				.SubscribeToText(textScore);
		}
	}
}