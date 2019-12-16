using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
	public static Ball instance;

	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private GameObject			shotLinePrefab;         // 생성될 ShotLine 프리팹
	[SerializeField]
	private GameObject			regenParticle;          // 공 재생성 파티클 
	[SerializeField]
	private GameObject			destroyParticle;        // 공 파괴 파티클 
	[SerializeField]
	private GameObject			doubleParticle;         // 더블 파티클
	[SerializeField]
	private BallParticler		ballParticler;          // 볼 파티클러
	[SerializeField]
	private Material			holderSprite;           // 홀더 스프라이트
	[SerializeField]
	private DamageGauge			damageGauge;			// 대미지 게이지

	// 수치
	public	 float				holdingCooltime = 1f;	// 홀딩 쿨타임

	// 인스펙터 비노출 변수
	// 일반
	public	bool				canDash = true;			// 더블 샷 가능?
	[HideInInspector]
	public	Transform			parentTransform;		// 부모의 트랜스폼
	
	private GameObject			shotLine;               // ShotLine오브젝트
	private Rigidbody2D			rigidbody2d;            // 이 오브젝트의 리짓바디
	private GameObject			ballInvObj;             // 공의 물리 오브젝트

	// 수치
	[HideInInspector]
	public	bool				isHolding;              // 홀딩 상태

	private int					isGhost;                // 통과 상태
	private bool				isCool;					// 쿨다운 상태
	private int					damage;					// 현재 대미지


	// 초기화
	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}

		parentTransform = GetComponentInParent<Transform>();
		rigidbody2d		= GetComponentInParent<Rigidbody2D>();
		ballInvObj		= transform.parent.gameObject;
		isHolding		= false;
		isGhost			= 0;
		isCool			= false;
		damage			= 0;
	}

	// 시작
	private void Start()
	{
		ResetDash();
	}

	// 프레임
	private void Update()
	{
		if (isGhost >= 1)
		{
			// 공 통과상태로 변환
			ballInvObj.layer = 15;
		}
		else
		{
			// 공 통과 안됨상태로 변환
			ballInvObj.layer = 9;
		}
	}

	// 트리거 진입
	private void OnTriggerEnter2D(Collider2D other)
	{
		//// 홀더일경우 홀더에 바인딩함
		//if (bindedHolder == null && (other.gameObject.CompareTag("Holder")))
		//{
		//	BindingHolder(other.gameObject);
		//}

		// 강화 홀더일 경우 파괴하고 점수증가
		if (other.gameObject.CompareTag("PowerHolder"))
		{
			Holder otherTargetHolder = other.GetComponent<Holder>();
			
			otherTargetHolder.DestroyParticle();
			ObjectPoolManager.Release("Holder", other.gameObject);

			GameManager.instance.AddScore(otherTargetHolder.holderPower);
		}

		// 코인일 경우 코인 증가
		if (other.gameObject.CompareTag("Coin"))
		{
			Coin targetCoin = other.GetComponent<Coin>();

			targetCoin.DestroyParticle();
			Destroy(other.gameObject);

			GameManager.instance.AddCoin(1);
		}

		if (other.gameObject.CompareTag("WarWall"))
		{
#if DEBUG
			Debug.Log("GameOver");
#else
				GameManager.instance
#endif
		}

		// 레이저일 경우 게임 종료
		if other.gameObject.CompareTag("Laser"))
		{
			if (isGhost <= 0)
			{
#if DEBUG
				Debug.Log("GameOver");
#else
				GameManager.instance.GameOver();
#endif
			}
		}

		// 벽일 경우 이펙트 발생 및 대쉬 초기화, 바운스 카운트 증가
		if (other.gameObject.CompareTag("Wall"))
		{
			if (!canDash)
			{
				// 초기화 이펙트
				Instantiate(doubleParticle, transform.position, Quaternion.identity);

				// 대쉬 초기화
				ResetDash();
			}
		}

		// 구체일 경우 대미지를 줌
		if (other.gameObject.CompareTag("Circle"))
		{
			other.gameObject.GetComponent<Circle>().Dealt(damage);

			damage = 0;
			SetDamage();
		}

		// 코어일경우 대미지 증가
		if (other.gameObject.CompareTag("Core"))
		{
			damage++;
			SetDamage();
		}
	}

	// 홀딩
	public void Holding()
	{
		// 쿨다운 중인지 확인
		if (!isCool && isGhost <= 0)
		{
			// 홀딩
			isHolding = true;

			// 속도 제어
			rigidbody2d.velocity = Vector2.zero;

			// 공 통과상태로 변화
			isGhost++;

			// 슛라인 생성
			CreateShotLine();

			// 시간 제어
			Time.timeScale = 0.3f;

			if (PowerGauge.instance != null)
			{
				// 게이지 중지
				PowerGauge.instance.StopReduce();
			}
		}
		else
		{
		}
	}

	// 홀더에 언홀딩
	public void UnHolding()
	{
		// 홀딩되어있으면
		if (isHolding)
		{
			// 홀더에서 탈출
			isHolding = false;

			if (PowerGauge.instance != null)
			{
				// 게이지 재시작
				PowerGauge.instance.ReReduce();
			}

			// 슛라인만 따로 파괴된 경우를 위해 예외처리
			// 슛라인이 살아있으면
			if (shotLine != null)
			{
				// 캐치 했는지 판정
				shotLine.GetComponent<ShotLine>().Judgment();

				// 슛라인 파괴
				Destroy(shotLine.gameObject);
			}

			// 공 원래상태로 변화
			isGhost--;

			// 물리량 초기화
			rigidbody2d.velocity = Vector3.zero;

			// 터치 방향을 향해 날아감
			// 날아갈 벡터의 방향
			Vector2 shotVector = ((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position).normalized;
			rigidbody2d.AddForce(shotVector * GameManager.instance.ShotPower, ForceMode2D.Impulse);
			

			// 홀딩 쿨다운 시작
			StartCoroutine(HoldingCooldown());

			// 시간 제어
			Time.timeScale = 1f;
		}
		// 홀딩이 안되어있음
		else
		{
			// 물리량 초기화
			rigidbody2d.velocity = Vector3.zero;

			// 터치 방향을 향해 날아감
			// 날아갈 벡터의 방향
			Vector2 shotVector = ((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position).normalized;
			rigidbody2d.AddForce(shotVector * GameManager.instance.ShotPower, ForceMode2D.Impulse);

			// 홀딩 쿨다운 시작
			StartCoroutine(HoldingCooldown());
		}
	}

	// 대쉬
	public void Dash(Vector2 startPos, Vector2 endPos)
	{
		if (canDash)
		{
			// 더블 사용
			canDash = false;

			// 일정시간 벽 통과가능 상태로 변환
			StartCoroutine(MomentInvisible());

			// 파티클
			Instantiate(doubleParticle, transform.position, Quaternion.identity);

			// 쉐이더 변환
			ShaderManager.instance.ChangeBaseColor(false);

			// 물리량 초기화
			rigidbody2d.velocity = Vector3.zero;

			// 물리량 대입
			Vector2 shotVector = (startPos - endPos).normalized;
			rigidbody2d.AddForce(shotVector * -GameManager.instance.ShotPower * 1.05f, ForceMode2D.Impulse);

		}
	}

	// 대쉬 초기화
	public void ResetDash()
	{
		// 초기화
		canDash = true;
		
		// 쉐이더 변환
		ShaderManager.instance.ChangeBaseColor(true);
	}

	// 대미지 증가
	private void SetDamage()
	{
		if (damageGauge != null)
		{
			damageGauge.SetGauge(damage);
		}
	}

	// 공 파괴
	public void BallDestroy()
	{
		// 충돌체 제거 및 물리량 초기화
		GetComponent<CircleCollider2D>().enabled = false;
		rigidbody2d.velocity = Vector3.zero;
		
		// 파괴 파티클
		Instantiate(destroyParticle, transform.position, Quaternion.identity);

		// 시각화 해제
		transform.GetChild(0).gameObject.SetActive(false);
		ballParticler.SetParticle(false);
	}

	// 공 재생성
	public void RegenBall()
	{
		// 생성 파티클
		Instantiate(regenParticle, transform.position, Quaternion.identity);

		// 충돌체 등록
		GetComponent<CircleCollider2D>().enabled = true;

		// 시각화
		transform.GetChild(0).gameObject.SetActive(true);
		ballParticler.SetParticle(true);
	}

	// 슛라인 생성
	private void CreateShotLine()
	{
		shotLine = Instantiate(shotLinePrefab, transform.position, Quaternion.identity, transform);

		if (canDash)
		{
			shotLine.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<SpriteRenderer>().material = holderSprite;
		}
	}

	// 일정시간 공 무적 및 통과 상태
	private IEnumerator MomentInvisible()
	{
		isGhost++;

		yield return new WaitForSeconds(0.5f);
		Debug.Log("Over");

		isGhost--;
	}

	// 홀딩 쿨다운
	private IEnumerator HoldingCooldown()
	{
		isCool = true;

		yield return new WaitForSeconds(holdingCooltime);

		isCool = false;
	}
}
