using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
	public class CoverSlider : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
	{
		// 인스펙터 노출 변수
		// 일반
		[SerializeField]
		private Text[] 			slideWayText;			// 방향 텍스트
			
		// 수치
		public  int 			disValue = 3000;		// 거리에 따른 페이드 비율
		public  float 			slideDisPer = 0.5f;		// 슬라이드 거리 비율
		
		// 인스펙터 비노출 변수
		// 일반
		private RectTransform 	thisRect;				// 이 이미지의 rect transform
		private Image 			thisImg;				// 이 이미지
//		private Vector2 		originPos;				// 최초 위치
		private Vector2 		startPos;				// 시작 위치
//		private Vector2 		previousPos;			// 이전 위치
		private Vector2 		midPos;					// 중앙 위치
		private Color 			imgColor;				// 이미지 색
		private float 			originAlpha;			// 최초 알파
		private int 			slideDis;				// 슬라이드 거리
		

		// 초기화
		private void Awake()
		{
			thisRect 	= GetComponent<RectTransform>();
			thisImg  	= GetComponent<Image>();
//			originPos 	= thisRect.position;
			startPos 	= thisRect.position;
			imgColor    = Color.white;
			midPos 		= new Vector2(Screen.width / 2f, Screen.height / 2f);
			originAlpha = 0;
			slideDis 	= (int)(midPos.x * slideDisPer);
			
			Debug.Log(slideDis);
		}

		// 드래그 시작
		public void OnBeginDrag(PointerEventData eventData)
		{
			startPos 	= eventData.position;
//			previousPos = startPos;
		}
		
		// 드래그 중
		public void OnDrag(PointerEventData eventData)
		{
			// 알파 조절
			imgColor.a = (Vector2.Distance(eventData.position, startPos) - slideDis) / disValue;
			
//			// 위치 조절
//			thisRect.position -= (Vector3)(previousPos - eventData.position);
//			previousPos = eventData.position;
			
			Debug.Log(imgColor.a);
			
			// 방향 측정
			// 왼쪽
			if (eventData.position.x < startPos.x - slideDis)
			{
				slideWayText[0].color = imgColor;
				Debug.Log("left");
			}
			// 오른쪽
			if (eventData.position.x > startPos.x + slideDis)
			{
				slideWayText[1].color = imgColor;
				Debug.Log("right");	
			}
			// 위쪽
			if (eventData.position.y > startPos.y + slideDis)
			{	
				slideWayText[2].color = imgColor;
				Debug.Log("up");
			}
			// 아래쪽
			if (eventData.position.y < startPos.y - slideDis)
			{	
				slideWayText[3].color = imgColor;
				Debug.Log("down");
			}
		}

		// 드래그 종료
		public void OnEndDrag(PointerEventData eventData)
		{
			for (int i = 0; i < 4; i++)
			{
				StartCoroutine(UIEffecter.instance.FadeAlpha(slideWayText[i], originAlpha, 0.1f, false, false));	
			}
			
			//StartCoroutine(UIEffecter.instance.FadePosition(thisRect, originPos, 0.1f, false, false));
		}
	}
}
