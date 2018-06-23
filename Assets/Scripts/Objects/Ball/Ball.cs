using UnityEngine;
using System.Collections;

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
	private GameObject			collisionEffect;        // 벽 충돌 이펙트
	[SerializeField]
	private BallParticler		ballParticler;			// 볼 파티클러

	// 인스펙터 비노출 변수
	// 일반
	[HideInInspector]
	public	bool				canDouble = true;       // 더블 샷 가능?

	private GameObject			targetHolder;			// 현재 타겟이된 홀더
	private GameObject			shotLine;               // ShotLine오브젝트
	private Rigidbody2D			rigidbody2d;            // 이 오브젝트의 리짓바디
	private GameObject			ballInvObj;             // 공의 물리 오브젝트
	
	// 수치
	[HideInInspector]
	public  bool				isHolding;              // 홀딩 상태를 나타냄

	private bool				canHolding;				// 공이 홀딩 가능한지


	// 초기화
	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		
		rigidbody2d			= GetComponentInParent<Rigidbody2D>();
		ballInvObj			= transform.parent.gameObject;

		isHolding = false;

		canHolding = true;
	}

	// 시작
	private void Start()
	{
		ResetDouble();
	}

	// 트리거 진입
	private void OnTriggerEnter2D(Collider2D other)
	{
		// 강화 홀더일 경우 파괴하고 점수증가
		if (other.gameObject.CompareTag("PowerHolder"))
		{
			Holder otherTargetHolder = other.GetComponent<Holder>();
				
			otherTargetHolder.DestroyParticle();
			Destroy(other.gameObject);

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

		// 장애물일 경우 게임 종료
		if (other.gameObject.CompareTag("WarWall") || other.gameObject.CompareTag("Laser"))
		{
			//GameManager.instance.GameOver();
			Debug.Log("GameOver");
		}

		// 벽일 경우 이펙트 발생 및 더블 초기화, 바운스 카운트 증가
		if (other.gameObject.CompareTag("Wall"))
		{
			// 파티클 효과
			Instantiate(collisionEffect, transform.position, Quaternion.identity);

			// 더블 초기화
			ResetDouble();
		}
	}

	// 홀더에 언바인딩
	public void UnbindingHolder()
	{
		if (isHolding)
		{
			// 홀딩중일시 홀딩 해제
			UnholdingHolder();
		}

		// 시간 제어
		Time.timeScale = 1f;
	}

	// 홀더에 홀딩
	public void HoldingHolder()
	{
		if (canHolding)
		{
			// 홀딩 쿨다운
			StartCoroutine(HoldingCooldown());

			// 시간 제어
			Time.timeScale = 0.5f;

			// 속도 제어
			rigidbody2d.velocity = Vector3.Normalize(transform.position) * GameManager.instance.shotPower * 5;

			// 홀딩 상태로 전환
			isHolding = true;

			// 공 통과상태로 변화
			ballInvObj.layer = 15;

			// 슛라인 생성
			CreateShotLine();

			return;
		}
	}

	// 홀더에 언홀딩
	public void UnholdingHolder()
	{
		// 홀더에서 탈출
		isHolding = false;
		
		// 슛라인만 따로 파괴된 경우
		if (shotLine == null)
		{
			Penalty();
		}
		// 정상 작동
		else
		{
			// 캐치 했는지 판정
			targetHolder = shotLine.GetComponent<ShotLine>().Judgment();

			// 슛라인 파괴
			Destroy(shotLine.gameObject);

			// 물리량 초기화
			rigidbody2d.velocity = Vector3.zero;

			// 타겟 홀더를 향해 날아감
			if (targetHolder != null)
			{
				// 날아갈 벡터의 방향
				Vector3 shotVector = (targetHolder.transform.position - transform.position);
				
				// 날아갈 파워 설정
				shotVector = Vector3.Normalize(shotVector);

				// 가즈아
				rigidbody2d.AddForce(shotVector * GameManager.instance.shotPower * 750f);
			}
			// 캐치 실패
			else
			{
				Penalty();
			}
		}

		// 공 원래상태로 변화
		ballInvObj.layer = 9;
	}

	// 더블 샷
	public void DoubleShot(Vector3 startPos, Vector3 endPos)
	{
		if (canDouble)
		{
			// 더블 사용
			canDouble = false;

			// 일정시간 벽 통과가능 상태로 변환
			StartCoroutine(MomentInvisible());

			// 파티클
			Instantiate(doubleParticle, transform.position, Quaternion.identity);

			// 물리량 대입
			rigidbody2d.velocity = Vector3.Normalize(startPos - endPos) * GameManager.instance.shotPower * -15f;

			// 쉐이더 변환
			ShaderManager.instance.ChangeBaseColor(false);
		}
	}

	// 더블 초기화
	public void ResetDouble()
	{
		// 초기화
		canDouble = true;

		// 쉐이더 변환
		ShaderManager.instance.ChangeBaseColor(true);
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
		ballParticler.SetParticle(true);
	}
	
	// 슛라인 생성
	private void CreateShotLine()
	{
		shotLine = Instantiate(shotLinePrefab, transform.position, Quaternion.identity, transform);

		if (canDouble)
		{
			shotLine.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = Color.HSVToRGB(ShaderManager.themeColor[0], 0.3f, 1f);
		}
	}

	// 패널티
	private void Penalty()
	{
		rigidbody2d.velocity = Vector3.Normalize(transform.position) * GameManager.instance.shotPower * 15f;
	}

	// 일정시간 공 통과상태
	private IEnumerator MomentInvisible()
	{
		ballInvObj.layer = 15;

		yield return new WaitForSeconds(0.5f);
		
		ballInvObj.layer = 9;
	}
	
	// 차지 재사용 쿨타임
	private IEnumerator HoldingCooldown()
	{
		canHolding = false;

		yield return new WaitForSeconds(1f);

		canHolding = true;
	}
}
