using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holder : MonoBehaviour
{
	// 인스펙터 비노출 변수
	// 일반 변수
	private GameManager   gameManager;          // 게임 매니저 
    private HolderManager holderManager;        // 홀더 매니저
	private Rigidbody2D	  rigidbody2d;          // 리지드바디 2d

	// 수치
	private float minPowr = 10;         // 최소
	private float maxPowr = 20;         // 최대


	// 초기화
	void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		holderManager = GameObject.Find("HolderManager").GetComponent<HolderManager>();
		rigidbody2d = GetComponent<Rigidbody2D>();
	}

	// 시작
	private void Start()
	{
		rigidbody2d.velocity = RandomVec2();
	}

	// 삭제
	private void OnDestroy()
    {
        // 홀더 리스트에서 해당 항목을 삭제
        //holderManager.holderList.RemoveAt(0);
    }

	// 랜덤 벡터
	private Vector2 RandomVec2()
	{
		Vector2 value;

		// 랜덤 정규백터 * 랜덤 스칼라
		value = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
		value *= Random.Range(minPowr, maxPowr);

		return value;
	}
}
