using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class StartFade : MonoBehaviour
	{
		// 인스펙터 비노출 변수
		// 일반
		private Image		thisImg;                // 이 이미지
		private Text		thisText;				// 이 텍스트


		// 초기화
		private void Awake()
		{
			thisImg   = GetComponent<Image>();

			if (thisImg == null)
			{
				thisText = GetComponent<Text>();
			}
		}

		// 시작
		private void OnEnable()
		{
			if (thisImg == null)
			{
				thisText.color = new Color(thisText.color.r, thisText.color.g, thisText.color.b, 0);

				//StartCoroutine(UIManager.instance.FadeOut(thisText));
				UIEffecter.instance.StartCoroutine(UIEffecter.instance.FadeAlpha(thisText, 1, 0.2f, false, false));
			}
			else
			{
				thisImg.color = new Color(thisImg.color.r, thisImg.color.g, thisImg.color.b, 0);

				//StartCoroutine(UIManager.instance.FadeOut(thisImg));
				UIEffecter.instance.StartCoroutine(UIEffecter.instance.FadeAlpha(thisImg, 1, 0.2f, false, false));
			}
		}
	}
}
