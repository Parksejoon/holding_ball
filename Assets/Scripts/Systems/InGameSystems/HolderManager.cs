using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolderManager : MonoBehaviour
{
	// 싱글톤
	public static HolderManager instance;							// 인스턴스

	// 델리게이트
	private delegate IEnumerator HolderPattern();					// 홀더 샷 패턴 델리게이트

	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private GameObject	holderPrefab;					// 생성될 Holder 프리팹
	[SerializeField]
	private GameObject	coinPrefab;						// 생성될 Coin 프리팹
	[SerializeField]
	private float		power;							// 발사 파워
	[SerializeField]
	private float		minRespawnTime;					// 리스폰 최소시간
	[SerializeField]
	private float		maxRespawnTime;					// 리스폰 최대시간
	[SerializeField]
	private int 		amount;							// 소환되는 양
	
	public	Material	originHolderMat;				// 기본 홀더 머티리얼

	// 인스펙터 비노출 변수
	// 일반
	[HideInInspector]
	public  List<Transform>		holderList = new List<Transform>();    // 홀더 리스트

	// 수치
	private float			pastTime;                              // 경과 시간
	private float			goalTime;                              // 목표 시간
	private bool			isPasting;                             // 시간이 흘러가고있는가?

	// 홀더 패턴
	private HolderPattern[] lv1_holderPatterns;                 // 레벨1 패턴 리스트
	private HolderPattern[] lv2_holderPatterns;                 // 레벨2 패턴 리스트
	private HolderPattern[] lv3_holderPatterns;                 // 레벨3 패턴 리스트


	// 초기화
	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}

		// 패턴 델리게이트 초기화
		lv1_holderPatterns = new[]
		{
			new HolderPattern(Coinar),
			AllwaySlug,
			ForwaySlugShift,
			OnewayWideSlug,
			TwowayLineCompress,
			TwowaySlug,
			MultiwayLineRotation
		};

		lv2_holderPatterns = new[]
		{
			new HolderPattern(Coinar),
			ForwaySlugShift,
			TwowayLineCompress,
			TwowaySlug,
			OnewaySquare,
			OnewayArrow,
			MultiwayLineRotation
		};

		lv3_holderPatterns = new[]
		{
			new HolderPattern(Coinar),
			TwowaySlug,
			MultiwayLineRotation,
			ForwayLineRotation,
			OnewayArrow,
			OnewayCircle
		};
	}

	// 시작
	public void Start()
	{
		ObjectPoolManager.AddObject("Holder", holderPrefab, transform);
		ObjectPoolManager.Create("Holder", 600);

		ObjectPoolManager.AddObject("Coin", coinPrefab, transform);
		ObjectPoolManager.Create("Coin", 50);

		StartCoroutine(PatternCoroutine());
	}

	// 랜덤 패턴
	private void RunPattern()
	{
		int index = Random.Range(-1, 10);

		switch (GameManager.instance.level)
		{
			case 1:
				StartCoroutine(lv1_holderPatterns[(index % (lv1_holderPatterns.Length - 1)) + 1]());
				break;

			case 2:
				StartCoroutine(lv2_holderPatterns[(index % (lv2_holderPatterns.Length - 1)) + 1]());
				break;

			default:
			case 3:
				StartCoroutine(lv3_holderPatterns[(index % (lv3_holderPatterns.Length - 1)) + 1]());
				break;
		}
	}

	// 패턴 반복 코루틴
	private IEnumerator PatternCoroutine()
	{
		while (true)
		{
			RunPattern();

			yield return new WaitForSeconds(Random.Range(minRespawnTime, maxRespawnTime));
		}
	}

	// ================================================================
	// ================================================================
	// ========================== 계산용 함수 ==========================

	
	// 각도와 스칼라에 따른 방향 벡터
	private Vector2 WayVector2(float degree, float finalPower)
	{
		return new Vector2(Mathf.Cos(degree * Mathf.PI / 180),
							Mathf.Sin(degree * Mathf.PI / 180))
							* finalPower;
	}

	// ================================================================
	// ================================================================
	// ========================= LV1 패턴 목록 =========================

	// 모든 방향 분사 / 탄막수를 늘리며 4연사
	private IEnumerator AllwaySlug()
	{
		Holder	target;                             // 타겟 홀더
		float	angle;                              // 방향 각도
		float	addAngle;							// 더해지는 각도

		
		for (int k = 4; k >= 1; k--)
		{
			angle = 0;
			addAngle = (360 / (amount / k));

			for (int i = 0; i <= amount / k; i++)
			{
				target = ObjectPoolManager.GetGameObject("Holder", transform.position).GetComponent<Holder>();
				target.SetVelo(WayVector2(angle, power));

				// 분사량에 따라 각도 조절
				angle += addAngle;
			}

			yield return new WaitForSeconds(0.05f);
		}
	}
	
	// 4방향 서로 다른속도로 슬러그 / 2연사
	private IEnumerator ForwaySlugShift()
	{
		Holder	target;                             // 타겟 홀더
		float	angle;                              // 방향 각도
		float	finalPower;                         // 최종 파워
		float	addAngle;							// 더해지는 각도


		for (int k = 0; k < 2; k++)
		{ 
			for (int i = 0; i < 4; i++)
			{
				angle = 0;
				finalPower = power * UnityEngine.Random.Range(0.7f, 1.3f);
				addAngle = 90 / (amount / 4);

				for (int j = 0; j < amount / 4; j++)
				{
					target = ObjectPoolManager.GetGameObject("Holder", transform.position).GetComponent<Holder>();
					target.SetVelo(WayVector2(angle + (90 * i), finalPower));
					
					angle += addAngle;
				}
			}

			yield return new WaitForSeconds(0.15f);
		}
	}

	// 단방향 넓은 슬러그 / 5연사
	private IEnumerator OnewayWideSlug()
	{
		Holder	target;											// 타겟 홀더
		int		count;											// 카운트
		float	angle = UnityEngine.Random.Range(0, 360);		// 방향 각도
		float	addAngle = (40 / (amount / 10));                // 더해지는 각도
		float	countAngle;										// 계산 각도
		
		
		for (int i = 0; i < 5; i++)
		{
			countAngle = angle;
			count = 0;

			while (count < amount)
			{
				target = ObjectPoolManager.GetGameObject("Holder", transform.position).GetComponent<Holder>();
				target.SetVelo(WayVector2(countAngle, power));

				countAngle += addAngle;

				count += 5;
			}

			angle += 5;

			yield return new WaitForSeconds(0.1f);
		}
	}

	// ================================================================
	// ================================================================
	// ========================= LV2 패턴 목록 =========================

	// 좌우 180도에서 가운데로 모이면서 발사 / 
	private IEnumerator TwowayLineCompress()
	{
		Holder	target;                                     // 타겟 홀더
		float	angle = UnityEngine.Random.Range(0, 360);   // 현재 각도
		float	addAngle = 6;								// 더해지는 각도
		float	minusAngle = 90;							// 간격

		
		for (int i = 0; i < amount; i++)
		{
			target = ObjectPoolManager.GetGameObject("Holder", transform.position).GetComponent<Holder>();
			target.SetVelo(WayVector2(angle + minusAngle, power));
			
			target = ObjectPoolManager.GetGameObject("Holder", transform.position).GetComponent<Holder>();
			target.SetVelo(WayVector2(angle - minusAngle, power));
			
			minusAngle -= addAngle;

			yield return new WaitForSeconds(0.05f);
		}
	}

	// 180도 양방향 분사 / 3연사
	private IEnumerator TwowaySlug()
	{
		Holder	target;                 // 타겟 홀더
		float	angle;                  // 방향 각도
		float	addAngle = 3;			// 더해지는 각도

		
		for (int k = 3; k >= 0; k--)
		{
			angle = UnityEngine.Random.Range(0, 360);

			for (int i = 0; i < amount / 5; i++)
			{
				target = ObjectPoolManager.GetGameObject("Holder", transform.position).GetComponent<Holder>();
				target.SetVelo(WayVector2(angle, power));
				
				target = ObjectPoolManager.GetGameObject("Holder", transform.position).GetComponent<Holder>();
				target.SetVelo(WayVector2(180 + angle, power));
				
				angle += addAngle;
			}

			yield return new WaitForSeconds(0.3f);
		}
	}

	// 단 방향으로 네모 발사
	private IEnumerator OnewaySquare()
	{
		Holder	target;                                         // 타겟 홀더
		float	angle = UnityEngine.Random.Range(0, 360);		// 방향 각도
		float	gapPosition;                                    // 갭 거리

		for (int k = 0; k < 5; k++)
		{
			gapPosition = 0;
			for (int i = 0; i <= amount / 10; i++)
			{
				target = ObjectPoolManager.GetGameObject("Holder", transform.position).GetComponent<Holder>();
				target.transform.position = WayVector2(angle + 90, gapPosition);
				target.SetVelo(WayVector2(angle, power * 2f));

				target = ObjectPoolManager.GetGameObject("Holder", transform.position).GetComponent<Holder>();
				target.transform.position = WayVector2(angle - 90, gapPosition);
				target.SetVelo(WayVector2(angle, power * 2f));

				gapPosition += 0.5f;

				yield return new WaitForSeconds(0.01f);
			}

			for (int i = -1; i <= amount / 10; i++)
			{
				target = ObjectPoolManager.GetGameObject("Holder", transform.position).GetComponent<Holder>();
				target.transform.position = WayVector2(angle + 90, gapPosition);
				target.SetVelo(WayVector2(angle, power * 2f));

				target = ObjectPoolManager.GetGameObject("Holder", transform.position).GetComponent<Holder>();
				target.transform.position = WayVector2(angle - 90, gapPosition);
				target.SetVelo(WayVector2(angle, power * 2f));

				gapPosition -= 0.5f;

				yield return new WaitForSeconds(0.01f);
			}

			yield return new WaitForSeconds(0.02f);
		}
	}

	// ================================================================
	// ================================================================
	// ========================= LV3 패턴 목록 =========================

	// 다방향으로 OnewayLineRotation을 발사
	private IEnumerator MultiwayLineRotation()
	{
		StartCoroutine(OnewayLineRotation());
		StartCoroutine(OnewayLineRotation());
		StartCoroutine(OnewayLineRotation());

		yield return null;
	}

	// 한 방향에서 한줄로 지그재그로 발사
	private IEnumerator OnewayLineRotation()
	{
		Holder	target;                         // 타겟 홀더
		float	angle;							// 현재 각도
		float	addAngle = 360 / amount;        // 더해지는 각도
		int		direction = 1;                  // 방향


		angle = Random.Range(0, 360);
		for (int i = 0; i < amount; i++)
		{
			target = ObjectPoolManager.GetGameObject("Holder", transform.position).GetComponent<Holder>();
			target.SetVelo(WayVector2(angle, power * 1.5f));
			
			angle += addAngle * direction;

			if (i == Random.Range(i, i + 3)) direction = -direction;

			yield return new WaitForSeconds(0.05f);
		}
	}

	// 4방향으로 한줄씩 회전하며 나가는거
	private IEnumerator ForwayLineRotation()
	{
		Holder	target;                                     // 타겟 홀더
		int		count;										// 카운트
		float	angle = UnityEngine.Random.Range(0, 90);    // 현재 각도
		int		direction = 1;								// 방향


		for (int i = 0; i < 3; i++)
		{
			count = 0;

			if (Random.Range(0, 2) == 1)
			{
				direction = -direction;
			}
			
			while (count < amount / 7)
			{
				for (int j = 0; j < 4; j++)
				{
					target = ObjectPoolManager.GetGameObject("Holder", transform.position).GetComponent<Holder>();
					target.SetVelo(WayVector2(angle + (90 * j), power * 2f));
				}

				angle += 5 * direction;
				count++;

				yield return new WaitForSeconds(0.02f);
			}

			yield return new WaitForSeconds(0.1f);
		}
	}

	// 단 방향으로 화살표 발사
	private IEnumerator OnewayArrow()
	{
		Holder	target;											// 타겟 홀더
		int		count;											// 카운트
		float	angle;											// 방향 각도
		float	gapAngle;                                       // 갭 각도

		for (int i = 0; i < 10; i++)
		{
			angle = UnityEngine.Random.Range(0, 360);
			gapAngle = 0;
			count = 0;

			while (count < amount / 2)
			{
				target = ObjectPoolManager.GetGameObject("Holder", transform.position).GetComponent<Holder>();
				target.SetVelo(WayVector2(angle + gapAngle, power * 2f));

				target = ObjectPoolManager.GetGameObject("Holder", transform.position).GetComponent<Holder>();
				target.SetVelo(WayVector2(angle - gapAngle, power * 2f));

				gapAngle += 2f;

				count += amount / 10;

				yield return new WaitForSeconds(0.02f);
			}

			yield return null;
		}
	}

	// 원 발사 / 탄막 수와 속도를 높이며 3연사
	private IEnumerator OnewayCircle()
	{
		Holder	target;                 // 타겟 홀더
		float	angle;                  // 발사 방향 각도
		float	positionAngle;			// 위치 각도
		float	addAngle;				// 더해지는 각도


		angle = UnityEngine.Random.Range(0, 360);

		for (int k = 3; k >= 1; k--)
		{
			addAngle = (360 / (amount / (2 * k)));
			positionAngle = angle;

			for (int i = 0; i <= amount / (2 * k); i++)
			{
				target = ObjectPoolManager.GetGameObject("Holder", transform.position).GetComponent<Holder>();
				target.transform.position = WayVector2(positionAngle, 5 / k);
				target.SetVelo(WayVector2(angle, power * 3 / k));

				positionAngle += addAngle;
			}

			yield return new WaitForSeconds(0.8f);
		}
	}


	
	// 코인
	private IEnumerator Coinar()
	{
		Coin	target;										// 타겟 홀더
		float	angle = 0;                                  // 발사 각도
		float	addAngle = (360 / 30);						// 더해지는 각도


		for (int i = 0; i < 30; i++)
		{
			target = ObjectPoolManager.GetGameObject("Coin", transform.position).GetComponent<Coin>();
			target.SetVelo(WayVector2(angle, power / 3));
	
			angle += addAngle;
		}

		yield return null;
	}
}
