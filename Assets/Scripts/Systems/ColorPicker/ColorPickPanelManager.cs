using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ColorPickPanelManager : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private CoverSlider			mainCoverSlider;        // 메인 커버 슬라이더
	[SerializeField]
	private CoverSlider			pickCoverSlider;        // 컬러피커 커버 슬라이더
	[SerializeField]
	private GameObject			colorPicker;            // 컬러 피커// 일반
	[SerializeField]
	private BallParticler		ballParticler;          // 볼 파티클러
	[SerializeField]
	private ParticlePicker[]	particlePickers;        // 파티클 피커들
	[SerializeField]
	private GameObject[]		images;					// 이미지들

	// 인스펙터 비노출 변수
	// 일반
	private Image				pickCoverSliderImg;		// 컬러피커 커버 슬라이더 이미지

	// 수치
	private bool				isColorPicking;         // 컬러 피커가 열려있는 상태인가
	private Vector2				colorPickerOriginPos;   // 컬러 피커 원래 위치
	

	// 초기화
	private void Awake()
	{
		pickCoverSliderImg		= pickCoverSlider.GetComponent<Image>();

		colorPickerOriginPos	= colorPicker.transform.position;
	}

	// 시작 초기화
	private void Start()
	{
		mainCoverSlider.slideFuncs[1] = OnOffColorPicker;
		pickCoverSlider.slideFuncs[1] = OnOffColorPicker;
	}

	// 이미지 온오프
	private void SetImages(bool enabled)
	{
		Vector2 goalVec = Vector2.zero;

		if (enabled)
		{
			goalVec = new Vector2(0.1f, 0);
		}

		foreach (GameObject image in images)
		{
			UIEffecter.instance.FadeEffect(image, goalVec, 0.1f, UIEffecter.FadeFlag.ALPHA);
		}
	}
	
	// 컬러 피커창 열기
	public void OnOffColorPicker()
	{
		StartCoroutine(OnOffColorPickerRoutine());
	}

	// 컬러 피커 열기 루틴
	private IEnumerator OnOffColorPickerRoutine()
	{
		if (!isColorPicking)
		{
			// 패널 중앙으로 & 슬라이더 온
			UIEffecter.instance.FadeEffect(colorPicker, UIManager.instance.midPos, 0.1f, UIEffecter.FadeFlag.POSITION);
			pickCoverSliderImg.raycastTarget = true;
			SetImages(true);

			StartCoroutine(SetParticler(0.2f, true));
		}
		else
		{
			// 패널 사이드로 & 슬라이더 오프
			UIEffecter.instance.FadeEffect(colorPicker, colorPickerOriginPos, 0.1f, UIEffecter.FadeFlag.POSITION);
			pickCoverSliderImg.raycastTarget = false;
			SetImages(false);

			StartCoroutine(SetParticler(0, false));
		}

		isColorPicking = !isColorPicking;
		mainCoverSlider.StopSlide();

		yield return null;
	}

	// 파티클 온오프 루틴
	private IEnumerator SetParticler(float time, bool enabled)
	{
		yield return new WaitForSeconds(time);

		ballParticler.SetParticle(enabled);

		foreach (ParticlePicker particlePicker in particlePickers)
		{
			particlePicker.SetParticle(enabled);
		}

	}
}
