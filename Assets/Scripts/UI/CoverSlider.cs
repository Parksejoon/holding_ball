using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CoverSlider : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
	public delegate void	SlideFunc();			// 슬라이드 시 호출할 함수 델리게이트

	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private Image[] 		slideWayImg;			// 방향별 이미지
			
	// 수치
	public  int 			disValue = 3000;		// 거리에 따른 페이드 비율
	public  float 			slideDisPer = 0.5f;		// 슬라이드 거리 비율
		
	// 인스펙터 비노출 변수
	// 일반
	private RectTransform 	thisRect;				// 이 이미지의 rect transform
	private Color 			imgColor;				// 이미지 색
	private RectTransform[]	slideWayRect;			// 방향별 rect transform
	private Vector2[] 		slideWayOriginPos;		// 방향별 원래 위치
	private bool[] 			isSliding;              // 슬라이드중인지				
	private SlideFunc		targetSlideFunc;        // 슬라이드 호출 함수 델리게이트
	private Vector3			maxAngle;				// 360도 벡터

	[HideInInspector]
	public  SlideFunc[]		slideFuncs;				// 슬라이드시 호출될 함수들 ( left right up down 순서 )

	// 수치
	private int 			slideMovePer = 1;		// 슬라이드시 움직이는 비율
	private Vector2 		startPos;				// 시작 위치
	private Vector2 		midPos;					// 중앙 위치
	private int 		 	slideDis;				// 슬라이드 거리
	private float 			originAlpha;            // 최초 알파


	// 초기화
	private void Awake()
	{
		thisRect 		= GetComponent<RectTransform>();
		imgColor		= Color.white;
		isSliding 		= new bool[4] { true, true, true, true };
		targetSlideFunc	= new SlideFunc(StopSlide);
		slideFuncs		= new SlideFunc[] { StopSlide, StopSlide, StopSlide, StopSlide };
		maxAngle		= new Vector3(0, 0, 360);

		// 배열 변수 초기화
		slideWayRect = new RectTransform[4];
		for (int i = 0; i < 4; i++)
		{
			slideWayRect[i] = slideWayImg[i].gameObject.GetComponent<RectTransform>();
		}
			
		slideWayOriginPos = new Vector2[4];
		for (int i = 0; i < 4; i++)
		{
			slideWayOriginPos[i] = slideWayRect[i].position;
		}
			
		startPos 	= thisRect.position;
		midPos 		= new Vector2(Screen.width / 2f, Screen.height / 2f);
		slideDis 	= (int)(midPos.x * slideDisPer);
		originAlpha = 0;
	}

	// 드래그 시작
	public void OnBeginDrag(PointerEventData eventData)
	{
		startPos = eventData.position;
	}
		
	// 드래그 중
	public void OnDrag(PointerEventData eventData)
	{
		// 알파 조절
		float pointerDis = Vector2.Distance(eventData.position, startPos);
			
		imgColor.a = (pointerDis - slideDis) / disValue;
		targetSlideFunc = StopSlide;
			
		// 방향 측정
		// 왼쪽
		if (eventData.position.x < startPos.x - slideDis && isSliding[0])
		{
			slideWayImg[0].color = imgColor;
			slideWayRect[0].position = Vector2.Lerp(slideWayOriginPos[0], midPos, Mathf.Max(0, imgColor.a / slideMovePer));
			slideWayRect[0].rotation = Quaternion.Euler(Vector3.Lerp(Vector3.zero, maxAngle, Mathf.Max(0, imgColor.a / slideMovePer)));

			isSliding[1] = isSliding[2] = isSliding[3] = false;

			if (imgColor.a >= 0.7f)
			{
				targetSlideFunc = slideFuncs[0];
			}
		}
		// 오른쪽
		if (eventData.position.x > startPos.x + slideDis && isSliding[1])
		{
			slideWayImg[1].color = imgColor;
			slideWayRect[1].position = Vector2.Lerp(slideWayOriginPos[1], midPos, Mathf.Max(0, imgColor.a / slideMovePer));
			slideWayRect[1].rotation = Quaternion.Euler(Vector3.Lerp(Vector3.zero, maxAngle, Mathf.Max(0, imgColor.a / slideMovePer)));

			isSliding[0] = isSliding[2] = isSliding[3] = false;

			if (imgColor.a >= 0.7f)
			{
				targetSlideFunc = slideFuncs[1];
			}
		}
		// 위쪽
		if (eventData.position.y > startPos.y + slideDis && isSliding[2])
		{	
			slideWayImg[2].color = imgColor;
			slideWayRect[2].position = Vector2.Lerp(slideWayOriginPos[2], midPos, Mathf.Max(0, imgColor.a / slideMovePer));
			slideWayRect[2].rotation = Quaternion.Euler(Vector3.Lerp(Vector3.zero, maxAngle, Mathf.Max(0, imgColor.a / slideMovePer)));

			isSliding[0] = isSliding[1] = isSliding[3] = false;

			if (imgColor.a >= 0.7f)
			{
				targetSlideFunc = slideFuncs[2];
			}
		}
		// 아래쪽
		if (eventData.position.y < startPos.y - slideDis && isSliding[3])
		{	
			slideWayImg[3].color = imgColor;
			slideWayRect[3].position = Vector2.Lerp(slideWayOriginPos[3], midPos, Mathf.Max(0, imgColor.a / slideMovePer));
			slideWayRect[3].rotation = Quaternion.Euler(Vector3.Lerp(Vector3.zero, maxAngle, Mathf.Max(0, imgColor.a / slideMovePer)));

			isSliding[0] = isSliding[1] = isSliding[2] = false;

			if (imgColor.a >= 0.7f)
			{
				targetSlideFunc = slideFuncs[3];
			}
		}
	}

	// 드래그 종료
	public void OnEndDrag(PointerEventData eventData)
	{
		targetSlideFunc();
	}

	// 슬라이드 마무리
	private void StopSlide()
	{
		// 알파 원래대로
		for (int i = 0; i < 4; i++)
		{
			UIEffecter.instance.FadeEffect(slideWayImg[i].gameObject, new Vector2(originAlpha, 0), 0.1f, UIEffecter.FadeFlag.ALPHA);
		}

		// 위치 원래대로
		for (int i = 0; i < 4; i++)
		{
			UIEffecter.instance.FadeEffect(slideWayRect[i].gameObject, slideWayOriginPos[i], 0.1f, UIEffecter.FadeFlag.POSITION);
		}

		// 플래그 삭제
		for (int i = 0; i < 4; i++)
		{
			isSliding[i] = true;
		}
	}
}
