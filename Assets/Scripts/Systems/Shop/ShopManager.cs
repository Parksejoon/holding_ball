using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private GameObject	coinText;			// 코인 텍스트
	[SerializeField]
	private GameObject	coinImg;            // 코인 이미지
	[SerializeField]
	private GameObject	cover;              // 커버 이미지
	[SerializeField]
	private GameObject	buttonImg;          // 버튼 이미지

	// 인스펙터 비노출 변수
	// 일반
	private Button		buyButton;			// 구매 버튼

	// 수치
	private bool		enabled = false;	// 활성화 상태 


	// 초기화
	private void Awake()
	{
		new ShopParser();

		buyButton = buttonImg.GetComponent<Button>();
	}

	// 시작
	private void Start()
	{
		buyButton.onClick.AddListener(OnOffPurchaseWindow);
	}

	// 구매 창 열고 닫기
	public void OnOffPurchaseWindow()
	{
		if (enabled)
		{
			ClosePurchaseWindow();
		}
		else
		{
			OpenPurchaseWindow();
		}

		enabled = !enabled;
	}

	// 구매 창 오픈
	private void OpenPurchaseWindow()
	{
		buyButton.onClick.RemoveAllListeners();
		buyButton.onClick.AddListener(BuyColor);

		// 커버 온
		cover.SetActive(true);
		UIEffecter.instance.FadeEffect(cover, new Vector2(0.5f, 0), 0.2f, UIEffecter.FadeFlag.ALPHA);

		// 나머지 요소 페이드
		UIEffecter.instance.FadeEffect(coinImg, Vector2.one, 0.2f, UIEffecter.FadeFlag.ALPHA);
		UIEffecter.instance.FadeEffect(coinText, Vector2.one, 0.2f, UIEffecter.FadeFlag.ALPHA);
		UIEffecter.instance.FadeEffect(buttonImg, new Vector2(1.8f, 1.8f), 0.2f, UIEffecter.FadeFlag.SCALE);
	}

	// 구매 창 닫기
	private void ClosePurchaseWindow()
	{
		buyButton.onClick.RemoveAllListeners();
		buyButton.onClick.AddListener(OnOffPurchaseWindow);

		// 페이드
		UIEffecter.instance.FadeEffect(cover, Vector2.zero, 0.2f, UIEffecter.FadeFlag.ALPHA | UIEffecter.FadeFlag.FINDISABLE);
		UIEffecter.instance.FadeEffect(coinImg, Vector2.zero, 0.2f, UIEffecter.FadeFlag.ALPHA);
		UIEffecter.instance.FadeEffect(coinText, Vector2.zero, 0.2f, UIEffecter.FadeFlag.ALPHA);
		UIEffecter.instance.FadeEffect(buttonImg, Vector2.one, 0.2f, UIEffecter.FadeFlag.SCALE);
	}

	// 색 구매
	public void BuyColor()
	{
		Debug.Log("BuyColor!");
	}
}
