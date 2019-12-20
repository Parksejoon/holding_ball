using UnityEngine;
using UnityEngine.UI;

public class CurrentVersion : MonoBehaviour
{
	// 인스펙터 비노출 변수
	// 일반
	private Text	targetText;      // 표시할 텍스트


	// 초기화
	private void Awake()
	{
		targetText = gameObject.GetComponent<Text>();
	}

	// 시작
	private void Start()
	{
		// 기능추가.밸런스조절/버그제거
		targetText.text = "ver." + Application.version.ToString();

		//Debug.Log(Application.version);
	}
}
