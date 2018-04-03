using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // 인스펙터 노출 변수
	// 일반
    [SerializeField]
	private GameObject		shotLinePrefab;         // 생성될 ShotLine 프리팹
	[SerializeField]
	private GameObject		destroyParticle;        // 공 파괴 파티클 
	[SerializeField]
	private GameObject		doubleParticle;         // 더블 파티클 

	// 인스펙터 비노출 변수
	// 일반 변수
	[HideInInspector]
	public  GameObject		bindedHolder;           // 볼이 바인딩되어있는 홀더
	[HideInInspector]
	public	bool			canDouble = true;       // 더블 샷 가능?

	private Camera			camera;					// 카메라
	private GameObject		targetHolder;			// 현재 타겟이된 홀더
    private GameObject		shotLine;               // ShotLine오브젝트
	private GameManager		gameManager;            // 게임 매니저
	private ShaderManager	shaderManager;			// 쉐이더 매니저
    private Rigidbody2D		rigidbody2d;            // 이 오브젝트의 리짓바디
	private	Transform		ballTransform;          // 공의 트랜스폼
	private bool			isBallPull = false;     // 공 당기는 상태인지
	
	// 수치
	[HideInInspector]
	public  bool			isHolding;              // 홀딩 상태를 나타냄
    [HideInInspector]
	public  float			shotPower = 3f;         // 발사 속도



    // 초기화
    private void Awake()
    {
		camera		  = GameObject.Find("Main Camera").GetComponent<Camera>();
        gameManager   = GameObject.Find("GameManager").GetComponent<GameManager>();
		shaderManager = GameObject.Find("GameManager").GetComponent<ShaderManager>();
        rigidbody2d	  = GetComponentInParent<Rigidbody2D>();
		ballTransform = transform.parent;

        isHolding = false;
    }

	// 트리거 진입
	private void OnTriggerEnter2D(Collider2D other)
	{
		// 홀더일경우 홀더에 바인딩함
		if (bindedHolder == null && (other.gameObject.tag == "Holder" || other.gameObject.tag == "PowerHolder"))
		{
			BindingHolder(other.gameObject);
        }
    }

    // 트리거 탈출
    private void OnTriggerExit2D(Collider2D other)
    {
		// 현재 바인딩중인 홀더일경우 바인딩을 해제함
		if (bindedHolder == other.gameObject && (other.gameObject.tag == "Holder" || other.gameObject.tag == "PowerHolder"))
		{
			UnbindingHolder();
		}
	}

    // 홀더에 바인딩
    private void BindingHolder(GameObject holder)
    {
		// 바인딩 홀더를 설정
        bindedHolder = holder;

		// 시간 제어
		Time.timeScale = 0.7f;
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

		// 바인딩 홀더를 초기화
		bindedHolder = null;
    }

    // 홀더에 홀딩
    public bool HoldingHolder()
    {
        // 바인딩된 홀더가 있는지 확인
        if (bindedHolder != null)
        {
			// 시간 제어
			Time.timeScale = 0.5f;

			// 속도 제어
			rigidbody2d.velocity = bindedHolder.GetComponent<Rigidbody2D>().velocity;
			
			// 홀딩 상태로 전환
			isHolding = true;

			// 슛라인 생성
			CreateShotLine();
            
            return true;
        }
        else
		{
			// 홀딩 실패	
			rigidbody2d.velocity = Vector3.Normalize(new Vector3(transform.position.x, transform.position.y)) * 15f;

			return false;
        }
    }

    // 홀더에 언홀딩
    public void UnholdingHolder()
	{
		// 홀더에서 탈출
		isHolding = false;

		// 시간 제어
		Time.timeScale = 1f;

		// 홀더만 따로 파괴된 경우
		if (bindedHolder == null)
		{
			Destroy(shotLine.gameObject);

			rigidbody2d.velocity = Vector3.Normalize(new Vector3(transform.position.x, transform.position.y)) * 10f;
		}
		// 슛라인만 따로 파괴된 경우
		else if (shotLine == null)
		{
			rigidbody2d.velocity = Vector3.Normalize(new Vector3(transform.position.x, transform.position.y)) * 15f;
		}
		// 정상 작동
		else
		{
			// 홀더 파괴
			//bindedHolder.tag = "Untagged";
			bindedHolder.GetComponent<Holder>().DestroyParticle();
			Destroy(bindedHolder.gameObject);

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
				rigidbody2d.AddForce(shotVector * shotPower * gameManager.shotPower);
			}
			// 캐치 실패
			else
			{
				rigidbody2d.velocity = Vector3.Normalize(new Vector3(transform.position.x, transform.position.y)) * 15f;
			}
		}
	}

	// 슛라인 생성
	private void CreateShotLine()
	{
		shotLine = Instantiate(shotLinePrefab, transform.position, Quaternion.identity, transform);

		if (canDouble)
		{
			shotLine.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = Color.HSVToRGB(ShaderManager.nowH, 0.3f, 1f);
		}
	}

	// 더블 샷
	public void DoubleShot(Vector3 startPos, Vector3 endPos)
	{
		if (canDouble)
		{
			// 더블 사용
			canDouble = false;

			// 파티클
			Instantiate(doubleParticle, transform.position, Quaternion.identity);

			// 물리량 대입
			rigidbody2d.velocity = Vector3.Normalize(startPos - endPos) * shotPower * -5f;

			// 쉐이더 변환
			shaderManager.BallColor(false);
		}
	}

	// 더블 초기화
	public void ResetDouble()
	{
		// 초기화
		canDouble = true;

		// 쉐이더 변환
		shaderManager.BallColor(true);
	}

	// 공 파괴
	public void BallDestroy()
	{
		Instantiate(destroyParticle, transform.position, Quaternion.identity);

		Destroy(transform.parent.gameObject);
	}

	// 공 당기기 제어
	public IEnumerator BallPullManager()
	{
		isBallPull = true;
		StartCoroutine(BallPull());

		yield return new WaitForSeconds(0.4f);

		isBallPull = false;
	}

	// 공 당기기
	private IEnumerator BallPull()
	{
		Vector2 tempVector = rigidbody2d.velocity;

		GetComponentInParent<Collider2D>().enabled = false;
		rigidbody2d.velocity = Vector2.zero;

		while (isBallPull)
		{
			ballTransform.position = Vector2.Lerp(ballTransform.position, Vector2.zero, 0.04f);

			yield return new WaitForSeconds(0.01f);
		}

		GetComponentInParent<Collider2D>().enabled = true;
		rigidbody2d.velocity = tempVector;
	}
}
