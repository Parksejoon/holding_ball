using UnityEngine;

public class Wall : MonoBehaviour
{
	public static bool isInvincible = false;
	public static float signValue = 1;        // 전체 회전 방향
		
	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private GameObject	collisionEffect;      // 벽 충돌 이펙트

	// 수치
	[SerializeField]
	private float		originalHealth = 350; // 원시 체력
										
	// 인스펙터 비노출 변수
	// 일반
	private Ball		ball;                 // 공
	private float		health;               // 체력
		
	// 수치
	private float		rotationSpeed;        // 회전 속도


	// 초기화
	private void Awake()
	{
		ball			= GameObject.Find("Ball").GetComponent<Ball>();

		rotationSpeed	= Random.Range(-0.5f, 0.5f);

		ResetHP();
	}

	// 시작
	private void Start()
	{
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360)));
	}

	// 매프레임
	void Update()
	{
		transform.Rotate(Vector3.forward * rotationSpeed * signValue * GameManager.instance.timeValue);
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
