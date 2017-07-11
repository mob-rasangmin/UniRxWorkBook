using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UniRx;
using UniRx.Triggers;
using UniRx.Diagnostics;

namespace UniRxWorkBook.Study
{
	public class Study05_ReactiveProperty : MonoBehaviour
	{
		public IntReactiveProperty intReactiveProperty = new IntReactiveProperty();
		public StringReactiveProperty stringReactiveProperty = new StringReactiveProperty();

		public Text log;


		public ReactiveProperty<int> intReactivePropertyGeneric = new ReactiveProperty<int>();



		void Start()
		{
			Observable.CombineLatest
				(
					intReactiveProperty,
					stringReactiveProperty,
					(number, str) => str + " : " + number
				)
				.Debug("CombineLatest")	//.Debugはログを取るためのオペレータ。値自体に何も操作しない。
				.SubscribeToText(log)
				.AddTo(this);
		}


		public void SetNumber(int value)
		{
			intReactiveProperty.Value = value;
		}


		public void SetString(string value)
		{
			stringReactiveProperty.Value = value;
		}
	}
}
