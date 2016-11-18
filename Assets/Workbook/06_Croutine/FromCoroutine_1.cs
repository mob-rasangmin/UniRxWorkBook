/*

課題6: FromCoroutine_1

https://mobcast.atlassian.net/wiki/pages/viewpage.action?pageId=75432125

Observable.FromCoroutineを使い、コルーチンをUniRx化しなさい。

* CoroutineA〜Dの、各コルーチンの動作を確認しなさい。

* ボタンAを押すと、CoroutineA を実行するように実装しなさい。
  完了後、textに「A実行完了」と表示しなさい。
  【ヒント】Observable.FromCoroutine、SelectMany

* ボタンBを押すと、CoroutineA が全て完了してから CoroutineB が開始されるように実装しなさい。
  完了後、textに「B実行完了」と表示しなさい。
  【ヒント】Concat

* ボタンCを押すと、CoroutineC を実行するように実装しなさい。
  完了後、textに「C実行完了」と表示しなさい。
  ただし、ボタンAと違い、OnNextを受け取れる形に変更し、textを書き換えなさい.
  【ヒント】Observable.FromCoroutineValue

* ボタンDを押すと、CoroutineC が全て完了してから CoroutineD が開始されるように実装しなさい。
  完了後、textに「D実行完了」と表示しなさい。
  ただし、ボタンBと違い、OnNextを受け取れる形に変更し、textを書き換えなさい.

 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UniRx;
using UniRx.Triggers;
using System;

public class FromCoroutine_1 : MonoBehaviour {

	[SerializeField]Button buttonA;
	[SerializeField]Button buttonB;
	[SerializeField]Button buttonC;
	[SerializeField]Button buttonD;

	[SerializeField]Text text;


	// Use this for initialization
	void Start () {
	}

	IEnumerator CoroutineA()
	{
		Debug.Log( "I have a pen.");
		yield return new WaitForSeconds(1);
		Debug.Log( "I have an apple.");
		yield return new WaitForSeconds(1);
		Debug.Log( "oh...");
		yield return new WaitForSeconds(1);
		Debug.Log( "Apple-pen");
		yield return new WaitForSeconds(1);
	}

	IEnumerator CoroutineB()
	{
		Debug.Log( "I have a pen.");
		yield return new WaitForSeconds(1);
		Debug.Log( "I have an apple.");
		yield return new WaitForSeconds(1);
		Debug.Log( "oh...");
		yield return new WaitForSeconds(1);
		Debug.Log( "Pineapple-pen");
		yield return new WaitForSeconds(1);
	}

	IEnumerator CoroutineC()
	{
		yield return "I have a pen.";
		yield return new WaitForSeconds(1);
		yield return "I have an apple.";
		yield return new WaitForSeconds(1);
		yield return "oh...";
		yield return new WaitForSeconds(1);
		yield return "Apple-pen";
		yield return new WaitForSeconds(1);
	}

	IEnumerator CoroutineD()
	{
		yield return "I have a pen.";
		yield return new WaitForSeconds(1);
		yield return "I have a pineapple.";
		yield return new WaitForSeconds(1);
		yield return "oh...";
		yield return new WaitForSeconds(1);
		yield return "Pineapple-pen";
		yield return new WaitForSeconds(1);
	}
}
