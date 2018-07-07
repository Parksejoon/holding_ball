using System.IO;
using UnityEngine;

public class Indexer
{
	// 일반 변수
	private string  dataPath;			// 데이터 경로


	// 생성자
	public Indexer()
	{
		dataPath = "Assets/Resources/Data/ColorIndex.txt";
	}

	// 인덱스 저장
	public void SetColorIndex(int index)
	{
		FileStream		fs = new FileStream(dataPath, FileMode.Open);
		StreamReader	sr = new StreamReader(fs);

		string source = sr.ReadLine();
		for (int i = 0; i < index; i++)
		{
			source = sr.ReadLine();
		}

		string[] indexes = source.Split();

		PlayerPrefs.SetInt("BallColor", int.Parse(indexes[0]));
		PlayerPrefs.SetInt("SubColor", int.Parse(indexes[1]));
		PlayerPrefs.SetInt("WarWallColor", int.Parse(indexes[2]));
		PlayerPrefs.SetInt("TopBackColor", int.Parse(indexes[3]));
		PlayerPrefs.SetInt("BotBackColor", int.Parse(indexes[4]));
	}
}
