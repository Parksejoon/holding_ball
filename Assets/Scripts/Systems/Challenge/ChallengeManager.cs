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
	private GameObject			challengeText;              // 챌린지 텍스트
	[SerializeField]
	private GameObject			challengeName;              // 챌린지 이름
	[SerializeField]
	private GameObject			challengePanel;             // 챌린지 패널
	[SerializeField]
	private GameObject			challengeStick;             // 챌린지 스틱

	[TextArea]
	public	string[]			challengeNames;				// 챌린지 이름들

	// 비노출 변수
	// 일반
	private Coroutine			currentCoroutine;			// 현재 코루틴


	// 초기화 
	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	// 점수 도전과제
	public void ClearScoreChallenge(int score)
	{
		// 1000점마다 도전과제 1단계씩
		// 1 ~ 5
		if (score <= 2500)
		{
			ClearChallenge(score / 500);
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

			// 도전과제 클리어 애니메이션 아웃풋
			StartCoroutine(ChallengeClear(index));
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

	// 도전과제 클리어
	private IEnumerator ChallengeClear(int index)
	{
		if (currentCoroutine != null)
		{
			yield return currentCoroutine;
		}

		currentCoroutine = StartCoroutine(ChallengeClearAnimation(index));
	}

	// 도전과제 클리어 애니메이션
	private IEnumerator ChallengeClearAnimation(int index)
	{
		Coroutine fadeCor = null;

		// 도전과제 이름 갱신
		challengeName.GetComponent<Text>().text = challengeNames[index];

		// 값들을 조정하고
		challengePanel.transform.position = new Vector2(0, 50);
		challengeStick.transform.localScale = new Vector2(2, 0.07f);

		challengePanel.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
		challengeStick.GetComponent<Image>().color = new Color(1, 1, 1, 1);

		challengeName.GetComponent<Text>().color = new Color(1, 1, 1, 1);
		challengeText.GetComponent<Text>().color = new Color(1, 1, 1, 1);


		// 패널을 켬
		challengePanel.SetActive(true);

		// 챌린지 텍스트, 챌린지 이름을 알파페이드
		UIEffecter.instance.FadeEffect(challengeText, Vector2.one, 0.1f, UIEffecter.FadeFlag.ALPHA);
		UIEffecter.instance.FadeEffect(challengeName, Vector2.one, 0.1f, UIEffecter.FadeFlag.ALPHA);

		// 스틱을 스케일 페이드
		UIEffecter.instance.FadeEffect(challengeStick, new Vector2(8f, 0.07f), 7f, UIEffecter.FadeFlag.SCALE);

		// 챌린지 패널을 아래로 포지션 페이드
		UIEffecter.instance.FadeEffect(challengePanel, new Vector2(0, 37), 0.1f, UIEffecter.FadeFlag.POSITION, ref fadeCor);

		yield return fadeCor;

		// 챌린지 패널을 아래로 포지션 페이드
		UIEffecter.instance.FadeEffect(challengePanel, new Vector2(0, 33), 4f, UIEffecter.FadeFlag.POSITION, ref fadeCor);

		yield return new WaitForSeconds(2f);

		// 챌린지 패널, 텍스트, 챌린지 이름, 스틱을 알파페이드
		UIEffecter.instance.FadeEffect(challengePanel, Vector2.zero, 1f, UIEffecter.FadeFlag.ALPHA);
		UIEffecter.instance.FadeEffect(challengeText, Vector2.zero, 1f, UIEffecter.FadeFlag.ALPHA);
		UIEffecter.instance.FadeEffect(challengeName, Vector2.zero, 1f, UIEffecter.FadeFlag.ALPHA);
		UIEffecter.instance.FadeEffect(challengeStick, Vector2.zero, 1f, UIEffecter.FadeFlag.ALPHA);

		yield return fadeCor;

		// 패널을 끔
		challengePanel.SetActive(false);
	}
}
