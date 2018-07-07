using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class ColorPicker : MonoBehaviour, IPointerClickHandler
{
	// 인스펙터 노출 변수
	// 수치
	public  int		index;			// 몇 번째 피커인지

	// 인스펙터 비노출 변수
	// 일반
	private Image	image;			// 이 피커의 이미지


	// 초기화
	private void Awake()
	{
		image = GetComponent<Image>();

		image.color = Parser.instance.GetColor(index);
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
		Debug.Log(gameObject.name + " CLICK");
	}

	// 오프 애니메이션
	private void OffAnimation()
	{
		Debug.Log(gameObject.name);
	}

	// 온 애니메이션
	private IEnumerator OnAnimation()
	{
		yield return new WaitForSeconds(0.1f * index);

		Debug.Log(gameObject.name);
	}
}
