﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolderManager : MonoBehaviour
{
	// 싱글톤
	public static HolderManager instance;							// 인스턴스

	// 델리게이트
	private delegate IEnumerator HolderAlgorithm();					// 홀더 샷 알고리즘 델리게이트

	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private GameObject	holderPrefab;					// 생성될 Holder 프리팹
	[SerializeField]
	private GameObject	coinPrefab;						// 생성될 Coin 프리팹
	[SerializeField]
	private float		fixX;							// 생성 고정 X좌표
	[SerializeField]
	private float		fixY;							// 생성 고정 Y좌표
	[SerializeField]
	private float		power;							// 발사 파워
	[SerializeField]
	private float		minRespawnTime;					// 리스폰 최소시간
	[SerializeField]
	private float		maxRespawnTime;					// 리스폰 최대시간
	[SerializeField]
	private float		minTerm;						// 중간 텀 최소시간
	[SerializeField]
	private float		maxTerm;						// 중간 텀 최대시간
	[SerializeField]
	private float		amount;							// 소환되는 양
	
	public	Material	originHolderMat;				// 기본 홀더 머티리얼

	// 인스펙터 비노출 변수
	// 일반
	[HideInInspector]
	public  List<Transform>   holderList = new List<Transform>();    // 홀더 리스트
    
	private HolderAlgorithm[] holderAlgorithm;                       // 홀더 샷 알고리즘 목록

	// 수치
	private float			pastTime;                              // 경과 시간
	private float			goalTime;                              // 목표 시간
	private bool			isPasting;                             // 시간이 흘러가고있는가?


	// 초기화
	private void Awake()
	{
		instance = this;
	}

	// 시작
	public void Start()
	{
		ObjectPoolManager.AddObject("Holder", holderPrefab, transform);
		ObjectPoolManager.Create("Holder", 600);

		// Tornado Slug Round Compression Quarter Shift Coinar
		// 알고리즘 델리게이트 초기화
		holderAlgorithm = new []
		{
			new HolderAlgorithm(Tornado),
			Slug,
			Round,
			Compression,
			Quarter,
			Shift,
			Coinar
		};
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
					RandomPattern();

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

	// 각도와 스칼라에 따른 방향 벡터
	private Vector2 WayVector2(float radian, float finalPower)
	{
		return new Vector2((Mathf.Cos(radian * Mathf.PI / 180) * finalPower), Mathf.Sin(radian * Mathf.PI / 180) * finalPower);
	}

	// 랜덤 패턴
	private void RandomPattern()
	{
		// 0	   1    2     3           4       5     6
		// Tornado Slug Round Compression Quarter Shift Coinar
		int index = UnityEngine.Random.Range(0, 50);

		if (index >= 48)
		{
			index = 6;
		}
		else
		{
			index %= 6;
		}

		StartCoroutine(holderAlgorithm[0]());
	}

	// 회오리
	private IEnumerator Tornado()
	{
		Holder target;												// 타겟 홀더
		float		term = UnityEngine.Random.Range(minTerm, maxTerm);	// 텀
		int			count = 0;											// 카운트
		float		angle = 0;											// 현재 각도
		float		addAngle = 360 / amount;							// 더해지는 각도
	
		
		while (count < amount)
		{
			// 생성
			target = ObjectPoolManager.GetGameObject("Holder", new Vector3(fixX, fixY, 0)).GetComponent<Holder>();
			//target = Instantiate(holderPrefab, new Vector3(fixX, fixY, 0), Quaternion.identity, transform).GetComponent<Holder>();

			// 방향으로 힘 적용
			target.SetVelo(WayVector2(angle, power));

			// 각도 추가
			angle += addAngle;

			count++;

			yield return new WaitForSeconds(term);
		}
	}

	// 양방향 분사
	private IEnumerator Slug()
	{
		Holder target;										  // 타겟 홀더
		float		term = UnityEngine.Random.Range(minTerm, maxTerm);        // 텀
		int			count = 0;							   	      // 카운트
		float		angle;										  // 방향 각도
		float		addAngle = (50 / (amount / 10));			  // 더해지는 각도

		while (count < amount)
		{
			angle = UnityEngine.Random.Range(0, 360);

			for (int i = 0; i < amount / 10; i++)
			{
				// 생성
				target = ObjectPoolManager.GetGameObject("Holder", new Vector3(fixX, fixY, 0)).GetComponent<Holder>();
				//target = Instantiate(holderPrefab, new Vector3(fixX, fixY, 0), Quaternion.identity, transform).GetComponent<Holder>();

				// 방향으로 힘 적용
				target.SetVelo(WayVector2(angle, power));

				// 생성
				target = ObjectPoolManager.GetGameObject("Holder", new Vector3(fixX, fixY, 0)).GetComponent<Holder>();
				//target = Instantiate(holderPrefab, new Vector3(fixX, fixY, 0), Quaternion.identity, transform).GetComponent<Holder>();

				// 방향으로 힘 적용
				target.SetVelo(WayVector2(180 + angle, power));

				// 분사량에 따라 각도 조절
				angle += addAngle;
			}

			count += (int)(amount / 10) * 2;

			yield return new WaitForSeconds((term + 0.5f) * 2f);
		}
	}

	// 전반향 분사
	private IEnumerator Round()
	{
		Holder target;												// 타겟 홀더
		float		term = UnityEngine.Random.Range(minTerm, maxTerm);	// 텀
		int			count = 0;											// 카운트
		float		angle;												// 방향 각도
		float		addAngle = (360 / (amount / 2));					// 더해지는 각도

		while (count < amount)
		{
			angle = 0;

			for (int i = 0; i < amount / 2; i++)
			{
				// 생성
				target = ObjectPoolManager.GetGameObject("Holder", new Vector3(fixX, fixY, 0)).GetComponent<Holder>();
				//target = Instantiate(holderPrefab, new Vector3(fixX, fixY, 0), Quaternion.identity, transform).GetComponent<Holder>();

				// 방향으로 힘 적용
				target.SetVelo(WayVector2(angle, power));

				// 분사량에 따라 각도 조절
				angle += addAngle;
			}

			count += (int)amount / 4;

			yield return new WaitForSeconds(term + 0.5f);
		}
	}

	// 압축
	private IEnumerator Compression()
	{
		Holder target;										// 타겟 홀더
		float		term = UnityEngine.Random.Range(minTerm, maxTerm);      // 텀
		int			count = 0;                                  // 카운트
		float		angle = UnityEngine.Random.Range(0, 360);				// 현재 각도
		float		addAngle = 90 / (amount / 2);				// 더해지는 각도
		float		minusAngle = 90;							// 간격

		while (count < amount)
		{
			// 생성
			target = ObjectPoolManager.GetGameObject("Holder", new Vector3(fixX, fixY, 0)).GetComponent<Holder>();
			//target = Instantiate(holderPrefab, new Vector3(fixX, fixY, 0), Quaternion.identity, transform).GetComponent<Holder>();

			// 방향으로 힘 적용
			target.SetVelo(WayVector2(angle + minusAngle, power));

			// 생성
			target = ObjectPoolManager.GetGameObject("Holder", new Vector3(fixX, fixY, 0)).GetComponent<Holder>();
			//target = Instantiate(holderPrefab, new Vector3(fixX, fixY, 0), Quaternion.identity, transform).GetComponent<Holder>();

			// 방향으로 힘 적용
			target.SetVelo(WayVector2(angle - minusAngle, power));

			// 각도 추가
			minusAngle -= addAngle;

			count++;

			yield return new WaitForSeconds(term);
		}
	}

	// 4분할
	private IEnumerator Quarter()
	{
		Holder target;									    // 타겟 홀더
		float		term = UnityEngine.Random.Range(minTerm, maxTerm);      // 텀
		int			count = 0;                                  // 카운트
		float		angle = UnityEngine.Random.Range(0, 90);				// 현재 각도
		
		while (count < amount)
		{
			for (int i = 0; i < 4; i++)
			{
				// 생성
				target = ObjectPoolManager.GetGameObject("Holder", new Vector3(fixX, fixY, 0)).GetComponent<Holder>();
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

	// 변속
	private IEnumerator Shift()
	{
		Holder target;										 // 타겟 홀더
		float		term = UnityEngine.Random.Range(minTerm, maxTerm);       // 텀
		int			count = 0;                                   // 카운트
		float		angle;								   	     // 방향 각도
		float		finalPower;									 // 최종 파워
		float		addAngle = 90 / (amount / 4);				 // 더해지는 각도

		while (count < amount)
		{
			for (int i = 0; i < 4; i++)
			{
				angle = 0;
				finalPower = power * UnityEngine.Random.Range(0.5f, 1.5f);

				for (int j = 0; j < amount / 4; j++)
				{
					// 생성
					target = ObjectPoolManager.GetGameObject("Holder", new Vector3(fixX, fixY, 0)).GetComponent<Holder>();
					//target = Instantiate(holderPrefab, new Vector3(fixX, fixY, 0), Quaternion.identity, transform).GetComponent<Holder>();

					// 방향으로 힘 적용
					target.SetVelo(WayVector2(angle + (90 * i), finalPower));

					// 분사량에 따라 각도 조절
					angle += addAngle;
				}
			}

			count += (int)amount / 2;

			yield return new WaitForSeconds(term + 0.5f);
		}
	}

	// 코인
	private IEnumerator Coinar()
	{
		Coin target;												// 타겟 홀더
		float	term = UnityEngine.Random.Range(minTerm, maxTerm);		// 텀
		float	angle = 0;                                              // 발사 각도
		float	addAngle = (360 / (amount / 2));						// 더해지는 각도

		for (int i = 0; i < amount / 2; i++)
		{
			// 생성
			target = ObjectPoolManager.GetGameObject("Holder", new Vector3(fixX, fixY, 0)).GetComponent<Coin>();
			//target = Instantiate(coinPrefab, new Vector3(fixX, fixY, 0), Quaternion.identity, transform).GetComponent<Holder>();

			// 방향으로 힘 적용
			target.SetVelo(WayVector2(angle, power));

			// 분사량에 따라 각도 조절
			angle += addAngle;
		}

		yield return null;
	}
}
