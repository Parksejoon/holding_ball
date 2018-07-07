using System.Collections;
using System.IO;
using UnityEngine;

public class Parser
{
	// 일반 변수
	private string		dataPath;								// 데이터 경로

	private ArrayList	colorListR = new ArrayList();			// RGB값들 리스트
	private ArrayList	colorListG = new ArrayList();
	private ArrayList	colorListB = new ArrayList();


	// 생성자
	public Parser()
	{
		dataPath = "Assets/Resources/Data/ColorData.txt";
		
		// 색 데이터 파싱
		FileStream	 fs = new FileStream(dataPath, FileMode.Open);
		StreamReader sr = new StreamReader(fs);

		string source = sr.ReadLine();
		while (source != null)
		{
			string[] result = source.Split();

			colorListR.Add(float.Parse(result[0]) / 255f);
			colorListG.Add(float.Parse(result[1]) / 255f);
			colorListB.Add(float.Parse(result[2]) / 255f);

			source = sr.ReadLine();
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
