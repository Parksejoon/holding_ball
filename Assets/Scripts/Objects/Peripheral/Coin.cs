using UnityEngine;
using Random = UnityEngine.Random;

public class Coin : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private ParticleSystem	destroyParticle;
		
	// 수치
	public	float			bounceCount;         // 튕길 수 있는 횟수

	// 인스펙터 비노출 변수
	// 수치
	private Vector2			velo;				// 속도


	// 시작
	private void Start()
	{
		bounceCount = Random.Range(1, 4);
	}

	// 매 프레임
	private void Update()
	{
		transform.Rotate(new Vector3(0, 0, 3) * GameManager.instance.timeValue);
		transform.Translate(velo * Time.smoothDeltaTime);
	}

	// 속도 조절
	public void SetVelo(Vector2 value)
	{
		velo = value;
	}

	// 파괴 이펙트
	public void DestroyParticle()
	{
		// 파티클 생성
		Instantiate(destroyParticle, transform.position, Quaternion.identity);
	}
}
