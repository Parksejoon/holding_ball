using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public enum PanelNum
	{
		START,
		MAIN
	}

	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private GameObject[]	panels;					// UI 모음
	[SerializeField]
	private Image			cover;                  // 페이드 커버
	[SerializeField]
	private Text			scoreText;              // 점수 텍스트
	[SerializeField]
	private Image			pauseButton;			// 퍼즈버튼

	// 수치
	[SerializeField]
	private float			fadeValue = 0.03f;		// 페이드 속도
	[SerializeField]
	private float			fadeTime = 0.03f;       // 페이드 텀


	// UI 온 / 오프
	public void ControlPanel(int index, bool isOn)
	{
		panels[index].SetActive(isOn);
	}

	// 시작 UI 제거
	public void DelStartUI()
	{
		StartCoroutine(MoveUI(panels[(int)PanelNum.START].GetComponent<RectTransform>(), new Vector2(0, 0), new Vector2(0, -1200), 0.02f));
		StartCoroutine(FadeIn(null));
		StartCoroutine(StartRoutine());
	}

	// 메인 UI 생성

	// 페이드 아웃
	public IEnumerator FadeOut(Image target)
	{
		// 타겟이 없으면 default image 사용
		if (target == null)
		{
			target = cover;
		}

		// 페이드 설정 및 실행
		float fadeAlpha = 0f;

		target.enabled = true;
		while (target.color.a < 1f)
		{
			target.color = new Color(target.color.r, target.color.g, target.color.b, fadeAlpha);

			fadeAlpha += fadeValue;

			yield return new WaitForSeconds(fadeTime);
		}
	}

	// 페이드 아웃(텍스트)
	public IEnumerator FadeOut(Text target)
	{
		// 페이드 설정 및 실행
		float fadeAlpha = 0f;

		target.enabled = true;
		while (target.color.a < 1f)
		{
			target.color = new Color(target.color.r, target.color.g, target.color.b, fadeAlpha);

			fadeAlpha += fadeValue;

			yield return new WaitForSeconds(fadeTime);
		}
	}

	// 페이드 인
	public IEnumerator FadeIn(Image target)
	{
		// 타겟이 없으면 default image 사용
		if (target == null)
		{
			target = cover;
		}

		// 페이드 설정 및 실행
		float fadeAlpha = target.color.a;
		
		while (target.color.a > 0f)
		{
			target.color = new Color(target.color.r, target.color.g, target.color.b, fadeAlpha);

			fadeAlpha -= fadeValue;

			yield return new WaitForSeconds(fadeTime);
		}
		target.enabled = false;
	}

	// 이동
	public IEnumerator MoveUI(RectTransform target, Vector2 startVec2, Vector2 endVec2, float speed)
	{
		float range = 0f;

		while (range < 1)
		{
			target.offsetMax = Vector2.Lerp(startVec2, endVec2, range);
			target.offsetMin = Vector2.Lerp(startVec2, endVec2, range);
			range += speed;

			yield return new WaitForSeconds(0.01f);
		}
	}

	// 시작버튼 클릭 루틴
	private IEnumerator StartRoutine()
	{
		yield return new WaitForSeconds(0.2f);

		// 메인패널
		ControlPanel((int)PanelNum.MAIN, true);
		StartCoroutine(FadeOut(pauseButton));
		StartCoroutine(FadeOut(scoreText));

		yield return new WaitForSeconds(0.8f);

		// 시작패널
		ControlPanel((int)PanelNum.START, false);

	}
}
