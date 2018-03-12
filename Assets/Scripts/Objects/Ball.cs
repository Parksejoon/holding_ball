using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // 인스펙터 노출 변수
	// 일반
    [SerializeField]
	private GameObject shotLinePrefab;          // 생성될 ShotLine 프리팹
	
	// 인스펙터 비노출 변수
    // 일반 변수
    [HideInInspector]
	public  GameObject  bindedHolder;           // 볼이 바인딩되어있는 홀더

    private GameObject  targetHolder;			// 현재 타겟이된 홀더
    private GameObject  shotLine;				// ShotLine오브젝트
    private GameManager gameManager;			// 게임 매니저
    private Rigidbody2D rigidbody2d;			// 이 오브젝트의 리짓바디

    // 수치
    [HideInInspector]
	public  bool        isHolding;              // 홀딩 상태를 나타냄
    [HideInInspector]
	public  float       shotPower = 3f;         // 발사 속도


    // 초기화
    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        rigidbody2d = GetComponentInParent<Rigidbody2D>();
		
        isHolding = false;
    }

	// 시작
	private void Start()
	{
		rigidbody2d.AddForce(Vector2.left * 1000f);
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
		if (bindedHolder == other.gameObject && other.gameObject.tag == "Holder")
		{
			UnbindingHolder();
		}
	}

    // 홀더에 바인딩
    void BindingHolder(GameObject holder)
    {
		// 바인딩 홀더를 설정
        bindedHolder = holder;

		Time.timeScale = 0.2f;
    }

    // 홀더에 언바인딩
    void UnbindingHolder()
	{
		// 바인딩 홀더를 초기화
		bindedHolder = null;
	
		Time.timeScale = 1f;
    }

    // 홀더에 홀딩
    public bool HoldingHolder()
    {
        // 바인딩된 홀더가 있는지 확인
        if (bindedHolder != null)
        {
            // 볼의 속도를 영벡터로 변경
            rigidbody2d.velocity = Vector2.zero;

			// 홀더 속도를 영벡터로 변경
			bindedHolder.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

			// 홀딩 상태로 전환
			isHolding = true;

			// 볼의 위치를 중앙으로 이동
            // *ball position = holder position*

            // 슛라인 생성
            shotLine = Instantiate(shotLinePrefab, transform.position, Quaternion.identity, transform);
            
            return true;
        }
        else
        {
			// 홀딩 실패
			// *GameManager에서 처리함*

            return false;
        }
    }

    // 홀더에 언홀딩
    public void UnholdingHolder()
	{
		// 홀더에서 탈출
		isHolding = false;

		// 홀더 파괴
		bindedHolder.tag = "Untagged";
		Destroy(bindedHolder.gameObject);

		// 캐치 했는지 판정
		targetHolder = shotLine.GetComponent<ShotLine>().Judgment();

		// 슛라인 파괴
		Destroy(shotLine.gameObject);

        // 타겟 홀더를 향해 날아감
        if (targetHolder != null)
        {
			// 날아갈 벡터의 방향
            Vector3 shotVector = (targetHolder.transform.position - transform.position);

			// 날아갈 파워 설정
            shotVector = Vector3.Normalize(shotVector);
            rigidbody2d.AddForce(shotVector * shotPower * gameManager.shotPower);

			// 점수 추가
			gameManager.AddScore(1);
        }

		// 원래 시간으로 초기화
		Time.timeScale = 1f;
    }
}
