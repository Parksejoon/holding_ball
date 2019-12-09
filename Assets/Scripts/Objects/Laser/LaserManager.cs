﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserManager : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private GameObject	laserPrefab;                // 레이저 프리팹

	// 수치
	public int			amountPerShot = 1;			// 한 번 발사시 나가는 양

	[SerializeField]
	private float		minDelay;					// 최소 딜레이
	[SerializeField]
	private float		maxDelay;					// 최대 딜레이


	// 초기화
	private void Awake()
	{
	}

	// 시작
	private void Start()
	{
		StartCoroutine(LaserLoop());
	}

	// 레이저 생성
	public void CreateLaser(float speed)
	{
		Laser targetLaser = Instantiate(laserPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360))), transform).GetComponent<Laser>();

		targetLaser.rotationSpeed = speed;
	}

	// 레이저 루틴
	private IEnumerator LaserLoop()
	{
		while (true)
		{
			yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
			//yield return new WaitForSeconds(1f);

			for (int i = 0; i < amountPerShot; i++)
			{
				//CreateLaser(Mathf.Min(GameManager.instance.level / 5f, 0.8f));
				CreateLaser(0);
				
				yield return new WaitForSeconds(0.2f);
			}

			//amountPerShot = Mathf.Min((GameManager.instance.level / 3) + 1, 4);
			amountPerShot = 1;
		}
	}
}