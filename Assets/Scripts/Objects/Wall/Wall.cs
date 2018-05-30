using System;
using Objects.Peripheral;
using UnityEngine;

namespace Objects.Wall
{
	public class Wall : MonoBehaviour
	{
		public static bool isInvincible = false;

		// 인스펙터 노출 변수
		// 일반
		[SerializeField]
		private GameObject	collisionEffect;      // 벽 충돌 이펙트

		// 수치
		[SerializeField]
		private float		originalHealth = 350; // 원시 체력
										
		// 인스펙터 비노출 변수
		// 일반
		private Ball.Ball		ball;                 // 공
		private float		health;				  // 체력


		// 초기화
		private void Awake()
		{
			ball		= GameObject.Find("Ball").GetComponent<Ball.Ball>();

			ResetHP();
		}

		// 충돌체 진입
		private void OnCollisionEnter2D(Collision2D collision)
		{
			// 볼일경우
			if (collision.gameObject.CompareTag("Ball"))
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
			if (collision.gameObject.CompareTag("Holder"))
			{
				// 파괴 전 홀더 검사
				GameManager.instance.HolderCheck(collision.gameObject);
				Destroy(collision.gameObject);
			}

			// 코인일경우
			if (collision.gameObject.CompareTag("Coin"))
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
			if (!isInvincible)
			{
				health -= damage;

				// 체력 0 -> 파괴
				if (health <= 0)
				{
					WallDestroy();
				}
			}
		}

		// 벽 파괴
		private void WallDestroy()
		{
			// 게임 매니저로 전달
			GameManager.instance.WallDestroy();
		}

		// 체력 초기화
		public void ResetHP()
		{
			health = originalHealth;
		}
	}
}
