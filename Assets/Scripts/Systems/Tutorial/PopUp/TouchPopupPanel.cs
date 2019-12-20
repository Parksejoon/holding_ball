using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchPopupPanel : MonoBehaviour, IPointerDownHandler
{
	
	// 터치
	public void OnPointerDown(PointerEventData pointerEventData)
	{
		GameManager.instance.isTouch = true;

		//Ball.instance.StopBall();
		gameObject.SetActive(false);
	}
}
