using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private TouchPanel	touchPanel;         // 터치 패널
	[SerializeField]
	private Transform	ball;               // 공

	// 수치
	public int		level = 1;              // 레벨
	public float	timeValue = 1f;         // 시간 값

	// 인스펙터 비노출 변수
	// 일반
	private Touch			touch;          // 터치 구조체
	private CameraEffect	cameraEffect;   // 카메라 이펙트

	// 수치
	[HideInInspector]
	public bool isTouch;                    // 현제 터치의 상태

	private int score;                      // 점수
	private int bestScore;                  // 최고점수
	private int coin;                       // 코인
	private bool previousIsTouch;           // 이전 터지의 상태
	private bool canTouch = true;           // 터치 가능?
	private bool isSecond;                  // 두 번째 목숨?

	private const float shotPower = 13f;					// 발사 속도
	public float ShotPower { get { return shotPower; } }	// getter

	// 초기화
	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}

		cameraEffect = Camera.main.GetComponent<CameraEffect>();

		isTouch = false;
		previousIsTouch = false;
	}

	// 시작
	private void Start()
	{
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
				if (Ball.instance.isHolding)
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

	// 데이터 초기화
	public void Initialize(int _coin, int _bestScore)
	{
		coin = _coin;
		bestScore = _bestScore;
	}

	// 홀딩 처리
	private void HoldingBall()
	{
		Ball.instance.Holding();
	}

	// 언홀딩 처리
	private void UnHoldingBall()
	{
		Ball.instance.UnHolding();
	}

	// 점수 상승
	public void AddScore(int upScore)
	{
		score += upScore;
		UIEffecter.instance.SetText(0, score.ToString());
		
		PowerGauge.instance.AddPower(upScore * 0.6f);

		// 1000점당 1레벨
		level = (score / 1000) + 1;
	}

	// 코인 상승
	public void AddCoin(int upCoin)
	{
		coin += upCoin;
		UIEffecter.instance.SetText(1, coin.ToString());

		PlayerPrefs.SetInt("Coin", coin);
	}

	// 게임 오버
	public void GameOver()
	{
		// 공 파괴 및 터치 금지 설정
		touchPanel.enabled = false;
		Ball.instance.UnHolding();
		Ball.instance.BallDestroy();

		// 계속할것인지
		Continue();
	}
		
	// 자살
	public void Suicide()
	{
		UIManager.instance.OnOffPause();

		// 공 파괴 및 터치 금지 설정
		touchPanel.enabled = false;
		Ball.instance.UnHolding();
		Ball.instance.BallDestroy();

		FastStopGame();
	}

	// 씬 로드
	public void SceneLoad(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}

	// 광고 종료 콜백 메소드
	public void HandleShowResult(ShowResult result)
	{
		if (result == ShowResult.Finished)
		{
			// 공 재생성 및 터치 금지 해제
			touchPanel.enabled = true;
			Ball.instance.RegenBall();
		}
		else
		{
			StopGame();
		}
	}

	// 광고 보여주기
	public void ShowRewardedAd()
	{
		RevivalManager.instance.DeleteRevivalPanel();

		if (Advertisement.IsReady("rewardedVideo"))
		{
			var options = new ShowOptions { resultCallback = HandleShowResult };
			Advertisement.Show("rewardedVideo", options);
		}

		ball.position = new Vector2(0, -3.73f);
	}

	// 종료
	public void StopGame()
	{
		// 카메라 줌 인
		cameraEffect.ZoomIn();

		// 데이터 저장
		PlayerPrefs.SetInt("Coin", coin);
		PlayerPrefs.SetInt("LastScore", score);
		PlayerPrefs.SetInt("BestScore", Mathf.Max(score, bestScore));
		PlayerPrefs.Save();

		// 종료처리
		StartCoroutine(OverCor());
	}
	
	// 빠른 종료
	public void FastStopGame()
	{
		PlayerPrefs.SetInt("Coin", coin);
		PlayerPrefs.SetInt("LastScore", score);
		PlayerPrefs.SetInt("BestScore", Mathf.Max(score, bestScore));
		PlayerPrefs.Save();

		StartCoroutine(FastOver());
	}

	// 계속하기 루틴
	private void Continue()
	{
		if (!isSecond)
		{
			isSecond = true;

			RevivalManager.instance.OutputRevivalPanel();
		}
		else
		{
			StopGame();
		}		
	}

	// 종료 UI 코루틴
	private IEnumerator OverCor()
	{
		yield return new WaitForSeconds(2.5f);

		UIEffecter.instance.FadeEffect(UIEffecter.instance.panels[3], new Vector2(1, 0), 0.1f, UIEffecter.FadeFlag.ALPHA);

		yield return new WaitForSeconds(2f);

		SceneLoad("MainScene");
	}

	// 빠른 종료 코루틴
	private IEnumerator FastOver()
	{
		UIEffecter.instance.FadeEffect(UIEffecter.instance.panels[3], new Vector2(1, 0), 0.1f, UIEffecter.FadeFlag.ALPHA);

		yield return new WaitForSeconds(1f);

		SceneLoad("MainScene");
	}
}
