using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
	public class Slider : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
	{
		// 인스펙터 비노출 변수
		// 일반
		private RectTransform 	thisRect;				// 이 이미지의 rect transform
		private Vector2 		startPos;				// 시작 위치


		// 초기화
		private void Awake()
		{
			thisRect = GetComponent<RectTransform>();
			startPos = thisRect.position;
		}

		// 드래그 시작
		public void OnBeginDrag(PointerEventData eventData)
		{
			
		}
		
		// 드래그 중
		public void OnDrag(PointerEventData eventData)
		{
			thisRect.position = eventData.position;
		}

		// 드래그 종료
		public void OnEndDrag(PointerEventData eventData)
		{
			StartCoroutine(UIEffecter.instance.FadePosition(thisRect, startPos, 0.05f, false, false));
		}
	}
}
