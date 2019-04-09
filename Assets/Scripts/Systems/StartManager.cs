﻿using UnityEngine;

public class StartManager : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private Rigidbody2D		ballRigidbody2d;            // 공의 트랜스폼
	[SerializeField]
	private Camera			targetCamera;               // 카메라

	// 인스펙터 비노출 변수
	// 일반
	private Indexer			indexer;					// 인덱서


	// 초기화
	private void Awake()
	{
		indexer			= new Indexer();
	}

	// 프레임
	private void Update()
	{
		// 클릭 처리 ( PC )
		if (Input.GetMouseButtonDown(0))
		{
			Initialize();
			GameStart();
		}
	}
	
	// 스크립트 초기화
	private void Initialize()
	{
		GameManager.instance.enabled = true;
		LaserManager.instance.enabled = true;
		HolderManager.instance.enabled = true;
	}

	// 게임 시작
	private void GameStart()
	{
		Vector2 targetVec2;

		targetVec2 = targetCamera.ScreenToWorldPoint(Input.mousePosition);
		targetVec2 = Vector3.Normalize(targetVec2);
		targetVec2 *= 10f;
		
		ballRigidbody2d.velocity = targetVec2;

		Destroy(this);
	}
}
