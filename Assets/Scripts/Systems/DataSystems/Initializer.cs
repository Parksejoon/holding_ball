using UnityEngine;

public class Initializer : MonoBehaviour
{
	// 시작
	private void Start()
	{
		Initialize();
	}

	// 초기화
	private void Initialize()
	{
		Application.targetFrameRate = 60;

		int coin	  = PlayerPrefs.GetInt("Coin", 0);
		int bestScore = PlayerPrefs.GetInt("BestScore", 0);
		
		GameManager.instance.Initialize(coin, bestScore);
		UIEffecter.instance.SetText(1, coin.ToString());
		UIEffecter.instance.SetText(0, bestScore.ToString());

		// 도전 과제 시스템 초기화
		//ChallengeManager.instance.ClearScoreChallenge(bestScore);
		//ChallengeManager.instance.ClearCoinChallenge(coin);
	}
}
