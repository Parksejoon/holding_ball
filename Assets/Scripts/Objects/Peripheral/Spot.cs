using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class Spot : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 수치
	[SerializeField]
	private float				speed;                  // 속도

	// 인스펙터 비노출 변수
	// 일반
	private Text				timerText;				// 타이머
	private GameObject			spriteObj;              // 스프라이트 오브젝트
	private SpriteRenderer		spriteRenderer;			// 스르파이트 렌더러
	private CircleCollider2D	circleCollider2D;       // 이 오브젝트의 충돌체

	// 수치
	private bool				isSpoting = false;      // 스팟에 들어와있는지
	private Color				spriteColor;


	// 초기화
	private void Awake()
	{
		timerText			= GetComponent<Text>();
		spriteObj			= transform.GetChild(0).gameObject;
		spriteRenderer		= spriteObj.GetComponent<SpriteRenderer>();
		circleCollider2D	= GetComponent<CircleCollider2D>();

		spriteColor			= spriteRenderer.color;
	}

	// 시작
	private void Start()
	{
		StartCoroutine("ReduceRoutine");
		StartCoroutine(CheckTimer());
	}

	// 트리거 진입
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Ball"))
		{
			isSpoting = true;
		}
	}

	// 트리거 탈출
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Ball"))
		{
			isSpoting = false;
		}
	}

	// 잭팟
	public void Jackpot()
	{
		// 충돌 중지
		circleCollider2D.enabled = false;

		// 코루틴 중지
		StopCoroutine("ReduceRoutine");

		// 사라짐 이펙트
		StartCoroutine(JackpotRoutine());

		// 효과 실행
		Ball.instance.ResetDouble();
		WallManager.instance.CreateWall(Random.Range(0, 5), 60, 1);
	}

	// 영역 축소 루틴
	private IEnumerator ReduceRoutine()
	{
		Vector3 originScale = transform.localScale;
		float timer = 1f;

		while (transform.localScale.x > 0)
		{
			transform.localScale = Vector2.Lerp(Vector2.zero, originScale, timer);

			timer -= 0.001f;

			yield return new WaitForSeconds(speed);
		}
		
		Destroy(gameObject);
	}

	// 잭팟 루틴
	private IEnumerator JackpotRoutine()
	{
		UIEffecter.instance.FadeEffect(spriteObj, Vector2.zero, 1f, UIEffecter.FadeFlag.ALPHA);

		yield return new WaitForSeconds(3f);

		Destroy(gameObject);
	}

	// 시간 측정 루틴
	private IEnumerator CheckTimer()
	{
		while (true)
		{
			if (isSpoting)
			{
				spriteColor.a += 0.01f;
				spriteRenderer.color = spriteColor;
				
				if (spriteColor.a >= 1f)
				{
					Jackpot();
				}
			}

			yield return new WaitForSeconds(0.1f);
		}
	}
}
