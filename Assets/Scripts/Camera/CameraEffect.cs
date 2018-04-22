using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffect : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private UnityStandardAssets.ImageEffects.Bloom bloom;				// 번짐 효과 스크립트

	// 수치
	public int				flashCount = 30;							// 플래쉬 단계
	public float			flashPower = 0.04f;							// 플래쉬 파워
	public int				zoomCount = 30;								// 줌 단계
	public float			zoomPower = 0.01f;							// 줌 파워

	// 인스펙터 비노출 변수
	// 일반
	private Camera			camera;                                     // 카메라
	private CameraChase		cameraChase;								// 카메라 추적


	// 초기화
	private void Awake()
	{
		camera		= GetComponent<Camera>();
		cameraChase = GetComponent<CameraChase>();
	}

	// 줌인효과
	public void ZoomIn()
	{
		StartCoroutine(ZoomInCor());
	}
	
	// 줌아웃효과
	public void ZoomOut()
	{
		StartCoroutine(ZoomOutCor());

		cameraChase.NextSize();
		FlashBoom();
	}

	// 플래쉬 효과
	public void FlashBoom()
	{
		StartCoroutine(FlashBoomCor());
	}

	// 줌인 코루틴
	private IEnumerator ZoomInCor()
	{
		int counter = zoomCount;

		while (counter-- > 0)
		{
			camera.orthographicSize -= zoomPower;

			yield return new WaitForSeconds(0.01f);
		}
	}

	// 카메라 줌아웃
	private IEnumerator ZoomOutCor()
	{
		for (int i = 0; i < 50; i++)
		{
			// 카메라 줌아웃
			camera.orthographicSize += 0.1f;

			yield return new WaitForSeconds(0.01f);
		}
	}

	// 플래쉬 증가 코루틴
	private IEnumerator FlashBoomCor()
	{
		int counter = flashCount;
		float thresholdValue = 0.6f / flashCount;

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
