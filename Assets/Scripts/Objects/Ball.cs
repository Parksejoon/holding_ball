using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // 일반 변수
    private GameObject  bindedHolder;         // 볼이 바인딩되어있는 홀더
    private GameObject  targetHolder;         // 현재 타겟이된 홀더
    private GameManager gameManager;          // 게임 매니저
    public  GameObject  shotLinePrefab;       // 생성될 ShotLine 프리팹
    private GameObject  shotLine;             // ShotLine오브젝트

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
        transform.Translate(Vector3.down * Time.deltaTime * (GameManager.moveSpeed / 3) * speed);
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
        GameManager.moveSpeed = 2.0f;
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
            // 홀더에 락
            isHolding = true;
            speed = 0f;
            transform.parent = bindedHolder.transform;
            transform.localPosition = Vector3.zero;

            // 슛라인 생성
            shotLine = Instantiate(shotLinePrefab, Vector3.zero, Quaternion.identity, transform);
            
            return true;
        }
        else
        {
            // 홀딩 실패
            return false;
        }
    }

    // 홀더에 언홀딩
    public void UnholdingHolder()
    {
        // 홀딩 해제
        isHolding = false;
        speed = 1f;
        transform.parent = null;
        
        // 다음 홀더를 향해 날아감
        targetHolder = shotLine.GetComponent<ShotLine>().GetCatchHolder();;
        Destroy(targetHolder);
    }
}
