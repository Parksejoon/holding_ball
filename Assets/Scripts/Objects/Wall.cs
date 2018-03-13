using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 수치
	public bool isWarWall = false;    // 게임 오버 벽인지 여부

	// 인스펙터 비노출 변수
	// 일반
	private float		health = 100;         // 체력
	private GameManager gameManager;          // 게임 매니저


	// 초기화
	private void Awake()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	// 충돌체 진입
	private void OnCollisionEnter2D(Collision2D collision)
	{
		// 볼일경우
		if (collision.gameObject.tag == "Ball")
		{
			// 벽 체력 깍임
			Vector3 ballVelo = collision.gameObject.GetComponent<Rigidbody2D>().velocity;

			AddDamage(Mathf.Abs(ballVelo.x) + Mathf.Abs(ballVelo.y));
		}

		// 홀더일경우
		if (collision.gameObject.tag == "Holder")
		{
			// 파괴
			Destroy(collision.gameObject);
		}
	}

	// 체력 감소
	private void AddDamage(float damage)
	{
		health -= damage;
		
		// 체력 0 -> 파괴
		if (health <= 0)
		{
			WallDestroy();
		}
	}

	// 벽 파괴
	private void WallDestroy()
	{
		// 게임 매니저로 전달
		gameManager.WallDestroy();

		// 워 월인지 확인
		if (isWarWall)
		{
			gameManager.GameOver();
		}

		Destroy(gameObject);
	}
}
