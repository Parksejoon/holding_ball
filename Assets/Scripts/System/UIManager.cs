using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public enum PanelNum
	{
		START,
		MAIN,
		END
	}

	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private GameObject[] panels;				// UI 모음
	

	// UI 온 / 오프
	public void ControlPanel(int index, bool isOn, bool withFade)
	{
		// 페이드
		if (withFade)
		{
			StartCoroutine(ControlPanelFade(index, isOn));
		}
		// 논 페이드
		else
		{
			panels[index].SetActive(isOn);
		}
	}

	// UI 페이드 온 / 오프
	private IEnumerator ControlPanelFade(int index, bool isOn)
	{
		// 페이드 인
		if (isOn)
		{

		}
		// 페이드 아웃
		else
		{

		}
	}
}
