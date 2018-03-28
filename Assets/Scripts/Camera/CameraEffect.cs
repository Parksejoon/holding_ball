using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffect : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private UnityStandardAssets.ImageEffects.Bloom bloom;       // 번짐 효과 스크립트

	// 수치
	public int   flashCount = 30;                               // 플래쉬 단계
	public float flashPower = 0.04f;                            // 플래쉬 파워


	// 플래쉬 효과
	public void FlashBoom()
	{
		StartCoroutine(FlashOn());
	}

	// 플래쉬 증가 코루틴
	private IEnumerator FlashOn()
	{
		int counter = flashCount;

		while (counter-- > 0)
		{
			bloom.bloomIntensity += flashPower;

			yield return new WaitForSeconds(0.005f);
		}

		while (counter++ < flashCount - 1)
		{
			bloom.bloomIntensity -= flashPower;

			yield return new WaitForSeconds(0.001f);
		}
	}
}
