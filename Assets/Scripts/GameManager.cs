using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 일반 변수
    private Ball  ball;                       // 볼
    private Touch touch;                      // 터치 구조체

    // 수치
    public  float moveSpeed;                  // 움직임 속도
    private float protostasisMoveSpeed = 8;   // 초기의 움직임 속도 ( 베이스 )
    private bool  isTouch;                    // 현제 터치의 상태
    private bool  previousIsTouch;            // 이전 터지의 상태

    // 초기화
    void Awake()
    {
        previousIsTouch = false;
        isTouch = false;

        ball = GameObject.Find("Ball").GetComponent<Ball>();
        InitializeSpeed();
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

        if (isTouch != previousIsTouch)
        {
            if (isTouch)
            {
                HoldingBall();
            }
            else
            {
                UnHoldingBall();
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
        Debug.Log("aa");

        if (ball.HoldingHolder())
        {
            // 홀딩 성공
            moveSpeed = 0;
        }
        else
        {
            // 홀딩 실패
        }
    }

    // 언홀딩 처리
    void UnHoldingBall()
    {
        moveSpeed = protostasisMoveSpeed;
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
    
