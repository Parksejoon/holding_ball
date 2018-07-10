using System.Collections;
using System.IO;
using UnityEngine;

public class Parser
{
	public static Parser instance;								// 싱글톤 인스턴스

	// 일반 변수
	private string		dataPath;								// 데이터 경로

	private ArrayList	colorListR = new ArrayList();			// RGB값들 리스트
	private ArrayList	colorListG = new ArrayList();
	private ArrayList	colorListB = new ArrayList();


	// 생성자
	public Parser()
	{
		// 싱글톤 초기화
		if (instance == null)
		{
			instance = this;
		}

		// 데이터 파싱
		dataPath = "Data/ColorData";

		TextAsset	textAsset	= Resources.Load<TextAsset>(dataPath);
		string[]	colorText	= textAsset.text.Split('\n');

		foreach (string source in colorText)
		{
			string[] result = source.Split();

			colorListR.Add(float.Parse(result[0]) / 255f);
			colorListG.Add(float.Parse(result[1]) / 255f);
			colorListB.Add(float.Parse(result[2]) / 255f);
		}
	}

	// 초기화
	public void ResetData()
	{
		PlayerPrefs.DeleteAll();
	}

	// 색 가져오기
	public Color GetColor(int index)
	{
		return new Color((float)colorListR[index], (float)colorListG[index], (float)colorListB[index]);
	}
}
