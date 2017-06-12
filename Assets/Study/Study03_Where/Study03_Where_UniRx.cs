using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;

namespace UniRxWorkBook.Study
{
	/// <summary>
	/// 再生すると、ボタンを押している間、Cubeが回転します.マウスが上に行くほど速く回転します.
	/// </summary>
	public class Study03_Where_UniRx : MonoBehaviour
    {
        private void Start()
        {
			this.UpdateAsObservable ()						// Updateのタイミングで通知が飛ぶ (Trigger)
				.Select(_ => Input.mousePosition.y )		// マウスY座標に変換.
				.Where(posY => Input.GetMouseButton(0) )	// マウスを押していたら通過させる.
				.Subscribe (posY => RotateCube(posY) );		// 通知を受け取ったら、キューブを回転させる
        }

		private void RotateCube(float y)
        {
			this.transform.rotation = Quaternion.AngleAxis(1.0f * y / Screen.height, Vector3.up)*this.transform.rotation;
        }
    }
}
