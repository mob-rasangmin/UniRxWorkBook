/*

Confluence : ヒント、回答等はこちら
https://mobcast.atlassian.net/wiki/pages/viewpage.action?pageId=80445442


【UnityWebRequestを使った通信】
Unityのスクリプトより、以下のzipファイルをダウンロードしなさい。
ただし、ローカルストレージに保存する必要はありません。
https://github.com/neuecc/UniRx/archive/5.5.0.zip


（練習問題）
https://docs.unity3d.com/ja/current/Manual/UnityWebRequest.html
https://docs.unity3d.com/ja/current/ScriptReference/Experimental.Networking.UnityWebRequest.html
・初めの状態は、ボタンを押すとUnityWebRequest.Getを使ってダウンロードが実行され、完了時にコンソールに「WebRequest完了」と表示されます。確認しなさい。
　　・★WWWと比較し、使い方の違いを把握しましょう。
・UnityWebRequest.Getを使ってダウンロードしなさい。完了後、Textに「WebRequest完了」と表示しなさい。
・UnityWebRequest.Getを使って、プログレスバー付きでダウンロードしなさい。
・UnityWebRequest.Getを使って、Textに「現在のダウンロード済みサイズ」を表示しなさい。

※練習問題で利用したスクリプトは、残しておきましょう。のちほど、UnityWebRequestとObservableWebRequestを比較します。


（問題）
・ObservableWebRequest.Getを使って、ダウンロードしなさい。完了後、Textに「ObservableWebRequest完了」と表示しなさい。
・ObservableWebRequest.Getを使って、プログレスバー付きでダウンロードしなさい。
・ObservableWebRequest.Getを使って、Textに「現在のダウンロード済みサイズ」を表示しなさい。
・通信中はボタンを押せない(interactable=false)ようにしなさい。


（注意）
WebRequestは、通信が完了するまでレスポンスヘッダーを取得できません（WWWと同様）。

*/

using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using System;
using System.Linq;
using UniRx.Diagnostics;


public class DownloadByUnityWebRequest : MonoBehaviour
{

	[SerializeField]Button downLoadButton;
	[SerializeField]Button cancelButton;
	[SerializeField]Text text;
	[SerializeField]Slider progressBar;

	private string url = "https://github.com/neuecc/UniRx/archive/5.5.0.zip";

	// Use this for initialization
	void Start()
	{
		var progressNotifier = new ScheduledNotifier<UnityWebRequest>();
		progressNotifier.Subscribe(x => 
		{
			progressBar.value = x.downloadProgress;
			text.text = x.downloadedBytes.ToString();
		});

		IDisposable webRequestStream = null;
		downLoadButton.OnClickAsObservable()
			.Do(action => text.text = "WebRequest開始")
			.Do(action => ButtonChange(true))
			.Subscribe(_ =>
			{
				webRequestStream = ObservableWebRequest.Get((url), null, null, progressNotifier)
				.Do(action => ButtonChange(false))
				.DoOnCancel(() => ButtonChange(false))
				.SubscribeToText(text, unityWebRequest => "WebRequest完了\nダウンロードサイズ : " + unityWebRequest.downloadedBytes);
			});
		
		cancelButton.OnClickAsObservable()
			.Where(_ => webRequestStream != null)
			.Subscribe(x => webRequestStream.Dispose() );
	}

	private void ButtonChange(bool downLoadOn)
	{
		downLoadButton.interactable = !downLoadOn;
		cancelButton.interactable = downLoadOn;

		if (downLoadOn == false)
			progressBar.value = 0;
		
	}


	/// <summary>
	/// urlを指定して、完了したらコンソールに「WebRequest完了」って表示するコルーチン
	/// </summary>
	IEnumerator DownloadWebRequest(string url)
	{
		Debug.Log("WebRequest開始");
		UnityWebRequest web = UnityWebRequest.Get(url);
		yield return web.Send();
	
		Debug.Log("WebRequest完了: ダウンロードサイズ" + web.downloadedBytes);
	}
}
