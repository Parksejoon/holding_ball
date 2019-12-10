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
		};

		lv1_laserPatterns = new[]
		{
			new SpecialLaserPattern(ForwayRotationLaser),
		};

		lv1_laserPatterns = new[]
		{
			new SpecialLaserPattern(ForwayRotationLaser),
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
				break;

			case 2:
				break;

			default:
			case 3:
				break;
		}
	}

	// ========================= 패턴 목록 =========================
	// ========================= 패턴 목록 =========================
	// ========================= 패턴 목록 =========================
	// 4방향
	private IEnumerator ForwayRotationLaser()
	{
		float rotationPivot = Random.Range(1, 360) % 90;
		Laser targetLaser;

		targetLaser = ObjectPoolManager.GetGameObject("Laser", transform.position, Quaternion.Euler(new Vector3(0, 0, rotationPivot + 0))).GetComponent<Laser>();
		targetLaser.rotationSpeed = 1f;

		targetLaser = ObjectPoolManager.GetGameObject("Laser", transform.position, Quaternion.Euler(new Vector3(0, 0, rotationPivot + 90))).GetComponent<Laser>();
		targetLaser.rotationSpeed = 1f;

		targetLaser = ObjectPoolManager.GetGameObject("Laser", transform.position, Quaternion.Euler(new Vector3(0, 0, rotationPivot + 180))).GetComponent<Laser>();
		targetLaser.rotationSpeed = 1f;

		targetLaser = ObjectPoolManager.GetGameObject("Laser", transform.position, Quaternion.Euler(new Vector3(0, 0, rotationPivot + 270))).GetComponent<Laser>();
		targetLaser.rotationSpeed = 1f;

		yield return null;
	}
	
}
