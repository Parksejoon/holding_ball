using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private GameObject	collisionEffect;      // 벽 충돌 이펙트

	// 수치
	[SerializeField]
	private float		originalHealth = 200; // 원시 체력
										
	// 인스펙터 비노출 변수
	// 일반
	private GameManager gameManager;          // 게임 매니저
	private Ball		ball;                 // 공
	private float		health;				  // 체력


	// 초기화
	private void Awake()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		ball		= GameObject.Find("Ball").GetComponent<Ball>();

		ResetHP();
	}

	// 충돌체 진입
	private void OnCollisionEnter2D(Collision2D collision)
	{
		// 볼일경우
		if (collision.gameObject.tag == "Ball")
		{
			// 파티클 효과
			Instantiate(collisionEffect, collision.contacts[0].point, Quaternion.identity);

			// 벽 체력 깍임
			Vector3 ballVelo = collision.gameObject.GetComponent<Rigidbody2D>().velocity;

			AddDamage(Mathf.Abs(ballVelo.x) + Mathf.Abs(ballVelo.y));

			// 더블 초기화
			ball.ResetDouble();
		}

		// 홀더일경우
		if (collision.gameObject.tag == "Holder")
		{
			// 홀더의 파워 검사
			Holder targetHolder = collision.gameObject.GetComponent<Holder>();

			// 파괴 전 홀더 검사
			gameManager.HolderCheck(collision.gameObject);
			Destroy(collision.gameObject);
		}

		// 코인일경우
		if (collision.gameObject.tag == "Coin")
		{
			Coin targetCoin = collision.gameObject.GetComponent<Coin>();

			if (targetCoin.bounceCount > 0)
			{
				targetCoin.bounceCount--;
			}
			else
			{
				Destroy(collision.gameObject);
			}
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
	}

	// 체력 초기화
	public void ResetHP()
	{
		health = originalHealth;
	}
}
