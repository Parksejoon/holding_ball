using System.Collections;
using CameraSystem;
using Objects;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace System
{
	public class GameManager : MonoBehaviour
	{
		public static GameManager instance;
	
		// 인스펙터 노출 변수
		// 수치
		[SerializeField]
		private float			levelTimer = 1f;			// 레벨 타이머
		
		public  float			timeValue = 1f;				// 시간 값

		// 인스펙터 비노출 변수
		// 일반
		private Ball			ball;                       // 볼
		private Touch			touch;                      // 터치 구조체
		private CameraEffect	cameraEffect;               // 카메라 이펙트
		private Parser			parser;						// 데이터 파서
		private int				wallCount;  				// 벽 카운트

		// 수치
		[HideInInspector]
		public  float			shotPower = 10;             // 발사 속도
		[HideInInspector]
		public  bool			isTouch;                    // 현제 터치의 상태
	
		private int				score;	                    // 점수
		private int				bestScore;					// 최고점수
		private int				coin;						// 코인
		private int				level;   					// 레벨
		private bool			previousIsTouch;            // 이전 터지의 상태
		private bool			canTouch = true;            // 터치 가능?
		private bool			isSecond;					// 두 번째 목숨?
	

		// 초기화
		private void Awake()
		{
			if (instance == null)
			{
				instance 		= this;				
			}
			
			ball			= GameObject.Find("Ball").GetComponent<Ball>();
			cameraEffect	= GameObject.Find("Main Camera").GetComponent<CameraEffect>();
			parser			= new Parser();

			isTouch		    = false;
			previousIsTouch = false;
		}

		// 시작
		private void Start()
		{
			PowerCompute();
			StartCoroutine(LevelTimer());
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

		// 데이터 초기화
		public void Initialize(int _coin, int _bestScore)
		{
			coin = _coin;
			bestScore = _bestScore;
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
			UIEffecter.instance.SetText(0, score.ToString());
			//UIManager.instance.SetText(0, score.ToString());
		}

		// 코인 상승
		public void AddCoin(int upCoin)
		{
			coin += upCoin;
			UIEffecter.instance.SetText(1, coin.ToString());
			//UIManager.instance.SetText(1, coin.ToString());

			parser.SetCoin(coin);
		}

		// 벽 파괴
		public void WallDestroy()
		{
			// 월 매니저로 벽 확대 요청
			WallManager.instance.NextWalls();

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
			// 공 파괴 및 터치 금지 설정
			GameObject.Find("TouchPanel").GetComponent<TouchPanel>().enabled = false;
			ball.BallDestroy();

			// 계속할것인지
			StartCoroutine(Continue());
		}


		// 씬 로드
		public void SceneLoad(string sceneName)
		{
			SceneManager.LoadScene(sceneName);
		}

		// 계속하기 루틴
		private IEnumerator Continue()
		{
			if (!isSecond)
			{
				yield return new WaitForSeconds(3f);
			
				// 공 재생성 및 터치 금지 해제
				GameObject.Find("TouchPanel").GetComponent<TouchPanel>().enabled = true;
				ball.RegenBall();

				isSecond = true;
			}
			else
			{
				// 데이터 저장
				parser.SetCoin(coin);
				parser.SetLastScore(score);
				parser.SetBestScore(Mathf.Max(score, bestScore));

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

		// 종료 UI 코루틴
		private IEnumerator OverCor()
		{
			yield return new WaitForSeconds(2.5f);

			UIEffecter.instance.FadeAlphaFunc(0, 3, 1, 0.1f, false, false);
			//UIManager.instance.StartCoroutine(UIManager.instance.FadeOut((Image)null));

			yield return new WaitForSeconds(2f);

			SceneLoad("MainScene");
		}
	}
}
    
