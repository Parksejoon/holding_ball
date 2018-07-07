using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private GameObject		spotPrefab;					// 스팟 프리팹

	// 수치
	[SerializeField]
	private float			levelTimer = 30f;           // 레벨 타이머
	[SerializeField]
	private float			spotTimer = 10f;            // 스팟 타이머

	public int				level = 0;                  // 레벨
	public float			shotPower = 10;             // 발사 속도
	public float			timeValue = 1f;             // 시간 값

	// 인스펙터 비노출 변수
	// 일반
	private Touch			touch;                      // 터치 구조체
	private CameraEffect	cameraEffect;               // 카메라 이펙트

	// 수치
	[HideInInspector]
	public  bool			isTouch;                    // 현제 터치의 상태
	
	private int				score;	                    // 점수
	private int				bestScore;					// 최고점수
	private int				coin;						// 코인
	private bool			previousIsTouch;            // 이전 터지의 상태
	private bool			canTouch = true;            // 터치 가능?
	private bool			isSecond;					// 두 번째 목숨?
	

	// 초기화
	private void Awake()
	{
		if (instance == null)
		{
			instance 	= this;				
		}
			
		cameraEffect	= GameObject.Find("Main Camera").GetComponent<CameraEffect>();

		isTouch		    = false;
		previousIsTouch = false;
	}

	// 시작
	private void Start()
	{
		PowerCompute();
		StartCoroutine(LevelTimer());
		StartCoroutine(SpotTimer());
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
		Ball.instance.HoldingHolder();
	}

	// 언홀딩 처리
	private void UnHoldingBall()
	{
		Ball.instance.UnholdingHolder();
	}
	
	// 발사 속도 계산기
	private void PowerCompute()
	{
		shotPower = Mathf.Min(level * 0.17f + 1, 2f);
	}

	// 점수 상승
	public void AddScore(int upScore)
	{
		// ** 스코어 이펙트 추가 예정 **
		score += upScore;
		UIEffecter.instance.SetText(0, score.ToString());

		if (score % 100 == 0)
		{
			WallManager.instance.CreateWalls();
		}
	}

	// 코인 상승
	public void AddCoin(int upCoin)
	{
		coin += upCoin;
		UIEffecter.instance.SetText(1, coin.ToString());

		PlayerPrefs.SetInt("Coin", coin);
	}

	// 벽 파괴
	public void WallDestroy()
	{
		// 월 매니저로 벽 확대 요청
		WallManager.instance.InitWalls();
	}

	// 홀더 체크
	public void HolderCheck(GameObject target)
	{
		if (Ball.instance.bindedHolder == target)
		{
			// 언홀딩
			Ball.instance.UnbindingHolder();
		}
	}

	// 게임 오버
	public void GameOver()
	{
		// 공 파괴 및 터치 금지 설정
		GameObject.Find("TouchPanel").GetComponent<TouchPanel>().enabled = false;
		Ball.instance.BallDestroy();

		// 계속할것인지
		StartCoroutine(Continue());
	}
		
	// 자살
	public void Suicide()
	{
		UIEffecter.instance.SetUI(2, false);
		timeValue = 1f;
		Time.timeScale = 1f;
		isSecond = true;
			
		GameOver();
	}

	// 씬 로드
	public void SceneLoad(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}

	// 스팟 생성
	public void CreateSpot()
	{
		Vector2 createPos = new Vector2(Random.Range(-1f, 1f), Random.Range(1f, 1f)).normalized * Random.Range(-100f, 100f);

		UIEffecter.instance.FadeEffect(Instantiate(spotPrefab, createPos, Quaternion.identity, transform).transform.GetChild(0).gameObject, new Vector2(0.01f, 0), 0.1f, UIEffecter.FadeFlag.ALPHA);
	}

	// 계속하기 루틴
	private IEnumerator Continue()
	{
		if (!isSecond)
		{
			yield return new WaitForSeconds(3f);
			
			// 공 재생성 및 터치 금지 해제
			GameObject.Find("TouchPanel").GetComponent<TouchPanel>().enabled = true;
			Ball.instance.RegenBall();

			isSecond = true;
		}
		else
		{
			// 데이터 저장
			PlayerPrefs.SetInt("Coin", coin);
			PlayerPrefs.SetInt("LastScore", score);
			PlayerPrefs.SetInt("BestScore", Mathf.Max(score, bestScore));
			PlayerPrefs.Save();

			// 종료처리
			cameraEffect.ZoomIn();
			StartCoroutine(OverCor());
		}		
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

	// 스팟 생성 타이머
	private IEnumerator SpotTimer()
	{
		while (true)
		{
			yield return new WaitForSeconds(spotTimer);
			
			CreateSpot();	
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
}
