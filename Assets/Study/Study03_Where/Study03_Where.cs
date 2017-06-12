using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;

namespace UniRxWorkBook.Study
{
	/// <summary>
	/// 再生すると、ボタンを押している間、Cubeが回転します.マウスが上に行くほど速く回転します.
	/// </summary>
	public class Study03_Where : MonoBehaviour
    {
        private void Update()
        {
			//マウスを押していたら
			if (Input.GetMouseButton (0))
			{
				RotateCube (Input.mousePosition.y);
			}
        }

		private void RotateCube(float y)
		{
			this.transform.rotation = Quaternion.AngleAxis(1.0f * y / Screen.height, Vector3.up)*this.transform.rotation;
		}
    }
}
