using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Holder : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 일반
	public  ParticleSystem  destroyParticle;       // 파괴 파티클

	// 인스펙터 비노출 변수
	// 일반 변수
	private SpriteRenderer	sprite;                // 스프라이트
	private Transform		ballTransform;         // 공
	private Rigidbody2D		rigidbody2d;		   // 리지드 바디 2d

	// 수치
	[HideInInspector]
	public  int				holderPower; 	       // 홀더 파워
	[HideInInspector]
	public  float			rotationPower = 1f;    // 회전 속도
	

	// 초기화
	private void Awake()
	{
		sprite		  = transform.GetChild(0).GetComponent<SpriteRenderer>();
		rigidbody2d = GetComponent<Rigidbody2D>();
	}

	// 활성화
	private void OnEnable()
	{
		// 홀더 리스트에 추가
		HolderManager.instance.holderList.Add(transform);
	}

	// 비활성화
	private void OnDisable()
	{
		// 홀더 리스트에서 해당 항목을 삭제
		HolderManager.instance.holderList.Remove(transform);
	}


	// 삭제
	public void DeleteHolder()
	{
		// 코루틴 중지 (오류 방지)
		StopCoroutine("Destroyer");

		// 오브젝트 풀에 추가
		HolderManager.instance.objectPoolManager.PushObjectInPool(rigidbody2d);
	}

	// 파티클 효과
	public void DestroyParticle()
	{
		// 파티클 생성
		Instantiate(destroyParticle, transform.position, Quaternion.identity);
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
		if (sprite.color.a == 1)
		{
			StartCoroutine(Destroyer());
		}
	}

	// 파괴 비주얼 이펙트
	private IEnumerator Destroyer()
	{
		float range = 0.1f;
		float rangeValue = 0.02f;

		Vector3 startPoisition = transform.position;
		
		ballTransform = Ball.instance.transform;

		yield return new WaitForSeconds(Random.Range(0.8f, 1.4f));

		while (true)
		{
			transform.position = Vector3.Slerp(startPoisition, ballTransform.position, range);
			range += rangeValue;
			rangeValue += 0.001f;

			yield return new WaitForSeconds(0.01f);
		}
	}
}
