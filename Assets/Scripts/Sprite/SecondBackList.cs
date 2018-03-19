using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondBackList : MonoBehaviour
{
	// 정적 변수
	public static float timer = 0;				// 타이머

	// 인스펙터 노출 변수
	// 수치
	[SerializeField]
	private int			starCount;              // 별 갯수		
	[SerializeField]
	private float	    rotationSpeed;			// 회전 속도
	[SerializeField]
	private GameObject	starPrefab;             // 별 프리팹


	// 초기화
	private void Awake()
	{
		for (int i = 0; i < starCount; i++)
		{
			Instantiate(starPrefab, transform);
		}
	}

	// 프레임
	private void Update()
	{
		timer += Time.deltaTime;

		transform.rotation = Quaternion.Euler(0, 0, rotationSpeed * timer);
	}
}
