using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchPanel : MonoBehaviour, IPointerDownHandler
{
	// 인스펙터 비노출 변수
	// 일반
	private GameManager gameManager;        // 게임 매니저


	// 초기화
	private void Awake()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	// 터치 시작
	public void OnPointerDown(PointerEventData pointerEventData)
	{
		gameManager.isTouch = true;
	}
}
