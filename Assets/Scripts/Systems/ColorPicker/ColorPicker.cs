using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;

public class ColorPicker : MonoBehaviour, IPointerClickHandler
{
	// 인스펙터 노출 변수
	private int		index;       // 몇 번째 피커인지


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
