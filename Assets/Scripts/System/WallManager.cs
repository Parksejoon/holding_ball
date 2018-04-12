using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private GameObject			wallsPrefab;                // 벽 프리팹
	[SerializeField]
	private Material			warWallsMat;                // 위험 벽 질감

	// 수치
	public  float				wallsScale = 1f;            // 벽의 크기

	[SerializeField]
	private float				originalRotationSpeed = 1f; // 원시 회전속도

	// 인스펙터 비노출 변수
	// 일반
	[HideInInspector]
	public	int					level = 0;                  // 벽 레벨
	
	private BackGroundManager	backGroundManager;			// 뒷배경 매니저
	private Transform			wallsTransform;             // 벽들의 트랜스폼
	private float				rotationSpeed;				// 회전속도


	// 초기화
	private void Awake()
	{
		backGroundManager = GameObject.Find("BackGround").GetComponent<BackGroundManager>();
		wallsTransform    = GameObject.Find("Walls"). GetComponent<Transform>();

		rotationSpeed = originalRotationSpeed;
	}

	// 프레임
	private void Update()
	{
		wallsTransform.Rotate(Vector3.forward * rotationSpeed);
	}

	// 벽 확장
	public void NextWalls()
	{
		// 벽 체력 초기화
		for (int i = 0; i < 5
			; i++)
		{
			wallsTransform.GetChild(i).GetComponent<Wall>().ResetHP();
		}
		
		// 배경 크기 설정
		backGroundManager.NextScale(wallsScale);

		// 벽 확장 코루틴 실행
		StartCoroutine(NextWallsCor());
		
		// 레벨 증가
		level++;

		if (level >= 4)
		{
			wallsScale = 1f;
		}
	}

	// 벽 확장 코루틴
	IEnumerator NextWallsCor()
	{
		float scaleValue = wallsScale * wallsTransform.localScale.x;      // 크기값 설정
		float interValue = 0f;

		Vector2 startVec2 = wallsTransform.localScale;
		Vector2 endVec2 = new Vector2(scaleValue, scaleValue);

		rotationSpeed *= -1;
		while (interValue <= 1)
		{

			wallsTransform.localScale = Vector3.Lerp(startVec2, endVec2, interValue);

			interValue += 0.1f;

			yield return new WaitForSeconds(0.05f);
		}
	}
}
