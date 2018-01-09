using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holder : MonoBehaviour
{
    // 일반 변수
    private GameManager gameManager;          // 게임 매니저


    // 초기화
    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // 움직임 처리
    void FixedUpdate()
    {
        transform.Translate(Vector3.up * Time.deltaTime * gameManager.moveSpeed);
    }
}
