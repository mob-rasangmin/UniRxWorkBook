/*
【Subjectを使いこなす】


以下の4つのSubjectについて、特徴を捉えましょう
* 【Subject】
    SubjectとはIObserverとIObservableの両方を実装する、Rxのストリームソースである
    OnNextを呼ぶと、下流のストリームに値を発行する

* 【BehaviorSubject】
    BehaviorSubjectは Subject に「直前の」OnNextを記憶させる機能を持たせたものである
    Subjectとの大きな違いとしては、Subscribeした瞬間に「直前の」値を発行してくる点である

* 【ReplaySubject】
    ReplaySubjectは過去のOnNextを全て記憶し、Subscribe時に全てまとめて発行する機能をもつSubjectである
    BehaviorSubjectとの違いは、BehaviorSubjectは直前の値のみをキャッシュするものであり、ReplaySubjectは過去全てをキャッシュする

* 【AsyncSubject】
    AsyncSubjectは非同期処理を模したSubjectである
    OnNextが発行されるたびに最新の値をキャッシュし、OnCompletedの実行時に最新のOnNextを１つだけ通知する


(問題)
いくつか実行のパターンを示す。各Subjectについて、この順序でボタンを押してどうなるか確認しなさい。
また、これ以外にもボタンを押す順序を変えて試行しなさい。

1. Subscribeした後に値を発行し、その後ストリームが終了する
    Subscribe → OnNext x2 → OnCompleted

2. Subscribeする前に値を複数発行する
    OnNext x2 → Subscribe → OnNext x2 → OnComplated

3. 複数Subscribeした後に値を発行し、その後ストリームが終了する
    Subscribe x2 → OnNext x2 → OnCompleted


追加問題：GameObject/UI/Dropdown より、ドロップダウンUIを追加し、4つのSubjectをソースコードの変更なしに切り替えられるようにしなさい
        また、切り替えたタイミングで、Reset() を呼び出しなさい

*/

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using UniRx;
using UniRx.Triggers;
using System;
using System.Linq;


public class Subject : MonoBehaviour
{

	[SerializeField]Button buttonToSubscribe;
	[SerializeField]Button buttonToNext;
	[SerializeField]Button buttonToComplete;
	[SerializeField]Button buttonToReset;
	[SerializeField]Text resultText;

	int onNextCount = 0;
	ISubject<int> subject;

	// Use this for initialization
	void Start ()
	{
		//subjectを初期化.
		Reset ();

		// Subscribeボタンが押されたらSubjectをSubscribeしてresultTextに表示する
		buttonToSubscribe.OnClickAsObservable ().Subscribe (_ => {
			if (subject != null) {
				Debug.Log ("OnSubscribe");
				subject.Subscribe (
					count => resultText.text += count + ", ", //OnNext
					() => resultText.text += "OnCompleted, "); //OnCompleted
			}
		});

		// OnNextボタンが押されたら今が何度目のOnNextであるかを発行する
		buttonToNext.OnClickAsObservable ().Subscribe (_ => {
			if (subject != null) {
				subject.OnNext (++onNextCount);
				Debug.Log ("OnNext : " + onNextCount);
			}
		});

		// OnCompletedボタンが押されたらOnCompletedを発行する
		buttonToComplete.OnClickAsObservable ().Subscribe (_ => {
			if (subject != null) {
				Debug.Log ("OnCompleted");
				subject.OnCompleted ();
			}
		});

		// Resetボタンが押されたら全体を初期化する
		buttonToReset.OnClickAsObservable ().Subscribe (_ => Reset());
	}

	/// <summary>
	/// Subjectリセット.
	/// </summary>
	void Reset()
	{
		if (subject != null) {
			Debug.Log ("OnCompleted (Reset)");
			subject.OnCompleted ();
		}


		//▼▼▼▼ どれか一つを有効にする ▼▼▼▼
		subject =
			new Subject<int> ();
		//			new BehaviorSubject<int> (0);
		//			new ReplaySubject<int> ();
		//			new AsyncSubject<int> ();
		//▲▲▲▲ どれか一つを有効にする ▲▲▲▲


		resultText.text = "";
		onNextCount = 0;
	}
}