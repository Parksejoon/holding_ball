using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchPanel : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
	// 인스펙터 비노출 변수
	// 일반
	private int			currentTouchCount = 0;  // 최근 터치 횟수
	private Vector3		dragStartPosition;		// 드래그 시작 위치


	// 터치 시작
	public void OnPointerDown(PointerEventData pointerEventData)
	{
		GameManager.instance.isTouch = true;
	}

	// 드래그 시작
	public void OnBeginDrag(PointerEventData pointerEventData)
	{
		dragStartPosition = pointerEventData.position;
	}

	// 드래그 ( begin과 end를 위해 존재 )
	public void OnDrag(PointerEventData pointerEventData)
	{
	}

	// 드래그 종료
	public void OnEndDrag(PointerEventData pointerEventData)
	{
		if (Vector3.Distance(dragStartPosition, pointerEventData.position) >= 300f)
		{
			StartCoroutine(Double(pointerEventData));
		}
	}

	// 더블샷 호출 코루틴(한 프레임에 addforce 두번되서 속도 비정상적임)
	private IEnumerator Double(PointerEventData pointerEventData)
	{
		Ball.instance.DoubleShot(dragStartPosition, pointerEventData.position);

		yield return null;
	}
}
