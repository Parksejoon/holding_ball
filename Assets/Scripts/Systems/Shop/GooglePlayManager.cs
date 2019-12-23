using UnityEngine;

public class GooglePlayManager : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private CoverSlider coverSlider;        // 커버 슬라이더


	// 시작
	private void Start()
	{
		coverSlider.slideFuncs[0] = GoStarScore;
	}

	// 별점 메뉴 열기
	public void GoStarScore()
	{
		coverSlider.StopSlide();

		// 도전과제 클리어
		//ChallengeManager.instance.ClearChallenge(8);
	}
}
