using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeManager : MonoBehaviour
{
	public static ChallengeManager instance;

	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private ParticlePicker[]	particlePickers;            // 파티클 피커들
	[SerializeField]
	private GameObject			challengeText;				// 챌린지 텍스트


	// 초기화 
	private void Awake()
	{
		instance = this;
	}

	// 점수 도전과제
	public void ClearScoreChallenge(int score)
	{
		// 1000점마다 도전과제 1단계씩
		// 1 ~ 5
		if (score <= 5000)
		{
			ClearChallenge(score / 1000);
		}
	}

	// 코인 도전과제
	public void ClearCoinChallenge(int coin)
	{
		if (coin >= 100)
		{
			ClearChallenge(6);
		}
		else if (coin >= 200)
		{
			ClearChallenge(7);
		}
	}

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

			// 도전과제 클리어 문구 아웃풋
			StartCoroutine(ChallengeClearText());
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

	// 도전과제 클리어 문구
	private IEnumerator ChallengeClearText()
	{
		UIEffecter.instance.FadeEffect(challengeText, Vector2.one, 0.2f, UIEffecter.FadeFlag.ALPHA);

		yield return new WaitForSeconds(0.8f);
		
		UIEffecter.instance.FadeEffect(challengeText, Vector2.zero, 0.2f, UIEffecter.FadeFlag.ALPHA);
	}
}
