using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class ColorPickerList : MonoBehaviour, IPointerClickHandler
{
	// 인스펙터 노출 변수
	// 수치
	public	string			targetColor;				// 타겟 컬러

	// 인스펙터 비노출 변수
	// 일반
	private ColorPicker[]	colorPickerArray;           // 컬러 피커 배열
	private Image			image;						// 이미지

	// 수치
	private bool			isEnalbed = false;			// 현재 활성화 상태인지


	// 초기화
	private void Awake()
	{
		colorPickerArray	= GetComponentsInChildren<ColorPicker>();
		image				= GetComponent<Image>();
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
