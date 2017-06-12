using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;

namespace UniRxWorkBook.Study
{
	/// <summary>
	/// 再生すると、Cubeが回転します.
	/// </summary>
	public class Study01_Subscribe_UniRx : MonoBehaviour
    {
        private void Start()
        {
			this.UpdateAsObservable ()				// Updateのタイミングで通知が飛ぶ (Trigger)
				.Subscribe (_ => RotateCube() );	// 通知を受け取ったら、キューブを回転させる
        }

        private void RotateCube()
        {
            this.transform.rotation = Quaternion.AngleAxis(1.0f, Vector3.up)*this.transform.rotation;
        }
    }
}
