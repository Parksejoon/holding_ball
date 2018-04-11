using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private ParticleSystem	destroyParticle;
		
	// 수치
	public float			bounceCount = 0;			// 튕길 수 있는 횟수
		

	// 파괴 이펙트
	public void DestroyParticle()
	{
		// 파티클 생성
		Instantiate(destroyParticle, transform.position, Quaternion.identity);
	}
}
