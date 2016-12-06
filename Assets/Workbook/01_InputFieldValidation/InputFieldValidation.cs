/*

課題1: InputFieldValidation

https://mobcast.atlassian.net/wiki/pages/viewpage.action?pageId=75432062

Buttonをクリックした時に、InputFieldで入力した値をTextに表示しなさい。ただし、以下の制約を設ける。
* InputFieldの内容が nullもしくは空文字だった場合、ボタンは押せない
* InputFieldの内容とTextの内容が同じ場合、ボタンは押せない

*/
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Diagnostics;

public class InputFieldValidation : MonoBehaviour
{
	[SerializeField]InputField inputField;

	[SerializeField]Button button;

	[SerializeField]Text text;

	void Start()
	{
		var inputFieldSteam = inputField.OnValueChangedAsObservable();

		inputFieldSteam
			.Select(str => str.Length != 0 && str != text.text)
			.StartWith(false)
			.SubscribeToInteractable(button);

		button.OnClickAsObservable()
			.Select( value => inputField.text )
			.SubscribeToText(text);

	}
}
