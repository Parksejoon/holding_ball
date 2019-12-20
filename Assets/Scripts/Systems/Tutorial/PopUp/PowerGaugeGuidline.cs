using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerGaugeGuidline : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 일반
	public Image[] gauges;              // 게이지 ui들


	// 시작
	private void Start()
	{
		StartCoroutine(AnimationCoroutine());
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

	// 애니메이션 코루틴
	private IEnumerator AnimationCoroutine()
	{
		for (int i = 0; i < 4; i++)
		{
			SetGauge(i);

			yield return new WaitForSeconds(1f);
		}

		StartCoroutine(AnimationCoroutine());
	}
}
