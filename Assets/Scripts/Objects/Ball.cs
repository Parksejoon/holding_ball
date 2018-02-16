using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // 인스펙터 노출 변수
    [SerializeField]
    private GameObject shotLinePrefab;       // 생성될 ShotLine 프리팹

    [Space(20)]

    // 일반 변수
    [HideInInspector]
    public  GameObject  bindedHolder;         // 볼이 바인딩되어있는 홀더
    private GameObject  targetHolder;         // 현재 타겟이된 홀더
    private GameObject  shotLine;             // ShotLine오브젝트
    private Transform   parent;               // 이 오브젝트의 부모
    private GameManager gameManager;          // 게임 매니저
    private Rigidbody2D rigidbody2d;          // 이 오브젝트의 리짓바디

    // 수치
    [HideInInspector]
    public  bool        isHolding;            // 홀딩 상태를 나타냄
    [HideInInspector]
    public  float       shotPower = 3f;       // 발사 속도


    // 초기화
    void Awake()
    {
        parent = transform.parent;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        rigidbody2d = GetComponent<Rigidbody2D>();

        isHolding = false;
    }

    // 초기화
    void Start()
    {
        rigidbody2d.AddForce(Vector2.up * 100);
    }

    // 트리거 진입
    void OnTriggerEnter2D(Collider2D other)
    {
        // 홀더일경우 홀더에 바인딩함
        if (bindedHolder == null && other.gameObject.tag == "Holder")
        {
            BindingHolder(other.gameObject);
        }
    }

    // 트리거 탈출
    void OnTriggerExit2D(Collider2D other)
    {
        // 현재 바인딩중인 홀더일경우 바인딩을 해제함
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

        return;
    }

    // 홀더에 언바인딩
    void UnbindingHolder()
    {
        bindedHolder = null;
        gameManager.InitializeSpeed();

        return;
    }

    // 홀더에 홀딩
    public bool HoldingHolder()
    {
        // 바인딩된 홀더가 있는지 확인
        if (bindedHolder != null)
        {
            // 물리량 초기화
            rigidbody2d.velocity = Vector2.zero;

            // 홀더의 자식으로 변경
            isHolding = true;
            transform.parent = bindedHolder.transform;
            transform.localPosition = Vector3.zero;

            // 슛라인 생성
            shotLine = Instantiate(shotLinePrefab, transform.position, Quaternion.identity, transform);
            
            return true;
        }
        else
        {
            // 홀딩 실패 ( GameManager에서 처리함 )

            return false;
        }
    }

    // 홀더에 언홀딩
    public void UnholdingHolder()
    {
        // 홀더에서 탈출
        isHolding = false;
        transform.parent = parent;

        // 캐치 했는지 확인
        targetHolder = shotLine.GetComponent<ShotLine>().Judgment();

        // 슛라인 삭제
        Destroy(transform.GetChild(0).gameObject);

        // 타겟 홀더를 향해 날아감
        if (targetHolder != null)
        {
            Vector3 shotVector = (targetHolder.transform.position - transform.position);

            shotVector = Vector3.Normalize(shotVector);
            rigidbody2d.AddForce(shotVector * shotPower * gameManager.shotPower);
        }
        
        return;
    }
}
