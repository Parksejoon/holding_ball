using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holder : MonoBehaviour
{
	// 인스펙터 비노출 변수
	// 일반 변수
    private HolderManager holderManager;        // 홀더 매니저

	// 수치
	private float		  minPowr = 10f;        // 최소
	private float		  maxPowr = 20f;        // 최대
	private float		  timer = 0f;			// 타이머


	// 초기화
	private void Awake()
    {
		holderManager = GameObject.Find("GameManager").GetComponent<HolderManager>();
	}

	// 프레임
	private void Update()
	{
		timer += Time.deltaTime;

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
}
