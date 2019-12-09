using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMaker : MonoBehaviour
{
	public static CircleMaker instance;

	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private GameObject circlePrefab;		// 구체 프리팹

	// 수치
	[SerializeField]
	private float respawnTime;				// 재생성 시간


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
		StartCoroutine(RespawnCor());
	}

	// 구체 생성
	public void CreateCircle()
	{
		Vector2 createPos = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * Random.Range(10f, 50f);

		Instantiate(circlePrefab, createPos, Quaternion.identity, transform);
	}

	// 재생성 코루틴
	private IEnumerator RespawnCor()
	{
		while (true)
		{
			// 재생성
			CreateCircle();

			yield return new WaitForSeconds(respawnTime);
		}
	}
}
