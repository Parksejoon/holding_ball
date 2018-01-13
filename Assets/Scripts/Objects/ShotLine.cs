using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotLine : MonoBehaviour
{
    // 수치
    private float expandSpeed = 0.05f;      // 범위 확대 속도
    private float addRange;                 // 추가되는 범위


    // 초기화
    void Awake()
    {
        addRange = expandSpeed * (GameManager.moveSpeed / 10);
        print(addRange);
    }

    // 프레임
    void Update()
    {
        // 점점 범위 확대
        transform.localScale += new Vector3(addRange, addRange);
    }
}
