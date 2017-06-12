using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;

namespace UniRxWorkBook.Study
{
	/// <summary>
	/// 再生すると、Cubeが回転します.
	/// </summary>
	public class Study01_Subscribe : MonoBehaviour
    {
        private void Update()
        {
			RotateCube ();
        }

        private void RotateCube()
        {
            this.transform.rotation = Quaternion.AngleAxis(1.0f, Vector3.up)*this.transform.rotation;
        }
    }
}
