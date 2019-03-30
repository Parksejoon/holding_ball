using UnityEngine.UI;
using UnityEngine;

public class TimerText : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private Text	timerText;          // 타이머 텍스트

	// 인스펙터 비노출 변수
	// 수치
	private float	timer;               // 타이머


	// 초기화
	private void Awake()
	{
		if (timerText == null)
		{
			timerText = GetComponent<Text>();
		}
	}

	// 프레임
	private void Update()
	{
		timer += Time.deltaTime;
		timerText.text = timer.ToString();
	}
}
