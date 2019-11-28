using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialLaserManager : MonoBehaviour
{
	// 싱글톤
	public static SpecialLaserManager instance;

	// 델리게이트
	private delegate IEnumerator SpecialLaserPattern();     // 특수 레이저 패턴 델리게이트


	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private GameObject		laserPrefab;                // 레이저 프리팹


	// 인스펙터 비노출 변수
	// 일반
	private SpecialLaserPattern[] laserPatterns;			// 특수 레이저 패턴 리스트


	// 초기화
	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	// 시작
	private void Start()
	{
		
	}


	// ========================= 패턴 목록 =========================
	// ========================= 패턴 목록 =========================
	// ========================= 패턴 목록 =========================
	// 단일 대형 레이저
	private IEnumerator BigLaser()
	{
		Vector2 position;                               // 발사 위치

		// 방향 설정 후
		position = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;

		// 거리 계산
		position *= 100;

		// 레이저 생성
		Laser targetLaser = Instantiate(laserPrefab,
			position,
			Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360))),
			transform
			).GetComponent<Laser>();

		yield return null;
	}
}
