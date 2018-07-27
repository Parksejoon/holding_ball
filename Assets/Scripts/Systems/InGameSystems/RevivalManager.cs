using System.Collections;
using UnityEngine;

public class RevivalManager : MonoBehaviour
{
	public static RevivalManager instance;

	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private GameObject	revivalPanel;			// 부활 패널
	[SerializeField]
	private GameObject	revivalButton;          // 부활 버튼
	[SerializeField]
	private GameObject	cover;					// 커버

	// 수치
	[SerializeField]
	private float		outputTime;             // 패널 활성화 시간

	// 인스펙터 비노출 변수
	// 일반
	IEnumerator			ApearCoroutine;			// 활성화 코루틴
	IEnumerator			DisapearCoroutine;		// 비활성화 코루틴



	// 초기화
	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	// 부활 패널 활성화
	public void OutputRevivalPanel()
	{
		ApearCoroutine = ApearRevivalPanel();
		DisapearCoroutine = DisapearRevivalPanel();

		StartCoroutine(ApearCoroutine);
	}

	// 부활 패널 삭제
	public void DeleteRevivalPanel()
	{
		StopCoroutine(ApearCoroutine);
		StopCoroutine(DisapearCoroutine);

		revivalPanel.SetActive(false);
	}

	// 패널 활성화 코루틴
	private IEnumerator ApearRevivalPanel()
	{
		yield return new WaitForSeconds(1f);

		revivalPanel.SetActive(true);
		UIEffecter.instance.FadeEffect(revivalButton, Vector2.one, 1f, UIEffecter.FadeFlag.ALPHA);
		UIEffecter.instance.FadeEffect(cover, new Vector2(0.7f, 0.0f), 0.5f, UIEffecter.FadeFlag.ALPHA);

		yield return new WaitForSeconds(1.5f);

		StartCoroutine(DisapearCoroutine);
	}

	// 부활 패널 삭제 코루틴
	private IEnumerator DisapearRevivalPanel()
	{
		UIEffecter.instance.FadeEffect(revivalButton, Vector2.zero, outputTime, UIEffecter.FadeFlag.SCALE | UIEffecter.FadeFlag.ALPHA);

		yield return new WaitForSeconds(outputTime);

		Debug.Log("ASD");

		GameManager.instance.StopGame();
	}
}
