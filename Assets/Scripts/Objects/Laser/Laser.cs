using System.Collections;
using UnityEngine;

public class Laser : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private GameObject		shotParticle;				// 발사 파티클
	// 수치

	// 인스펙터 비노출 변수
	// 일반
	private BoxCollider2D	boxCollider2D;              // 이 오브젝트의 충돌체

	// 수치
	[HideInInspector]
	public  float			rotationSpeed;              // 회전 속도
	private float			startDelay = 2f;			// 발사 전 딜레이


	// 초기화
	private void Awake()
	{
		boxCollider2D = GetComponent<BoxCollider2D>();
	}

	// 시작
	private void Start()
	{
		StartCoroutine(ShotLaser());
	}

	// 프레임
	private void FixedUpdate()
	{
		transform.Rotate(Vector3.forward * rotationSpeed);
	}

	// 트리거 진입
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Wall"))
		{
			other.GetComponent<Wall>().DamDealWall();
		}
	}

	// 딜레이 조절
	public void SetStartDelay(float delay)
	{
		startDelay = delay;
	}

	// 레이저 발사 코루틴
	private IEnumerator ShotLaser()
	{
		yield return new WaitForSeconds(startDelay);

		Destroy(transform.Find("LaserReady").gameObject);
		Instantiate(shotParticle, transform.position, transform.rotation, transform).transform.localPosition = new Vector3(0.28f, 0, 0);
		boxCollider2D.enabled = true;

		yield return new WaitForSeconds(1f);

		boxCollider2D.enabled = false;

		yield return new WaitForSeconds(6f);

		Destroy(gameObject);
	}
}
