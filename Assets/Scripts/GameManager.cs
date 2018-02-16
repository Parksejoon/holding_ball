using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // 정적변수
    static public float moveSpeed;           // 움직임 속도

    // 인스펙터 노출 변수
    [SerializeField]
    private Text  scoreText;                  // 점수

    // 일반 변수
    private Ball  ball;                       // 볼
    private Touch touch;                      // 터치 구조체

    // 수치
    private float protostasisMoveSpeed = 8;   // 초기의 움직임 속도 ( 베이스 )
    private int   score = 0;                  // 점수
    private bool  isTouch;                    // 현제 터치의 상태
    private bool  previousIsTouch;            // 이전 터지의 상태
    [HideInInspector]
    public  float shotPower = 0;              // 발사 속도


    // 초기화
    void Awake()
    {
        InitializeSpeed();
        
        ball = GameObject.Find("Ball").GetComponent<Ball>();

        isTouch = false;
        previousIsTouch = false;
    }

    // 프레임
    void Update()
    {
        // 클릭 처리 ( PC )
        if (Input.GetMouseButtonDown(0))
        {
            isTouch = true;
        }

        if (!Input.GetMouseButton(0))
        {
            isTouch = false;
        }

        //// 터치 처리 ( 모바일 )
        
        //if (Input.touchCount > 0)
        //{
        //    for (int i = 0; i < Input.touchCount; i++)
        //    {
        //        touch = Input.GetTouch(i);
        //        if (touch.phase == TouchPhase.Began)
        //        {
        //            isTouch = true;

        //            break;
        //        }
        //    }
        //}

        // 홀딩 처리
        if (isTouch != previousIsTouch)
        {
            if (isTouch)
            {
                // 홀딩 처리
                HoldingBall();
            }
            else
            {
                if (ball.isHolding)
                {
                    // 언홀딩 처리
                    UnHoldingBall();
                }

            }
        }

        previousIsTouch = isTouch;
    }
    
    // 속도 초기화
    public void InitializeSpeed()
    {
        moveSpeed = protostasisMoveSpeed;
    }

    // 홀딩 처리
    void HoldingBall()
    {
        if (ball.HoldingHolder())
        {
            // 홀딩 성공
            moveSpeed = 0;
        }
        else
        {
            // *홀딩 실패
        }

        return;
    }

    // 언홀딩 처리
    void UnHoldingBall()
    {
        moveSpeed = protostasisMoveSpeed;
        ball.UnholdingHolder();

        return;
    }

    // 캐치 퍼펙트판정
    public void PerfectCatch()
    {
        shotPower = 200f;
        AddScore(2);
    }

    // 캐치 굿판정
    public void GoodCatch()
    {
        shotPower = 100f;
        AddScore(1);
    }

    // 캐치 페일판정
    public void FailCatch()
    {
        shotPower = 0f;
    }

    // 점수 상승
    public void AddScore(int upScore)
    {
        score += upScore;
        scoreText.text = score.ToString();
    }

    // 일정 시간동안 속도를 바꾸는 함수
    IEnumerator FallSpeedChangeMoment(float changeSpeed, float changeTime)
    {
        float tempSpeed = moveSpeed;


        moveSpeed = changeSpeed;

        yield return new WaitForSeconds(changeTime);

        moveSpeed = tempSpeed;
    }
}
    
