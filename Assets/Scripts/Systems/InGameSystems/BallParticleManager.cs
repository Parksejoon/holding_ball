using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallParticleManager : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private	Material	ballAfterEffect;            // 공 파티클

	public	Sprite[]	particleTextures;			// 파티클 텍스쳐 모음


	// 볼 파티클 재설정
	public void SetBallParticle(int index)
	{
		ballAfterEffect.SetTexture("_MainTex", particleTextures[index].texture);
	}
}
