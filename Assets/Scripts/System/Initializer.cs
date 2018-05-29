using UnityEngine;

namespace System
{
	public class Initializer : MonoBehaviour
	{
		// 인스펙터 비노출 변수
		// 일반
		private Parser			parser;                     // 파서


		// 초기화
		private void Awake()
		{
			parser		= new Parser();
		}

		// 시작
		private void Start()
		{
			Initialize();
		}

		// 초기화
		private void Initialize()
		{
			int coin 	  = parser.GetCoin();
			int bestScore = parser.GetBestScore();
			int lastScore = parser.GetLastScore();


			GameManager.instance.Initialize(coin, bestScore);
			UIManager.instance.SetText(1, coin.ToString());
			UIManager.instance.SetText(2, bestScore.ToString());
			UIManager.instance.SetText(3, lastScore.ToString());
		}
	}
}
