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

    // 수치
    private float            expandSpeed = 1f;         // 범위 확대 속도
	private float			 power = 5f;			   // 범위 확대 범위
	

    // 초기화
    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        shotLineCollider = GetComponent<ShotLineCollider>();

		expandSpeed *= 0.1f;
    }

    // 프레임
    void Update()
    {
		// 타이머
		float speedScale = Mathf.Cos(timer * power) * expandSpeed + expandSpeed;

		timer += Time.deltaTime;

		// f(x)=cos(x*3)0.5+0.5
		// 점점 범위 확대

		transform.localScale += new Vector3(speedScale, speedScale);
    } 

    // 현재 가지고있는 홀더를 반환
    public GameObject Judgment()
    {
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

        return catchHolder;
    }
}
