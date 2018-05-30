using System;
using System.Collections;
using UI;
using UnityEngine;

namespace Systems
{
	public class UIManager : MonoBehaviour
	{
		public static UIManager instance;
		
		// 인스펙터 노출 변수
		// 일반
//		[SerializeField]
//		private UnityEngine.UI.Text[]		texts;					// 텍스트 모음
//		[SerializeField]
//		private GameObject[]				panels;					// UI 모음
//		[SerializeField]
//		private Image						cover;                  // 페이드 커버
//		[SerializeField]
//		private Image						firstCover;				// 시작용 커버
//
//		// 수치
//		[SerializeField]
//		private float						fadeValue = 0.03f;		// 페이드 속도
//		[SerializeField]
//		private float						fadeTime = 0.03f;       // 페이드 텀

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

//		// 텍스트 설정
//		public void SetText(int ind, string str)
//		{
//			texts[ind].text = str;
//		}

		// 퍼즈 체크
		// ReSharper disable once UnusedMember.Global
		public void CheckPause()
		{
			// 퍼즈 해제
			// ReSharper disable once CompareOfFloatsByEqualityOperator
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
			//ControlPanel((int)PanelNum.PAUSE, true);

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
			//ControlPanel((int)PanelNum.PAUSE, false);

			// 타임 스케일 복구
			Time.timeScale = originalTimeScale;
			GameManager.instance.timeValue = 1f;
		}
		
//		// UI 온 / 오프
//		public void ControlPanel(int index, bool enable)
//		{
//			panels[index].SetActive(enable);
//			//UIEffecter.instance.SetUI(index, enable);
//		}

		// 시작 UI 제거
		public void DelStartUI()
		{
			UIEffecter.instance.FadePositionFunc(0, new Vector2(0, -3000), 0.1f, false, false);
			//StartCoroutine(UIEffecter.instance.FadePosition(panels[(int) PanelNum.START].GetComponent<RectTransform>(), new Vector2(0, -3000), 0.1f));
			UIEffecter.instance.FadeAlphaFunc(0, 4, 0, 0.1f, true, false);
			//StartCoroutine(FadeIn(firstCover));
			StartCoroutine(StartRoutine());
		}

//		// 페이드 아웃
//		public IEnumerator FadeOut(Image target)
//		{
//			// 타겟이 없으면 default image 사용
//			if (target == null)
//			{
//				target = cover;
//			}
//
//			// 페이드 설정 및 실행
//			float fadeAlpha = 0f;
//
//			target.enabled = true;
//			while (target.color.a < 1f)
//			{
//				target.color = new Color(target.color.r, target.color.g, target.color.b, fadeAlpha);
//
//				fadeAlpha += fadeValue;
//
//				yield return new WaitForSeconds(fadeTime);
//			}
//		}
//
//		// 페이드 아웃(텍스트)
//		public IEnumerator FadeOut(UnityEngine.UI.Text target)
//		{
//			// 페이드 설정 및 실행
//			float fadeAlpha = 0f;
//
//			target.enabled = true;
//			while (target.color.a < 1f)
//			{
//				target.color = new Color(target.color.r, target.color.g, target.color.b, fadeAlpha);
//
//				fadeAlpha += fadeValue;
//
//				yield return new WaitForSeconds(fadeTime);
//			}
//		}
//
//		// 페이드 인
//		public IEnumerator FadeIn(Image target)
//		{
//			// 타겟이 없으면 default image 사용
//			if (target == null)
//			{
//				target = cover;
//			}
//
//			// 페이드 설정 및 실행
//			float fadeAlpha = target.color.a;
//		
//			while (target.color.a > 0f)
//			{
//				target.color = new Color(target.color.r, target.color.g, target.color.b, fadeAlpha);
//
//				fadeAlpha -= fadeValue;
//
//				yield return new WaitForSeconds(fadeTime);
//			}
//			target.enabled = false;
//		}

		// 시작버튼 클릭 루틴
		private IEnumerator StartRoutine()
		{
			yield return new WaitForSeconds(0.2f);

			// 메인패널
			UIEffecter.instance.SetUI(1, true);
			//ControlPanel((int)PanelNum.MAIN, true);

			yield return new WaitForSeconds(0.8f);

			// 시작패널
			UIEffecter.instance.SetUI(0, true);
			//ControlPanel((int)PanelNum.START, false);

		}
	}
}
