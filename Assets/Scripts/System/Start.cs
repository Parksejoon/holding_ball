using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start : MonoBehaviour
{
	// 인스펙터 비노출 변수
	// 일반
	private GameManager		gameManager;				// 게임 매니저
	private HolderManager	holderManager;				// 홀더 매니저
	private Rigidbody2D		ballRigidbody2d;			// 공의 트랜스폼
	private Camera			camera;                     // 카메라


	// 초기화
	private void Awake()
	{
		gameManager     = GetComponent<GameManager>();
		holderManager   = GetComponent<HolderManager>();
		ballRigidbody2d = GameObject.Find("BallCollider").GetComponent<Rigidbody2D>();
		camera		    = GameObject.Find("Main Camera").GetComponent<Camera>();
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
		gameManager.enabled = true;
		holderManager.enabled = true;
	}

	// 게임 시작
	private void GameStart()
	{
		Vector2 targetVec2;

		targetVec2 = camera.ScreenToWorldPoint(Input.mousePosition);
		targetVec2 = Vector3.Normalize(targetVec2);
		targetVec2 *= 10f;
		
		ballRigidbody2d.velocity = targetVec2;

		Destroy(this);
	}
}
