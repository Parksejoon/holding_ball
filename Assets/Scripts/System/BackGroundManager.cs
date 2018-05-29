using System.Collections;
using UnityEngine;

namespace System
{
	public class BackGroundManager : MonoBehaviour
	{
		// 인스펙터 비노출 변수
		// 일반
		private Transform backgroundTransform;              // 배경 트랜스폼


		// 초기화
		private void Awake()
		{
			backgroundTransform = transform;
		}

		// 크기 조절
		public void NextScale(float wallsScale)
		{
			float backSize = backgroundTransform.localScale.x;
			float scaleValue = wallsScale * backSize;

			StartCoroutine(NextScaleCor(backgroundTransform.localScale, new Vector3(scaleValue, 1, scaleValue)));
		}

		// 크기 조절 코루틴
		private IEnumerator NextScaleCor(Vector3 startScale, Vector3 endScale)
		{
			float scaleValue = 0;

			while (scaleValue < 1)
			{
				backgroundTransform.localScale = Vector3.Lerp(startScale, endScale, scaleValue);

				scaleValue += 0.05f;

				yield return new WaitForSeconds(0.05f);
			}
		}
	}
}
