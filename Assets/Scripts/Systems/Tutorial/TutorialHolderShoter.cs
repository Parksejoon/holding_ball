using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHolderShoter : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private GameObject holderPrefab;                    // 생성될 Holder 프리팹


	// 시작
	public void Start()
	{
		ObjectPoolManager.Init();
		ObjectPoolManager.AddObject("Holder", holderPrefab, transform);
		ObjectPoolManager.Create("Holder", 100);
	}

	// 트리거 진입
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Ball"))
		{
			StartCoroutine(OnewayWideSlug());
			gameObject.GetComponent<Collider2D>().enabled = false;
		}
	}

	// 각도와 스칼라에 따른 방향 벡터
	private Vector2 WayVector2(float degree, float finalPower)
	{
		return new Vector2(Mathf.Cos(degree * Mathf.PI / 180),
							Mathf.Sin(degree * Mathf.PI / 180))
							* finalPower;
	}

	// 단방향 슬러그
	private IEnumerator OnewayWideSlug()
	{
		Holder target;                                          // 타겟 홀더
		int count;                                          // 카운트
		float angle = 140;     // 방향 각도
		float addAngle = 10;                // 더해지는 각도
		float countAngle;                                       // 계산 각도

		
		while (true)
		{
			countAngle = angle;
			count = 0;

			while (count < 10)
			{
				target = ObjectPoolManager.GetGameObject("Holder", transform.position).GetComponent<Holder>();
				target.SetVelo(WayVector2(countAngle, 15));

				countAngle += addAngle;

				count += 1;
			}

			yield return new WaitForSeconds(1f);
		}
	}
}
