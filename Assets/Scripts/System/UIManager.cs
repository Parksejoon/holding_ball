using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public enum PanelNum
	{
		START,
		MAIN
	}

	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private GameObject[]	panels;					// UI 모음
	[SerializeField]
	private Image			cover;                  // 페이드 커버

	// 수치
	[SerializeField]
	private float			fadeValue = 0.03f;		// 페이드 속도
	[SerializeField]
	private float			fadeTime = 0.03f;       // 페이드 텀


	// UI 온 / 오프
	public void ControlPanel(int index, bool isOn)
	{
		panels[index].SetActive(isOn);
	}

	// 페이드 아웃
	public IEnumerator FadeOut()
	{
		float fadeAlpha = 0f;

		cover.enabled = true;
		while (cover.color.a < 1f)
		{
			cover.color = new Color(cover.color.r, cover.color.g, cover.color.b, fadeAlpha);

			fadeAlpha += fadeValue;

			yield return new WaitForSeconds(fadeTime);
		}
	}
}
