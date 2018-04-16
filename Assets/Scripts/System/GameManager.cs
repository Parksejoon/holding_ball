using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // 인스펙터 노출 변수
	// 일반
    [SerializeField]
	private Text			scoreText;                  // 점수
	[SerializeField]
	private Text			coinText;					// 코인	
	
	// 수치
	[SerializeField]
	private float			levelTimer = 1f;			// 레벨 타이머

	// 인스펙터 비노출 변수
	// 일반
	private WallManager		wallManager;				// 월 매니저
	private Ball			ball;                       // 볼
    private Touch			touch;                      // 터치 구조체
	private CameraEffect	cameraEffect;               // 카메라 이펙트
	private UIManager		uiManager;                  // UI 매니저
	private Parser			parser;						// 데이터 파서
	private int				wallCount = 0;				// 벽 카운트

	// 수치
	[HideInInspector]
	public  float			shotPower = 10;             // 발사 속도
	[HideInInspector]
	public  bool			isTouch;                    // 현제 터치의 상태
	
	private int				score = 0;                  // 점수
	private int				bestScore;					// 최고점수
	private int				coin;						// 코인
	private int				level = 0;					// 레벨
    private bool			previousIsTouch;            // 이전 터지의 상태
	private bool			canTouch = true;			// 터치 가능?
	

    // 초기화
    private void Awake()
    {
		wallManager		= GameObject.Find("WallManager").GetComponent<WallManager>();
        ball			= GameObject.Find("Ball").GetComponent<Ball>();
		cameraEffect	= GameObject.Find("Main Camera").GetComponent<CameraEffect>();
		uiManager		= GameObject.Find("Canvas").GetComponent<UIManager>();

        isTouch		    = false;
        previousIsTouch = false;
    }

	// 시작
	private void Start()
	{
		PowerCompute();
		StartCoroutine(LevelTimer());

		parser = new Parser();

		coin = parser.GetCoin();
		bestScore = parser.GetBestScore();
		parser.SetColorIndex(0, 0, 1, 2, 3);
	}

	// 프레임
	private void Update()
    {
		//// 클릭 처리 ( PC )
		//if (Input.GetMouseButtonDown(0))
		//{
		//	isTouch = true;
		//}

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
		if (isTouch != previousIsTouch && canTouch)
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


		// 테스트 키
		if (Input.GetKeyDown(KeyCode.K))
		{
			GameOver();
		}
    }

    // 홀딩 처리
    private void HoldingBall()
    {
		ball.HoldingHolder();
    }

    // 언홀딩 처리
    private void UnHoldingBall()
    {
        ball.UnholdingHolder();
    }
	
	// 발사 속도 계산기
	private void PowerCompute()
	{
		shotPower = Mathf.Min(Mathf.Max(1f, (level / 30f)), 1.5f);
	}

    // 점수 상승
    public void AddScore(int upScore)
    {
		// ** 스코어 이펙트 추가 예정 **
		score += upScore;
        scoreText.text = score.ToString();
    }

	// 코인 상승
	public void AddCoin(int upCoin)
	{
		coin += upCoin;
		coinText.text = coin.ToString();
	}

	// 벽 파괴
	public void WallDestroy()
	{
		// 월 매니저로 벽 확대 요청
		wallManager.NextWalls();

		if (wallCount++ < 4)
		{
			cameraEffect.ZoomOut();
		}
	}

	// 홀더 체크
	public void HolderCheck(GameObject target)
	{
		if (ball.bindedHolder == target)
		{
			// 언홀딩
			ball.UnbindingHolder();
		}
	}

	// 게임 오버
	public void GameOver()
	{
		// 데이터 저장
		parser.SetCoin(coin);
		parser.SetBestScore(Mathf.Max(score, bestScore));

		PlayerPrefs.Save();


		// 종료처리
		GameObject.Find("TouchPanel").GetComponent<TouchPanel>().enabled = false;
		ball.BallDestroy();
		cameraEffect.ZoomIn();
		StartCoroutine(OverCor());
 
		//Destroy(camera.GetComponent<CameraChase>());
	}

	// 씬 로드
	public void SceneLoad(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}

	// 레벨 타이머
	private IEnumerator LevelTimer()
	{
		while (true)
		{
			yield return new WaitForSeconds(levelTimer);

			level++;
			PowerCompute();
		}
	}

	// 종료 UI 코루틴
	private IEnumerator OverCor()
	{
		yield return new WaitForSeconds(2.5f);

		uiManager.StartCoroutine(uiManager.FadeOut((Image)null));

		yield return new WaitForSeconds(2f);

		SceneLoad("MainScene");
	}
}
    
