﻿using UnityEngine;

public class WallManager : MonoBehaviour
{
	public static WallManager instance;

	// 인스펙터 노출 변수
	// 일반
	public  GameObject			wallPrefab;	                // 벽 프리팹
	
	// 수치
	[SerializeField]
	private float				originalRotationSpeed = 1f; // 원시 회전속도

	// 인스펙터 비노출 변수
	// 일반
	private Transform			wallsTransform;             // 벽들의 트랜스폼
	private Orbit[]				orbits;						// 궤도들
	private float				rotationSpeed;				// 회전속도


	// 초기화
	private void Awake()
	{
		if (instance == null)
		{
			instance = this;				
		}

		wallsTransform  = GameObject.Find("Walls"). GetComponent<Transform>();
		orbits			= GetComponentsInChildren<Orbit>();

		rotationSpeed = originalRotationSpeed;
	}

	// 시작
	private void Start()
	{
		for (int i = 0; i < orbits.Length; i++)
		{
			orbits[i].CreateWall(Random.Range(30, 38), i + 1);
		}
	}

	// 프레임
	private void Update()
	{
		wallsTransform.Rotate(Vector3.forward * rotationSpeed * GameManager.instance.timeValue * Time.deltaTime);
	}

	// 벽 초기화
	public void InitWalls()
	{
		// 궤도 방향 변경
		foreach (Orbit orbit in orbits)
		{
			orbit.ResetRotationSpeed(1);
		}

		// 회전 방향 전환
		rotationSpeed *= -1;
	}

	// 벽들 새로 생성
	public void CreateWalls()
	{
		int level = GameManager.instance.level;

		for (int i = 0; i < Mathf.Min(2, 5 - (level / 4)); i++)
		{
			orbits[Random.Range(0, 5)].CreateWall(Random.Range(Mathf.Min(8, 13 - level / 6), Mathf.Min(12, 20 - level / 6)), orbits.Length - i);
		}
	}

	// 벽 새로 생성
	public void CreateWall(int orbitNum, int wallCount, int wallStack)
	{
		orbits[orbitNum].CreateWall(wallCount, wallStack);
	}
}
