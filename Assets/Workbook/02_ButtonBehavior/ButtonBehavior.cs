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

public class ButtonBehavior : MonoBehaviour {

	[SerializeField]Button buttonA;

	[SerializeField]Button buttonB;

	[SerializeField]Button buttonC;

	[SerializeField]Button buttonD;

	[SerializeField]Text text;


	// Use this for initialization
	void Start () {
		//...
	}
}
