using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
	// 인스펙터 비노출 변수
	// 일반
	private	Orbit	orbit;      // 궤도


	// 초기화
	private void Awake()
	{
		orbit = transform.GetComponentInChildren<Orbit>();
	}

	// 시작
	private void Start()
	{
		
	}

	// 데미지를 받음
	public void Dealt(int damage)
	{
		// 벽 생성
		orbit.CreateWall(10);

		// 태그를 변경한 후
		gameObject.tag = "Untagged";

		// 이 스크립트 삭제
		Destroy(this);
	}
}
