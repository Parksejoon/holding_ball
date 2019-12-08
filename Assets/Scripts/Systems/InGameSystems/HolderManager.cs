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
	private float		term;							// 중간 텀 시간
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
			OnewayWideSlug
		};

		lv2_holderPatterns = new[]
		{
			new HolderPattern(Coinar),
			TwowayLineCompress,
			TwowaySlug
		};

		lv3_holderPatterns = new[]
		{
			new HolderPattern(Coinar),
			OnewayLineRotation,
			ForwayLineRotation,
			OnewayArrow
		};
	}

	// 시작
	public void Start()
	{
		ObjectPoolManager.AddObject("Holder", holderPrefab, transform);
		ObjectPoolManager.Create("Holder", 600);

		ObjectPoolManager.AddObject("Coin", coinPrefab, transform);
		ObjectPoolManager.Create("Coin", 50);
	}

	// 프레임
	public void Update()
	{
		// 볼이 홀딩상태 아닐때만 시간을 측정
		if (!Ball.instance.isHolding)
		{
			// 카운트중인지 확인 후 카운트 진행
			if (isPasting)
			{
				pastTime += Time.deltaTime;

				// 홀더를 생성
				if (pastTime >= goalTime)
				{
					// 랜덤 패턴으로 생성 시작
					RunPattern();

					// 카운트 종료
					isPasting = false;
				}
			}
			// 아니라면 카운트 시작
			else
			{
				// 카운트 초기화
				pastTime = 0;
				goalTime = UnityEngine.Random.Range(minRespawnTime, maxRespawnTime);

				// 카운트 시작
				isPasting = true;
			}
		}
	}

	// 랜덤 패턴
	private void RunPattern()
	{
		int index = UnityEngine.Random.Range(-1, 50);

		//switch (GameManager.instance.level)
		//{
		//	case 1:
		//		StartCoroutine(lv1_holderPatterns[(index % 3) + 1]());
		//		break;

		//	case 2:
		//		StartCoroutine(lv2_holderPatterns[(index % 3) + 1]());
		//		break;

		//	default:
		//	case 3:
		//		StartCoroutine(lv3_holderPatterns[(index % 3) + 1]());
		//		break;
		//}

		StartCoroutine(lv3_holderPatterns[3]());
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

	// 모든 반향 분사
	private IEnumerator AllwaySlug()
	{
		Holder	target;                             // 타겟 홀더
		int		count = 0;                          // 카운트
		float	angle;                              // 방향 각도
		float	addAngle = (360 / (amount / 2));    // 더해지는 각도


		while (count < amount)
		{
			angle = 0;

			for (int i = 0; i < amount / 2; i++)
			{
				// 생성
				target = ObjectPoolManager.GetGameObject("Holder", transform.position).GetComponent<Holder>();
				//target = Instantiate(holderPrefab, new Vector3(fixX, fixY, 0), Quaternion.identity, transform).GetComponent<Holder>();

				// 방향으로 힘 적용
				target.SetVelo(WayVector2(angle, power));

				// 분사량에 따라 각도 조절
				angle += addAngle;
			}

			count += amount / 4;

			yield return new WaitForSeconds(term + 0.5f);
		}
	}
	
	// 4방향 서로 다른속도로 슬러그처럼 나가는거
	private IEnumerator ForwaySlugShift()
	{
		Holder	target;                             // 타겟 홀더
		int		count = 0;							// 카운트
		float	angle;                              // 방향 각도
		float	finalPower;                         // 최종 파워
		float	addAngle = 90 / (amount / 4);       // 더해지는 각도


		while (count < amount)
		{
			for (int i = 0; i < 4; i++)
			{
				angle = 0;
				finalPower = power * UnityEngine.Random.Range(0.5f, 1.5f);

				for (int j = 0; j < amount / 4; j++)
				{
					// 생성
					target = ObjectPoolManager.GetGameObject("Holder", transform.position).GetComponent<Holder>();
					//target = Instantiate(holderPrefab, new Vector3(fixX, fixY, 0), Quaternion.identity, transform).GetComponent<Holder>();

					// 방향으로 힘 적용
					target.SetVelo(WayVector2(angle + (90 * i), finalPower));

					// 분사량에 따라 각도 조절
					angle += addAngle;
				}
			}

			count += amount / 2;

			yield return new WaitForSeconds(term + 0.5f);
		}
	}

	// 단방향 넓은 슬러그
	private IEnumerator OnewayWideSlug()
	{
		Holder	target;											// 타겟 홀더
		int		count = 0;										// 카운트
		float	angle = UnityEngine.Random.Range(0, 360);		// 방향 각도
		float	addAngle = (40 / (amount / 10));                // 더해지는 각도
		float	countAngle;										// 계산 각도
		

		while (count < amount)
		{
			countAngle = angle;

			for (int i = 0; i < amount / 5; i++)
			{
				// 생성
				target = ObjectPoolManager.GetGameObject("Holder", transform.position).GetComponent<Holder>();
				//target = Instantiate(holderPrefab, new Vector3(fixX, fixY, 0), Quaternion.identity, transform).GetComponent<Holder>();

				// 방향으로 힘 적용
				target.SetVelo(WayVector2(countAngle, power));

				// 분사량에 따라 각도 조절
				countAngle += addAngle;
			}

			count += (amount / 10) * 2;
			angle += 10f;

			yield return new WaitForSeconds(term);
		}
	}

	// ================================================================
	// ================================================================
	// ========================= LV2 패턴 목록 =========================

	// 좌우에서 가운데로 모이면서 발사하는거
	private IEnumerator TwowayLineCompress()
	{
		Holder	target;                                     // 타겟 홀더
		int		count = 0;									// 카운트
		float	angle = UnityEngine.Random.Range(0, 360);   // 현재 각도
		float	addAngle = 90 / (amount / 2);				// 더해지는 각도
		float	minusAngle = 90;							// 간격


		while (count < amount)
		{
			// 생성
			target = ObjectPoolManager.GetGameObject("Holder", transform.position).GetComponent<Holder>();
			//target = Instantiate(holderPrefab, new Vector3(fixX, fixY, 0), Quaternion.identity, transform).GetComponent<Holder>();

			// 방향으로 힘 적용
			target.SetVelo(WayVector2(angle + minusAngle, power));

			// 생성
			target = ObjectPoolManager.GetGameObject("Holder", transform.position).GetComponent<Holder>();
			//target = Instantiate(holderPrefab, new Vector3(fixX, fixY, 0), Quaternion.identity, transform).GetComponent<Holder>();

			// 방향으로 힘 적용
			target.SetVelo(WayVector2(angle - minusAngle, power));

			// 각도 추가
			minusAngle -= addAngle;

			count++;

			yield return new WaitForSeconds(term);
		}
	}

	// 양방향 분사
	private IEnumerator TwowaySlug()
	{
		Holder	target;                                 // 타겟 홀더
		int		count = 0;                              // 카운트
		float	angle;                                  // 방향 각도
		float	addAngle = (50 / (amount / 10));        // 더해지는 각도


		while (count < amount)
		{
			angle = UnityEngine.Random.Range(0, 360);

			for (int i = 0; i < amount / 5; i++)
			{
				// 생성
				target = ObjectPoolManager.GetGameObject("Holder", transform.position).GetComponent<Holder>();
				//target = Instantiate(holderPrefab, new Vector3(fixX, fixY, 0), Quaternion.identity, transform).GetComponent<Holder>();

				// 방향으로 힘 적용
				target.SetVelo(WayVector2(angle, power));

				// 생성
				target = ObjectPoolManager.GetGameObject("Holder", transform.position).GetComponent<Holder>();
				//target = Instantiate(holderPrefab, new Vector3(fixX, fixY, 0), Quaternion.identity, transform).GetComponent<Holder>();

				// 방향으로 힘 적용
				target.SetVelo(WayVector2(180 + angle, power));

				// 분사량에 따라 각도 조절
				angle += addAngle;
			}

			count += (amount / 10) * 2;

			yield return new WaitForSeconds(term * 3f);
		}
	}

	// ================================================================
	// ================================================================
	// ========================= LV3 패턴 목록 =========================

	// 한 방향에서 한줄로 지그재그로 나오는거
	private IEnumerator OnewayLineRotation()
	{
		Holder	target;                         // 타겟 홀더
		int		count = 0;                      // 카운트
		float	angle = 0;                      // 현재 각도
		float	addAngle = 360 / amount;        // 더해지는 각도


		while (count < amount)
		{
			// 생성
			target = ObjectPoolManager.GetGameObject("Holder", transform.position).GetComponent<Holder>();
			//target = Instantiate(holderPrefab, new Vector3(fixX, fixY, 0), Quaternion.identity, transform).GetComponent<Holder>();

			// 방향으로 힘 적용
			target.SetVelo(WayVector2(angle, power));

			// 각도 추가
			angle += addAngle;

			count++;

			yield return new WaitForSeconds(term);
		}
	}

	// 4방향으로 한줄씩 회전하며 나가는거
	private IEnumerator ForwayLineRotation()
	{
		Holder	target;                                     // 타겟 홀더
		int		count = 0;                                  // 카운트
		float	angle = UnityEngine.Random.Range(0, 90);	// 현재 각도


		while (count < amount)
		{
			for (int i = 0; i < 4; i++)
			{
				// 생성
				target = ObjectPoolManager.GetGameObject("Holder", transform.position).GetComponent<Holder>();
				//target = Instantiate(holderPrefab, new Vector3(fixX, fixY, 0), Quaternion.identity, transform).GetComponent<Holder>();

				// 방향으로 힘 적용
				target.SetVelo(WayVector2(angle + (90 * i), power));
			}

			if (count % 20 >= 0 && count % 20 <= 5)
			{
				// 각도 추가
				angle += (90 / amount) * 3;
			}

			count++;

			yield return new WaitForSeconds(term);
		}
	}

	// 단 방향으로 네모 발사
	private IEnumerator OnewaySquare()
	{
		Holder	target;                                         // 타겟 홀더
		int		count = 0;                                      // 카운트
		float	angle = UnityEngine.Random.Range(0, 360);		// 방향 각도
		float	gapAngle;                                       // 갭 각도

		gapAngle = 0;
		while (count < amount / 2)
		{
			target = ObjectPoolManager.GetGameObject("Holder", transform.position).GetComponent<Holder>();
			target.SetVelo(WayVector2(angle + gapAngle, power));

			target = ObjectPoolManager.GetGameObject("Holder", transform.position).GetComponent<Holder>();
			target.SetVelo(WayVector2(angle - gapAngle, power));

			gapAngle += 2f;

			count += amount / 25;

			yield return new WaitForSeconds(term);
		}
		
		while (count <= amount)
		{
			target = ObjectPoolManager.GetGameObject("Holder", transform.position).GetComponent<Holder>();
			target.SetVelo(WayVector2(angle + gapAngle, power * 2f));

			target = ObjectPoolManager.GetGameObject("Holder", transform.position).GetComponent<Holder>();
			target.SetVelo(WayVector2(angle - gapAngle, power * 2f));

			gapAngle -= 2f;

			count += amount / 25;

			yield return new WaitForSeconds(term);
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

				yield return new WaitForSeconds(term / 5f);
			}

			yield return null;
		}
	}


	// 코인
	private IEnumerator Coinar()
	{
		Coin	target;										// 타겟 홀더
		float	angle = 0;                                  // 발사 각도
		float	addAngle = (360 / (amount / 2));			// 더해지는 각도


		for (int i = 0; i < amount / 2; i++)
		{
			// 생성
			target = ObjectPoolManager.GetGameObject("Coin", transform.position).GetComponent<Coin>();
			//target = Instantiate(coinPrefab, new Vector3(fixX, fixY, 0), Quaternion.identity, transform).GetComponent<Holder>();

			// 방향으로 힘 적용
			target.SetVelo(WayVector2(angle, power));

			// 분사량에 따라 각도 조절
			angle += addAngle;
		}

		yield return null;
	}
}
