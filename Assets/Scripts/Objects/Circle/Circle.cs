using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
	// 인스펙터 비노출 변수
	// 일반
	private	Orbit				orbit;			// 궤도
	private SpriteRenderer[]	sprites;        // 스프라이트들

	// 수치
	private int		hp;							// 체력


	// 초기화
	private void Awake()
	{
		orbit = transform.GetComponentInChildren<Orbit>();
		sprites = GetComponentsInChildren<SpriteRenderer>();

		hp = 3;
	}

	// 시작
	private void Start()
	{
		for (int i = 0; i < 3; i++)
		{
			UIEffecter.instance.FadeEffect(sprites[i].gameObject, new Vector2(0.3f, 0.3f), 0.4f, UIEffecter.FadeFlag.ALPHA);
		}
	}

	// 데미지를 받음
	public void Dealt(int damage)
	{
		hp = Mathf.Max(hp - damage, 0);
		
		for (int i = hp; i < 3; i++)
		{
			UIEffecter.instance.FadeEffect(sprites[i].gameObject, Vector2.one, 0.4f, UIEffecter.FadeFlag.ALPHA);
		}

		if (hp <= 0)
		{
			// 벽 생성
			orbit.CreateWall(10);

			// 태그를 변경
			gameObject.tag = "Core";
		}
	}
}
