using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 일반

	// 수치
	[SerializeField]
	private float	    rotationSpeed = 1f;         // 회전속도

	// 인스펙터 비노출 변수
	// 일반
	private GameManager gameManager;				// 게임매니저
	private float		timer = 0f;					// 타이머	
	

	// 초기화
	private void Awake()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	// 프레임
	private void Update()
	{
		timer += Time.deltaTime;

		transform.rotation = Quaternion.Euler(0, 0, timer * rotationSpeed);
	}
}
