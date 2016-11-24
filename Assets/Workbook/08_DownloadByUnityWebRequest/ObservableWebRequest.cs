using System;
using System.Linq;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

#if UNITY_5_4_OR_NEWER
using UnityEngine.Networking;

#else
using UnityEngine.Experimental.Networking;
#endif

#if !UniRxLibrary
using ObservableUnity = UniRx.Observable;
#endif

namespace UniRx
{
	#if !(UNITY_METRO || UNITY_WP8) && (UNITY_4_4 || UNITY_4_3 || UNITY_4_2 || UNITY_4_1 || UNITY_4_0_1 || UNITY_4_0 || UNITY_3_5 || UNITY_3_4 || UNITY_3_3 || UNITY_3_2 || UNITY_3_1 || UNITY_3_0_0 || UNITY_3_0 || UNITY_2_6_1 || UNITY_2_6)
	// Fallback for Unity versions below 4.5
	using Hash = System.Collections.Hashtable;
	using HashEntry = System.Collections.DictionaryEntry;    
	
#else
	// Unity 4.5 release notes:
	// WWW: deprecated 'WWW(string url, byte[] postData, Hashtable headers)',
	// use 'public WWW(string url, byte[] postData, Dictionary<string, string> headers)' instead.
	using Hash = System.Collections.Generic.Dictionary<string, string>;
	using HashEntry = System.Collections.Generic.KeyValuePair<string, string>;
	#endif

	public static partial class ObservableWebRequest
	{
		static UnityWebRequest CreateWebRequest(string url, byte[] postData, Hash headers)
		{
			//Create UnityWebRequest
			UnityWebRequest web = (postData != null)
				? new UnityWebRequest(url, UnityWebRequest.kHttpVerbPOST, new DownloadHandlerBuffer(), new UploadHandlerRaw(postData))
				: UnityWebRequest.Get(url);

			//Set Header
			if (headers != null)
			{
				foreach (var pair in headers)
				{
					if (!string.IsNullOrEmpty(pair.Value))
						web.SetRequestHeader(pair.Key, pair.Value);
				}
			}
			return web;
		}

		public static IObservable<UnityWebRequest> Get(string url, Hash headers = null, IProgress<float> reportProgress = null, IProgress<UnityWebRequest> webRequestProgress =null)
		{
			return ObservableUnity.FromCoroutine<UnityWebRequest>((observer, cancellation) => Fetch(CreateWebRequest(url, null, headers), observer, reportProgress, webRequestProgress, cancellation));
		}

		public static IObservable<UnityWebRequest> Post(string url, byte[] postData, Hash headers = null, IProgress<float> reportProgress = null, IProgress<UnityWebRequest> webRequestProgress = null)
		{
			return ObservableUnity.FromCoroutine<UnityWebRequest>((observer, cancellation) => Fetch(CreateWebRequest(url, postData, headers), observer, reportProgress, webRequestProgress, cancellation));
		}


		static IEnumerator Fetch(UnityWebRequest www, IObserver<UnityWebRequest> observer, IProgress<float> reportProgress, IProgress<UnityWebRequest> webRequestProgress, CancellationToken cancel)
		{
			www.Send();

			using (www)
			{
				while (!www.isDone && !cancel.IsCancellationRequested)
				{
					if (reportProgress != null)
					{
						try
						{
							reportProgress.Report(www.downloadProgress);
						}
						catch (Exception ex)
						{
							observer.OnError(ex);
							yield break;
						}
					}

					if (webRequestProgress != null)
					{
						try
						{
							webRequestProgress.Report(www);
						}
						catch (Exception ex)
						{
							observer.OnError(ex);
							yield break;
						}
					}
					yield return null;
				}

				if (cancel.IsCancellationRequested)
				{
					www.Abort();
					yield break;
				}

				if (reportProgress != null)
				{
					try
					{
						reportProgress.Report(www.downloadProgress);
					}
					catch (Exception ex)
					{
						observer.OnError(ex);
						yield break;
					}
				}

				if (www.isError)
				{
					observer.OnError(new WebRequestErrorException(www, www.responseCode, www.error));
				}
				else
				{
					observer.OnNext(www);
					observer.OnCompleted();
				}
			}
		}
	}

	public class WebRequestErrorException : Exception
	{
		public string RawErrorMessage { get; private set; }

		public string Text { get; private set; }

		public HttpStatusCode StatusCode { get; private set; }

		public Dictionary<string, string> ResponseHeaders { get; private set; }

		public UnityWebRequest webRequest { get; private set; }

		public WebRequestErrorException(UnityWebRequest www, long responseCode, string text)
		{
			this.webRequest = www;
			this.RawErrorMessage = www.error;
			this.ResponseHeaders = www.GetResponseHeaders();
			this.Text = text; 
			this.StatusCode = (HttpStatusCode)responseCode;
		}

		public override string ToString()
		{
			var text = this.Text;
			if (string.IsNullOrEmpty(text))
			{
				return RawErrorMessage;
			}
			else
			{
				return RawErrorMessage + " " + text;
			}
		}
	}
    
}