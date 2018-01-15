using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotLine : MonoBehaviour
{
    // 일반 변수
    private GameObject       catchHolder;              // 영역에 들어온 홀더들
    private ShotLineCollider shotLineCollider;         // 슛라인 충돌검사


    // 수치
    private float            expandSpeed = 0.05f;      // 범위 확대 속도
    private float            addRange;                 // 추가되는 범위


    // 초기화
    void Awake()
    {
        catchHolder = null;
        shotLineCollider = GetComponent<ShotLineCollider>();

        addRange = expandSpeed * (GameManager.moveSpeed / 10);
        print(addRange);
    }

    // 프레임
    void Update()
    {
        // 점점 범위 확대
        transform.localScale += new Vector3(addRange, addRange);
    }

    // 현재 가지고있는 홀더를 반환
    public GameObject GetCatchHolder()
    {
        shotLineCollider.Judgment();

        return catchHolder;
    }
}
