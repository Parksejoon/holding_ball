using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndLine : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private GameObject	fadeCover;          // 페이드 커버
	[SerializeField]
	private GameObject	blackFadeCover;     // 페이드 커버
	[SerializeField]
	private string		mainScene;          // 메인 씬 이름


	// 트리거 진입
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Ball"))
		{
			StartCoroutine(EndCoroutine());
		}
	}

	// 종료 코루틴
	private IEnumerator EndCoroutine()
	{
		Coroutine fadeCor = null;

		UIEffecter.instance.FadeEffect(fadeCover, Vector2.one, 1f, UIEffecter.FadeFlag.ALPHA, ref fadeCor);

		yield return fadeCor;
		yield return new WaitForSeconds(3f);

		UIEffecter.instance.FadeEffect(blackFadeCover, Vector2.one, 1f, UIEffecter.FadeFlag.ALPHA, ref fadeCor);

		yield return fadeCor;

		PlayerPrefs.SetInt("EndTutorial", 1);
		PlayerPrefs.Save();
		SceneManager.LoadScene(mainScene);
	}
}
