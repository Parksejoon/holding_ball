using System.Collections;
using UnityEngine;

public class Laser : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private GameObject		shotParticle;				// 발사 파티클
	// 수치
	[SerializeField]
	private float			startDelay = 2f;			// 발사 전 딜레이

	// 인스펙터 비노출 변수
	// 일반
	private BoxCollider2D	boxCollider2D;              // 이 오브젝트의 충돌체

	// 수치
	[HideInInspector]
	public  float			rotationSpeed;				// 회전 속도


	// 초기화
	private void Awake()
	{
		boxCollider2D = GetComponent<BoxCollider2D>();
	}

	// 시작
	private void Start()
	{
		StartCoroutine(ShotLaser());

		if (Random.Range(0f, 1f) > 0.5f)
		{
			rotationSpeed = -rotationSpeed;
		}
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
			other.GetComponent<Wall>().AddStack(-1);
		}
	}

	// 레이저 발사 코루틴
	private IEnumerator ShotLaser()
	{
		yield return new WaitForSeconds(startDelay);

		Instantiate(shotParticle, Vector3.zero, transform.rotation, transform).transform.localPosition = new Vector3(0.23f, 0, 0); ;
		boxCollider2D.enabled = true;

		yield return new WaitForSeconds(0.8f);

		boxCollider2D.enabled = false;

		yield return new WaitForSeconds(3f);

		Destroy(gameObject);
	}
}
