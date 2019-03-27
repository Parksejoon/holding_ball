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
	private BallParticler		ballParticler;          // 볼 파티클러
	[SerializeField]
	private Material			holderSprite;			// 홀더 스프라이트

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
	public	bool				isHolding;              // 홀딩 상태

	private int					isGhost;				// 통과 상태


	// 초기화
	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		
		rigidbody2d	= GetComponentInParent<Rigidbody2D>();
		ballInvObj	= transform.parent.gameObject;
		isHolding	= false;
		isGhost		= 0;
	}

	// 시작
	private void Start()
	{
		ResetDouble();
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
			if (!canDouble)
			{
				// 초기화 이펙트
				Instantiate(doubleParticle, transform.position, Quaternion.identity);
			}

			// 더블 초기화
			ResetDouble();
		}
	}

	// 홀딩
	public bool Holding()
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

		return true;
	}

	// 홀더에 언홀딩
	public void UnHolding()
	{
		// 홀더에서 탈출
		isHolding = false;
		
		// 슛라인만 따로 파괴된 경우
		if (shotLine == null)
		{
			Debug.Log("ShotLine is null");
		}
		// 정상 작동
		else
		{
			// 홀더 파괴
			//bindedHolder.GetComponent<Holder>().DestroyParticle();
			//Destroy(bindedHolder.gameObject);

			// 캐치 했는지 판정
			targetHolder = shotLine.GetComponent<ShotLine>().Judgment();

			// 슛라인 파괴
			Destroy(shotLine.gameObject);

			// 물리량 초기화
			rigidbody2d.velocity = Vector3.zero;
			
			// 마우스를 향해 날아감
			// 날아갈 벡터의 방향
			Vector2 shotVector = ((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position).normalized;

			Debug.Log(shotVector.x + " " + shotVector.y);
			

			// 가즈아
			rigidbody2d.AddForce(shotVector * GameManager.instance.shotPower);

			StartCoroutine(EnableDrag());
		}

		// 공 원래상태로 변화
		isGhost--;

		// 시간 제어
		Time.timeScale = 1f;
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

			Debug.Log("DoubleShot");

			// 물리량 대입
			//rigidbody2d.velocity = Vector3.Normalize(startPos - endPos) * -GameManager.instance.shotPower * 1.5f;

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

		if (canDouble)
		{
			shotLine.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<SpriteRenderer>().material = holderSprite;
		}
	}

	// 일정시간 공 통과상태
	private IEnumerator MomentInvisible()
	{
		isGhost++;

		yield return new WaitForSeconds(0.5f);

		isGhost--;
	}

	// 잠시동안 선형 저항 강화
	private IEnumerator EnableDrag()
	{
		yield return new WaitForSeconds(0.1f);

		rigidbody2d.velocity *= 0.45f;
			
	}
}
