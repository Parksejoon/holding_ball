using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupZone : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private GameObject		panel;      // 팝업패널


	// 트리거 진입
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.transform.CompareTag("Ball"))
		{
			ShowPanel();
		}
	}

	// 패널 켜기
	private void ShowPanel()
	{
		panel.SetActive(true);
		Time.timeScale = 0;
		Destroy(gameObject);
	}
}
