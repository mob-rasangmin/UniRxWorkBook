/*
【Publishを使いこなす〜その１　パスワードの入力チェックを実装する〜】

１：ToggleA,B,C,Dを以下のように実装しなさい

* ToggleA: inputFieldに大文字が１文字でも含まれていればOnになる

* ToggleB: inputFieldに数字が１文字でも含まれていればOnになる

* ToggleC: inputFieldが８文字以上であればOnになる

* ToggleD: ToggleA, B, Cが全てOnならばOnになる


２：Debugを使って、無駄なストリームが生成されていないことを確認しなさい


３：ToggleDがOnの時にintaractable=true になるようなボタンを実装しなさい
*/

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using UniRx;
using UniRx.Triggers;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using UniRx.Diagnostics;
using UnityEngine.Events;

public class MultiStream_1 : MonoBehaviour
{
	[SerializeField]InputField inputField;
	[SerializeField]Toggle toggleA;
	[SerializeField]Toggle toggleB;
	[SerializeField]Toggle toggleC;
	[SerializeField]Toggle toggleD;
	[SerializeField]Button button;

	void Start()
	{
		StartUniRx();
	}


	void NotUniRx()
	{
		toggleA.isOn = false;
		toggleB.isOn = false;
		toggleC.isOn = false;
		toggleD.isOn = false;
		button.interactable = false;

		Regex UpperRegex = new Regex(@"[A-Z]");
		Regex numberRegex = new Regex(@"[0-9]");

		inputField.onValueChanged.AddListener(str =>
		{
			toggleA.isOn = UpperRegex.IsMatch(str);
			toggleB.isOn = numberRegex.IsMatch(str);
			toggleC.isOn = str.Length > 8;
		});

		List<Toggle> toggleList = new List<Toggle>() { toggleA, toggleB, toggleC };

		toggleA.onValueChanged.AddListener(value => { toggleD.isOn = toggleList.All(toggle => toggle.isOn == true); });
		toggleB.onValueChanged.AddListener(value => { toggleD.isOn = toggleList.All(toggle => toggle.isOn == true); });
		toggleC.onValueChanged.AddListener(value => { toggleD.isOn = toggleList.All(toggle => toggle.isOn == true); });
		toggleD.onValueChanged.AddListener(value => { button.interactable = value; });
	}
	
	void StartUniRx ()
	{
		var inputFieldSteam = inputField.OnValueChangedAsObservable().Publish().RefCount();

		Regex UpperRegex = new Regex(@"[A-Z]");
		Regex numberRegex = new Regex(@"[0-9]");
		Regex strCountRegex = new Regex(".{8,}");

		var toggleAStream = inputFieldSteam.Select(str => UpperRegex.IsMatch(str)).Publish(false).RefCount();
		var toggleBStream = inputFieldSteam.Select(str => numberRegex.IsMatch(str)).Publish(false).RefCount();
		var toggleCStream = inputFieldSteam.Select(str => strCountRegex.IsMatch(str)).Publish(false).RefCount();
		var toggleDStream = Observable.CombineLatest(toggleAStream, toggleBStream, toggleCStream, (a, b, c) => a & b & c).Publish(false).RefCount();

		toggleAStream.Subscribe(Value => toggleA.isOn = Value);
		toggleBStream.Subscribe(Value => toggleB.isOn = Value);
		toggleCStream.Subscribe(Value => toggleC.isOn = Value);
		toggleDStream.Subscribe(Value => toggleD.isOn = Value);
		toggleDStream.SubscribeToInteractable(button);
	}
}

