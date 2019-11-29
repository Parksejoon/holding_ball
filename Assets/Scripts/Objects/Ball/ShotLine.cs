using System.Collections;
using UnityEngine;

public class ShotLine : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private GameObject		 afterEffect;              // 잔상

	// 인스펙터 비노출 변수
	// 일반 변수
	private ShotLineCollider shotLineCollider;         // 슛라인 충돌검사
	private float			 timer;					   // 타이머


	// 초기화
	private void Awake()
	{
		shotLineCollider = GetComponent<ShotLineCollider>();
	}
	
	// 시작
	private void Start()
	{
		StartCoroutine(TimeDestroy());
	}

	// 프레임
	private void Update()
	{
		// 타이머
		timer += Time.deltaTime;

		// 그래프 계산식
		float speedScale = (Mathf.Sin(timer * 7 - 1.5f) + 1) * 20f;
		//float speedScale = (4f * GameManager.instance.shotPower * (timer - 0.12f) * (timer - 0.12f)) + 0.2f;

		// 점점 범위 확대
		transform.localScale = new Vector3(speedScale, speedScale);
	} 

	// 현재 가지고있는 홀더를 반환
	public void Judgment()
	{
		// 홀더 + 판정 초기화
		shotLineCollider.Judgment();

		// 잔상효과
		GameObject tempObject = Instantiate(afterEffect, transform.position, Quaternion.identity);

		tempObject.transform.localScale = (transform.localScale) * 0.2f;
		UIEffecter.instance.FadeEffect(tempObject, Vector2.zero, 0.7f, UIEffecter.FadeFlag.ALPHA | UIEffecter.FadeFlag.FINDESTROY);
	}

	// 자동 파괴
	private IEnumerator TimeDestroy()
	{
		yield return new WaitForSeconds(1.5f);

		Destroy(gameObject);
	}
}
