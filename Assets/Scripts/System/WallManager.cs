using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private Object				wallsPrefab;                // 벽 프리팹
	[SerializeField]
	private Material			warWallsMat;                // 위험 벽 질감

	// 수치
	public  float				wallsScale = 1f;            // 벽의 크기

	[SerializeField]
	private float				rotationSpeed = 1f;         // 회전속도

	// 인스펙터 비노출 변수
	// 일반
	[HideInInspector]
	public	int					level = 0;                  // 벽 레벨

	private BackGroundManager	backGroundManager;			// 뒷배경 매니저
	private Transform			wallsTransform;				// 벽들의 트랜스폼
	private float				timer = 0f;                 // 타이머

	// 초기화
	private void Awake()
	{
		backGroundManager = GameObject.Find("BackGround").GetComponent<BackGroundManager>();
		wallsTransform    = GameObject.Find("Walls"). GetComponent<Transform>();
	}

	// 프레임
	private void Update()
	{
		timer += Time.deltaTime;

		wallsTransform.rotation = Quaternion.Euler(0, 0, timer * rotationSpeed);
	}

	// 벽 재생성
	public void CreateWalls(int score)
	{
		// 현재 벽 크기
		float wallsSize = wallsTransform.localScale.x;
		
		// 생성
		GameObject walls = Instantiate(wallsPrefab, new Vector3(0, 0, 0), Quaternion.identity, transform) as GameObject;            // 벽 생성

		// 기존 벽을 파괴
		Destroy(wallsTransform.gameObject);

		// 초기화
		wallsTransform = walls.GetComponent<Transform>();

		// 벽 크기 및 회전량 설정
		float scaleValue = wallsScale * wallsSize;      // 크기값 설정

		wallsTransform.localScale = new Vector3(scaleValue, scaleValue);
		wallsTransform.rotation		  = Quaternion.Euler(0, 0, 0);

		rotationSpeed *= -1;

		// 배경 크기 설정
		backGroundManager.NextScale(wallsScale);

		// 월 워 설정
		for (int i = 0; i < Mathf.Min(4, level + 1); i++)
		{
			Transform targetWall = walls.transform.GetChild(i);

			targetWall.GetComponent<Wall>().ActiveWarWall();
			targetWall.GetComponent<Renderer>().material = warWallsMat;
		}
		
		// 레벨 증가
		level++;

		if (level >= 4)
		{
			wallsScale = 1f;
		}

		// 타이머 재설정
		timer = Random.Range(0, 90);
	}
}
