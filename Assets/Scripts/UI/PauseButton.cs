using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseButton : MonoBehaviour, IPointerClickHandler
{
	// 정적 변수
	public static bool isPause = false;				// 퍼즈 상태인지		


	// 인스펙터 비노출 변수
	// 수치
	private float originalTimeScale;				// 원래 타임스케일 값


	// 클릭
	public void OnPointerClick(PointerEventData pointerEventData)
	{
		// 퍼즈 해제
		if (isPause)
		{
			Continue();
		}
		// 퍼즈
		else
		{
			Pause();
		}
	}

	// 퍼즈
	private void Pause()
	{
		isPause = true;

		// 타임 스케일 저장
		originalTimeScale = Time.timeScale;
		
		// 정지
		Time.timeScale = 0f;
	}

	// 해제
	private void Continue()
	{
		isPause = false;

		// 타임 스케일 복구
		Time.timeScale = originalTimeScale;
	}
}
