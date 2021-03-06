﻿/*
【FPSカウンターを実装する】
FPS(Frame Per Second)カウンターとは、ゲームが１秒間に何フレーム更新(Update)されたかを表すものである。
（問題）
UniRxを使って、FPSカウンターを実装せよ。
ボタンを押すと、擬似的に負荷が掛かるようになっている。FPSカウンターが実際に動作することを確認せよ。
（追加問題）ボタンを押ししている間、負荷が掛かるようにUniRxを実装せよ。
（追加問題）「ボタンを押ししている間」という処理を、拡張メソッドを使って実装せよ。
*/

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using UniRx;
using UniRx.Triggers;
using System;
using System.Linq;


public class FpsCounter : MonoBehaviour
{
	[SerializeField]Text textFPS;
	[SerializeField]Button buttonLoad;
	[SerializeField][Range(1000,3000)] int loadLevel = 2000;

	// Use this for initialization
	void Start ()
	{
		//テキストにFPSを表示する.



		//ボタンを押すと、意図的に負荷(Load)をかけ、FPSを低下させる.
		//引数が大きくなるほど、大きな負荷になる.
		buttonLoad.OnClickAsObservable()
			.Subscribe (_=>Load(loadLevel));
	}

	/// <summary>
	/// 負荷(Load)をかけるメソッド.
	/// </summary>
	public void Load(int x)
	{
		string value = "";
		Enumerable.Range (0, x).ForEach (_ => value += _);

		//15000文字以上Debug.Logするとエラーになった覚えがあるので、エラー回避.
		Debug.Log (value.Substring(0,Mathf.Min(value.Length, 15000)));
	}
}

