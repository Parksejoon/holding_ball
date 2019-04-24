using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Holder : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 일반
	public  ParticleSystem  destroyParticle;			// 파괴 파티클

	[SerializeField]
	private ParticleSystem	originDestroyParticle;		// 기본 파괴 파티클

	// 인스펙터 비노출 변수
	// 일반 변수
	private SpriteRenderer	sprite;             // 스프라이트
	private Transform		ballTransform;      // 공
	private Coroutine		coroutine;			// 코루틴

	// 수치
	[HideInInspector]
	public  int				holderPower;            // 홀더 파워

	private Vector2			velo;					// 속도
	private bool			firstDisable = true;    // 첫 초기화
	private bool			isDestroying = false;	// 파괴 진행중
	

	// 초기화
	private void Awake()
	{
		sprite	= transform.GetChild(0).GetComponent<SpriteRenderer>();
	}

	// 프레임
	private void Update()
	{
		transform.Translate(velo * Time.smoothDeltaTime);
	}

	// 삭제
	private void OnDisable()
	{
		if (firstDisable)
		{
			firstDisable = false;
		}
		else
		{
			// 홀더 리스트에서 해당 항목을 삭제
			HolderManager.instance.holderList.Remove(transform);
			velo = Vector2.zero;

			if (coroutine != null)
			{
				// 코루틴 중지 (오류 방지)
				StopCoroutine(coroutine);
			}
		}
	}

	// 생성
	private void OnEnable()
	{
		Initialize();
	}

	// 초기화
	private void Initialize()
	{
		HolderManager.instance.holderList.Add(transform);

		isDestroying = false;
		destroyParticle = originDestroyParticle;
		holderPower = 0;
		gameObject.tag = "Holder";
		sprite.color = Color.white;
		transform.GetChild(0).gameObject.GetComponent<Renderer>().material = HolderManager.instance.originHolderMat;
	}

	// 파티클 효과
	public void DestroyParticle()
	{
		// 파티클 생성
		Instantiate(destroyParticle, transform.position, Quaternion.identity);
	}

	// 속도 지정
	public void SetVelo(Vector2 value)
	{
		velo = value;
	}

	// 랜덤 벡터
	private Vector2 RandomVec2()
	{
		// 반환 벡터
		Vector2 value;

		value = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));

		return value;
	}

	// 파괴 시작
	public void StartDestroyer()
	{
		// 파괴 진행중이 아닐때만 코루틴 실행
		if (!isDestroying && gameObject.activeInHierarchy)
		{
			coroutine = StartCoroutine(Destroyer());
		}
	}

	// 파괴 비주얼 이펙트
	private IEnumerator Destroyer()
	{
		float range = 0.1f;
		float rangeValue = 0.02f;

		isDestroying = true;
	
		Vector3 startPoisition = transform.position;

		yield return new WaitForSeconds(Random.Range(0.8f, 1.2f));

		while (true)
		{
			transform.position = Vector3.Slerp(startPoisition, Ball.instance.parentTransform.position, range);
			range += rangeValue;
			rangeValue += 0.01f;

			yield return new WaitForSeconds(0.01f);
		}
	}
}
