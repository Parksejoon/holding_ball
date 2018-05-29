using System.Collections;
using UnityEngine;

namespace Particle
{
	public class SelfDestroy : MonoBehaviour
	{
		// 인스펙터 노출 변수
		// 수치
		[SerializeField]
		private float timer = 1f;                   // 몇초 후 삭제될지


		// 시작
		private void Start()
		{
			StartCoroutine(Destroyer());
		}

		// 파괴 코루틴
		private IEnumerator Destroyer()
		{
			yield return new WaitForSeconds(timer);

			Destroy(gameObject);
		}
	}
}
