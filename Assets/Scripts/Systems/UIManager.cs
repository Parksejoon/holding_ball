using System.Collections;
using Systems.InGameSystems;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Systems
{
	public class UIManager : MonoBehaviour
	{
		public static UIManager instance;
		
		// 인스펙터 노출 변수
		// 일반
		[SerializeField]
		private Button 						restartButton;			// 재시작 버튼
		
		// 인스펙터 비노출 변수
		// 수치
		private float						originalTimeScale;		// 원래 타임스케일 값
		
		
		// 초기화
		private void Awake()
		{
			if (instance == null)
			{
				instance = this;
			}
		}
		
		// 퍼즈 체크
		public void CheckPause()
		{
			// 퍼즈 해제
			if (Time.timeScale == 0)
			{
				Continue();
			}
			// 퍼즈
			else
			{
				Pause();
			}
		}

		// 일시정지
		private void Pause()
		{
			UIEffecter.instance.SetUI(2, true);

			// 타임 스케일 저장
			originalTimeScale = Time.timeScale;

			// 정지
			Time.timeScale = 0f;
			GameManager.instance.timeValue = 0f;
		}

		// 계속하기
		private void Continue()
		{
			UIEffecter.instance.SetUI(2, false);
		
			// 타임 스케일 복구
			Time.timeScale = originalTimeScale;
			GameManager.instance.timeValue = 1f;
		}

		// 시작 UI 제거 및 시작
		public void SetUIs()
		{
			restartButton.interactable = true;
			
			UIEffecter.instance.FadePositionFunc(0, new Vector2(0, -3000), 0.1f, false, false);
			UIEffecter.instance.FadeAlphaFunc(0, 4, 0, 0.1f, true, false);
			
			StartCoroutine(StartRoutine());
		}

		// 시작버튼 클릭 루틴
		private IEnumerator StartRoutine()
		{
			yield return new WaitForSeconds(0.2f);

			// 메인패널
			UIEffecter.instance.SetUI(1, true);
		
			yield return new WaitForSeconds(0.8f);

			// 시작패널
			UIEffecter.instance.SetUI(0, true);
		}
	}
}
