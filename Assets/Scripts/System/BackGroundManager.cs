using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundManager : MonoBehaviour
{
	// 인스펙터 비노출 변수
	// 일반
	private Transform backgroundTransform;              // 배경 트랜스폼


	// 초기화
	private void Awake()
	{
		backgroundTransform = transform;
	}

	// 크기 조절
	public void NextScale(float wallsScale)
	{
		float backSize = backgroundTransform.localScale.x;
		float scaleValue = (wallsScale) * backSize;

		backgroundTransform.localScale = new Vector3(scaleValue, 1, scaleValue);
	}
}
