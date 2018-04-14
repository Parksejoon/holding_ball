using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarWall : MonoBehaviour
{
	public static float	signValue = 1;			// 전체 회전 방향

	// 인스펙터 비노출 변수
	// 일반
	private GameManager gameManager;            // 게임매니저

	// 수치
	private float		rotationSpeed;			// 회전 속도


	// 초기화
	private void Awake()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		rotationSpeed = Random.Range(-0.5f, 0.5f);
	}

	// 시작
	private void Start()
	{
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360)));
	}

	// 매프레임
	void Update()
	{
		transform.Rotate(Vector3.forward * rotationSpeed * signValue);
	}
}
