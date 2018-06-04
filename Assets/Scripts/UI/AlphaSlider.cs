using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
	public class AlphaSlider : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
	{
		// 인스펙터 노출 변수
		// 수치
		public int 				disValue = 3000;		// 거리에 따른 페이드 비율
		
		// 인스펙터 비노출 변수
		// 일반
		private RectTransform 	thisRect;				// 이 이미지의 rect transform
		private Image 			thisImg;				// 이 이미지
		private Vector2 		originPos;				// 최초 위치
		private Vector2 		startPos;				// 시작 위치
		private Vector2 		previousPos;			// 이전 위치
		private Color 			imgColor;				// 이미지 색
		private float 			originAlpha;			// 최초 알파
		

		// 초기화
		private void Awake()
		{
			thisRect 	= GetComponent<RectTransform>();
			thisImg  	= GetComponent<Image>();
			originPos 	= thisRect.position;
			startPos 	= thisRect.position;
			imgColor    = thisImg.color;
			originAlpha = imgColor.a;
		}

		// 드래그 시작
		public void OnBeginDrag(PointerEventData eventData)
		{
			startPos 	= eventData.position;
			previousPos = startPos;
		}
		
		// 드래그 중
		public void OnDrag(PointerEventData eventData)
		{
			// 알파 조절
			imgColor.a = 1 - (Vector2.Distance(eventData.position, startPos) / disValue) - originAlpha / 2;
			thisImg.color = imgColor;
			
			// 위치 조절
			thisRect.position -= (Vector3)(previousPos - eventData.position);
			previousPos = eventData.position;
		}

		// 드래그 종료
		public void OnEndDrag(PointerEventData eventData)
		{
			StartCoroutine(UIEffecter.instance.FadeAlpha(thisImg, originAlpha, 0.05f, false, false));
			StartCoroutine(UIEffecter.instance.FadePosition(thisRect, originPos, 0.05f, false, false));
		}
	}
}
