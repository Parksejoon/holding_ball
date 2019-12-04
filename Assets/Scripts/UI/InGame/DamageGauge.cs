using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageGauge : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 일반
	public Image[]		gauges;				// 게이지 ui들


	// 초기화
	private void Awake()
	{
		
	}

	// 시작
	private void Start()
	{
		
	}

	// 게이지 갱신
	public void SetGauge(int count)
	{
		int i;

		for (i = 0; i < Mathf.Min(3, count); i++)
		{
			gauges[i].fillAmount = 1;
		}

		for (; i < 3; i++)
		{
			gauges[i].fillAmount = 0;
		}
	}
}
