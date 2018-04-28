using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartFade : MonoBehaviour
{
	// 인스펙터 비노출 변수
	// 일반
	private UIManager	uiManager;              // UI 매니저
	private Image		thisImg;                // 이 이미지
	private Text		thisText;				// 이 텍스트


	// 초기화
	private void Awake()
	{
		uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
		thisImg   = GetComponent<Image>();

		if (thisImg == null)
		{
			thisText = GetComponent<Text>();
		}
	}

	// 시작
	private void OnEnable()
	{
		if (thisImg == null)
		{
			thisText.color = new Color(thisText.color.r, thisText.color.g, thisText.color.b, 0);

			uiManager.StartCoroutine(uiManager.FadeOut(thisText));
		}
		else
		{
			thisImg.color = new Color(thisImg.color.r, thisImg.color.g, thisImg.color.b, 0);

			uiManager.StartCoroutine(uiManager.FadeOut(thisImg));
		}
	}
}
