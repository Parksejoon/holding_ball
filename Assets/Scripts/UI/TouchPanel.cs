using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchPanel : MonoBehaviour, IPointerDownHandler
{
	// 인스펙터 비노출 변수
	// 일반
	private GameManager gameManager;            // 게임 매니저
	private Ball		ball;					// 공
	private int			currentTouchCount = 0;  // 최근 터치 횟수
	private float		timer = 0;				// 타이머


	// 초기화
	private void Awake()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		ball		= GameObject.Find("Ball").GetComponent<Ball>();
	}

	// 프레임
	private void Update()
	{
		// 타이머 계산
		timer += Time.deltaTime;

		if (timer >= 0.2f)
		{
			currentTouchCount = 0;
		}
	}

	// 터치 시작
	public void OnPointerDown(PointerEventData pointerEventData)
	{
		timer = 0;

		// 더블 가능일시
	  	if (++currentTouchCount >= 2 & ball.canDouble)
		{
			ball.DoubleShot();
		}
		// 불가능일시
		else
		{
			gameManager.isTouch = true;
		}
	}
}
