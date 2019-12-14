using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialStarter : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private string		tutorialScene;              // 튜토리얼 씬 이름


	// 시작
	// 모든 스크립트에서 awake가 끝난 직후 처음으로 실행되는 start (아마도)
	private void Start()
	{
		if (PlayerPrefs.GetInt("FirstStart", 1) == 1)
		{
			//SceneManager.LoadScene(tutorialScene);
		}
	}
}
