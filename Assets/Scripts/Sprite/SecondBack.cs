using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondBack : MonoBehaviour
{
	// 인스펙터 비노출 변수
	// 일반
	private float   rotationSpeed;       // 회전률
	

	// 초기화
	private void Awake()
	{
		Initialize();
	}

	// 프레임
	private void Update()
	{
		transform.Rotate(Vector3.forward * rotationSpeed);
	}

	// 전체 초기화
	private void Initialize()
	{
		rotationSpeed = Random.Range(0.8f, 1.2f);

		transform.position   = new Vector2(Random.Range(-20, 20), Random.Range(-20, 20));
		transform.localScale = new Vector2(Random.Range(0.5f, 1.5f), Random.Range(0.5f, 1.5f));
		transform.rotation	 = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 90)));
	}
}
