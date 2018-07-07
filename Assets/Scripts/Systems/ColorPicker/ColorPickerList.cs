using UnityEngine.EventSystems;
using UnityEngine;

public class ColorPickerList : MonoBehaviour, IPointerClickHandler
{
	// 인스펙터 비노출 변수
	// 일반
	private ColorPicker[]	colorPickerArray;			// 컬러 피커 배열

	// 수치
	private bool			isEnalbed = false;			// 현재 활성화 상태인지


	// 초기화
	private void Awake()
	{
		colorPickerArray = GetComponentsInChildren<ColorPicker>();
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
		foreach (ColorPicker colorPicker in colorPickerArray)
		{
			colorPicker.SetOn();
		}
	}

	// 컬러피커 오프
	private void OffColorPicker()
	{
		foreach (ColorPicker colorPicker in colorPickerArray)
		{
			colorPicker.SetOff();
		}
	}
}
