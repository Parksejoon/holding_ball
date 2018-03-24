using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holder : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 일반
	public  ParticleSystem  destroyParticle;       // 파괴 파티클

	// 인스펙터 비노출 변수
	// 일반 변수
	private HolderManager	holderManager;         // 홀더 매니저
	private SpriteRenderer	sprite;				   // 스프라이트

	// 수치
	//[HideInInspector]
	public  int				holderPower = 0;       // 홀더 파워
	[HideInInspector]
	public  float			rotationPower = 1f;    // 회전 속도

	private float			minPowr = 10f;         // 최소
	private float			maxPowr = 20f;         // 최대
	private float			timer = 0f;			   // 타이머


	// 초기화
	private void Awake()
    {
		holderManager = GameObject.Find("GameManager").GetComponent<HolderManager>();
		sprite		  = transform.GetChild(0).GetComponent<SpriteRenderer>();
	}

	// 프레임
	private void Update()
	{
		timer += Time.deltaTime * rotationPower;

		transform.rotation = Quaternion.Euler(new Vector3(0, 0, timer * 200f));
	}

	// 시작
	private void Start()
	{
		// 홀더 리스트에 추가
		holderManager.holderList.Add(transform);
	}

	// 삭제
	private void OnDestroy()
    {
        // 홀더 리스트에서 해당 항목을 삭제
        holderManager.holderList.Remove(transform);
		
		// *issue : instatiate function must not to use in OnDestroy()
		// 파티클 생성
		Instantiate(destroyParticle, transform.position, Quaternion.identity);

		// 코루틴 중지 (오류 방지)
		StopCoroutine("Destroyer");
    }

	// 랜덤 벡터
	private Vector2 RandomVec2()
	{
		// 반환 벡터
		Vector2 value;

		// 랜덤 단위벡터 * 랜덤 스칼라
		value = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
		value *= Random.Range(minPowr, maxPowr);

		return value;
	}

	// 자폭(?)
	public IEnumerator Destroyer()
	{
		float alpha = 1f;
		
		while (true)
		{
			sprite.color = new Color(1f, 1f, 1f, alpha);

			alpha = 0;

			Debug.Log(sprite.color.a);

			yield return new WaitForSeconds(0.1f);

			if (sprite.color.a <= 0)
			{
				Destroy(gameObject);
			}
		}
	}
}
