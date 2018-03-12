using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // 인스펙터 노출 변수
	// 일반
    [SerializeField]
	private Text		scoreText;                  // 점수
	[SerializeField]
	private int			perfectScore = 2;		    // 퍼펙트시 추가 점수
	[SerializeField]
	private int			goodScore = 1;		        // 굿시 추가 점수
	[SerializeField]
	private int			failScore = 0;              // 페일시 추가 점수
	[SerializeField]
	private float		perfectPower = 200f;        // 퍼펙트시 슛 파워
	[SerializeField]
	private float		goodPower = 100f;           // 굿시 슛 파워
	[SerializeField]
	private float		failPower = 0;              // 페일시 슛 파워

	// 인스펙터 비노출 변수
	// 일반
	private WallManager wallManager;				// 월 매니저
	private Ball		ball;                       // 볼
    private Touch		touch;                      // 터치 구조체

	// 수치
	[HideInInspector]
	public  float	    shotPower = 0;              // 발사 속도

	private float		protostasisMoveSpeed = 8;   // 초기의 움직임 속도 ( 베이스 )
    private int			score = 0;                  // 점수
    private bool		isTouch;                    // 현제 터치의 상태
    private bool		previousIsTouch;            // 이전 터지의 상태
	private int			level = 1;				    // 레벨


    // 초기화
    void Awake()
    {
		wallManager = GameObject.Find("WallManager").GetComponent<WallManager>();
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

    // 홀딩 처리
    void HoldingBall()
    {
        if (ball.HoldingHolder())
        {
            // 홀딩 성공
			// *Score 처리*
        }
        else
        {
            // 홀딩 실패
			// *Gauge 및 기타 처리*
        }

        return;
    }

    // 언홀딩 처리
    void UnHoldingBall()
    {
        ball.UnholdingHolder();

        return;
    }

    // 캐치 퍼펙트판정
    public void PerfectCatch()
    {
        shotPower = perfectPower;
        AddScore(perfectScore);
    }

    // 캐치 굿판정
    public void GoodCatch()
    {
        shotPower = goodPower;
        AddScore(goodScore);
    }

    // 캐치 페일판정
    public void FailCatch()
    {
        shotPower = failPower;
		AddScore(failScore);
    }

    // 점수 상승
    public void AddScore(int upScore)
    {
        score += upScore;
        scoreText.text = score.ToString();
    }

	// 벽 파괴
	public void WallDestroy()
	{
		// 월 매니저로 벽 새로생성 요청
		wallManager.CreateWalls(level + score);
	}
}
    
