/*
【HotとColdを理解する】
http://qiita.com/toRisouP/items/f6088963037bfda658d3

スクリプトをHot変換しなさい。
*/

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using UniRx;
using UniRx.Triggers;
using System;
using System.Linq;


public class HotAndCold : MonoBehaviour
{

	[SerializeField]Button button;
	[SerializeField]Text textA;
	[SerializeField]Text textB;


	// Use this for initialization
	void Start()
	{
		button.OnClickAsObservable()
			.Delay(TimeSpan.FromSeconds(1))
			.Select(_ => "Clicked!")
			.SubscribeToText(textA);

		button.OnClickAsObservable()
			.Delay(TimeSpan.FromSeconds(1))
			.Select(_ => "Clicked!")
			.SubscribeToText(textB);
	}
}
