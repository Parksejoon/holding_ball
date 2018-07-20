using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeManager : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private ParticlePicker[]	particlePickers;			// 파티클 피커들


	// 도전과제 클리어
	// 파티클 획득 = 도전과제 클리어
	public void ClearChallenge(int index)
	{
		// 만약 파티클이 획득 안되어있으면 도전과제 클리어 안된 것
		if (!ShopParser.instance.GetParticlePurchaseData(index))
		{
			// 파티클 획득
			ShopParser.instance.SetParticlePurchaseData(index, true);

			// 파티클 갱신
			RefreshParticle();
		}
	}

	// 파티클 갱신
	private void RefreshParticle()
	{
		foreach (ParticlePicker particlePicker in particlePickers)
		{
			particlePicker.Refresh();
		}
	}
}
