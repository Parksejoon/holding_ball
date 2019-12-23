using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private GameObject		coinText;			// 코인 텍스트
	[SerializeField]
	private GameObject		coinImg;            // 코인 이미지
	[SerializeField]
	private GameObject		cover;              // 커버 이미지
	[SerializeField]
	private GameObject		buttonImg;          // 버튼 이미지
	[SerializeField]
	private GameObject		colorImg;           // 색 이미지
	[SerializeField]
	private ColorPicker[]	colorPickers;		// 컬러 피커들

	// 수치
	public int				colorPrice;			// 색 가격

	// 인스펙터 비노출 변수
	// 일반
	private Button			buyButton;          // 구매 버튼
	private Coroutine[]		coroutines;			// 사용한 코루틴들

	// 수치
	private bool			isEnabled = false;	// 활성화 상태 


	// 초기화
	private void Awake()
	{
		new ShopParser();

		buyButton = buttonImg.GetComponent<Button>();
		coroutines = new Coroutine[0];
	}

	// 시작
	private void Start()
	{
		buyButton.onClick.AddListener(OnOffPurchaseWindow);
	}

	// 구매 창 열고 닫기
	public void OnOffPurchaseWindow()
	{
		if (isEnabled)
		{
			ClosePurchaseWindow();
		}
		else
		{
			OpenPurchaseWindow();
		}

		isEnabled = !isEnabled;
	}

	// 구매 창 오픈
	private void OpenPurchaseWindow()
	{
		//StopAllCoroutine();

		buyButton.onClick.RemoveAllListeners();
		buyButton.onClick.AddListener(BuyColor);

		//coroutines = new Coroutine[4];

		// 커버 온
		cover.GetComponent<Image>().raycastTarget = true;
		UIEffecter.instance.FadeEffect(cover, new Vector2(0.5f, 0), 0.2f, UIEffecter.FadeFlag.ALPHA);

		// 나머지 요소 페이드
		UIEffecter.instance.FadeEffect(coinImg, Vector2.one, 0.2f, UIEffecter.FadeFlag.ALPHA);
		UIEffecter.instance.FadeEffect(coinText, Vector2.one, 0.2f, UIEffecter.FadeFlag.ALPHA);
		UIEffecter.instance.FadeEffect(buttonImg, new Vector2(1.8f, 1.8f), 0.2f, UIEffecter.FadeFlag.SCALE);
	}

	// 구매 창 닫기
	private void ClosePurchaseWindow()
	{
		//StopAllCoroutine();

		buyButton.onClick.RemoveAllListeners();
		buyButton.onClick.AddListener(OnOffPurchaseWindow);

		//coroutines = new Coroutine[4];
		
		// 커버 오프
		cover.GetComponent<Image>().raycastTarget = false;
		UIEffecter.instance.FadeEffect(cover, Vector2.zero, 0.2f, UIEffecter.FadeFlag.ALPHA);

		// 나머지 요소 페이드
		UIEffecter.instance.FadeEffect(coinImg, Vector2.zero, 0.2f, UIEffecter.FadeFlag.ALPHA);
		UIEffecter.instance.FadeEffect(coinText, Vector2.zero, 0.2f, UIEffecter.FadeFlag.ALPHA);
		UIEffecter.instance.FadeEffect(buttonImg, Vector2.one, 0.2f, UIEffecter.FadeFlag.SCALE);
	}

	// 모든 코루틴 중지
	private void StopAllCoroutine()
	{
		for (int i = 0; i < coroutines.Length; i++)
		{
			StopCoroutine(coroutines[i]);
		}
	}

	// 색 구매
	public void BuyColor()
	{
		int coin = PlayerPrefs.GetInt("Coin", 0);

		// 돈이 colorPrice원 이상이면
		if (coin >= colorPrice)
		{
			// 돈 차감
			coin -= colorPrice;
			PlayerPrefs.SetInt("Coin", coin);
			PlayerPrefs.Save();

			UIEffecter.instance.SetText(1, coin.ToString());

			// 남은 색을 배열로 정리
			string[] dataArr = PlayerPrefs.GetString("ColorPurchase").Split(',');
			ArrayList colorArr = new ArrayList();

			for (int i = 0; i < dataArr.Length; i++)
			{
				if (dataArr[i] == "0")
				{
					colorArr.Add(i);
				}
			}

			// 남은 색이 1개 이상이면
			if (colorArr.Count >= 1)
			{
				// 랜덤으로 색 설정
				int targetIndex = Random.Range(0, colorArr.Count - 1);
				int target = (int)colorArr[targetIndex];
				Color newColor = Parser.instance.GetColor(target);

				// 색 저장
				ShopParser.instance.SetColorPurchaseData(target, true);

				// 색 갱신
				RefreshColor();

				// 색 시각화
				newColor.a = 0;
				colorImg.GetComponent<Image>().color = newColor;

				StartCoroutine(VisualizeColorRoutine());
			}
			// 색이 전부 있음
			else
			{
				//ChallengeManager.instance.ClearChallenge(9);
			}
		}
		// 돈이없당
		else
		{
			//ChallengeManager.instance.ClearChallenge(10);
		}
	}

	// 색 갱신
	private void RefreshColor()
	{
		foreach (ColorPicker colorPicker in colorPickers)
		{
			colorPicker.Refresh();
		}
	}

	// 색 표시 루틴 
	private IEnumerator VisualizeColorRoutine()
	{
		buyButton.interactable = false;
		UIEffecter.instance.FadeEffect(colorImg, Vector2.one, 0.4f, UIEffecter.FadeFlag.ALPHA);

		yield return new WaitForSeconds(1.5f);

		UIEffecter.instance.FadeEffect(colorImg, Vector2.zero, 0.4f, UIEffecter.FadeFlag.ALPHA);
		buyButton.interactable = true;
	}
}
