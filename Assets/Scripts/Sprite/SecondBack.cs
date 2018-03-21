using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondBack : MonoBehaviour
{
	// 인스펙터 비노출 변수
	// 일반
	private Sprite  sprite;              // 대상 스프라이트
	private float   rotationSpeed;       // 회전률
	

	// 초기화
	private void Awake()
	{
		Initialize();
	}

	// 프레임
	private void Update()
	{
		transform.rotation = Quaternion.Euler(0, 0, rotationSpeed * 10 * SecondBackList.timer);
	}

	// 전체 초기화
	private void Initialize()
	{
		sprite		  = GetComponent<Sprite>();
		rotationSpeed = Random.Range(0.8f, 1.2f);

		transform.position   = new Vector2(Random.Range(-40, 40), Random.Range(-40, 40));
		transform.localScale = new Vector2(Random.Range(0.5f, 1.5f), Random.Range(0.5f, 1.5f));
	}
}
