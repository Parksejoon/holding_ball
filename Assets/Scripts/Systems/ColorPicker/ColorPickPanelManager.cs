using System.Collections;
using UnityEngine;

public class ColorPickPanelManager : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private CoverSlider mainCoverSlider;        // 메인 커버 슬라이더
	[SerializeField]
	private CoverSlider pickCoverSlider;        // 컬러피커 커버 슬라이더
	[SerializeField]
	private GameObject	colorPicker;			// 컬러 피커

	// 인스펙터 비노출 변수
	// 수치
	private bool		isColorPicking;         // 컬러 피커가 열려있는 상태인가
	private Vector2		colorPickerOriginPos;   // 컬러 피커 원래 위치


	// 초기화
	private void Awake()
	{
		colorPickerOriginPos = colorPicker.transform.position;
	}

	// 시작 초기화
	private void Start()
	{
		mainCoverSlider.slideFuncs[1] = OnOffColorPicker;
		pickCoverSlider.slideFuncs[1] = OnOffColorPicker;
	}
	
	// 컬러 피커창 열기
	public void OnOffColorPicker()
	{
		StartCoroutine(OnOffColorPickerRoutine());
	}

	// 컬러 피커 열기 루틴
	private IEnumerator OnOffColorPickerRoutine()
	{
		if (isColorPicking)
		{
			// 패널 중앙으로
			UIEffecter.instance.FadeEffect(colorPicker, UIManager.instance.midPos, 0.2f, UIEffecter.FadeFlag.POSITION);
		}
		else
		{
			// 패널 사이드로
			UIEffecter.instance.FadeEffect(colorPicker, colorPickerOriginPos, 0.2f, UIEffecter.FadeFlag.POSITION);
		}

		isColorPicking = !isColorPicking;
		mainCoverSlider.StopSlide();

		yield return null;
	}
}
