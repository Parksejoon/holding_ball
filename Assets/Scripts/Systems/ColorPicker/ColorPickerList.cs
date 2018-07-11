using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class ColorPickerList : MonoBehaviour, IPointerClickHandler
{
	// 인스펙터 노출 변수
	// 일반
	public	GameObject		cover;						// 커버

	// 수치
	public	string			targetColor;				// 타겟 컬러

	// 인스펙터 비노출 변수
	// 일반
	private ColorPicker[]	colorPickerArray;           // 컬러 피커 배열
	private Image			image;                      // 이미지
	private Image			coverImage;                 // 커버 이미지
	private Vector2			originPos;					// 처음 위치

	// 수치
	private bool			isEnalbed = false;			// 현재 활성화 상태인지


	// 초기화
	private void Awake()
	{
		colorPickerArray	= GetComponentsInChildren<ColorPicker>();
		image				= GetComponent<Image>();
		coverImage			= cover.GetComponent<Image>();
	}

	// 클릭시
	public void OnPointerClick(PointerEventData pointerEventData)
	{
		if (isEnalbed)
		{
			OffColorPicker();
		}
		else
		{
			OnColorPicker();
		}
	}

	// 컬러피커 온
	private void OnColorPicker()
	{
		// 처음 위치 저장
		originPos = GetComponent<RectTransform>().position;

		// 커버 온
		coverImage.raycastTarget = true;
		UIEffecter.instance.FadeEffect(cover, new Vector2(0.5f, 0), 0.3f, UIEffecter.FadeFlag.ALPHA);

		// 피커 온
		UIEffecter.instance.FadeEffect(gameObject, Vector2.zero, 0.2f, UIEffecter.FadeFlag.POSITION);

		StartCoroutine(MomentRaycastOff(2f));
		isEnalbed = true;

		foreach (ColorPicker colorPicker in colorPickerArray)
		{
			colorPicker.SetOn();
		}
	}

	// 컬러피커 오프
	public void OffColorPicker()
	{
		// 커버 오프
		coverImage.raycastTarget = false;
		UIEffecter.instance.FadeEffect(cover, Vector2.zero, 0.3f, UIEffecter.FadeFlag.ALPHA);

		// 피커 오프
		UIEffecter.instance.FadeEffect(gameObject, originPos, 0.2f, UIEffecter.FadeFlag.POSITION);

		StartCoroutine(MomentRaycastOff(0.3f));
		isEnalbed = false;

		foreach (ColorPicker colorPicker in colorPickerArray)
		{
			colorPicker.SetOff();
		}
	}

	// 일정시간 클릭 중지
	private IEnumerator MomentRaycastOff(float time)
	{
		image.raycastTarget = false;

		yield return new WaitForSeconds(time);

		image.raycastTarget = true;
	}
}
