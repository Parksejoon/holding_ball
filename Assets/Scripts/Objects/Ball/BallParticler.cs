using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallParticler : MonoBehaviour
{
	// 인스펙터 비노출 변수
	// 일반
	private ParticleSystem	ballParticle;        // 공 파티클


	// 초기화
	private void Awake()
	{
		ballParticle = GetComponent<ParticleSystem>();

		ballParticle.Stop();
	}

	// 파티클 온
	public void SetParticle(bool enabled)
	{
		if (enabled)
		{
			ballParticle.Play();
		}
		else
		{
			ballParticle.Stop();
		}
	}
}
