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
		targetText.text = Application.version;
	}
}
