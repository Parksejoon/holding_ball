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

		if (PlayerPrefs.GetInt("FirstPurchaseParser", 0) == 0)
		{
			FirstInitialize();
		}
	}

	// 첫 초기화
	private void FirstInitialize()
	{
		PlayerPrefs.SetInt("FirstPurchaseParser", 1);

		string particlePurchase	= "1,0,0,0,0,0,0,0,0,0,0,";
		string colorPurchase	= "1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,";

		PlayerPrefs.SetString("ParticlePurchase", particlePurchase);
		PlayerPrefs.SetString("ColorPurchase", colorPurchase);

		PlayerPrefs.Save();
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

		PlayerPrefs.SetString("ParticlePurchase", dataResult);
		PlayerPrefs.Save();
	}

	// 색 구매 기록 불러오기
	public bool GetColorPurchaseData(int index)
	{
		string[] dataArr = PlayerPrefs.GetString("ColorPurchase").Split(',');

		if (dataArr[index] == "0")
		{
			return false;
		}
		else
		{
			return true;
		}
	}

	// 색 구매 기록 저장하기
	public void SetColorPurchaseData(int index, bool isPurchase)
	{
		string[] dataArr = PlayerPrefs.GetString("ColorPurchase").Split(',');
		string dataResult = "";

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

		PlayerPrefs.SetString("ColorPurchase", dataResult);
		PlayerPrefs.Save();
	}
}
