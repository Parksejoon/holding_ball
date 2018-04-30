using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Initializer : MonoBehaviour
{
	// 인스펙터 비노출 변수
	// 일반
	private GameManager		gameManager;                // 게임 매니저
	private UIManager		uiManager;					// UI 매니저
	private Parser			parser;                     // 파서


	// 초기화
	private void Awake()
	{
		gameManager = GetComponent<GameManager>();
		uiManager	= GameObject.Find("Canvas").GetComponent<UIManager>();
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
		int coin = parser.GetCoin();
		int bestScore = parser.GetBestScore();
		int lastScore = parser.GetLastScore();


		gameManager.Initialize(coin, bestScore);
		uiManager.SetText(1, coin.ToString());
		uiManager.SetText(2, bestScore.ToString());
		uiManager.SetText(3, lastScore.ToString());
	}
}
