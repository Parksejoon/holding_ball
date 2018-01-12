using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // 일반 변수
    public  GameObject  bindedHolder;         // 볼이 바인딩되어있는 홀더
    private GameManager gameManager;          // 게임 매니저

    // 수치
    private float       speed = 1f;           // 볼 자체의 속도
    private bool        isHolding;            // 홀딩 상태를 나타냄


    // 초기화
    void Awake()
    {
        isHolding = false;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // 프레임 ( 물리 처리 )
    void FixedUpdate()
    {
        transform.Translate(Vector3.down * Time.deltaTime * (gameManager.moveSpeed / 3) * speed);
    }

    // 트리거 진입
    void OnTriggerEnter2D(Collider2D other)
    {
        if (bindedHolder == null && other.gameObject.tag == "Holder")
        {
            BindingHolder(other.gameObject);
        }
    }

    // 트리거 탈출
    void OnTriggerExit2D(Collider2D other)
    {
        if (bindedHolder == other.gameObject)
        {
            UnbindingHolder();
        }
    }

    // 홀더에 바인딩
    void BindingHolder(GameObject holder)
    {
        bindedHolder = holder;
        gameManager.moveSpeed = 2.0f;
    }

    // 홀더에 언바인딩
    void UnbindingHolder()
    {
        bindedHolder = null;
        gameManager.InitializeSpeed();
    }

    // 홀더에 홀딩
    public bool HoldingHolder()
    {
        // 바인딩된 홀더가 있는지 확인
        if (bindedHolder != null)
        {
            // 반인딩 -> 홀딩으로 전환
            isHolding = true;
            speed = 0;
            transform.parent = bindedHolder.transform;
            transform.localPosition = Vector3.zero;

            return true;
        }
        else
        {
            // 홀딩 실패
            return false;
        }
    }
}
