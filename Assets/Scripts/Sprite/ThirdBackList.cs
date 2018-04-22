using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdBackList : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private GameObject	outlinePrefab;              // 외곽 장식 프리팹

	// 수치
	[SerializeField]
	private int			outlineCount;				// 외곽 장식 갯수


	// 초기화
	private void Awake()
	{
		for (int i = 0; i < outlineCount; i++)
		{
			Instantiate(outlinePrefab, transform);
		}
	}
}
