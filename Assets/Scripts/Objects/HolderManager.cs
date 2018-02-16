using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolderManager : MonoBehaviour
{
    // 인스펙터 노출 변수
    [SerializeField]
    private GameObject holderPrefab;                               // 생성될 Holder 프리팹
    [SerializeField]
    private float      rangeX;                                     // 생성 X좌표 랜덤 범위
    [SerializeField]
    private float      fixY;                                       // 생성 고정 Y좌표
    [SerializeField]
    private float      minRespawnTime;                             // 리스폰 최소시간
    [SerializeField]
    private float      maxRespawnTime;                             // 리스폰 최대시간
    [SerializeField]
    private float      amount;                                     // 한번 리스폰될때 양

    // 일반 변수
    [HideInInspector]
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
                    // 지정된 양만큼 홀더를 생성
                    for (int i = 0; i < amount; i++)
                    {
                        holderList.Add((Instantiate(holderPrefab, new Vector3(Random.Range(-rangeX, rangeX), fixY), Quaternion.identity, transform)).transform);
                    }

                    isPasting = false;
                }
            }
            // 아니라면 카운트 시작
            else
            {
                pastTime = 0;
                goalTime = Random.Range(minRespawnTime, maxRespawnTime);

                isPasting = true;
            }
        }
    }
}
