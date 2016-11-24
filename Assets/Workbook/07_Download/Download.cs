/*

https://mobcast.atlassian.net/wiki/pages/viewpage.action?pageId=79921162

【WWWを使った通信】
Unityのスクリプトより、以下のzipファイルをダウンロードしなさい。
ただし、ローカルストレージに保存する必要はありません。
https://github.com/neuecc/UniRx/archive/5.5.0.zip


（練習問題）
https://docs.unity3d.com/ja/current/ScriptReference/WWW.html
・初めの状態は、ボタンを押すとWWW.Getを使ってダウンロードが実行され、完了時にコンソールに「WWW完了」と表示されます。確認しなさい。
・WWW.Getを使ってダウンロードしなさい。完了後、Textに「WWW完了」と表示しなさい。
・WWW.Getを使って、プログレスバー付きでダウンロードしなさい。

※練習問題で利用したスクリプトは、残しておきましょう。のちほど、WWWとObservableWWWと比較します。

（問題）
・ObservableWWW.Getを使って、ダウンロードしなさい。完了後、Textに「ObservableWWW完了」と表示しなさい。
・ObservableWWW.Getを使って、プログレスバー付きでダウンロードしなさい。
・通信中はボタンを押せない(interactable=false)ようにしなさい。

*/

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using UniRx;
using UniRx.Triggers;
using System;
using System.Linq;


public class Download : MonoBehaviour
{

	[SerializeField]Button button;
	[SerializeField]Text text;
	[SerializeField]Slider progressBar;


	// Use this for initialization
	void Start()
	{
		button.OnClickAsObservable()
			.Subscribe(_ =>
		{
			StartCoroutine(DownloadWWW("https://github.com/neuecc/UniRx/archive/5.5.0.zip"));
		});

	}

	/// <summary>
	/// urlを指定して、完了したらコンソールに「WWW完了」って表示するコルーチン
	/// </summary>
	IEnumerator DownloadWWW(string url)
	{
		Debug.Log("WWW開始");
		WWW www = new WWW(url);
		yield return www;
		Debug.Log("WWW完了: ダウンロードサイズ" + www.size);
	}
}
