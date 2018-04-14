using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parser
{
	// 아시발자퇴하고싶다

	// 시발 인생 좆같다

	// 코인 저장
	public void SetCoin(int value)
	{
		PlayerPrefs.SetInt("Coin", value);
	}

	// 코인 불러오기
	public int GetCoin()
	{
		return PlayerPrefs.GetInt("Coin", 0);
	}

	// 최고점수 저장
	public void SetBestScore(int value)
	{
		PlayerPrefs.SetInt("BestScore", value);
	}

	// 최고점수 불러오기
	public int GetBestScore()
	{
		return PlayerPrefs.GetInt("BestScore", 0);
	}

	// 초기화
	public void ResetData()
	{
		PlayerPrefs.DeleteAll();
	}
}
