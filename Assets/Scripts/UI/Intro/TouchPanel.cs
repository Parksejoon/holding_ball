using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchPanel : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
	// 인스펙터 노출 변수
	// 수치
	[SerializeField]
	private float		dragDist;				// 드래그 거리 민감도(낮을수록 민감)  

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
		if (Vector3.Distance(dragStartPosition, pointerEventData.position) >= dragDist)
		{
			StartCoroutine(DashCor(pointerEventData));
		}
	}

	// 더블샷 호출 코루틴(한 프레임에 addforce 두번되서 속도 비정상적임)
	private IEnumerator DashCor(PointerEventData pointerEventData)
	{
		yield return null;

		Ball.instance.Dash(dragStartPosition, pointerEventData.position);

		yield return null;
	}
}
