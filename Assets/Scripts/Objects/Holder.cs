using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holder : MonoBehaviour
{
    // 일반 변수
    private GameManager   gameManager;          // 게임 매니저 
    private HolderManager holderManager;        // 홀더 매니저

    // 수치
    private float       speed = 1f;             // 홀더 자체적 속도

    
    // 초기화
    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        holderManager = GameObject.Find("Main Objects").GetComponent<HolderManager>();
    }

    // 프레임 ( 물리 처리 )
    void FixedUpdate()
    {
        // 움직임
        transform.Translate(Vector3.up * Time.deltaTime * GameManager.moveSpeed * speed);
    }

    // 삭제
    void OnDestroy()
    {
        // 홀더 리스트에서 해당 항목을 삭제
        holderManager.holderList.RemoveAt(0);
    }
}
