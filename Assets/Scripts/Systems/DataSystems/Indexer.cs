using UnityEngine;

public class Indexer
{
	// 인덱스 저장
	public void SetColorIndex()
	{
		PlayerPrefs.SetInt("BallColor",		PlayerPrefs.GetInt("BallColorIndex",	0));
		PlayerPrefs.SetInt("SubColor",		PlayerPrefs.GetInt("SubColorIndex",		1));
		PlayerPrefs.SetInt("WarWallColor",	PlayerPrefs.GetInt("WarWallColorIndex", 2));
		PlayerPrefs.SetInt("TopBackColor",	PlayerPrefs.GetInt("TopBackColorIndex", 3));
		PlayerPrefs.SetInt("BotBackColor",	PlayerPrefs.GetInt("BotBackColorIndex", 4));
	}
}
