using System;
using System.Collections;
using Objects.Wall;
using UnityEngine;

namespace Systems.InGameSystems
{
	public class WallManager : MonoBehaviour
	{
		public static WallManager instance;
		
		// 인스펙터 노출 변수
		// 일반
		[SerializeField]
		private GameObject			warWallPrefab;				// 위험 벽 프리팹
		[SerializeField]
		private Material			warWallsMat;                // 위험 벽 질감
	
		// 수치
		public  float				wallsScale = 1f;            // 벽의 크기

		[SerializeField]
		private float				originalRotationSpeed = 1f; // 원시 회전속도

		// 인스펙터 비노출 변수
		// 일반
		[HideInInspector]
		public	int					level;	                    // 벽 레벨
	
		private BackGroundManager	backGroundManager;		 	// 뒷배경 매니저
		private Transform			wallsTransform;             // 벽들의 트랜스폼
		private Transform			warWallsTransform;			// 위험벽들의 트랜스폼
		private float				rotationSpeed;				// 회전속도


		// 초기화
		private void Awake()
		{
			if (instance == null)
			{
				instance = this;				
			}
			
			backGroundManager = GameObject.Find("BackGround").GetComponent<BackGroundManager>();
			wallsTransform    = GameObject.Find("Walls"). GetComponent<Transform>();
			warWallsTransform = GameObject.Find("WarWalls").GetComponent<Transform>();

			rotationSpeed = originalRotationSpeed;
		}

		// 프레임
		private void Update()
		{
			wallsTransform.Rotate(Vector3.forward * rotationSpeed * GameManager.instance.timeValue);
		}

		// 끝날때
		private void OnDestroy()
		{
			Color warWallColor = warWallsMat.GetColor("_Color");

			warWallsMat.SetColor("_Color", new Color(warWallColor.r, warWallColor.g, warWallColor.b, 1f));
		}

		// 벽 확장
		public void NextWalls()
		{
			// 벽 체력 초기화
			for (int i = 0; i < 5; i++)
			{
				wallsTransform.GetChild(i).GetComponent<Wall>().ResetHP();
			}
		
			// 배경 크기 설정
			backGroundManager.NextScale(wallsScale);

			// 전체 코루틴 실행
			StartCoroutine(NextWallsCor());

			if (level < 4)
			{
				StartCoroutine(WarWallMatFade(wallsTransform.localScale * wallsScale * 1.5f));
			}

			// 레벨 증가
			level++;

			if (level >= 4)
			{
				wallsScale = 1f;
			}
			else
			{
				WarWall.signValue *= 1.1f;
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
			WarWall.signValue *= -1;
			while (interValue <= 1)
			{
				wallsTransform.localScale = Vector3.Lerp(startVec2, endVec2, interValue);

				interValue += 0.01f;

				yield return new WaitForSeconds(0.005f);
			}
		}

		// 위험 벽 페이드 코루틴
		IEnumerator WarWallMatFade(Vector3 wallScale)
		{
			Wall.isInvincible = true;

			float alpha = 1f;
			Color warWallColor = warWallsMat.GetColor("_Color");


			// 벽 안보이게
			while (alpha >= 0)
			{
				alpha -= 0.05f;

				warWallsMat.SetColor("_Color", new Color(warWallColor.r, warWallColor.g, warWallColor.b, alpha));

				yield return new WaitForSeconds(0.05f);
			}
		

			// 새 벽 생성
			yield return new WaitForSeconds(0.15f);

			GameObject target = Instantiate(warWallPrefab, Vector3.zero, Quaternion.identity, warWallsTransform);

			target.transform.localScale = wallScale;

			yield return new WaitForSeconds(0.15f);


			// 벽 보이게
			while (alpha <= 1)
			{
				alpha += 0.08f;

				warWallsMat.SetColor("_Color", new Color(warWallColor.r, warWallColor.g, warWallColor.b, alpha));

				yield return new WaitForSeconds(0.05f);
			}

			yield return new WaitForSeconds(1f);

			Wall.isInvincible = false;
		}
	}
}
