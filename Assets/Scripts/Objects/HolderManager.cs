using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolderManager : MonoBehaviour
{
    // 인스펙터 노출 변수
    [SerializeField]
    private GameObject holderPrefab;                               // 생성될 Holder 프리팹

    [Space(20)]

    // 일반 변수
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
            // 홀더생성을 위해 카운트중인지 확인
            // 맞다면 카운트 진행
            if (isPasting)
            {
                pastTime += Time.deltaTime;

                // 카운트가 끝났으면 홀더를 생성하고 카운트 재시작
                if (pastTime >= goalTime)
                {
                    holderList.Add((Instantiate(holderPrefab, new Vector3(Random.Range(-4f, 4f), -8f), Quaternion.identity, transform)).transform);

                    isPasting = false;
                }
            }
            // 아니라면 카운트 시작
            else
            {
                pastTime = 0;
                goalTime = Random.Range(0.1f, 0.5f);

                isPasting = true;
            }
        }
    }
}
