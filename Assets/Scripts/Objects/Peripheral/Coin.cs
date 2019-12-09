using UnityEngine;
using Random = UnityEngine.Random;

public class Coin : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private ParticleSystem	destroyParticle;

	// 인스펙터 비노출 변수
	// 수치
	private Vector2			velo;				// 속도
	

	// 매 프레임
	private void Update()
	{
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
