using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidLineManager : MonoBehaviour
{
	public static GuidLineManager instance;

	// 인스펙터 노출 변수
	// 일반 
	[SerializeField]
	private SlideGuidLine	shopSlideGuidLine;		// 상점 슬라이드 가이드라인
	[SerializeField]
	private SlideGuidLine	startSlideGuidLine;		// 시작 슬라이드 가이드라인


	// 초기화
	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}

		// 테스트 코드
		PlayerPrefs.DeleteAll();
	}

	// 시작
	private void Start()
	{
		ShowShopGuidLine();
	}

	// 모든 가이드라인 끄기
	public void DisableAllGuidLine()
	{
		DisableShopGuidLine();
		DisableStartGuidLine();
	}

	// 상점 가이드라인 표시
	public void ShowShopGuidLine()
	{
		// 만약 첫 시작이면
		if (PlayerPrefs.GetInt("FirstStart", 1) == 1)
		{
			shopSlideGuidLine.StartAnimation();
		}
	}

	// 상점 가이드라인 끄기
	public void DisableShopGuidLine()
	{
		// 만약 첫 시작이면
		if (PlayerPrefs.GetInt("FirstStart", 1) == 1)
		{
			shopSlideGuidLine.StopAnimation();
		}
	}

	// 시작 가이드라인 표시
	public void ShowStartGuidLine()
	{
		// 만약 첫 시작이면
		if (PlayerPrefs.GetInt("FirstStart", 1) == 1)
		{
			startSlideGuidLine.StartAnimation();
		}
	}
	
	// 시작 가이드라인 끄기
	public void DisableStartGuidLine()
	{
		// 만약 첫 시작이면
		if (PlayerPrefs.GetInt("FirstStart", 1) == 1)
		{
			startSlideGuidLine.StopAnimation();
		}
	}

	// 시작시에 FirstStart 플래그를 0으로 바꿈
	public void StartSetValue()
	{
		// 첫 시작일때만
		if (PlayerPrefs.GetInt("FirstStart", 1) == 1)
		{
			//PlayerPrefs.SetInt("FirstStart", 0);
			//PlayerPrefs.Save();
		}
	}
}
