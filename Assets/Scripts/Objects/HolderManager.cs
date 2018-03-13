using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolderManager : MonoBehaviour
{
    // 인스펙터 노출 변수
	// 일반
    [SerializeField]
	private GameObject holderPrefab;                               // 생성될 Holder 프리팹
    [SerializeField]
	private float      fixX;                                       // 생성 고정 X좌표
    [SerializeField]
	private float      fixY;                                       // 생성 고정 Y좌표
	[SerializeField]
	private float	   power;                                      // 발사 파워
	[SerializeField]
	private float	   term = 0.5f;				                   // 중간 텀
	[SerializeField]
	private float      minRespawnTime;                             // 리스폰 최소시간
    [SerializeField]
	private float      maxRespawnTime;                             // 리스폰 최대시간
    [SerializeField]
	private float      amount;                                     // 소환되는 양

	// 인스펙터 비노출 변수
    // 일반
	public  List<Transform> holderList = new List<Transform>();    // 홀더 리스트
    
	private Ball            ball;                                  // 볼

    // 수치
    private float           pastTime;                              // 경과 시간
    private float           goalTime;                              // 목표 시간
    private bool            isPasting = false;                     // 시간이 흘러가고있는가?


    // 초기화
    void Start()
    {
        ball = GameObject.FindWithTag("Ball").GetComponent<Ball>();
    }

    // 프레임
    void Update()
    {
		// 볼이 홀딩상태 또는 바인딩상태가 아닐때만 시간을 측정
		if (ball.bindedHolder == null)
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
				goalTime = Random.Range(minRespawnTime, maxRespawnTime);

				// 카운트 시작
				isPasting = true;
			}
		}
	}

	// 랜덤 패턴
	private void RandomPattern()
	{
		StartCoroutine(Tornado());
	}

	// 회오리 패턴
	private IEnumerator Tornado()
	{
		int	  count = 0;							// 몇개를 발사했는지 카운트
		float nowAngle = 0;                         // 현재 각도
		float addAngle = 360 / amount;				// 더해지는 각도
		Rigidbody2D target;							// 타겟 홀더

		while (count < amount)
		{
			// 생성
			target = Instantiate(holderPrefab, new Vector3(fixX, fixY, 0), Quaternion.identity, transform).GetComponent<Rigidbody2D>();

			// 방향으로 힘 적용
			target.AddForce(new Vector2((Mathf.Cos(nowAngle * Mathf.PI / 180) * power), Mathf.Sin(nowAngle * Mathf.PI / 180) * power));

			// 각도 추가
			nowAngle += addAngle;

			count++;

			yield return new WaitForSeconds(term);
		}

		yield return null;
	}
}

