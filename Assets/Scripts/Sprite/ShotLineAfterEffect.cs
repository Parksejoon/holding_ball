using System.Collections;
using UnityEngine;

public class ShotLineAfterEffect : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 수치
	[SerializeField]
	private float		   fadePower = 0.07f;		// 페이드효과 파워

	// 인스펙터 비노출 변수
	// 일반
	private SpriteRenderer sprite;                  // 스프라이트


	// 초기화
	private void Awake()
	{
		sprite = GetComponent<SpriteRenderer>();
	}

	// 시작
	private void Start()
	{
		StartCoroutine(FadeOut());
	}

	// 사라짐
	private IEnumerator FadeOut()
	{
		float alpha = 1f;

		while (true)
		{
			sprite.color = new Color(1, 1, 1, alpha);

			alpha -= fadePower;

			yield return new WaitForSeconds(0.01f);

			if (sprite.color.a <= 0)
			{
				Destroy(gameObject);
			}
		}
	}
}
