﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public static UIManager instance;
		
	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private Button 			restartButton;          // 재시작 버튼
	[SerializeField]
	private StartManager	startManager;           // 시작 매니저
	[SerializeField]
	private CoverSlider		coverSlider;			// 커버 슬라이더
		
	// 인스펙터 비노출 변수
	// 수치
	private float			originalTimeScale;		// 원래 타임스케일 값
		
		
	// 초기화
	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	// 시작 초기화
	private void Start()
	{
		coverSlider.slideFuncs[3] = SetUIs;
	}

	// 퍼즈 체크
	public void CheckPause()
	{
		// 퍼즈 해제
		if (Time.timeScale == 0)
		{
			Continue();
		}
		// 퍼즈
		else
		{
			Pause();
		}
	}

	// 일시정지
	private void Pause()
	{
		UIEffecter.instance.SetUI(2, true);

		// 타임 스케일 저장
		originalTimeScale = Time.timeScale;

		// 정지
		Time.timeScale = 0f;
		GameManager.instance.timeValue = 0f;
	}

	// 계속하기
	private void Continue()
	{
		UIEffecter.instance.SetUI(2, false);
		
		// 타임 스케일 복구
		Time.timeScale = originalTimeScale;
		GameManager.instance.timeValue = 1f;
	}

	// 시작 UI 제거 및 시작
	public void SetUIs()
	{
		restartButton.interactable = true;
		startManager.enabled = true;
		
		StartCoroutine(StartRoutine());
	}

	// 시작버튼 클릭 루틴
	private IEnumerator StartRoutine()
	{
		UIEffecter.instance.FadeEffect(UIEffecter.instance.panels[0], new Vector2(0, 0), 0.8f, UIEffecter.FadeFlag.ALPHA | UIEffecter.FadeFlag.FINDIABL);
		UIEffecter.instance.FadeEffect(UIEffecter.instance.panels[4], new Vector2(0, 0), 0.5f, UIEffecter.FadeFlag.ALPHA | UIEffecter.FadeFlag.FINDIABL);

		yield return null;

		// 메인패널
		UIEffecter.instance.SetUI(1, true);
	}
}
