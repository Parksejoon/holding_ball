using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserManager : MonoBehaviour
{
	public static LaserManager instance;

	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private GameObject	laserPrefab;                // 레이저 프리팹

	// 수치
	[SerializeField]
	private float		minDelay;					// 최소 딜레이
	[SerializeField]
	private float		maxDelay;					// 최대 딜레이


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
		StartCoroutine(LaserLoop());
	}

	// 레이저 생성
	public void CreateLaser(float speed)
	{
		Laser targetLaser = Instantiate(laserPrefab, Vector3.zero, Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360))), transform).GetComponent<Laser>();

		targetLaser.rotationSpeed = speed;
	}

	// 레이저 루틴
	private IEnumerator LaserLoop()
	{
		while (true)
		{
			yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));

			CreateLaser(1f);
		}
	}
}
