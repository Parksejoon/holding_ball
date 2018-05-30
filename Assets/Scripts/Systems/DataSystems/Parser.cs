using System;
using System.Collections;
using System.IO;
using UnityEngine;

namespace Systems.DataSystems
{
	public class Parser
	{
		// 일반
		private string dataPath = "Assets/Resources/Data/ColorData.txt";


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

		// 최근점수 저장
		public void SetLastScore(int value)
		{
			PlayerPrefs.SetInt("LastScore", value);
		}
	
		// 최근점수 불러오기
		public int GetLastScore()
		{
			return PlayerPrefs.GetInt("LastScore", 0);
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

		// 컬러 인덱스를 저장
		public void SetColorIndex(int ball, int wall, int warWall, int topBack, int botBack)
		{
			PlayerPrefs.SetInt("BallColor", ball);
			PlayerPrefs.SetInt("WallColor", wall);
			PlayerPrefs.SetInt("WarWallColor", warWall);
			PlayerPrefs.SetInt("TopBackColor", topBack);
			PlayerPrefs.SetInt("BotBackColor", botBack);
		}

		// 컬러 불러오기
		public void GetColor(ShaderManager shaderManager)
		{
			// 데이터 파싱
			FileStream   fs = new FileStream(dataPath, FileMode.Open);
			StreamReader sr = new StreamReader(fs);
			ArrayList	 cListR = new ArrayList();
			ArrayList	 cListG = new ArrayList();
			ArrayList	 cListB = new ArrayList();

			string source = sr.ReadLine();	
			while (source != null)
			{
				string[] result = source.Split();

				cListR.Add(float.Parse(result[0]) / 255f);
				cListG.Add(float.Parse(result[1]) / 255f);
				cListB.Add(float.Parse(result[2]) / 255f);

				source = sr.ReadLine();
			}

			int index;


			// 베이스
			index = PlayerPrefs.GetInt("BallColor");
			shaderManager.baseColor = new Color((float)cListR[index], (float)cListG[index], (float)cListB[index]);


			// 벽
			index = PlayerPrefs.GetInt("WallColor");
			shaderManager.wallColor = new Color((float)cListR[index], (float)cListG[index], (float)cListB[index]);


			// 위험 벽
			index = PlayerPrefs.GetInt("WarWallColor");
			shaderManager.warWallColor = new Color((float)cListR[index], (float)cListG[index], (float)cListB[index]);


			// 뒷배경 위
			index = PlayerPrefs.GetInt("TopBackColor");
			shaderManager.topBackColor = new Color((float)cListR[index], (float)cListG[index], (float)cListB[index]);


			// 뒷배경 아래
			index = PlayerPrefs.GetInt("BotBackColor");
			shaderManager.botBackColor = new Color((float)cListR[index], (float)cListG[index], (float)cListB[index]);
		}
	}
}
