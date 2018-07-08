using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class ColorPicker : MonoBehaviour, IPointerClickHandler
{
	// 인스펙터 노출 변수
	// 수치
	public  int		index;          // 몇 번째 피커인지


	// 인스펙터 비노출 변수
	// 일반
	private Image	image;			// 이 피커의 이미지


	// 초기화
	private void Awake()
	{
		image = GetComponent<Image>();

		Color setColor = Parser.instance.GetColor(index);

		setColor.a = 0;
		image.color = setColor;
	}

	// 활성화
	public void SetOn()
	{
		StartCoroutine(OnAnimation());
	}

	// 비활성화
	public void SetOff()
	{
		OffAnimation();
	}

	// 클릭시
	public void OnPointerClick(PointerEventData pointerEventData)
	{
		ColorPickerList colorPickerList = GetComponentInParent<ColorPickerList>();

		PlayerPrefs.SetInt(colorPickerList.targetColor, index);
		colorPickerList.OffColorPicker();
	}

	// 오프 애니메이션
	private void OffAnimation()
	{
		UIEffecter.instance.FadeEffect(gameObject, Vector2.zero, 0.2f, UIEffecter.FadeFlag.ALPHA);
		image.raycastTarget = false;
	}

	// 온 애니메이션
	private IEnumerator OnAnimation()
	{
		yield return new WaitForSeconds(0.1f * index);

		UIEffecter.instance.FadeEffect(gameObject, Vector2.one, 0.2f, UIEffecter.FadeFlag.ALPHA);
		image.raycastTarget = true;
	}
}
