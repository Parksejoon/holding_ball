using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotLine : MonoBehaviour
{
    // 일반 변수
    public  GameManager      gameManager;              // 게임 매니저
    private GameObject       catchHolder;              // 영역에 들어온 홀더들
    private ShotLineCollider shotLineCollider;         // 슛라인 충돌검사

    // 수치
    private float            expandSpeed = 0.05f;      // 범위 확대 속도
    private float            addRange;                 // 추가되는 범위


    // 초기화
    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        shotLineCollider = GetComponent<ShotLineCollider>();

        addRange = expandSpeed * (GameManager.moveSpeed / 10);
        print(addRange);
    }

    // 프레임
    void Update()
    {
        // 점점 범위 확대
        transform.localScale += new Vector3(addRange, addRange);
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
