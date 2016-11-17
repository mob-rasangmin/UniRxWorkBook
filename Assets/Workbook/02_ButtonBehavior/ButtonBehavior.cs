/*

課題2: ButtonBehavior

https://mobcast.atlassian.net/wiki/pages/viewpage.action?pageId=75432125

ボタンA〜Dを、以下のように実装しなさい。

* ボタンA    2回目まではクリックしてもなにも起こらない。
			3回目以降は、クリックされたらTextに「ボタンA起動」と表示する。

* ボタンB  	4回目にクリックしたときだけ、Textに「ボタンB起動」と表示する。
			5回目以降はクリックしてもなにも起こらない。

* ボタンC	    2秒間押し続けたとき、Textに「ボタンC起動」と表示する。

* ボタンD	    ボタンA,B,Cを全て起動したときだけ、ボタンが有効化される。
			ボタンをクリックしたとき、Textに「ボタンD起動」と表示する。

 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UniRx;
using System;
using UnityEngine.EventSystems;
using UniRx.Triggers;

public class ButtonBehavior : MonoBehaviour {

	[SerializeField]Button buttonA;

	[SerializeField]Button buttonB;

	[SerializeField]Button buttonC;

	[SerializeField]Button buttonD;

	[SerializeField]Text text;


	// Use this for initialization
	void Start () {

		//aStream
		IObservable<Unit> aStream = buttonA.OnClickAsObservable().Skip(2);
		aStream.SubscribeToText(text , str => "ボタンA起動");

		//bStream
		IObservable<Unit> bStream = buttonB.OnClickAsObservable().Skip(3).First();
		bStream.SubscribeToText(text, str => "ボタンB起動");

		//cStream
		var trigger = buttonC.gameObject.AddComponent<ObservableEventTrigger>();
		var cancelStream = trigger.OnPointerUpAsObservable()
			.Merge(trigger.OnPointerExitAsObservable());

		var longPress = trigger.OnPointerDownAsObservable()
			.Throttle(TimeSpan.FromSeconds(1))
			.TakeUntil(cancelStream)
			.RepeatUntilDestroy(gameObject);

		IObservable<PointerEventData> cStream = null;
		cStream = trigger.OnPointerClickAsObservable()
					.SkipUntil(longPress)
					.First()
					.RepeatUntilDestroy(gameObject);
		cStream.Subscribe(_ => text.text = "ボタンC起動");

		//dStream
		//ボタンの活性化チェック
		this.UpdateAsObservable()
			.SkipUntil(aStream)
			.SkipUntil(bStream)
			.SkipUntil(cStream)
			.Select(x => true)
			.StartWith(false)
			.SubscribeToInteractable(buttonD);

		buttonD.OnClickAsObservable().SubscribeToText(text, str => "ボタンD起動");
		
	}
}
