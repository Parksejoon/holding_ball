using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotLineCollider : MonoBehaviour
{
    // 일반 변수
    private List<Transform> holderList;           // 홀더 리스트
    public  List<Transform> perfect;              // 퍼펙트 판정 오브젝트 리스트
    public  List<Transform> good;                 // 일반 판정 오브젝트 리스트


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

        perfect = new List<Transform>();
        good = new List<Transform>();


        for (int i = 0; i < holderList.Count; i++)
        {
            float distance;     // 거리


            // 거리를 측정해서 판정진행
            distance = Mathf.Sqrt(((holderList[i].position.x - x) * (holderList[i].position.x - x)) + ((holderList[i].position.y - y) * (holderList[i].position.y - y)));
            distance = Mathf.Abs(distance - range);
            
            // 퍼펙트
            if (distance < 0.1f)
            {
                perfect.Add(holderList[i]);
            }
            // 굿
            else if (distance < 0.3f)
            {
                good.Add(holderList[i]);
            }
        }

        return;
    }
}
