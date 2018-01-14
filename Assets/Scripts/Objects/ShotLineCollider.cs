using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotLineCollider : MonoBehaviour
{
    // 일반 변수
    private List<GameObject> holderList;           // 홀더 리스트


    // 초기화
    void Awake()
    {
        holderList = GameObject.Find("Main Objects").GetComponent<HolderManager>().holderList;            
    }

    // 프레임
    void Update()
    {
    }
}
