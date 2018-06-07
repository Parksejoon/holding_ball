using UnityEngine;
using UnityEngine.EventSystems;

public class TouchPanel : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
	// 인스펙터 비노출 변수
	// 일반
	private Ball		ball;					// 공
	private int			currentTouchCount = 0;  // 최근 터치 횟수
	private Vector3		dragStartPosition;		// 드래그 시작 위치


	// 초기화
	private void Awake()
	{
		ball = GameObject.Find("Ball").GetComponent<Ball>();
	}

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
			ball.DoubleShot(dragStartPosition, pointerEventData.position);
		}
	}
}
