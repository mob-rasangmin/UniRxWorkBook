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


public class MultiStream_1 : MonoBehaviour
{
	[SerializeField]InputField inputField;
	[SerializeField]Toggle toggleA;
	[SerializeField]Toggle toggleB;
	[SerializeField]Toggle toggleC;
	[SerializeField]Toggle toggleD;

	// Use this for initialization
	void Start ()
	{
	}
}