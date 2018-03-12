using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
	// 인스펙터 비노출 변수
	// 일반
	private float		health = 300;         // 체력
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
	}

	// 파괴시
	private void OnDestroy()
	{
		gameManager.WallDestroy();
	}

	// 체력 감소
	private void AddDamage(float damage)
	{
		health -= damage;

		if (health <= 0)
		{
			Destroy(gameObject);
		}
	}
	
}
