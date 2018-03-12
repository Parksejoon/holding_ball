using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotLine : MonoBehaviour
{
	// 인스펙터 비노출 변수
    // 일반 변수
    private GameManager      gameManager;              // 게임 매니저
    private GameObject       catchHolder;              // 영역에 들어온 홀더들
    private ShotLineCollider shotLineCollider;         // 슛라인 충돌검사
	private float			 timer = 0;				   // 타이머


    // 초기화
    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        shotLineCollider = GetComponent<ShotLineCollider>();
    }

    // 프레임
    void Update()
    {
		// 타이머
		timer += Time.deltaTime;

		// 그래프 계산식
		float speedScale = (10f * (timer - 0.12f) * (timer - 0.12f)) + 0.2f;

		// 점점 범위 확대
		transform.localScale += new Vector3(speedScale, speedScale);
    } 

    // 현재 가지고있는 홀더를 반환
    public GameObject Judgment()
    {
		// 홀더 + 판정 초기화
        catchHolder = null;
        shotLineCollider.Judgment();

        // 판정 검사
        // 퍼펙트 판정
        if (shotLineCollider.perfect.Count > 0)
        {
            catchHolder = shotLineCollider.perfect[0].gameObject;
            gameManager.PerfectCatch();
        }
        // 굿 판정
        else if (shotLineCollider.good.Count > 0)
        {
            catchHolder = shotLineCollider.good[0].gameObject;
            gameManager.GoodCatch();
        }
        // 페일 판정
        else
        {
            gameManager.FailCatch();
        }

		// 캐치된 홀더 반환
        return catchHolder;
    }
}
