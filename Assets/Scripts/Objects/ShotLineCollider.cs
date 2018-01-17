using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotLineCollider : MonoBehaviour
{
    // 일반 변수
    private List<Transform> holderList;           // 홀더 리스트
    public  List<Transform> perfect;              // 퍼펙트 판정 오브젝트 리스트
    public  List<Transform> great;                // 일반 판정 오브젝트 리스트


    // 초기화
    void Awake()
    {
        holderList = GameObject.Find("Main Objects").GetComponent<HolderManager>().holderList;            
    }
    
    // 판정 측정
    public void Judgment()
    {
        float range = transform.lossyScale.x / 2f;  // 반지름
        float x = transform.position.x;             // 중심 X좌표
        float y = transform.position.y;             // 중심 Y좌표


        for (int i = 0; i < holderList.Count; i++)
        {
            float distance;     // 거리


            distance = Mathf.Sqrt(((holderList[i].position.x - x) * (holderList[i].position.x - x)) + ((holderList[i].position.y - y) * (holderList[i].position.y - y)));
            distance = Mathf.Abs(distance - range);

            if (distance < 0.1f)
            {
                perfect.Add(holderList[i]);
                print("perfect!!!");
            }
            else if (distance < 0.3f)
            {
                great.Add(holderList[i]);
                print("good!");
            }
            else
            {
                print("fail..");
            }
        }

        return;
    }
}
