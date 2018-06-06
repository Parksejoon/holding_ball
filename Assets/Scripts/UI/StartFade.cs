using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class StartFade : MonoBehaviour
	{
		// 인스펙터 노출 변수
		// 수치
		[SerializeField]
		private float 		goalAlpha; 				// 목표 알파
		
		// 인스펙터 비노출 변수
		// 일반
		private Image		thisImg;                // 이 이미지
		private Text		thisText;				// 이 텍스트


		// 초기화
		private void Awake()
		{
			thisImg = GetComponent<Image>();

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

				UIEffecter.instance.FadeEffect(thisText.gameObject, new Vector2(goalAlpha, 0), 0.3f, UIEffecter.FadeFlag.ALPHA);
			}
			else
			{
				thisImg.color = new Color(thisImg.color.r, thisImg.color.g, thisImg.color.b, 0);

				UIEffecter.instance.FadeEffect(thisImg.gameObject, new Vector2(goalAlpha, 0), 0.3f, UIEffecter.FadeFlag.ALPHA);
			}
		}
	}
}
