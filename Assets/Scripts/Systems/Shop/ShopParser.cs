using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopParser
{
	public static ShopParser instance;          // 싱글톤 인스턴스
	

	// 생성자
	public ShopParser()
	{
		// 싱글톤 초기화
		if (instance == null)
		{
			instance = this;
		}
	}

	// 파티클 구매 기록 불러오기
	public bool GetParticlePurchaseData(int index)
	{
		string[] dataArr = PlayerPrefs.GetString("ParticlePurchase").Split(',');

		if (dataArr[index] == "0")
		{
			return false;
		}
		else
		{
			return true;
		}
	}

	// 파티클 구매 기록 저장하기
	public void SetParticlePurchaseData(int index, bool isPurchase)
	{
		string[]	dataArr = PlayerPrefs.GetString("ParticlePurchase").Split(',');
		string		dataResult = "";

		if (isPurchase)
		{
			dataArr[index] = "1";
		}
		else
		{
			dataArr[index] = "0";
		}

		foreach (string data in dataArr)
		{
			dataResult += data + ",";
		}
	}
}
