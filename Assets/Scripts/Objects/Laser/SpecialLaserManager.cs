using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialLaserManager : MonoBehaviour
{
	// 싱글톤
	public static SpecialLaserManager instance;

	// 델리게이트
	private delegate IEnumerator SpecialLaserPattern();			// 특수 레이저 패턴 델리게이트


	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private GameObject				laserPrefab;                // 레이저 프리팹


	// 인스펙터 비노출 변수
	// 일반
	private SpecialLaserPattern[]	lv1_laserPatterns;			// 레벨1 특수 레이저 패턴 리스트
	private SpecialLaserPattern[]	lv2_laserPatterns;			// 레벨2 특수 레이저 패턴 리스트
	private SpecialLaserPattern[]	lv3_laserPatterns;			// 레벨3 특수 레이저 패턴 리스트


	// 초기화
	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}

		// 패턴 델리게이트 초기화
		lv1_laserPatterns = new[]
		{
			new SpecialLaserPattern(ForwayRotationLaser),
			RandomwayStaticSlowLaser,
			Multiple_RotationContinuousLaser
		};

		lv2_laserPatterns = new[]
		{
			new SpecialLaserPattern(RandomwayStaticFastLaser),
			AllwayStaticLaser,
			AllwayTwoLaser
		};

		lv3_laserPatterns = new[]
		{
			new SpecialLaserPattern(RandomwayRotationMultiLaser),
			ThreewayRotationTwoLaser,
			ChaseLaser
		};
	}

	// 시작
	private void Start()
	{
		// 오브젝트 풀은 LaserManager에서 이미 설정함
	}

	// 특수패턴 사용
	public void ShotLaser()
	{
		int index = Random.Range(0, 10);

		switch (GameManager.instance.level)
		{
			case 1:
				StartCoroutine(lv1_laserPatterns[index % lv1_laserPatterns.Length]());
				break;

			case 2:
				StartCoroutine(lv2_laserPatterns[index % lv2_laserPatterns.Length]());
				break;

			default:
			case 3:
				StartCoroutine(lv3_laserPatterns[index % lv3_laserPatterns.Length]());
				break;
		}
	}

	// 위치값에서 각도값을 구하는 함수
	private float PositionToAngle(Vector2 position)
	{
		return Mathf.Atan2(position.normalized.y, position.normalized.x) * 180f / Mathf.PI;
	}

	// ================================================================
	// ================================================================
	// ========================= LV1 패턴 목록 =========================
	// 4방향 / 천천히 회전
	private IEnumerator ForwayRotationLaser()
	{
		float	rotationPivot = Random.Range(0, 90) % 90;
		int		direction = Random.Range(0, 2) == 0 ? -1 : 1;
		Laser	targetLaser;


		targetLaser = ObjectPoolManager.GetGameObject("Laser", transform.position, Quaternion.Euler(new Vector3(0, 0, rotationPivot + 0))).GetComponent<Laser>();
		targetLaser.rotationSpeed = 1f * direction;

		targetLaser = ObjectPoolManager.GetGameObject("Laser", transform.position, Quaternion.Euler(new Vector3(0, 0, rotationPivot + 90))).GetComponent<Laser>();
		targetLaser.rotationSpeed = 1f * direction;

		targetLaser = ObjectPoolManager.GetGameObject("Laser", transform.position, Quaternion.Euler(new Vector3(0, 0, rotationPivot + 180))).GetComponent<Laser>();
		targetLaser.rotationSpeed = 1f * direction;

		targetLaser = ObjectPoolManager.GetGameObject("Laser", transform.position, Quaternion.Euler(new Vector3(0, 0, rotationPivot + 270))).GetComponent<Laser>();
		targetLaser.rotationSpeed = 1f * direction;

		yield return null;
	}

	// 랜덤 6방향 / 천천히 소환
	private IEnumerator RandomwayStaticSlowLaser()
	{
		Laser targetLaser;


		for (int i = 0; i < 6; i++)
		{
			targetLaser = ObjectPoolManager.GetGameObject("Laser", transform.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360)))).GetComponent<Laser>();
			targetLaser.rotationSpeed = 0f;

			yield return new WaitForSeconds(0.5f);
		}

		yield return null;
	}

	// 돌면서 레이저 여러개 계속 소환
	private IEnumerator Multiple_RotationContinuousLaser()
	{
		StartCoroutine(RotationContinuousLaser());
		StartCoroutine(RotationContinuousLaser());
		StartCoroutine(RotationContinuousLaser());

		yield return null;
	}

	// 돌면서 레이저 계속 소환
	private IEnumerator RotationContinuousLaser()
	{
		int direction = Random.Range(0, 2) == 0 ? -1 : 1;
		Laser targetLaser;


		targetLaser = ObjectPoolManager.GetGameObject("Laser", transform.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360)))).GetComponent<Laser>();
		targetLaser.rotationSpeed = 1.5f * direction;

		for (int i = 0; i < 3; i++)
		{
			targetLaser = ObjectPoolManager.GetGameObject("Laser", transform.position, targetLaser.transform.rotation).GetComponent<Laser>();
			targetLaser.rotationSpeed = 1.5f * direction;

			yield return new WaitForSeconds(1.7f);
		}

		yield return null;
	}


	// ================================================================
	// ================================================================
	// ========================= LV2 패턴 목록 =========================

	// 랜덤 6방향 / 빠르게 소환
	private IEnumerator RandomwayStaticFastLaser()
	{
		Laser targetLaser;


		for (int i = 0; i < 13; i++)
		{
			targetLaser = ObjectPoolManager.GetGameObject("Laser", transform.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360)))).GetComponent<Laser>();
			targetLaser.rotationSpeed = 0f;

			yield return new WaitForSeconds(0.1f);
		}

		yield return null;
	}

	// 360도 36방향 / 돌면서 차례로 소환
	private IEnumerator AllwayStaticLaser()
	{
		int direction = Random.Range(0, 2) == 0 ? -1 : 1;
		Laser targetLaser;
		float pivotRotation = Random.Range(0, 30);


		for (int i = 0; i < 36; i++)
		{
			targetLaser = ObjectPoolManager.GetGameObject("Laser", transform.position, Quaternion.Euler(new Vector3(0, 0, pivotRotation + (i * 10 * direction)))).GetComponent<Laser>();
			targetLaser.rotationSpeed = 0f;

			yield return new WaitForSeconds(0.1f);
		}
	}

	// 18방향 x2 / 짧은 딜레이
	private IEnumerator AllwayTwoLaser()
	{
		Laser targetLaser;
		float pivotRotation = Random.Range(0, 30);


		for (int i = 0; i < 9; i++)
		{
			targetLaser = ObjectPoolManager.GetGameObject("Laser", transform.position, Quaternion.Euler(new Vector3(0, 0, pivotRotation + (i * 40)))).GetComponent<Laser>();
			targetLaser.rotationSpeed = 0f;
		}

		pivotRotation += 20;
		yield return new WaitForSeconds(0.5f);

		for (int i = 0; i < 9; i++)
		{
			targetLaser = ObjectPoolManager.GetGameObject("Laser", transform.position, Quaternion.Euler(new Vector3(0, 0, pivotRotation + (i * 40)))).GetComponent<Laser>();
			targetLaser.rotationSpeed = 0f;
		}
	}


	// ================================================================
	// ================================================================
	// ========================= LV3 패턴 목록 =========================

	// 랜덤 1방향 / 레이저 여러개 겹쳐서 소환
	private IEnumerator RandomwayRotationMultiLaser()
	{
		int direction = Random.Range(0, 2) == 0 ? -1 : 1;
		Laser targetLaser;


		targetLaser = ObjectPoolManager.GetGameObject("Laser", transform.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360)))).GetComponent<Laser>();
		targetLaser.rotationSpeed = 3f * direction;
		for (int i = 0; i < 4; i++)
		{
			targetLaser = ObjectPoolManager.GetGameObject("Laser", transform.position, targetLaser.transform.rotation).GetComponent<Laser>();
			targetLaser.rotationSpeed = 3f * direction;

			yield return new WaitForSeconds(0.1f);
		}
	}
	
	// 각도 맞춰서 3방향 x2 / 서로 반대로 회전
	private IEnumerator ThreewayRotationTwoLaser()
	{
		Laser targetLaser;
		float pivotRotation = Random.Range(0, 60);
		

		for (int i = 0; i < 3; i++)
		{
			targetLaser = ObjectPoolManager.GetGameObject("Laser", transform.position, Quaternion.Euler(new Vector3(0, 0, pivotRotation + (i * 120)))).GetComponent<Laser>();
			targetLaser.rotationSpeed = 2f;
		}

		pivotRotation += 60;
		
		for (int i = 0; i < 3; i++)
		{
			targetLaser = ObjectPoolManager.GetGameObject("Laser", transform.position, Quaternion.Euler(new Vector3(0, 0, pivotRotation + (i * 120)))).GetComponent<Laser>();
			targetLaser.rotationSpeed = -2f;
		}

		yield return null;
	}

	// 공 방향으로 / 여러개
	private IEnumerator ChaseLaser()
	{
		Laser targetLaser;
		
		targetLaser = ObjectPoolManager.GetGameObject("Laser", transform.position, Quaternion.Euler(new Vector3(0, 0, PositionToAngle(Ball.instance.transform.position)))).GetComponent<Laser>();
		targetLaser.rotationSpeed = 0f;

		for (int i = 0; i < 50; i++)
		{
			targetLaser.transform.rotation = Quaternion.Euler(new Vector3(0, 0, PositionToAngle(Ball.instance.transform.position)));

			yield return new WaitForSeconds(0.02f);
		}

		yield return new WaitForSeconds(0.2f);

		for (int i = 0; i < 5; i++)
		{
			targetLaser = ObjectPoolManager.GetGameObject("Laser", transform.position, Quaternion.Euler(new Vector3(0, 0, PositionToAngle(Ball.instance.transform.position)))).GetComponent<Laser>();
			targetLaser.SetStartDelay(0.4f);
			targetLaser.rotationSpeed = 0f;

			yield return new WaitForSeconds(0.1f);
		}
	}
}
